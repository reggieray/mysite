Title: Android's DayNight Theme
Published: 5/5/2019
Tags: 
- Android
- Kotlin
---
# Intro
If you want to include a dark theme to your app then the DayNight Theme addition to AppCompat included within the Support Library v23.2.0 release is one way to go. In this post I will be going over it.

# Options

Before going over the implementation it's worth exploring what options are available for setting the DayNight theme. The options are as follows:

* MODE_NIGHT_NO - forces the theme to never use night theme.
* MODE_NIGHT_YES - forces the theme to use night theme.
* MODE_NIGHT_AUTO - the time of day and your last known location (if your app has the location permissions) are used to automatically switch between day and night.
* MODE_NIGHT_FOLLOW_SYSTEM - uses the system's night mode setting to determine if it is night or not.

Check out the source code [Here](https://chromium.googlesource.com/android_tools/+/refs/heads/master/sdk/sources/android-25/android/support/v7/app/AppCompatDelegate.java?autodive=0%2F%2F%2F%2F%2F%2F%2F%2F%2F%2F%2F) to read the comments in full. It's also a good way to get an insight into what is going on underneath the hood.

# Code

When integrating DayNight theme into your app inherit from a `"Theme.AppCompat.DayNight"` theme. As you can see in my example I used the NoActionBar variant of that theme.

```Xml
<style name="AppTheme" parent="Theme.AppCompat.DayNight.NoActionBar">
    <!-- Customize your theme here. -->
    <item name="colorPrimary">@color/colorPrimary</item>
    <item name="colorPrimaryDark">@color/colorPrimaryDark</item>
    <item name="colorAccent">@color/colorAccent</item>
</style>
```

To apply the theme to your app set the default night mode either set it from your application `onCreate` method or your main activity `onCreate` method. It's important to do this before the UI has been drawn to the screen, otherwise you might not see the theme being applied straight away. Below's example is forcing the app to use night mode from the application class.

```Kotlin
class MyApp : Application() {

    override fun onCreate() {
        super.onCreate()
        AppCompatDelegate.setDefaultNightMode(AppCompatDelegate.MODE_NIGHT_YES)
    }
}
```

If you are changing night mode at runtime after the UI has been drawn to the screen then you will need to call `recreate()` from within your activity to redraw the UI elements to the screen and pick up your updated theme styles. Once you have called `recreate()`, you should see your app change before your very eyes.

# Customise

If you want to further customise your day/night theme then add your colors.xml or styles.xml under **res/values-night**. You can read more from the documentation [here](https://developer.android.com/guide/topics/resources/providing-resources.html?utm_campaign=android_launch_supportlibrary23.2_022216&utm_source=anddev&utm_medium=blog#NightQualifier)

# More reading

Read more about the DayNight theme form the following links:

[DayNight theme introduction](https://android-developers.googleblog.com/2016/02/android-support-library-232.html)   
[Dark theme documentation](https://developer.android.com/preview/features/darktheme)