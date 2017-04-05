---
layout: default
title: Solution Publishing
navigation_weight: 3
---
# Solution Publishing
As part of the effort to make solution authoring and publishing self-service, we are introducing a few changes in the way you get your solutions onto our platform.

_We now offer a self-service way to publish your patterns onto the CIS Gallery. This means that you can publish your authored solution & upload the associated 'Try It Now' dashboards all directly from the CIS 'Custom Solutions' view with no interaction with the CIS development team. This will allow you to publish your solutions per your desired schedule._

## What is in place today ?

Today, as authors, a typical workflow to move solutions into
CIS would be as follows:

- Create the solution either on your local workspace or SAW (Solution Authoring Workbench). 
- Deploy the solution onto "Custom Solutions" on CIS. 
- Run tests against your solution. 
- Iterate on the solution based on your tests and re-deploy if necessary. 

When you are confident that the solution is stable, typically, you would proceed to getting your solution checked into our source repository on [https://msdata.visualstudio.com](https://msdata.visualstudio.com). This is necessary because the process of deploying these patterns was a fairly manual one involving us using the checked in source of the solutions to deploy the solution onto CIS. A CIS engineer typically ran this while running a production deployment and, at times, would run it out of band as well.

When attempting to check in the solution to our source repos, you would: 

- Pull the CIQS git codebase onto your local machine. 
- Move the pattern files manually over to a specific directory under the CIS source code, being mindful of the file structure of your solution. 
- Commit your changes onto a feature branch. 
- Submit a pull request to our team. 
- Once the pull request is completed, we would review, approve and merge this into our source. 

Deploying this solution onto CIS would involve us moving all patterns in this source directory into our CIS repository as we mentioned earlier. This is problematic because it: 

- Does not give the author the freedom to publish his/her solution at their will. 
- Involves the overhead of engaging a CIS engineer to run a deployment script to load the solutions into CIS.

To mitigate this, we are introducing a few changes to this process.

## What's changing ?
- You will now be able to publish your solutions directly from the _"Custom Solutions" _tab on CIS (provided you are registered as an author for the solution).
- New solutions will require the authors to get in touch with the CIS team to have them issued a _Solution ID_ and registered as a valid author for the new solution. 
- Once this is done, he/she is free to publish at their own will. 
- The marking of new solutions as public is still controlled by the CIS team and will be done once approvals/clearance is provided.
- **Publishing to the Gallery will still be a manual process managed by us. But we are working with the Gallery team to include this into the self-service workflow.**
- As an author, you will continue to check in the source of your patterns into git into our CIQS repository. This is required as you will need the solution source to update it at a later time. 
- **It is required that all solution updates undergo code review via pull requests into this repository before any publishing can be done. This code review must be a pull request with the intent of merging the solution source into CIQS under the Product/Source/Patterns/Data folder. Do not publish solutions without checking the source in. Any ad-hoc publishing without a backing source check-in will be at the author’s risk.**

## So what has changed for authors ?

You now publish your solutions as and when you choose to do so. We will set up a set of designated
contributors for a solution and as long as you are one of them, you can go ahead and do it on CIS! Here’s how you do it:

1. Create your solution assets either on your desktop or on SAW web. 
2. Use SAW to deploy the solution assets onto CIS so that your solution will now be visible on "Custom Solutions". 
3. Run tests against your solution. 
4. Iterate on the solution based on your tests and re-deploy if necessary. 
5. Once you are confident of these changes, submit a Pull Request of the files you deployed via SAW to the CIQS team under the patterns folder (Note that as far as source control goes, nothing has changed. You still need to add your pattern source to the Product/Source/Patterns/Data folder.)
7. Once you have the required approvals on the pull request, hover over the solution in 'Custom Solutions' and click the 'Publish' button. 

1. This will now check if your logged in account has the necessary permissions to publish to this solution (Note: The solution name you use while publishing via SAW must match the solution id used in CIS e.g. Campaign Optimization with Spark has the id: _campaignhdi_).

1. If you lack the required permissions, contact [caqseng@microsoft.com](mailto:caqseng@microsoft.com) to get access.
1. Hit _'Publish'_ and wait for the publish to complete. 
1. Once complete, the gallery will refresh with your new pattern changes in _5 minutes_. 
2. Head over to the gallery and you should see your changes! 

 All current authors have been registered as designated contributors to their respective solution. However, if you lack the required permissions to publish your solution, contact us at [caqseng@microsoft.com](mailto:caqseng@microsoft.com). Should you have any issues while publishing do not hesitate to reach out to us at [caqseng@microsoft.com](mailto:caqseng@microsoft.com).

## Checking in Solution Source Code
### Environment set up
* Install [git for windows](https://git-for-windows.github.io/).
* Install [git credential manager](https://github.com/Microsoft/Git-Credential-Manager-for-Windows). [This typically comes installed as part of git for windows, but might require explicit install].
* Install [git-lfs](https://git-lfs.github.com/) client on your desktop.

### Clone the CIQS Source
* Open up a command prompt and create a directory where you would like the source code to be checked out : 
* Create a directory where you would like to clone to source (eg. **E:\mygitrepo**).
* cd to the directory 
```bash
cd E:\mygitrepo
```
* Clone the repository
```bash
git clone https://msdata.visualstudio.com/DefaultCollection/AlgorithmsAndDataScience/_git/CIQS
```
* The above clone command will take ~20-30 minutes to complete (pulling down the Pattern files from GIT LFS takes a while).

### Submit a Pull Request
To submit a codeflow/pull request, follow the steps outlined here: 
- [Making A Code Change](https://www.1eswiki.com/wiki/Making_a_code_change)
- [Getting a Code Change Checked in](https://www.1eswiki.com/wiki/Getting_a_code_change_into_the_product)

-CIS Team 
