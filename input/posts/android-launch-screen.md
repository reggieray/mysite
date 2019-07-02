Title: Android Launch Screen (Splash Screen)
Published: 7/1/2019
Tags: 
- Android
- Kotlin
---

There are a few approaches to implement a splash screen in Android. Some good and some not so good.

> The launch screen is a user’s first experience of your application.

So with this in mind, it's not a good idea to add a splash screen that adds an artificial delay or one that does too much logic in routing a user to the right screen. Both are unnecessary and further delay the user from experiencing your app and if we keep the quote in mind then it doesn't leave a very good first impression.

One approach that could be used without delaying a user longer than you ought too is the [Launch Screen](https://material.io/archive/guidelines/patterns/launch-screens.html#) pattern from Material Design. Implementing this pattern will be shown below and what you will end up with is a screen very similar to the image below:

<img src="https://material.io/archive/guidelines/assets/0B7WCemMG6e0VLWRuNnhlNFRsYjA/patterns-launch-screens.png" width="50%"/>


# The Code

Add a drawable with a bitmap. I've called this `launch_screen.xml`.

```xml
<?xml version="1.0" encoding="utf-8"?>

<layer-list xmlns:android="http://schemas.android.com/apk/res/android" android:opacity="opaque">

    <item android:drawable="@android:color/white"/>
    <item>

        <bitmap
                android:src="@drawable/ic_launcher"
                android:gravity="center"/>

    </item>

</layer-list>
```

Create a AppTheme.Launcher theme in `styles.xml` and set the drawable for android:windowBackground.

```xml
<resources>
    <!-- Base application theme. -->
    <style name="AppTheme" parent="Theme.AppCompat.DayNight.NoActionBar">
        <!-- Customize your theme here. -->
        <item name="colorPrimary">@color/colorPrimary</item>
        <item name="colorPrimaryDark">@color/colorPrimaryDark</item>
        <item name="colorAccent">@color/colorAccent</item>
    </style>

    <style name="AppTheme.Launcher">
        <item name="android:windowBackground">@drawable/launch_screen</item>
    </style>

</resources>
```

In the `manifest.xml` go to the main launcher activity and set the theme to the launcher theme just created.

```xml

<activity android:name=".ui.main.MainActivity" android:theme="@style/AppTheme.Launcher">
        <intent-filter>
            <action android:name="android.intent.action.MAIN"/>
            <category android:name="android.intent.category.LAUNCHER"/>
        </intent-filter>
 </activity>

```

`onCreate` of that activity set the theme back to your main theme. Also make sure it's called before calling `super`.

```Kotlin
override fun onCreate(savedInstanceState: Bundle?) {
    setTheme(R.style.AppTheme)
    super.onCreate(savedInstanceState)
}
```

# Summary

This should be the preferred way of implmenting a launch screen unless you want a fancy animation or your app is a game and you want to display the status of how far your app has initialized. It gives your app branding while giving a user short wait times which is what we should be striving for. 
