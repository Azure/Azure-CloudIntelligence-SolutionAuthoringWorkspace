---
layout: default
title: Home
navigation_weight: 1
---

# Authoring Azure Solutions with the Solution Authoring Workspace (SAW) and the Cloud Intelligence Quickstart (CIQS)

*CIQS* is a service that orchestrates solution deployment into a customer's *Azure* subscription by performing a series of provisioning activities that fall into two categories:
- Cloud resource creation (essentially, things that can be achieved with ARM templates)
- Execution of custom provisioning code (things that are not possible to achieve with ARM)

The deployment progress is surfaced to the user through a UX which allows collecting input parameters and displaying customizable and interactive multi-media content such as textual instructions, images, and embedded *Power BI* dashboards with live data visualization.

In the event of an error, the UX displays a message explaining the error and, under certain conditions, allows graceful recovery from the failure.

In order to unlock the full potential of our platform, we needed a good set of tools that would enable more teams and individuals to build new Solutions with as little effort as possible. The complexity of this process is why *SAW* or *Solution Authoring Workspace* came about as a tool that eliminates the hurdles from the authoring process and lets the developer focus on the problem rather than the specifics of a framework.

To get started, the first first to the [CUSTOM SOLUTIONS page](https://quickstart.azure.ai/CustomSolutions) and click *Setup*.  Then select the subscription and location for the tool to use when configuring the authoring environment.  The setup process takes roughly 2 minutes.

We provide 2 options:
- in-browser authoring environment (Requires no setup and is compatible with any modern browser and any host OS.)
- local installation (Windows only, requires downloading and installing an MSI package)

Both options offer an identical set of features.

The in-browser environment comes with a set of Samples that are ready to be consumed examples of *Cloud Intelligence* solutions. The Samples can serve as building blocks for new customer-facing Solutions. For example:

- [one of the samples](https://github.com/Azure/Azure-CloudIntelligence-SolutionAuthoringWorkspace/tree/master/Samples/004-cognitiveservices) demonstrates how to create a solution that would enable anybody with an Azure subscription to create an app similar to Microsoft's viral how-old.net using *Azure Cognitive Services APIs* and *Azure Functions*;
- [another notable example](https://github.com/Azure/Azure-CloudIntelligence-SolutionAuthoringWorkspace/tree/master/Samples/009-twitterstreaming) is stream analytics with ML demonstrating how to perform sentiment analysis of Twitter data. It shows how to write a data ingestion piece (implemented as a WebJob written in *Python*), send streaming data to an event hub, run it through a *Stream Analytics Job* that in turn calls *Machine Learning Web API* and finally, visualize the results inside an embedded *Power BI* dashboard.

To create a new Custom Solution and publish it into CIQS, put the source code into the Solution directory (for instance, let's take the cognitive services sample mentioned earlier).

Users can publish custom solutions into CIQS by running a shell command from the Windows console. The command is: saw push (If local installation is used, it would be available in the regular windows command prompt and the rest is the same.) After it completes, the solutions are immediately available for deployment and can be found in the *My Solutions* section.

Notable *SAW* features include:
- Full *Git* support in the Web environment (thanks to the *App Service Editor* aka *Visual Studio Online "Monaco"*); this is really great for collaborative solution authoring (example - git clone and then saw deploy)
- Local SAW environment allows incorporating your own tools into the authoring process
- Self-publishing into *Cloud Intelligence* gallery
- The ability to “Share” custom solutions to anyone with your link

Questions? [Contact us!](mailto:cisauthors@microsoft.com)


