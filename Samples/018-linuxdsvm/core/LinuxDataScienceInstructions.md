## Accessing your VM

You can access your newly created Data Science VM for Linux in several ways.

#### Terminal

Use the following credentials to log in:

* *VM Name*: {Outputs.vmName}
* *Admin Username*: {Outputs.adminUsername}
* *SSH Command*: {Outputs.sshConnection}

#### X2Go Client

You can download The X2Go client from the [X2Go site](http://wiki.x2go.org/doku.php/start).

* *Host*: {Outputs.dnsName}
* *Login*: {Outputs.adminUsername}
* *SSH port*: 22
* *Session Type*: Change the value to _XFCE_

#### PuTTY

You can download PuTTY from the [PuTTY site](http://www.putty.org/).

* *Host Name*: {Outputs.dnsName}
* *Port*: 22
* *Connection Type*: SSH
* *login as*: {Outputs.adminUsername}

#### Azure Portal

* Click [here]({Outputs.firstDataScienceVmUrl}) to view the first of your {Outputs.numInstances} DSVM instances on the Azure portal.
* Click [here]({Outputs.storageAccountUrl}) to view your Storage Account on the Azure portal.

## Resources
* [Documentation](https://docs.microsoft.com/en-us/azure/machine-learning/machine-learning-data-science-dsvm-ubuntu-intro)
*  [Data science on the Linux Data Science Virtual Machine](https://azure.microsoft.com/en-us/documentation/articles/machine-learning-data-science-linux-dsvm-walkthrough/)
* [Included Tools](https://docs.microsoft.com/en-us/azure/machine-learning/machine-learning-data-science-virtual-machine-overview)
*  [Pricing and alternative provisioning](https://azuremarketplace.microsoft.com/en-us/marketplace/apps/microsoft-ads.linux-data-science-vm-ubuntu)
*  [Team Data Science Process Overview](https://azure.microsoft.com/en-us/documentation/learning-paths/data-science-process/)
*  Contact Paul Shealy <paulsh@microsoft.com> if you have questions about this Data Science Virtual Machine for Linux.