# Garage Door Opener Web App and service - CSharp
The OpenerWebApp is used to send control signals to the Raspberry Pi. It is deployed separately form the Opener device.  I.e. the device code is deployed to the Raspberry Pi while the web app is deployed up to the cloud.

### Background
State are triggered upon receiving messages from the Azure IoT Hub. The IoT Hub sends state change messages to the Raspberry Pi when it receives messages from the Opener web app.  The IoT Hub is acting as a message broker in this situation i.e. receiving messages from one source and forwarding them on to a target.

## Create the Azure WebApp resource
* Create (or use an existing Resource Group), e.g.:
`az group create --name GarageDoorOpenerResourceGroup --location "West Europe"`
* Create a Service Plan (or reuse one) e.g.:
`az appservice plan create --name myAppServicePlan --resource-group myResourceGroup --sku FREE`

Now we can create the WebApp resource.
`az webapp create --resource-group myResourceGroup --plan myAppServicePlan --name <app-name> --deployment-local-git`

Note the `deploymentLocalGitUrl` key value returned from the `az webapp create` command (this is what the _--deployment-local-git_ option does).

## Deploy to Azure

Create a deployment user (if one hasn't been created already:
```
az webapp deployment user set --user-name <username> --password <password>

```
__Note:__ this user can be used for all future Azure deployments.

1st we need to add the Azure remote repository to Git.
`git remote add azure <deploymentLocalGitUrl>`

Deploy the web app (make sure the code's been committed to the local repo).:
`git push azure master`