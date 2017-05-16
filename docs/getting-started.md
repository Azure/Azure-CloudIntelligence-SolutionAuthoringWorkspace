---
layout: default
title: Getting started
navigation_weight: 2
---
# Getting started

Let's get this straight: we really strive to help you be as efficient as possible when building Solutions. That was and still remains the primary goal of the *Solution Authoring Workspace* (aka *SAW*).

## Wait! What *exactly* is *SAW*?

Good question!

*Solution Authoring Workspace* (SAW) has three ingredients:

- Documentation
  <br>You are reading it now. It will be getting better. We promise.
  
- [Samples](https://github.com/Azure/Azure-CortanaIntelligence-SolutionAuthoringWorkspace/tree/master/Samples)
  <br>They are simple and immediately deployable. Samples can serve as building blocks for the *real stuff* you have in mind.
  
- Tools
  <br>We got some tools to help you build new Solutions and get them into CIQS for testing, sharing and publishing. (Not sure what CIQS is? Check [this]({{ site.baseurl }}) out, maybe!)
  
## Hmm... Okay. And what are those tools?

They come in two packages. Pick whichever you like, or both!

But first, with the flip of a switch, become a Solution Author:
![Enabling authoring features]({{ site.baseurl }}\images\switch.png)

## That was easy! Now what?

Now come the packages.

### Package 1: In-browser Solution authoring environment

Works on any host OS and features Web-based *Visual Studio Code*, the Samples, and essential tools like *SAW CLI*, *Git*, *MSBuild*, *Python*, *npm* and many more. No configuration or installation is required.
![SAW Web]({{ site.baseurl }}\images\sawweb.png)

### Package 2: Local Solution development on Windows

We provide two options: latest stable release and latest build. The latter may be unstable at times, but it's the latest and the greatest! Launch either one directly from the Setup page (if you use Chrome or Firefox, make sure to follow the instructions the section below). A shortcut will also be created on your Desktop.

![]({{ site.baseurl }}/images/sawlocal.png)

#### One-time setup for Chrome and Firefox

SAW local is implemented as a ClickOnce Windows application. This means you will automatically get latests updates.

To make sure the application receives all necessarly configuration upon its initial launch, proper ClickOnce support needs to be enabled in *Chrome* and *Firefox*. (It works out of the box in *Edge* and *IE*.)

There are various third-party plugins that add ClickOnce support to these Browsers. We had success with [Meta4 ClickOnce Launcher](https://chrome.google.com/webstore/detail/meta4-clickonce-launcher/jkncabbipkgbconhaajbapbhokpbgkdc) (Chrome) and [FxClickOnce](https://addons.mozilla.org/en-US/firefox/addon/fxclickonce/) (Firefox)
