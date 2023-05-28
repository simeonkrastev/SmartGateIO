# SmartGateIO

Prototype multitier solution, managing chip card access through a gate.

# Steps to launch the solution for development and debugging:
1. Connect the Hardware
 - Connect the NodeMCU module to your PC.
 - Use the following wiring (TODO: Add link) to connect the RFID sensor.

2. Configure for your local network
 - On the server machine use ipconfig command to find your local IP.
 - In the Properties/launchSettings.json file fill in your local IP where mentioned.
 - In the wwwroot/constants.js file fill in your local IP.
 - In the Firmware/GateModule/constants.h file fill in your local IP and the network credentials.

 3. Launch the app from Visual Studio 2022