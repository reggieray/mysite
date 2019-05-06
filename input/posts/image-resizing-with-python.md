Title: Image resizing with python
Published: 11/10/2016
Tags: 
- Python
- Script
---
# Introduction

A while ago I needed to resize a few hundred images all to a similar size for one of my own projects. Instead of manually changing the size of each image, I used this opputunity to automate it and I used Python to do it.

# Code

```python
from PIL import Image

def main():
	count = 1
	while count < 3:
		resizeImage(count)
		count = count + 1

def resizeImage(picNumber):
	img = Image.open("pic_" + str(picNumber) + ".jpg")
	width, height = img.size
	if width > height and height > 600:
		width = width - ((width / height) * (height - 600))
		height = height - (height - 600)
	elif height > width and width > 600:
		height = height - ((height / width) * (width - 600))
		width = width - (width - 600)
	elif height == width and height > 600 and width > 600:
		height = 600
		width = 600
	img = img.resize((int(width), int(height)))
	img.save("pic_" + str(picNumber) + ".jpg", format='JPEG')


if __name__ == "__main__":
    main()
```

# The Details

There is only one import you need to install it. You'll have to look at installing Pillow relative to your own OS. In my example, I ran this on a MAC.

```PowerShell
pip install Pillow
``` 

Pillow is a image liabary for Python. Find out more here: https://python-pillow.org/. 

```python
from PIL import Image
```
For brevity of this example I hard coded the number of images I wanted to process. In my example I have set it to three, like below:

```python
def main():
	count = 1
	while count < 3:
		resizeImage(count)
		count = count + 1
```

Let's have a look at the resizeImage function. It takes in a number and expects image files to be named like 'pic_1.jpg'. It's also expecting the images to have at least a width or height greater than 600.

It does a simple calculation to adjust the image size. It does this by comparing the width and height and adjusting the size accordingly.

```python
def resizeImage(picNumber):
	img = Image.open("pic_" + str(picNumber) + ".jpg")
	width, height = img.size
	if width > height and height > 600:
		width = width - ((width / height) * (height - 600))
		height = height - (height - 600)
	elif height > width and width > 600:
		height = height - ((height / width) * (width - 600))
		width = width - (width - 600)
	elif height == width and height > 600 and width > 600:
		height = 600
		width = 600
	img = img.resize((int(width), int(height)))
	img.save("pic_" + str(picNumber) + ".jpg", format='JPEG')
```

# Summary

 It's a simple example of image manipulation but a useful one. Follow this link http://effbot.org/imagingbook/image.htm to have a look for more information about image manipulation using Python.