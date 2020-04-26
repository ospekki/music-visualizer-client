# WS281X Music Visualizer Client

This is a C# WPF program for Windows that listens to music and sends 
the created data via UDP connection to the Raspberry Pi Java server. The server controls WS281X led strip based on the data.

Visual Studio development environment is required for this project.

The server application:

https://github.com/ospekki/music-visualizer-server

This program uses the BASS audio library, which is free for non-commercial use.

https://www.un4seen.com/

[![alt text](https://www.dropbox.com/s/f71dd5qi3kmyc3o/Raspberry_Pi_WS2812B_LED_strip_music_visualizer.gif?raw=1)](https://www.youtube.com/watch?v=hVwcQ8_BEE8)

## Running the program

- Select the playback device.
- Enter the IP address of your Raspberry Pi. You can find the IP address by running "ifconfig" command.
  The server and the client application must be on the same network.
- Set the number of LEDs.
- When the server is running on the Raspberry Pi, you can click the "Enable" button. 

![alt text](https://www.dropbox.com/s/krc6nxnbgw805v1/image019.png?raw=1)
