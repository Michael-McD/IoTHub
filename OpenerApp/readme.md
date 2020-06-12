# Garage Door Opener Web App and service - CSharp
Raspberry Pi and two relay garage door controller running on Dotnet 3.1.  Has three states Up, Down and Off.

State are triggered upon receiving messages from the Azure IoT Hub. The IoT Hub sends state change messages to the Raspberry Pi when it receives messages from the Opener web app.  The IoT Hub is acting as a message broker in this situation i.e. receiving messages from one source and forwarding them on to a target.

## Create the Azure WebApp resource
* Create (or use an existing Resource Group), e.g.:
`az group create --name GarageDoorOpenerResourceGroup --location "West Europe"`
* Create a Service Plan (or reuse one) e.g.:
`az appservice plan create --name myAppServicePlan --resource-group myResourceGroup --sku FREE`

Now we can create the WebApp resource.
`az webapp create --resource-group myResourceGroup --plan myAppServicePlan --name <app-name> --deployment-local-git`


## Deploy to Azure

Create a deployment user (if one hasn't been created already:
```
az webapp deployment user set --user-name <username> --password <password>

```
__Note:__ this user can be used for all future Azure deployments.

Deploy the web app:
`