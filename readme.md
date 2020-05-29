# Garage Door Opener - CSharp
Raspberry Pi and two relay garage door controller running on Dotnet 3.1.  Has three states Up, Down and Off.

State are triggered upon receiving messages from the Azure IoT Hub. The IoT Hub sends state change messages to the Raspberry Pi when it receives messages from the Opener web app.  The IoT Hub is acting as a message broker in this situation i.e. receiving messages from one source and forwarding them on to a target.

The progame makes use of Visual Studio Codes `tasks` and `launch` configurations to run the code in remote debug mode on the Raspberry Pi while developing on a Linux PC.


## .vscode/task.json
There are 3 tasks
* build
* publish
* deploy

The deploy task is the more interesting.  It takes the `publish` task output and deploys it on the Pi. Resync is used to deploy only those files which change. This is more efficient than using SCP to copy the entire contents of the Publish task output.

Note: SSH keys are used for authentication. This requires that the host public key has been added to the Pi's `~/.ssh/authorized_keys` file.  See below on how this is setup.

## .vscode/launch.json
Configures remote debug mode and triggers the `deploy` task (and its `publish` and `build dependencies) and _launches_ the progame in remote debug mode.

See: [OmniSharp remote debugging](9https://github.com/OmniSharp/omnisharp-vscode/wiki/Attaching-to-remote-processes) and [OmniSharp remote debugging on Linux Arm devices](https://github.com/OmniSharp/omnisharp-vscode/wiki/Remote-Debugging-On-Linux-Arm).

## Azure IoT Hub
This requires both the Azure CLI and Azure IoT Sdk to be install on the development machine.

```
$ curl -sL https://aka.ms/InstallAzureCLIDeb | sudo bash
$ az extension add --name azure-iot```


## The code
Currently a WIP.


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
