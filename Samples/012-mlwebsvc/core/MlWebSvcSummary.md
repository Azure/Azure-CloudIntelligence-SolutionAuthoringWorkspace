This pattern demonstrates how to create a Machine Learning Workspace, copy an Experiment into it and deploy it as a Web service.

The pattern involves the following two steps:

1. Deploy an ARM template which creates a Storage Account, a Machine Learning Workspace and an Azure Function App Service. 
2. Invoke the Azure Function App Service. This Function App Service does the following:
	* Copy an Experiment from the Cortana Intelligence Gallery into the new Machine Learning Workspace.
	* Run the Experiment.
	* Deploy the Experiment as Machine Learning Web Service.
	* Return the URLs for the Experiment and the Web Service, as well as the API Key for the Web Service.

