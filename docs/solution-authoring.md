---
layout: default
title: Solution authoring
navigation_weight: 3
---
# Solution authoring
## Provisioning steps
### ArmDeployment
### Manual
Manual steps are most typically used to display post-deployment instructions. For example:
```xml
<Manual title="Done">
  <Instructions src="Instructions.md" format="markdown" />
</Manual>
```

CIQS has no restrictions as to where *Manual* steps can appear inside the __&lt;ProvisioningSteps&gt;__ section of the *Manifest*, nor is there any limit on their number.

The platform guarantees that no automated activities will be injected prior to the *Manual* steps that appear in the very beginning. This allows collecting all input parameters immediately after a deployment is created and performing the rest of the process without interruptions.

The two examples below demonstrate slightly different ways of collecting input parameters with *Manual* steps. The first one uses an ARM template as a *parameter source* and produces a configuration page exactly like __&lt;ArmDeployment&gt;__, except no deployment occurs afterwards. Instead, all parameters enter the **{Inputs.*}** variable pool.

```xml
<Manual parameterSource="armTemplate.json" title="Setup SQL server account">
  <Parameters>
    <Credential type="sql" username="sqlServerUsername" password="sqlServerPassword" />
  </Parameters>
</Manual>
```
If no **parameterSource** attribute is provided, all parameters need to be defined explicitly. This allows collecting arbitrary parameters and use them in various contexts like, for instance, as inputs of the  **Functions**.
```xml
<Manual title="Configure Twitter listener">
  <Parameters>
    <Parameter name="twitterKeywords" description="Twitter topics" type="string"
      defaultValue="@MicrosoftR,@OpenAtMicrosoft,@Azure,#CortanaIntelligence">

      <ExtraDescription>
        Comma-separated list of words, phrases, #hashtags and @mentions
      </ExtraDescription>
    </Parameter>
    <Parameter name="oauthConsumerKey" description="Consumer key (API key)" type="string" />
    <Parameter name="oauthConsumerSecret" description="Consumer secret (API secret)" type="string" />
    <Parameter name="oauthToken" description="Access token" type="string" />
    <Parameter name="oauthTokenSecret" description="Access token secret" type="string" />
  </Parameters>
</Manual>
```
Finally, if all parameters required to perform an ARM deployment are already present in the variable pool, _{Inputs.*}_ or _{Outputs.*}_, it is not necessary to explicitly define them as part of the __&lt;ArmDeployment&gt;__. Instead, use **autoResolveParameters**. For example, assuming that a *Manual* step has been used to collect parameters for *armTemplate.json*, the subsequent __&lt;ArmDeployment&gt;__ provisioning step can be defined as follows:

```xml
<ArmDeployment source="armTemplate.json" autoResolveParameters="true" title="Deploying..." />
```

An demonstration of this technique can be found in the [twitterstreaming SAW sample](https://github.com/Azure/Azure-CortanaIntelligence-SolutionAuthoringWorkspace/tree/master/Samples/009-twitterstreaming)

### Function
### WebJob
### AzureMLWebService
### SolutionDashboard
### WebJobDeployment
