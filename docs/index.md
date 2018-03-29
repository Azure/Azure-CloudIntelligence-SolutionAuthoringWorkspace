---
layout: default
title: Home
navigation_weight: 1
---
*This is based on my 5 minute CIQS/SAW infomercial at the All Hands on February, 24th. Putting it here as a placeholder for now.* -andrew
# Hello everybody.

Some of you may have heard about *CIQS* or *Cloud Intelligence Quick Start*, the deployment engine behind *Cloud Intelligence Solutions*. For those who haven't, *CIQS* is the service that orchestrates solution deployment into a customer's *Azure* subscription by performing a series of provisioning activities that generally fall into such categories as
- Cloud resource creation (essentially, things that can be achieved with ARM templates)
- Execution of custom provisioning code (things that are not possible to achieve with ARM)

The deployment progress is surfaced to the user through a simple and intuitive UX which allows collecting input parameters and displaying customizable and interactive multi-media content such as textual instructions, images, and embedded *Power BI* dashboards with live data visualization.

In the unlikely event of an error, the UX displays a friendly message and, under certain conditions, allows graceful recovery from the failure.

These capabilities make our platform an excellent medium for engaging with the customer, both business and technical decision maker; and also a convenient and powerful educational tool that, through example, helps drive the adoption of new and existing *Microsoft* cloud technology.

In order to unlock the full potential of our platform, however, we needed a good set of tools that would enable more teams and individuals to build new Solutions with as little effort as possible. Some of you, the early adopters, may remember how difficult (even painful) it was to create and debug a new solution. The complexity of it is the reason why *SAW* or *Solution Authoring Workspace* came about as a tool that eliminates most if not all hurdles from the authoring process and lets the developer fully focus on the problem rather than the specifics of our framework.

So, what does it take to create a new *Cloud Intelligence* Solution now?

The first step is going to the CUSTOM SOLUTIONS page (by the way it now contains an *Overview* section with links to many useful resources) and then clicking *Setup*. Becoming a solution author takes only a couple mouse clicks, which boil down to selecting the subscription and location for the tool to use when configuring the authoring environment. After selecting those, the setup takes roughly 2 minutes. And then you are all set!

We provide 2 options:
- in-browser authoring environment (It doesn't require any setup and is compatible with literally any host OS.)
- local installation (Currently works on Windows and requires downloading and installing a tiny MSI package)

Both options offer essentially the identical set of features.

So let's take a look at the in-browser environment. It comes with a set of Samples that are easy-to-follow simple and ready to be consumed examples of *Cloud Intelligence* solutions. The Samples can serve as building blocks for new customer-facing Solutions. For example:

- [one of the samples](https://github.com/Azure/Azure-CloudIntelligence-SolutionAuthoringWorkspace/tree/master/Samples/004-cognitiveservices) demonstrates how to create a solution that would enable anybody with an Azure subscription to create an app similar to Microsoft's viral how-old.net using *Azure Cognitive Services APIs* and *Azure Functions*;
- [another notable example](https://github.com/wdecay/twitterdemo) is stream analytics with ML demonstrating how to perform sentiment analysis of Twitter data. It shows how to write a data ingestion piece (implemented as a WebJob written in *Python*), send streaming data to an event hub, run it through a *Stream Analytics Job* that in turn calls *Machine Learning Web API* and finally, visualize the results inside an embedded *Power BI* dashboard.

Here's are the examples of the end results once the user deploys these solutions into their own subscription.
(images or links go here)

Let's see what it takes to create a new Custom Solution and publish it into CIQS. All that needs to be done is putting the source code into the Solution directory (for instance, let's take the cognitive services sample mentioned earlier).

Publishing custom solutions into CIQS is a matter of running a shell command from the Windows console. The command is: saw deploy (If local installation is used, it would be available in the regular windows command prompt and the rest is the same.) After it completes, the solutions are immediately available for deployment and can be found in the *My Solutions* section.

Finally, I would like to call out several cool things about *SAW*:
- Full *Git* support in the Web environment (thanks to the *App Service Editor* aka *Visual Studio Online "Monaco"*); this is really great for collaborative solution authoring (example - git clone and then saw deploy)
- Local SAW environment allows incorporating virtually any tools into the authoring process
- Self-publishing into *Cloud Intelligence* gallery is going to be available in the near future along with the “Share” feature allowing sharing the solution with a limited audience
- Fast adoption and lots of new Samples and features coming up

Questions? [Contact us!](mailto:cisauthors@microsoft.com)


