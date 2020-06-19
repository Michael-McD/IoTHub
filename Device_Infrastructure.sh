#!/bin/bash
echo "Building an Azure IoT Hub..."

# Used to insure the IoT Hub name is unique.
RANDOM_VALUE=y$RANDOM
# London
LOCATION=uksouth
RESOURCE_GROUP=GarageDoor
IOT_HUB_NAME=GarageDoorHub-$RANDOM_VALUE

az group create --name $RESOURCE_GROUP --location $LOCATION # az account list-locations | grep -E 'name'

echo "Creating IoT Hub name: " $IOT_HUB_NAME "(this can take a few minutes)"
az iot hub create --name $IOT_HUB_NAME --resource-group $RESOURCE_GROUP --sku F1 --location $LOCATION --partition-count 2
wait $!

# Register/create a device Id
echo "Registering a Device Id"
DEVICE_ID=DoorOpener
az iot hub device-identity create --hub-name $IOT_HUB_NAME --device-id $DEVICE_ID

# Get the Connection String
DEVICE_CONNECTION_STRING=$(az iot hub device-identity show-connection-string --hub-name $IOT_HUB_NAME --device-id $DEVICE_ID --output tsv)
echo -e "\033[33m Device Connection String: \033[39m"$DEVICE_CONNECTION_STRING 

SERVICE_CONNECTION_STRING=$(az iot hub show-connection-string --policy-name service --name $IOT_HUB_NAME --output tsv) 
echo -e "\033[33m Service Connection String: \033[0;39m" $SERVICE_CONNECTION_STRING 

