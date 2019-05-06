Title: Website up checker with Python
Published: 10/11/2016
Tags: 
- Python
- Script
---
# Intro

A while ago I wanted a way of checking if a website was up or not. What I ended up doing is creating a Python script to do this. This script was what I had been using on my Raspberry Pi. I would setup a cron job to run at a specific intervals that would fire off this script.

# Code

```python
import ssl
import urllib2

# Config Stuff

Website = "website you want to check"
Email = "your gmail account"
EmailPassword = "your password"
EmailRecipient = "your recipient"
EmailSubject = "subject"


def send_email(user, pwd, recipient, subject, body):
    import smtplib

    gmail_user = user
    gmail_pwd = pwd
    FROM = user
    TO = recipient if type(recipient) is list else [recipient]
    SUBJECT = subject
    TEXT = body

    # Prepare actual message
    message = """\From: %s\nTo: %s\nSubject: %s\n\n%s
    """ % (FROM, ", ".join(TO), SUBJECT, TEXT)
    try:
        server = smtplib.SMTP("smtp.gmail.com", 587)
        server.ehlo()
        server.starttls()
        server.login(gmail_user, gmail_pwd)
        server.sendmail(FROM, TO, message)
        server.close()
        print 'successfully sent the mail'
    except:
        print "failed to send mail"


context = ssl.SSLContext(ssl.PROTOCOL_TLSv1)

main_page_url = Website

hdr = {'User-Agent': 'Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.64 Safari/537.11',
       'Accept': 'text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8',
       'Accept-Charset': 'ISO-8859-1,utf-8;q=0.7,*;q=0.3',
       'Accept-Encoding': 'none',
       'Accept-Language': 'pl-PL,pl;q=0.8',
       'Connection': 'keep-alive'}

req = urllib2.Request(main_page_url, headers=hdr)

try:
    page = urllib2.urlopen(req,context=context)
    send_email(Email,EmailPassword,EmailRecipient,EmailSubject,page.getcode())
except urllib2.HTTPError, e:
	send_email(Email,EmailPassword,EmailRecipient,EmailSubject,e.fp.read())

content = page.read()
print content

```

# Breaking it down


```python
import ssl
import urllib2
```

urrllib2 is the libary to make the http request and ssl a libary so we can go over https.

```python
# Config Stuff

Website = "website you want to check"
Email = "your gmail account"
EmailPassword = "your password"
EmailRecipient = "your recipient"
EmailSubject = "subject"
```

This is just config stuff, change your settings here for what website you want to check and what gmail account your sending from and who your sending it too. 

```python
def send_email(user, pwd, recipient, subject, body):
    import smtplib

    gmail_user = user
    gmail_pwd = pwd
    FROM = user
    TO = recipient if type(recipient) is list else [recipient]
    SUBJECT = subject
    TEXT = body

    # Prepare actual message
    message = """\From: %s\nTo: %s\nSubject: %s\n\n%s
    """ % (FROM, ", ".join(TO), SUBJECT, TEXT)
    try:
        server = smtplib.SMTP("smtp.gmail.com", 587)
        server.ehlo()
        server.starttls()
        server.login(gmail_user, gmail_pwd)
        server.sendmail(FROM, TO, message)
        server.close()
        print 'successfully sent the mail'
    except:
        print "failed to send mail"
```

This is a send email function using smtplib configured for gmails smtp. It can handle recipient lists and as you can see it's configured to go over 587 which is smtps. It sends the email in a try and a catch of both would print the end result.

Now the interesting bit.

```python
context = ssl.SSLContext(ssl.PROTOCOL_TLSv1)

main_page_url = Website

hdr = {'User-Agent': 'Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.64 Safari/537.11',
       'Accept': 'text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8',
       'Accept-Charset': 'ISO-8859-1,utf-8;q=0.7,*;q=0.3',
       'Accept-Encoding': 'none',
       'Accept-Language': 'pl-PL,pl;q=0.8',
       'Connection': 'keep-alive'}

req = urllib2.Request(main_page_url, headers=hdr)

try:
    page = urllib2.urlopen(req,context=context)
    send_email(Email,EmailPassword,EmailRecipient,EmailSubject,page.getcode())
except urllib2.HTTPError, e:
	send_email(Email,EmailPassword,EmailRecipient,EmailSubject,e.fp.read())

content = page.read()
print content
```

