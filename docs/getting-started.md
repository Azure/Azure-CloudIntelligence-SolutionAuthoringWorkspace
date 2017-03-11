---
layout: default
title: Getting started
navigation_weight: 2
---
# Getting started

## SAW Web

### Known Issues and how to fix them:

#### Error when running saw deploy in SAW Web

Unhandled Exception: System.Reflection.TargetInvocationException: Exception has been thrown by the target of an invocation. ---> System.ArgumentNullException: Value cannot be null.
Parameter name: connectionString
   at Microsoft.WindowsAzure.Storage.CloudStorageAccount.Parse(String connectionString) in c:\Program Files (x86)\Jenkins\workspace\release_dotnet_master\Lib\Common\CloudStorageAccount.cs:line 486
   at Microsoft.Ciqs.Saw.Phases.SolutionDeployerPhase.set_SolutionStorageConnectionString(String value) in C:\work\saw\src\SawPhases\SolutionDeployerPhase.cs:line 31
   --- End of inner exception stack trace ---
... etc
   
#### How to fix    

Run the saw configure command from the SAW Web console.  The command is available to cut and paste (and click Enter) from CUSTOM SOLUTIONS > Setup and it should look something like this:

```saw configure -SolutionStorageConnectionString "DefaultEndpointsProtocol=https;AccountName=stgup88difkfhcjcj2;AccountKey=jhFT9xgQ86g766qMCrHejbMfnnaDlxzyt75EKXryl2Dc3145693f553HFq1QCitkVbfvYEEWF65mrURWOkuNGd66Q=="```

#### After fixing the error above you may get a 2nd error when running saw deploy in SAW Web

Unhandled Exception: System.IO.DirectoryNotFoundException: Could not find a part of the path 'D:\$Recycle.Bin\core'.
   at System.IO._Error.WinIOError(Int32 errorCode, String maybeFullPath)
   at System.IO.FileSystemEnumerableIterator 1.CommonInit()
   at System.IO.FileSystemEnumerableIterator 1..ctor(String path, String originalUserPath, String searchPattern, SearchOption searchOption, SearchResultHandler 1 resultHandler, Boolean checkHost)
... etc

#### How to fix    

Run the saw configure command from the SAW Web console. Cut and paste the following and click Enter.

```saw configure -SolutionDirectory "d:\home\saw\workspace\Solutions"```

## SAW Local (Windows only)
> **Alpha** version. It works, but the installer isn't signed, so expect some turbulence during the installation. Stable builds will be available soon.

[Download](https://ci.appveyor.com/project/wdecay/azure-cortanaintelligence-solutionauthoringworkspa/branch/master/artifacts) the latest build. Don't forget the configuration command from the Setup tab!
