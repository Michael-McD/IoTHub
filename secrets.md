# Secrets configuration

ASP.Net Core supports a number of integrated secret management options.  The two options used here:

* [Secret Manager](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.1&tabs=linux): Development and local runtime storage
* [Azure Key Vault](https://docs.microsoft.com/en-us/aspnet/core/security/key-vault-configuration?view=aspnetcore-3.1#use-managed-identities-for-azure-resources): Azure Web Apps deployments

## The Opener Daemon App on the Raspberry Pi
The __Opener__ Linux daemon running on the Pi uses the Secret Manager configuration to access the IoT Hub.  Secrets are stored on the Pi within a JSON document managed by the Secret Manager:

```
{
  "Opener.Models.SecretOptions": {
    "DeviceConnStr": <Pi's IoT connection string including secret access key>
  }
}
```
### Secrets on the Raspberry Pi
Secrets can be added by editing the JSON file directly or using the CLI. The Secret Manager is configured calling the `ServiceProviderBuilder.GetServiceProvider()` static method and accessed using returned service e.g. `services.GetRequiredService<IOptions<SecretOptions>>()`.  

__Note__ as the App running on the Pi is executing as a Linux daemon the Secret Mangers service configuration is not configured within the programs Main entry points Host Builder (as it would be if it where running as hosted ASP.Net HTTP client - which is what the Secret Manager was designed for).

### Secrets on the Opener Web App
The __OpenerApp__ which can be run both locally and on Azure uses both storage options.  For local execution it uses the Secret Manager and when deployed to and running on Azure it uses the the Azure Key Vault.

Configuration of both the local development secret access using the Secret Manager and the Azure deployed (i.e. the production deployment) is done within the CreateHostBuilder() method called in the Program Main entry point.  Actually the Secret Manager is configured automatically by the Host Builder.

### Azure Key Vault on the Opener Web App
CLI commands to set teh the Key Vault up adn configure teh App to be able to access it are outlined below.

Enable System Assigned Identity for the web app: `az webapp identity assign -g MyResourceGroup -n MyUniqueApp`

Find the apps Id: `az webapp identity show --name MyWebapp --resource-group MyResourceGroup`

Restart the webapp: `az webapp restart --name MyWebapp --resource-group MyResourceGroup`

### Key Vault setup

* Create the Key Vault: `az keyvault create --name MyKeyVault --resource-group "MyResourceGroup" --location us east`

* Get the Service Connection String from the output of the Device_Infrastructure.sh script (or run this `az iot hub show-connection-string --policy-name service --name MyIoTHub --output tsv`).

* Add the connection string: `az keyvault secret set --vault-name MyVaultName --name "SecretName" --value "MyIoTConnectionString"`

* Set the policy so the app can access the Key Vault: `az keyvault create --name MyKeyVault --resource-group "MyResourceGroup" --location us east`