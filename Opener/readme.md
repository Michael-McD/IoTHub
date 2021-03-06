# Garage Door Opener IoT Device - CSharp
Raspberry Pi and two relay garage door controller running on Dotnet 3.1.  Has three states Up, Down and Off.

State are triggered upon receiving messages from the Azure IoT Hub. The IoT Hub sends state change messages to the Raspberry Pi when it receives messages from the Opener web app.  The IoT Hub is acting as a message broker in this situation i.e. receiving messages from one source and forwarding them on to a target.

The progame makes use of Visual Studio Codes `tasks.json` and `launch.json` configurations to run the code in remote debug mode on the Raspberry Pi while developing on a Linux PC.  There is also a useful Deploy Release task.

The Pi's Opener program is designed to run as a Linux Daemon under `systemd`.  However it can still be launched from the command line if required e.g. `./Opener` on the Pi.  Details for setting up `systemd` __unit__ files is presented towards the end of this readme.md document.


## .vscode/task.json
There are 3 tasks
* build
* publish
* deploy

The deploy task is the more interesting.  It takes the `publish` task output and deploys it on the Pi. Resync is used to deploy only those files which change. This is more efficient than using SCP to copy the entire contents of the Publish task output.

Note: SSH keys are used for authentication. This requires that the host public key has been added to the Pi's `~/.ssh/authorized_keys` file.  See below on how this is setup.

## .vscode/launch.json
Configures remote debug mode and triggers the `deploy` task (and its `publish` and `build dependencies) and _launches_ the progame in remote debug mode on the Pi.

See: [OmniSharp remote debugging](9https://github.com/OmniSharp/omnisharp-vscode/wiki/Attaching-to-remote-processes) and [OmniSharp remote debugging on Linux Arm devices](https://github.com/OmniSharp/omnisharp-vscode/wiki/Remote-Debugging-On-Linux-Arm).

## Azure IoT Hub
This requires both the Azure CLI and Azure IoT Sdk to be install on the development machine.

```
$ curl -sL https://aka.ms/InstallAzureCLIDeb | sudo bash
$ az extension add --name azure-iot
```

Create the infrastructure using the `Device_Infrastructure.sh` shell script. You will need to be logged into the Azure CLI.  In addition to building the infrastructure the script will print out the the Device and Service connection strings. These need to be configured as secrets. 


## The code
Secrets including Device Connection info is stored using the new [Dotnet Secret Manager](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.1&tabs=windows).



### SSH requirements
The Pi must be running an SSH server (i.e. openSSh). This is not installed on Rasberrian Lite for example.
```
$ sudo apt update
$ sudo apt install openssh-server
```
Check that it's running: `sudo systemctl status ssh`

You will also need to add the hosts public SSH key (e.g. _id_rsa.pub_) to the Pi's authorized keys.
```
$ cd ~/.ssh
$ cat id_rsa.pub | ssh pi@192.168.0.18 'cat >> .ssh/authorized_keys'
```
In addition you will need to give your `pi` user permissions to write to the /opt/garage-door-opener folder.
```
$ sudo chown -R pi: /opt/garage-door-opener
$ chmod -R u=rw,go=r /opt/garage-door-opener
$ find /opt/garage-door-opener -type d | xargs chmod u+x
```
