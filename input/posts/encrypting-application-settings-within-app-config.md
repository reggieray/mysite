Title: Encrypting application settings within app.config
Published: 4/26/2019
Tags: 
- .Net
- ASP.Net
- XML
- Powershell
---
# Intro
Encrypting information within your config file without changing your code is as simple as running a command, through the use of aspnet_regiis.exe.

### Encrypt

In order for this to work properly make sure you have admin level access or run powershell/cmd as an administrator.

In my example I created a folder called temp, this is where I put my config file to run the command on. First copy over your app.config or web.config file to the temp folder and rename the file to **web.config**. This is important as the tool looks for this file to run on. 

I have included how the command syntax and an example.

```Powershell
".Net version"\aspnet_regiis -pef "Section" "Path exluding web.config"
```

```Powershell
%windir%\Microsoft.NET\Framework\v2.0.50727\aspnet_regiis -pef "appSettings" "C:\temp"
```
### Decrypt

If you want to decrypt the sections then it should look like this.


```Powershell
".Net version"\aspnet_regiis -pdf "Section" "Path exluding web.config"
```

```Powershell
aspnet_regiis.exe -pdf appSettings "C:\temp"
```

# Before and After

For reference this is what it would look like before and after encryption if you were to run it on the connection strings section.

Before:

```xml
<connectionStrings>
  <add name="MyLocalSQLServer" connectionString="Initial Catalog=aspnetdb;
data source=localhost;Integrated Security=SSPI;" providerName="System.Data.SqlClient"/>
</connectionStrings>
```
After:
```xml
<connectionStrings configProtectionProvider="MyUserDataProtectionConfigurationProvider">
    <EncryptedData>
      <CipherData>
        <CipherValue>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAWuizHyLxzk+U4/8NnMRAjQAAAAACAAAAAAADZgAAqAAAABAAAADLFqu00qT0BeGh6wmHHGySAAAAAASAAACgAAAAEAAAAM56z6ezglmufZGcta83RKGgAQAAtvDpExhD6h95lfWBt0FqZYzxupI2IepH/XlhLB5rsuuQDCJUL7XGIIsaVW2oNQxgNCPXxRSuCxHQ7vqgbh4G7xHk0wdyws5Ax4T/RYJbfYEF5KAPzShdmdoZdkY/FOWrVEgAr7LzKFBoDcPJBvgX+lfsJdBNeWRB5BqRX552dUFjtDlp8u3K/dA9twWDU2w/cRLMXJtVZ/y/ICI1fzXjX3u7sY9K1IC+5Hbi7nouCK6Ze5RLBnL0Zfdq0PyGlj2To4ftAYAT0SzkBaxlXRQSzMhX+7c+rgKpMqtG9XjAW26x3IJAp2/uAr2zOZqH+tskamHYSruhTicHJDTtP+r6Rs21y2QkRT9Hb9oPd9B5mDIzGtDkifWBbmwLv4XFuYcna1Zgny7McSxMI62jxayVlZKcS5dXV0shwLoUjbTDcXQmFKsRvo2sCW86wcN8ad02jhKCQMf9SWnZpd849mlqgMFiQQSFlZ6Q+vJLrXqVb8zmVZemQPQcY/DktgjOvjn0iOZ3zhl20fRENOa3ZIWvvK8p9pblz3sEfS19MAW0JtYUAAAAayvNPot3An7LaCTdFYrEip+fTU4=</CipherValue>
      </CipherData>
    </EncryptedData>
  </connectionStrings>
```

# Useful Info

Below is a reference table for the diffrent locations of where Aspnet_regiis.exe is located, depending on your version of .NET.

.NET Framework version | Aspnet_regiis.exe location
--- | ---
.NET Framework version 1 | %windir%\.NET\Framework\v1.0.3705
.NET Framework version 1.1 | %windir%\Microsoft.NET\Framework\v1.1.4322
.NET Framework version 2.0, version 3.0, and version 3.5 (32-bit systems) | %windir%\Microsoft.NET\Framework\v2.0.50727
.NET Framework version 2.0, version 3.0, and version 3.5 (64-bit systems) | %windir%\Microsoft.NET\Framework64\v2.0.50727
.NET Framework version 4 (32-bit systems) | %windir%\Microsoft.NET\Framework\v4.0.30319
.NET Framework version 4 (64-bit systems) | %windir%\Microsoft.NET\Framework64\v4.0.30319

The tool only supports encrypting certain sections which are listed below:

Sections you can encrypt:
* appSettings
* connectionStrings
* identity
* sessionState

Sections you can't encrypt:
* processModel
* runtime
* mscorlib
* startup
* system.runtime.remoting
* configProtectedData
* satelliteassemblies
* cryptographySettings
* cryptoNameMapping
* cryptoClasses

# Final thoughts

This was created as sort of a breif overview and refrence guide (mostly for me). I haven't covered the other options or what *-pef* actually does behind the scenes. There is quite a few more things you can do, for example you can encrypt custom sections and create an RSA key container. Find out more [Here](https://docs.microsoft.com/en-us/previous-versions/aspnet/53tyfkaw(v%3dvs.100)). 

I like that I don't have to make any code changes to make use of the feature and the fact it's just a command means I can inculde it as powershell script to run after a deployment if you use CD. It's worth mentioning if someone gained admin level access to your sever or machine they could easily run the tool to decrypt it, that's why it shouldn't been seen as the only way to secure sensitive config sections, but as one step of many. Although if someone gained admin level access, I suspect you will have bigger problems on your hands.

# Further reading

To find out more here are some links to the Microsoft documentation

[Encrypting Configuration Information Using Protected Configuration](https://docs.microsoft.com/en-us/previous-versions/aspnet/53tyfkaw(v%3dvs.100))    
[How To: Encrypt Configuration Sections in ASP.NET 2.0 Using RSA](https://docs.microsoft.com/en-us/previous-versions/msp-n-p/ff650304(v%3dpandp.10))    
[How To: Encrypt Configuration Sections in ASP.NET 2.0 Using DPAPI](https://docs.microsoft.com/en-us/previous-versions/msp-n-p/ff647398(v%3dpandp.10))