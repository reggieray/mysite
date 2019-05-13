Title: Android's DayNight Theme
Published: 5/5/2019
Tags: 
- Android
- Kotlin
---
# Intro
If you want to include a dark theme to your app then the DayNight Theme addition to AppCompat included within the Support Library v23.2.0 release gives you the ability implement this.

# Options

It's worth exploring what options are available for setting the DayNight theme.

* MODE_NIGHT_NO - forces the theme to never use night theme.
* MODE_NIGHT_YES - forces the theme to use night theme.
* MODE_NIGHT_AUTO - the time of day and your last known location (if your app has the location permissions) are used to automatically switch between day and night.
* MODE_NIGHT_FOLLOW_SYSTEM - uses the system's night mode setting to determine if it is night or not.

Look at the source code [Here](https://chromium.googlesource.com/android_tools/+/refs/heads/master/sdk/sources/android-25/android/support/v7/app/AppCompatDelegate.java?autodive=0%2F%2F%2F%2F%2F%2F%2F%2F%2F%2F%2F) to read the full comments about each option.

# How

First inherit from theme with a "Theme.AppCompat.DayNight" prefix or even just "Theme.AppCompat.DayNight"

```Xml
<style name="AppTheme" parent="Theme.AppCompat.DayNight.NoActionBar">
    <!-- Customize your theme here. -->
    <item name="colorPrimary">@color/colorPrimary</item>
    <item name="colorPrimaryDark">@color/colorPrimaryDark</item>
    <item name="colorAccent">@color/colorAccent</item>
</style>
```

To set the default night mode either set it from your application onCreate method or your main activity onCreate method. Below is forcing the app to use night mode from the app class.

```Kotlin
class MyApp : Application() {

    override fun onCreate() {
        super.onCreate()
        AppCompatDelegate.setDefaultNightMode(AppCompatDelegate.MODE_NIGHT_YES)
    }
}
```

If you are changing night mode from code after the activity has been created then you will need to call `recreate()` from within your activity to update those changes to the ui. This will display the changes immediately. 

# Customise

If you want to further customise your night theme then add your styles.xml under the **res/values-night**. For example creating a **res/values-night/colors.xml** and **res/values-night/styles.xml** will respectively override colors and styles from your main theme when night mode is on.

# Final thoughts

As Android users we've come to expect the ability to switch between dark and light themes for a few year now. It's a great feature to have and enables your users to have more control over the look and feel they desire. It's also increasingly seeping it's way through to the rest of Google's products (Chrome, cough, cough), which means more and more people will come to expect it. 

# More reading

[DayNight theme introduction](https://android-developers.googleblog.com/2016/02/android-support-library-232.html)   
[Dark theme documentation](https://developer.android.com/preview/features/darktheme)