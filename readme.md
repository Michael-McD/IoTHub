# Garage Door Opener Project - CSharp

This is an IoT Project using a Raspberry Pi 3, and an Azure WebApp communicating via an Azure IoT Hub.

Code for the Raspbery Pi is in the _Opener_ folder along with a [readme.md](Opener/readme.md) markdown document describing the project and it's build and deployment processes. 
The Device_Infrastructure.sh bash script creates the IoT Hub instance and reports both the device's URL and the IoT host services URL (as used in the OpenerApp).

Code and a [readme.md](OpenerApp/readme.md) document for the Opener Web App is in the _OpenerApp_ folder.
