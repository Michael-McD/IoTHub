# Garage Door Opener IoT Hub Project - CSharp

This is an IoT Project using a Raspberry Pi 3, and an Azure WebApp communicating via an Azure IoT Hub.

Code for the Raspberry Pi is in the _Opener_ folder along with a [readme.md](Opener/readme.md) markdown document describing the project and it's build and deployment processes. 
The Device_Infrastructure.sh bash script creates the IoT Hub instance and reports both the device's URL and the IoT host services URL (as used in the OpenerApp).  You will need to add these to a secret store 

* **Device Connection String** To User Manage Secrets on the Pi.
* **Service Connection String** To User Manage Secrets on the local development environment and on Azure's Key Vault.

Code and a [readme.md](OpenerApp/readme.md) document for the Opener Web App is in the _OpenerApp_ folder.
