# music-visualizer-client

This is a C# program for Windows that listens to the music and sends 
the created data via UDP connection to the Raspberry Pi Java server.

The server controls WS281X led strip based on the data.

Visual Studio development environment is required for this project.
https://drive.google.com/open?id=1mTp-3Gj95I5qR2uAdzMYqElf6FExTHIg
The server application
https://github.com/ospekki/music-visualizer-server

- Select the playback device.
- Enter the IP address of your Raspberry Pi. You can find the IP address by running "ifconfig" command.
  The server and the client application must be on the same network.
- Set the number of LEDs.
- When the server is running on the Raspberry Pi, you can click the "Enable" button. 

![alt text](https://drive.google.com/uc?export=download&id=1mTp-3Gj95I5qR2uAdzMYqElf6FExTHIg)
