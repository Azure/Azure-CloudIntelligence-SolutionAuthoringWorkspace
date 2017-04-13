---
layout: default
title: Solution Publishing
navigation_weight: 3
---
# Solution Publishing

<div class="alert alert-danger">
This is a test This is a test
</div>

As part of the effort to make solution authoring and publishing self-service, we are introducing a few changes in the way you get your solutions onto our platform.

_We now offer a self-service way to publish your patterns onto the CIS Gallery. This means that you can publish your authored solution & upload the associated 'Try It Now' dashboards all directly from the CIS 'Custom Solutions' view with no interaction with the CIS development team. This will allow you to publish your solutions per your desired schedule._

You now publish your solutions as and when you choose to do so. We will set up a set of designated
contributors for a solution and as long as you are one of them, you can go ahead and do it on CIS! Here’s how you do it:

## Steps
1. Contact the CIS team to have a solution ID issued for you at [caqseng@microsoft.com](mailto:caqseng@microsoft.com). 
2. Create your solution in your workspace. 
2. Test the solution by deploying it using SAW (Solution Authoring Workbench) into 'Custom Solutions'.
3. [Check the solution into CIS source code](#checking-in-solution-source-code) 
4. [Publish the solution](#publishing-your-solution-to-cis).

## Checking in Solution Source Code
### Environment set up
* Install [git for windows](https://git-for-windows.github.io/).
* Install [git credential manager](https://github.com/Microsoft/Git-Credential-Manager-for-Windows). [This typically comes installed as part of git for windows, but might require explicit install].
* Install [git-lfs](https://git-lfs.github.com/) client on your desktop.

### Clone the CIQS Source
* Open up a command prompt.
* Create a directory where you would like to clone to source (eg. **E:\mygitrepo**).
* cd to the directory 
```bash
cd E:\mygitrepo
```
* Ensure you have git lfs installed by typing:
```bash
git lfs
```
* You should see details of the installed version of git lfs like so:
  ```bash
  git-lfs/1.5.5 (GitHub; windows amd64; go 1.7.4; git c2dcd6f5)
  git lfs <command> [<args>]    
  ```
* If you don't see the above, **stop here** and go to [Environment set up](#environment-set-up) and install git lfs.
* Clone the repository
```bash
git clone https://msdata.visualstudio.com/DefaultCollection/AlgorithmsAndDataScience/_git/CIQS
```
* The above clone command will take ~20-30 minutes to complete (pulling down the Pattern files from GIT LFS takes a while).

### Submit a Pull Request
* Ensure you have the CIQS source code cloned from git. If not go to [Clone the CIQS Source](#clone-the-ciqs-source) and do so now.
* cd to the CIQS directory where your source is cloned. 
* cd to the *Patterns Data* directory.
```bash
  cd Product\Source\Patterns\Data 
```
* Create or cd into the folder for your pattern.
```bash
  cd vehicletelemetry
```
* Add the assets/ and core/ folders for your pattern here. 
* When you are ready with your changes, create a new branch for your pull request:
```bash 
  git checkout -b users/<YOUR MICROSOFT ALIAS HERE>/myfeature
```
* Add your changes to staging:
  * Added or edited files: ```git add file1.cs file2.cs folder\folder\file3.cs```
  * Deleted files: ```git rm unluckyfile.cs```
  * Moved files: simply add the new and remove the old
  * Stage everything added/edited/deleted: ```git add -A```
* Commit your changes locally:
```bash
  git commit -m "<Add a great commit message here>"
```
* Push your changes to the remote feature branch:
```bash
  git push -u origin users/<YOUR MICROSOFT ALIAS HERE>/myfeature
```
* Go to <a href="https://msdata.visualstudio.com/AlgorithmsAndDataScience/CIQS%20Platform/_git/CIQS/pullrequests" target="_blank">Pull Request Dashboard on MSData</a>.
* Select **New Pull Request**.
* Set "Review changes in Select a Branch" to:```users/<YOUR MICROSOFT ALIAS HERE>/myfeature```
* Keep the target branch as master and create the pull request. 

### Reacting to Feedback
People will leave feedback on your PR. In this case, you'll need to update your PR with changes. You do that locally on the same branch you started from.
* Checkout your local branch: ```git checkout users/<YOUR MICROSOFT ALIAS HERE>/myfeature```
* Make and commit your changes.
* Push your changes: ```git push```
* If others are working in your branch with you, you might have to [resolve merge conflicts](https://www.visualstudio.com/en-us/docs/git/tutorial/merging) before the push will complete.

High level details on making code changes on MS Data: 
- [Making A Code Change](https://www.1eswiki.com/wiki/Making_a_code_change)
- [Getting a Code Change Checked in](https://www.1eswiki.com/wiki/Getting_a_code_change_into_the_product)

### Finishing the PR
Once reviewers have signed off and policies are met, [complete the pull request](https://www.visualstudio.com/en-us/docs/git/pull-requests#complete-the-pull-request). If other changes have happened in your mainline branch, you may again have to deal with merge conflicts.

## Publishing your solution to CIS
1. Create your solution assets either on your desktop or on SAW web. 
2. Use SAW to deploy the solution assets onto CIS so that your solution will now be visible on "Custom Solutions". 
3. Run tests against your solution. 
4. Iterate on the solution based on your tests and re-deploy if necessary. 
5. Once you are confident of these changes, submit a Pull Request of the files you deployed via SAW to the CIQS team under the patterns folder (Note that as far as source control goes, nothing has changed. You still need to add your pattern source to the Product/Source/Patterns/Data folder.)
6. [Refer here for details on making a change and submitting a pull request](#checking-in-solution-source-code).
7. Once you have the required approvals on the pull request, hover over the solution in 'Custom Solutions' and click the 'Publish' button. 

1. This will now check if your logged in account has the necessary permissions to publish to this solution (Note: The solution name you use while publishing via SAW must match the solution id used in CIS e.g. Campaign Optimization with Spark has the id: _campaignhdi_).

1. If you lack the required permissions, contact [caqseng@microsoft.com](mailto:caqseng@microsoft.com) to get access.
1. Hit _'Publish'_ and wait for the publish to complete. 
1. Once complete, the gallery will refresh with your new pattern changes in _5 minutes_. 
2. Head over to the gallery and you should see your changes! 

 All current authors have been registered as designated contributors to their respective solution. However, if you lack the required permissions to publish your solution, contact us at [caqseng@microsoft.com](mailto:caqseng@microsoft.com). Should you have any issues while publishing do not hesitate to reach out to us at [caqseng@microsoft.com](mailto:caqseng@microsoft.com).

## Gallery Publish Details
The gallery publishing as of now **will still be a manual publish run by CIS team.** This is because we are waiting on a dependency from the Gallery team before we can include it into the publishing process. However, almost all the elements of a Gallery page are controlled by the author via the Manifest.xml file in your solution

### What Elements are parsed from Manifest.xml ? 
Taking [Vehicle Telemetry solution](https://gallery.cortanaintelligence.com/Solution/Vehicle-Telemetry-Analytics-9) as an example:

On the right pane,
- the Image is what is provided via the Manifest in ["ImageUrl" tag](https://msdata.visualstudio.com/AlgorithmsAndDataScience/CIQS%20Platform/_git/CIQS?path=%2FProduct%2FSource%2FPatterns%2FData%2Ftelcocustomerchurnv2%2Fcore%2FManifest.xml&version=GBmaster&_a=contents&line=6&lineStyle=plain&lineEnd=6&lineStartColumn=1&lineEndColumn=68) -- [transform code block](https://msdata.visualstudio.com/AlgorithmsAndDataScience/CIQS%20Platform/_git/CIQS?path=%2FProduct%2FSource%2FCaqs%2FUX%2FUtilities%2FCIGalleryConverter.cs&version=GBmaster&_a=contents&line=112&lineStyle=plain&lineEnd=112&lineStartColumn=13&lineEndColumn=57).
- The Services Used is generated from the ["Ingredients" tag](https://msdata.visualstudio.com/AlgorithmsAndDataScience/CIQS%20Platform/_git/CIQS?path=%2FProduct%2FSource%2FPatterns%2FData%2Ftelcocustomerchurnv2%2Fcore%2FManifest.xml&version=GBmaster&_a=contents&line=9&lineStyle=plain&lineEnd=15&lineStartColumn=1&lineEndColumn=17) -- [transform code block](https://msdata.visualstudio.com/AlgorithmsAndDataScience/CIQS%20Platform/_git/CIQS?path=%2FProduct%2FSource%2FCaqs%2FUX%2FUtilities%2FCIGalleryConverter.cs&version=GBmaster&_a=contents&line=318&lineStyle=plain&lineEnd=334&lineStartColumn=1&lineEndColumn=10).
- The Related Links have a few defaults that we put in. But you can add to it using the ["RelatedLinks" tag](https://msdata.visualstudio.com/AlgorithmsAndDataScience/CIQS%20Platform/_git/CIQS?path=%2FProduct%2FSource%2FPatterns%2FData%2Ftelcocustomerchurnv2%2Fcore%2FManifest.xml&version=GBmaster&_a=contents&line=18&lineStyle=plain&lineEnd=21&lineStartColumn=3&lineEndColumn=18) -- [transform code block](https://msdata.visualstudio.com/AlgorithmsAndDataScience/CIQS%20Platform/_git/CIQS?path=%2FProduct%2FSource%2FCaqs%2FUX%2FUtilities%2FCIGalleryConverter.cs&version=GBmaster&_a=contents&line=285&lineStyle=plain&lineEnd=294&lineStartColumn=1&lineEndColumn=14).
- The Tags section is added to using the ["Tags" keyword](https://msdata.visualstudio.com/AlgorithmsAndDataScience/_git/CIQS?path=%2FProduct%2FSource%2FPatterns%2FData%2Fopportunityscoringcrm%2Fsrc%2FCiqsInstaller%2FManifest.xml&version=GBmaster&_a=contents&line=20&lineStyle=plain&lineEnd=23&lineStartColumn=4&lineEndColumn=10) -- [transform code block](https://msdata.visualstudio.com/AlgorithmsAndDataScience/CIQS%20Platform/_git/CIQS?path=%2FProduct%2FSource%2FCaqs%2FUX%2FUtilities%2FCIGalleryConverter.cs&version=GBmaster&_a=contents&line=212&lineStyle=plain&lineEnd=218&lineStartColumn=1&lineEndColumn=14).
- The Related Items section is generated and we do not control it on gallery publish.

## Troubleshooting
### Try It Now Upload Failures
To begin, check the following:
- Ensure that the PBIX files that you are trying to upload are not corrupt. Are you able to open them on your local Power BI desktop ? 
- Does the path you specified in Manifest.xml to the Try It Now PBIX dashboards (You specify this under the ```<TryItNow>``` tag) match exactly (paths are case-sensitive) the relative path in your solution folder ? 

If you still see issues, contact [caqseng@microsoft.com](mailto:caqseng@microsoft.com).

## Notes
- You will now be able to publish your solutions directly from the _"Custom Solutions" _tab on CIS (provided you are registered as an author for the solution).
- New solutions will require the authors to get in touch with the CIS team to have them issued a _Solution ID_ and registered as a valid author for the new solution. 
- Once this is done, he/she is free to publish at their own will. 
- The marking of new solutions as public is still controlled by the CIS team and will be done once approvals/clearance is provided.
- **Publishing to the Gallery will still be a manual process managed by us. But we are working with the Gallery team to include this into the self-service workflow.**
- As an author, you will continue to check in the source of your patterns into git into our CIQS repository. This is required as you will need the solution source to update it at a later time. 
- **It is required that all solution updates undergo code review via pull requests into this repository before any publishing can be done. This code review must be a pull request with the intent of merging the solution source into CIQS under the Product/Source/Patterns/Data folder. Do not publish solutions without checking the source in. Any ad-hoc publishing without a backing source check-in will be at the author’s risk.**

-CIS Team 
