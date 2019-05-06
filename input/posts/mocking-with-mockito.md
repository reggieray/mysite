Title: Mocking with Mockito
Published: 1/24/2017
Tags: 
- Java
- Android
---
# Overview

In my previous post I created a sample Android app (Random Quiz), source code [Here](https://github.com/reggieray/Random-Quiz). In this sample app I used [Mockito](http://site.mockito.org/) to help write tests.

# Can you drink it?

Short answer no. Longer answer Mockito is a mocking framework that thinks it's a drink, the site even says that it "tastes really good". Besides it's self identity crisis, you can think of it as a tool to create mock class's. 

#### Mock a class, why?

Mocking isn't that useful in development unless your creating a mocking app, but in testing it becomes really useful.

Let's say for example you have a class you want to test but in order to test it you have to create all these other class's just in order to get to a stage so you can test what you wanted. Well with Mockito you don't have to create these other class's, you can mock it.

#### Are you taking the mock?

No really, when you want to test you just want to test a method of a class right? That method call's other methods from other class's and you want to make sure that the right methods are being called. This is where Mockito can help because not only can it mock a class, it can verify if a method was called and how many times. This saves you so much time especially if one of the class's you want to mock needs lots of methods to be implemented. 

# How to set it up

Taken straight from the website at this time of writing, add the following to your Gradle.

```java
repositories { jcenter() }
dependencies { testCompile "org.mockito:mockito-core:2.+" }
```

Always refer to the website though as things do change and you always want to be up to date. Refer to the example app if need be to double check you have added it in the correct place.

# Code

Taken straight from the example Android app I created, I have covered it in comments to describe what is going on.

```java
package com.matthewregis.randomquiz.ui.main;

import android.content.Context;

import org.junit.After;
import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.mockito.Mock;
import org.mockito.runners.MockitoJUnitRunner;

import static org.mockito.Mockito.times;
import static org.mockito.Mockito.verify;

/**
 * Created by reg on 28/11/2016.
 */

@RunWith(MockitoJUnitRunner.class)
public class MainPresenterTests {

    MainPresenter mainPresenter; // Concreate class that I want to test

    @Mock
    IMainView mMainView; // Provided a @Mock annotation to tell Mockito to create a mock object of this interface class
    @Mock
    Context mContext; // Similar to what the line above was doing but with Context which is used for many things like get resources


    @Before // Add this annotation to method of what you want to do before anything of the tests are run. You want to setup your classes here
    public void SetUp() throws Exception{ 
        mainPresenter = new MainPresenter(mContext); // I create new concrete class of MainPresenter to test
        mainPresenter.attachView(mMainView); // I attach the mocked view
    }

    @After // Add this after running all your tests
    public void TearDown() throws Exception{
        mainPresenter.detachView(); // Detaches view. Calling anything in the presenter that calls the view after this should throw a error 
    }

    @Test // Annotate tests with @Test 
    public void ShouldStartRandom10QuizOnRandomQuizBtnOnClick() throws Exception { // Used self descriptive names even though it is long winded you can pretty much guess what it should do
        mainPresenter.onRandom10BtnOnClick(); // call the method I want to test
        verify(mMainView, times(1)).StartRandom10Quiz(); // use verify on the mocked view to verifiy that the right method was called and called only once.
    }

    @Test
    public void ShouldStartSuddenDeathQuizOnSuddenDeathBtnOnClick() throws Exception {
        mainPresenter.onSuddenDeathBtnOnClick();
        verify(mMainView, times(1)).StartSuddenDeathQuiz();
        verify(mMainView, never()).StartRandom10Quiz();  // I could of also added this to test no other methods were called using never()
    }

    @Test
    public void ShouldShowTopScoresOnTopScoresBtnOnClick() throws Exception {
        mainPresenter.onTopScoresBtnOnClick();
        verify(mMainView, times(1)).ShowTopScores();
    }


}
```

The example above is quite straight forward which is good, no need to overcomplicate it more than it needs to be. There are however more advanced examples which you can find out how to do on Mockito's documentation site [Here](https://static.javadoc.io/org.mockito/mockito-core/2.6.8/org/mockito/Mockito.html).


# Summary

Mockito makes it easy to mock classes and gives you the ability to setup your tests without having to write your own mock classes.

It has become a essential part of writing unit tests in Android development.