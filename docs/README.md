# Adding AI to App

## Overview

This repository includes the demo of presentation [Adding AI to App](https://speakerdeck.com/ksivamuthu/adding-ai-to-app)

In this demo, we are using [Microsoft Cognitive Services API's](https://www.microsoft.com/cognitive-services/) to add artificial intelligence to a Xamarin.Forms application. 

The demo is based on these four categories.

### Computer Vision
[Microsoft Cognitive Services Computer API](https://www.microsoft.com/cognitive-services/en-us/computer-vision-api) is used to get tags and caption after taking a picture with device camera / gallery


### Face Emotion - Search Gifs using your face emotion
[Microsoft Cognitive Services Emotion API](https://www.microsoft.com/cognitive-services/en-us/emotion-api) is used to get your emotion after taking a picture of yourself with the device camera. Based on your face emotion, random Gif is shown from Giphy search.

### Custom Vision - Buckled up or not
[Microsoft Cognitive Services Custom Vision API](https://azure.microsoft.com/en-us/services/cognitive-services/custom-vision-service/) is used to train the images and predict whether the driver is buckled up car seat belt or not. It's using downloaded coreml/tensorflow models to do prediction offline.

### Text Analytics
[Microsoft Cognitive Services Text Analytics API](https://azure.microsoft.com/en-us/services/cognitive-services/text-analytics/?v=18.05) is used to analyze the text, detect language, entity recognition, sentiment analysis and keyphrase extraction.

You can also get in touch with me [@ksivamuthu](https://twitter.com/ksivamuthu)  I will be happy to help!

## How to run?

The demo requires,
- Visual Studio or VS for Mac with Xamarin tools installed

## Get API keys
1. To get a trial key and be able to use the APIs, go to [www.microsoft.com/cognitive-services](https://www.microsoft.com/cognitive-services/)
2. Create app and get API key in [Giphy Developer Portal](https://developers.giphy.com)
3. Update the keys in [XamAI/Settings.cs](/XamAI/Settings.cs)