The reason this is interesting is that the header is being faked, the reason I create this header is that some websites do not like responding to requests that look like scripts or bots which this is. So I added this header to look like a web browser.

I use urllib2.Request to make a request object with the url and header as its parameters. I then use urllib2.urlopen passing in the request object and the context which in this case is over https. This makes the request and if successful will bring you back a response with such things as http status code and the pages html source. This is all done within a try and a catch and you can see I know what type of error it would produce if it goes wrong "urllib2.HTTPError, e".

So if the request was successful I send the http status code via email to whoever I set it too.

If it fails I use the exception and pass it though to the email so I can see what has gone wrong and just for fun I print out the html source.

Note that if it fails because of a bogus url then you will get a stack trace as the request didn't go through.

```PowerShell
Traceback (most recent call last):
  File "web2.py", line 52, in <module>
    page = urllib2.urlopen(req,context=context)
  File "/Users/reg/anaconda2/lib/python2.7/urllib2.py", line 154, in urlopen
    return opener.open(url, data, timeout)
  File "/Users/reg/anaconda2/lib/python2.7/urllib2.py", line 431, in open
    response = self._open(req, data)
  File "/Users/reg/anaconda2/lib/python2.7/urllib2.py", line 449, in _open
    '_open', req)
  File "/Users/reg/anaconda2/lib/python2.7/urllib2.py", line 409, in _call_chain
    result = func(*args)
  File "/Users/reg/anaconda2/lib/python2.7/urllib2.py", line 1240, in https_open
    context=self._context)
  File "/Users/reg/anaconda2/lib/python2.7/urllib2.py", line 1197, in do_open
    raise URLError(err)
urllib2.URLError: <urlopen error [Errno 8] nodename nor servname provided, or not known>
```

This is an example of an error because I provided a bogus url, in this case https://matthewregiss.com.

If the request was successful but an error was returned from the server then it goes though "urllib2.HTTPError, e" and below is what I get from trying to goto https://google.com/404

```Html
<!DOCTYPE html>
<html lang=en>
  <meta charset=utf-8>
  <meta name=viewport content="initial-scale=1, minimum-scale=1, width=device-width">
  <title>Error 404 (Not Found)!!1</title>
  <style>
    *{margin:0;padding:0}html,code{font:15px/22px arial,sans-serif}html{background:#fff;color:#222;padding:15px}body{margin:7% auto 0;max-width:390px;min-height:180px;padding:30px 0 15px}* > body{background:url(//www.google.com/images/errors/robot.png) 100% 5px no-repeat;padding-right:205px}p{margin:11px 0 22px;overflow:hidden}ins{color:#777;text-decoration:none}a img{border:0}@media screen and (max-width:772px){body{background:none;margin-top:0;max-width:none;padding-right:0}}#logo{background:url(//www.google.com/images/branding/googlelogo/1x/googlelogo_color_150x54dp.png) no-repeat;margin-left:-5px}@media only screen and (min-resolution:192dpi){#logo{background:url(//www.google.com/images/branding/googlelogo/2x/googlelogo_color_150x54dp.png) no-repeat 0% 0%/100% 100%;-moz-border-image:url(//www.google.com/images/branding/googlelogo/2x/googlelogo_color_150x54dp.png) 0}}@media only screen and (-webkit-min-device-pixel-ratio:2){#logo{background:url(//www.google.com/images/branding/googlelogo/2x/googlelogo_color_150x54dp.png) no-repeat;-webkit-background-size:100% 100%}}#logo{display:inline-block;height:54px;width:150px}
  </style>
  <a href=//www.google.com/><span id=logo aria-label=Google></span></a>
  <p><b>404.</b> <ins>That’s an error.</ins>
  <p>The requested URL <code>/404</code> was not found on this server.  <ins>That’s all we know.</ins>
```


# Summary 

This is just an of example of how to interact with the web using python. I could of done lots more, I barely scratched the surface with what you can do.

This script was run on Python version 2.7.11.