Title: Random Quiz - An example Android App
Published: 12/28/2016
Tags: 
- Java
- Android
---
# Cutting to the chase

If you are like me you just want to see it working and play with the code. So here are the following links:

- [Play Store Link](https://play.google.com/store/apps/details?id=com.matthewregis.randomquiz)
- [Source Code Link](https://github.com/reggieray/Random-Quiz)

# Introduction

This app was created as an example of using MVP (Model View Presenter) pattern in a Android app.

Best practices are constanlty changing and I feel adoption for other desgin patterns will be common place in 2 years to something more like the MVVM pattern. This doesn't take anything away from MVP, as it's still a good use of a design pattern to use when creating a Android app. It will just be one of the many options available. 

In this blog I'll go over the MVP pattern, dependency injection with tests.

# MVP (Model View Presenter)

MVP is a software pattern that comes from MVC. You can read about it [here](https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93presenter).

Some important things I like to try and remember about MVP

- The view should not be concerned about the model.
- The view basically should be dumb. No logic should live here.
- Usually you don't test the view because it is dumb.
- The presenter should contain your logic, it's what drives your view.
- The presenter knows about the model and knows how to give it to the view.

Usually presenters and views are implemented with an interface and for example the presenter will interact with the interface.

A simple example: 

Presenter interface
```java
public interface ImainPresenter {
    void DoSomething();
}
```

View interface
```java

public interface ImainView {
    void ShowSomething();
} 
```

Implementing a presenter
```java
public class mainpresenter implements ImainPresenter{
  
  ImainView mMainView; // keep reference of the interface

  public mainpresenter(ImainView mainview){ // set the interface in the contructor
      this.mMainView = mainview; // mainview must implement ImainView in order for this to work
  }

  @Override
  void DoSomething(){ // implemented from the ImainPresenter interface
      mMainView.ShowSomething(); // calls a method from the ImainView interface
  }
}
```

Implementing a view with a activity
```java
public class mainActivity extends Activity implements ImainView{
  Button but;
  ImainPresenter mMainPresenter; //keep reference to the presenter interface

  @Override
  protected void onCreate(Bundle savedInstanceState)
      super.onCreate(savedInstanceState);
      setContentView(R.layout.activity_main);
      but = (Button) findViewById(R.id.AButton)
      mMainPresenter = new mainpresenter(this); // set the presenter here, pass this(mainActivity) into the contructor which is ok because this implements ImainView 
    }

  @Override
  protected void onResume() { // on the onResume event call the presenters method DoSomething()
      mMainPresenter.DoSomething();
  }

  @Override
  void ShowSomething(){ //implements the method from the ImainView interface
      but.setVisibility(View.VISIBLE); 
  }
}
```

With the simple example above you can see things are set out more cleary. Seperation of concerns, the view is only concerned with the view and not any bussiness logic.

Using the MVP pattern you can see that it becomes easier to test the presenter because its defined what it should do. In the simple example above the mainpresenter's method DoSomething() must call mainview's ShowSomething(). 

# Dependency Injection with Dagger 2

I won't bore you with the details of dependency injection, you can read about it [here](https://en.wikipedia.org/wiki/Dependency_injection).

What you should know about dependency injection is that it enables you to use your objects and classes to be used more freely in your project without concerning itself with having to create a object or class in order to work. 

In the .NET world you could use [Unity](https://unity.codeplex.com/), [Ninject](http://www.ninject.org/) and with .Net Core MVC it's baked into the product, which is good as it encorages good practices. In the Android world the only one worth mentioning as of now is Dagger 2. 

You can find out more about Dagger 2 [here](https://google.github.io/dagger/). It's worth mentioning that this was a fully supported project between Google and Square. It's also worth mentioning Square probably make the most well used libaries in the Android world and you should check them out and also follow [Jake Wharton](https://github.com/JakeWharton) who's part of Square (he knows his stuff, others too but Jake is more well known).  

I have to give credit to the following github repo's for I have followed there examples in implementing dependency injection in a way that makes it more easy to maintain and build upon.

- [Ribot Android Boilerplate](https://github.com/ribot/android-boilerplate)
- [Android Boilerplate](https://github.com/hitherejoe/Android-Boilerplate)

If you look in the source code for Random Quiz you will see this in the MainActivity. I have put comments with what's injected and what makes the magic happen.

```java
public class MainActivity extends BaseActivity implements IMainView {
    public final static String EXTRA_MESSAGE = "com.matthewregis.randomquiz.MESSAGE";

    @Inject MainPresenter mMainPresenter; // Using anotations to delcare what to inject

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        activityComponent().inject(this); // This is where the magic happens
        setContentView(R.layout.activity_main);
        ButterKnife.bind(this);
        mMainPresenter.attachView(this);
    }
```

And if you look at the presenter you will see this

```java
public class MainPresenter extends BasePresenter<IMainView> {

    Context mContext;

    @Inject
    public MainPresenter(@ApplicationContext Context context){ // it injects the application context here 
        this.mContext = context;
    }

```

Here is another example with QuizPresenter.

```java
public class QuizPresenter extends BasePresenter<IQuizView> {

    private Context mContext;
    private ILocalRepo mLocalRepo;

    OpentdbApi mOpentdbApi;
    Queue<ResultsBean> questions;
    List<Boolean> answered;

    GameMode mGameMode;

    @Inject
    public QuizPresenter(@ApplicationContext Context context, LocalRepo localRepo, OpentdbApi opentdbApi) {
        this.mContext = context;
        this.mLocalRepo = localRepo;
        this.mOpentdbApi = opentdbApi;
        this.initialize();
    }
```

You'll be asking how does it know to inject Context, localReop and OpentdbApi. It knows how to do this because it was setup in ApplicationComponent here:

```java
@Singleton
@Component(modules = ApplicationModule.class)
public interface ApplicationComponent {

    @ApplicationContext
    Context context();
    Application application();
    LocalRepo localReop();
    OpentdbApi opentbApi();
}
```

So when I declare a OpentdbApi and anotate it with @Inject Dagger 2 knows what to inject because i set it up above.

OpentdbApi looks like this.

```java
@Singleton
public class OpentdbApi {

    Context mContact;
    AsyncHttpClient asyncHttpClient;

    @Inject
    public OpentdbApi(@ApplicationContext Context context){
        this.mContact = context;
        asyncHttpClient = HttpAsyncClientFactory.GetInstance();
    }

    public void Get10RandomQuestions(JsonHttpResponseHandler jsonHttpResponseHandler){
        asyncHttpClient.get(String.format("%s%s", mContact.getString(R.string.opentdb_base_url), mContact.getString(R.string.opentdb_random_10_path)), jsonHttpResponseHandler);
    }
}
```

You can see the @Singleton anotation which means I only want it to exsist once in my application life cycle. You can also see I inject the context into the contructor.

# Testing with Mockito

Everyone wants to have a good code base and as few bugs as possible. To help with this you should create lots of tests to test your code. 

When you test code you need to create other classes. For example when you test a presenter you need to create a view to test with but you don't really care about the view, all you care about is the presenter and what it was supposed to do. 

This is where mocking comes in handy. You can mock a class (in this case a view). It basically creates a fake object for you to test with. Also it will know what methods were called which is good for you to verify if a method was called from a presenter.

Here is an example from Random Quiz. Here you can see the @Mock notation above IMainView.

```java
@RunWith(MockitoJUnitRunner.class)
public class MainPresenterTests {

    MainPresenter mainPresenter;

    @Mock
    IMainView mMainView;
    @Mock
    Context mContext;


    @Before
    public void SetUp() throws Exception{
        mainPresenter = new MainPresenter(mContext);
        mainPresenter.attachView(mMainView);
    }

    @After
    public void TearDown() throws Exception{
        mainPresenter.detachView();
    }

    @Test
    public void ShouldStartRandom10QuizOnRandomQuizBtnOnClick() throws Exception {
        mainPresenter.onRandom10BtnOnClick();
        verify(mMainView, times(1)).StartRandom10Quiz();
    }

    @Test
    public void ShouldStartSuddenDeathQuizOnSuddenDeathBtnOnClick() throws Exception {
        mainPresenter.onSuddenDeathBtnOnClick();
        verify(mMainView, times(1)).StartSuddenDeathQuiz();
    }

    @Test
    public void ShouldShowTopScoresOnTopScoresBtnOnClick() throws Exception {
        mainPresenter.onTopScoresBtnOnClick();
        verify(mMainView, times(1)).ShowTopScores();
    }
}
```

If we just look at this test.

```java
@Test
    public void ShouldStartRandom10QuizOnRandomQuizBtnOnClick() throws Exception {
        mainPresenter.onRandom10BtnOnClick();
        verify(mMainView, times(1)).StartRandom10Quiz();
    }
```

I try to be descriptive in what the test should be. So in this case when I click on Random 10 the presenter should call StartRandom10Quiz(). Above you can see I use verify on the mocked object and I say how many times I expect it to be called and then what method.

So with that in mind here is an example test for the simple MVP pattern I implemented earlier on.

```java
@RunWith(MockitoJUnitRunner.class)
public class mainpresenterTests {

    ImainPresenter mMainPresenter;

    @Mock
    ImainView mMainView; // here I create a mock main view

    @Before // this @Before anotation says to do this before running tests.
    public void SetUp() throws Exception{
        mMainPresenter = new mainpresenter(mMainView); // here I create the presenter and pass the mock view in
    }

    @Test // anotates a test
    public void ShouldCallShowSomthingOnDoSomthing() throws Exception {
        mMainPresenter.DoSomething();
        verify(mMainView, times(1)).ShowSomething();
    }
}
```

So hopfully DoSomething() will call ShowSomething() one time and the test will pass.

# Summary

When I first started out creating Android apps I followed examples and created code in the Activity or Fragment. While it is good to get going it's good to keep in mind that this is not best practice.

The benifits of using patterns like MVP or MVVM is that it should make it easier to work with in the long run. It also makes it easier for you to write tests which in turn leads to a more stable code base.

Feel free to download my example and play around with it.