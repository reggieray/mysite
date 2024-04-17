Title: Getting started with ML.NET
Published: 7/7/2022
Tags: 
- dotnet
- ML.NET
- AutoML
- dotnet 7
- csharp

---
# Intro

ML.NET is a free, open-source machine learning framework developed by Microsoft. It allows developers to easily integrate machine learning functionality into their applications using their favorite language. ML.NET offers a wide range of pre-built models and makes it easy to build custom machine learning models without requiring expertise in the field. It is regularly updated with new features and capabilities.

# Brief Example

 here's a example that shows how to use ML.NET to build a sentiment analysis model that can predict whether a piece of text is positive or negative:

 ```csharp
// Define the input data schema
var dataView = mlContext.Data.LoadFromTextFile<SentimentData>(dataPath, hasHeader: true, separatorChar: ',');

// Define the pipeline
var pipeline = mlContext.Transforms.Text.FeaturizeText("Features", "Text")
    .Append(mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: "Label", featureColumnName: "Features"));

// Train the model
var model = pipeline.Fit(dataView);
```

Once the model has been trained, you can use it to make predictions on new text data. For example, if you wanted to predict the sentiment of the following text: "I love ML.NET!", you could do so like this:

 ```csharp
// Define the input data
var input = new SentimentData()
{
    Text = "I love ML.NET!"
};

// Use the model to make a prediction
var prediction = model.Predict(input);
```

In this case, the model would likely predict that the sentiment of the text is positive.

As you can see, building a sentiment analysis model with ML.NET is simple and straightforward. With just a few lines of code, you can create a model that can accurately predict the sentiment of a piece of text.

# AutoML

It's even easier to get started with AutoML which is part of Visual Studio. [ML.NET Tutorial - Get started in 10 minutes](https://dotnet.microsoft.com/en-us/learn/ml-dotnet/get-started-tutorial/intro) as the name suggests is a tutorial on how to get started in 10 minutes. This is a guide that demonstrates how to build a ML model to predict sentiment analysis. For brevity I won't into too much detail of the tutorial, all the information is in the link above.

It says in 10 minutes, but I was able to do it in 2 minutes. Although I did already know what to do, that said I think it's worth highlighting I could do this at all in the space of a few minutes. I do feel I'm standing on the shoulders of giants to leverage this type of technology.

<iframe width="560" height="315" src="https://www.youtube.com/embed/_N1frA_5_D0" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>

The source code in the video is available [here](https://github.com/reggieray/sentiment-api)

A link is provided to the data that is used to train this model. The dataset linked is a direct file link from [UCI Machine Learning Repository](https://archive.ics.uci.edu/ml/datasets.php), a direct link is provided probably as not to distract you from the task at hand, but I think it's worth exploring even to just understand what datasets look like to train ML models. Another notable mention that I discovered is [kaggle.com](https://www.kaggle.com/) which also has multiple datasets available.

# Conclusion

Getting started with ML.NET was a lot easier than I expected, especially with AutoML and with the click of a few buttons I was able to train a model and then use that model in a API! I was pretty amazed what you can achieve and found it fascinating learning how the general flow goes into creating a ML model.

The clever stuff is abstracted away which is the training part, but it doesn't hide what it used to train the model, you can see this in the output when the training is running and if you wanted too, you could do more of deeper dive into the world of machine learning.


