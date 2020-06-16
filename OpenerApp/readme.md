# Garage Door Opener 
Web App and service - CSharp
The Garage Door Opener Web App is used to send control signals to the Raspberry Pi. It is deployed separately form the Opener device.  I.e. the device code is deployed to the Raspberry Pi while the web app is deployed up to the cloud.

### Background
The Pi's door commands are triggered upon receiving messages from the Azure IoT Hub. The IoT Hub sends commands to the Pi when it receives messages from the web app.  The IoT Hub is acting as a message broker in this situation i.e. receiving messages from one source and forwarding them on to a target.


## Create the Azure WebApp resource
* Create (or use an existing Resource Group), e.g.:
`az group create --name MyResourceGroup --location "us east"`
* Create a Service Plan (or reuse one) e.g.:
`az appservice plan create --name myAppServicePlan --resource-group MyResourceGroup --sku FREE`

Now we can create the WebApp resource.
`az webapp create --resource-group MyResourceGroup --plan MyAppServicePlan --name <app-name> --deployment-local-git`

Note the `deploymentLocalGitUrl` key value returned from the `az webapp create` command (this is what the _--deployment-local-git_ option does).


## Keys and Secrets
Two modes: 
* [Development](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.1&tabs=linux)
* [Production](https://docs.microsoft.com/en-us/aspnet/core/security/key-vault-configuration?view=aspnetcore-3.1#use-managed-identities-for-azure-resources)

Development uses the Secret Manager Tool to store secrets locally.  While in Production mode Secrets stored using the Azure Key Vault.

The secrets in question are the connection strings for the IoT Hub (the Service conn str) and the Device's connection string. Only the former needs to be configured for the web app. (The device always runs in developer mode.)  

Some useful commands: 

Enable System Assigned Identity for the web app: `az webapp identity assign -g MyResourceGroup -n MyUniqueApp`

Find the apps Id: `az webapp identity show --name MyWebapp --resource-group MyResourceGroup`

Restart the webapp: `az webapp restart --name MyWebapp --resource-group MyResourceGroup`

### Key Vault setup

* Create the Key Vault: `az keyvault create --name MyKeyVault --resource-group "MyResourceGroup" --location us east`

* Get the Service Connection String from the output of the Device_Infrastructure.sh script (or run this `az iot hub show-connection-string --policy-name service --name MyIoTHub --output tsv`).

* Add the connection string: `az keyvault secret set --vault-name MyVaultName --name "SecretName" --value "MyIoTConnectionString"`

* Set the policy so the app can access the Key Vault: `az keyvault create --name MyKeyVault --resource-group "MyResourceGroup" --location us east`

## Deploy to Azure

Create a deployment user (if one hasn't been created already:
```
az webapp deployment user set --user-name <username> --password <password>
```
__Note:__ this user can be used for all future Azure deployments.

Using the URL from the _web app create command_ add the Azure remote repository to the local git config: 
`git remote add azure <deploymentLocalGitUrl>`.  

Deploy the web app (make sure the code's been committed to the local repo).:
`git push azure master`

