# Immotionar ImmotionRoom Samples

Here you can find Immotionar ImmotionRoom SDK examples and tutorial projects for .NET 4.5 and Unity 5.x.

If you want to know more about ImmotionRoom, check the official [Immotionar website](http://www.immotionar.com)


## .NET 4.5 Examples

ImmotionRoomSamples
-----------------
This solution contains example projects for .NET 4.5. Each example has its own project:
* **WinFormSkeletonVisualizer** - A Windows Form application that shows you how to interact with the underlying Tracking Service in C# and get skeletal data from it

Hardware Pre-requisites
-----------------------
* 1 or more Microsoft Kinect v2 or v1 sensor(s)

Software Pre-requisites
-----------------------
* **Microsoft Windows 8.x or above**
* **.NET framework 4.5 installed**
* either **[Microsoft Kinect v2 Runtime](https://www.microsoft.com/en-us/download/details.aspx?id=44559)** or **[SDK](https://www.microsoft.com/en-us/download/details.aspx?id=44561)**; or **[Microsoft Kinect v1 Runtime](https://www.microsoft.com/en-us/download/details.aspx?id=40277)**

* **ImmotionRoom Runtime and SDK** v0.4.2 or above

    Currently the project is in beta round.
    If you want to take part to the beta, download the software **[here](http://www.immotionar.com/en/pricing/)**.

* **Visual Studio 2015** 

Getting Started
---------------
The projects work out of the box. Just compile and run them to make them work. For some projects additional NuGet package will be downloaded to make them work properly.
The source code is completely provided to let you learn how to develop various stuff using the ImmotionRoom SDK. We advice you to start from the WinFormSkeletonVisualizer demo,
that teaches you basic communication with the Tracking Service and the proper way to retrieve skeletal data from it. In this sample all the code is contained inside TestWindow.cs
file (the source code of the TestWindow window).


## Unity 3D Examples

ImmotionRoomSamples
-----------------
This project contains examples for Unity 5.x.  Each example is in its own scene:
* **BigBalls** - This example shows how to use natural hands/feet interactions
* **GirelloData** - This example shows how to get information about the current game area and set objects pose accordingly
* **NoLogs** - A quick sample to show you how to enable/disable or change log level for ImmotionRoom SDK logs.
* **SoccerField** - This example shows how to use natural feet interactions
* **Teleporting** - A concept for a teleporting station, showing how to move in a scene in addition to positional tracking and in-place walking.

Hardware Pre-requisites
-----------------------
* 1 or more Microsoft Kinect v2 or v1 sensor(s)
* A VR-ready PC (and 1 PC/mini-PC for each of additional Kinect v2/v1 sensor)
* A supported VR headset (Oculus Rift DK2/CV1, Samsung Gear VR, HTC Vive, OSVR HDK, Google Cardboard, Google Daydream)

Software Pre-requisites
-----------------------
* **Microsoft Windows 8.x or above**
* either **[Microsoft Kinect v2 Runtime](https://www.microsoft.com/en-us/download/details.aspx?id=44559)** or **[SDK](https://www.microsoft.com/en-us/download/details.aspx?id=44561)**; or **[Microsoft Kinect v1 Runtime](https://www.microsoft.com/en-us/download/details.aspx?id=40277)**

* **ImmotionRoom Runtime and SDK** v0.4.0 or above

    Currently the project is in beta round.
    If you want to take part to the beta, download the software **[here](http://www.immotionar.com/en/pricing/)**.

* **[Unity 3D 5.3.x or above](https://unity3d.com/get-unity/download)**
* **[Blender](https://www.blender.org/)**
* **[Oculus Utilities for Unity 5.x](https://developer3.oculus.com/downloads/game-engines/1.10.0/Oculus_Utilities_for_Unity_5/)**

Getting Started
---------------
Currently the samples are provided for *Oculus Rift* and *Samsung Gear VR*.
Once you open the project in Unity, you need to import the following Unity packages:
* **Oculus Utilities for Unity**
* **ImmotionRoom SkeletalTracking**
* **ImmotionRoom VR**
* **ImmotionRoom VR for Oculus**

If you want to use another supported VR headset, instead of the *Oculus Utilities for Unity* package, you need to import the VR headset plugin for the device you want to use and the *ImmotionRoom VR for [Vive, OSVR or Cardboard]* package.
Then, in each sample scene, you need to replace the **IRoomPlayerController.Oculus** with the **IRoomPlayerController.XXX** relative to the device you want to use (anc configure it accordingly).
These last steps are not necessary if you're using ImmotionRoom SDK v0.4.2 or above. In this case, you can change more easily the target headset in each scene using menu ImmotionRoom -> Platform Settings and selecting the device you want to build to.
Once the scene is ready, just press *Play* to start the experience. For further details, please refer to the SDK documentation.    

Project Layout
--------------
Projects commonly have the following folders:
* **ImmotionRoom** - ImmotionRoom SDK libraries and components
* **ImmotionRoom/Plugins** - Unity3D interface to the ImmotionRoom Runtime
* **ImmotionRoom/Skeletals** - ImmotionRoom SDK Skeletal Tracking components (from ImmotionRoom.Skeletal package)
* **ImmotionRoom/Skeletals/Example Scenes/Scenes** - Skeletal Tracking sample scenes
* **ImmotionRoom/VR** - ImmotionRoom SDK components for VR (from ImmotionRoom.VR package)
* **ImmotionRoom/VR/Example Scenes/Scenes** - VR sample scenes
* **ImmotionRoom/VR.Oculus** - ImmotionRoom SDK components for Oculus (from ImmotionRoom.VR.Oculus package)
* **ImmotionRoom/VR.Oculus/Example Scenes/Scenes** - VR sample scenes for Oculus/Gear VR
* **OVR** - Oculus Utilities for Unity libraries and components (from Oculus Utilities package)
* **IRoom.Samples/** - ImmotionRoom SDK samples provided in this repository
* **IRoom.Samples/Prefabs** - Prefabs used in the example scenes
* **IRoom.Samples/Scenes** - All the example scenes
* **IRoom.Samples/Scripts** - Scripts used by the example scenes
* **Plugins/** - Android and Oculus-specific libraries (from Oculus Utilities package)



Contribution
------------
Want to contribute? Great!

There are many ways you can participate in the project, for example:
* Create and share new amazing immersive VR samples using ImmotionRoom SDK
* [Submit bugs and feature requests](https://github.com/gianni-rg/immotionroom-samples/issues) and help us verify as they are checked-in
* Review [source code changes](https://github.com/gianni-rg/immotionroom-samples/pulls)

#### Code reviews
All submissions, including submissions by project members, require review. We use Github pull requests for this purpose.

#### Legal
If you contribute to the projects in any form, you will own the copyright to your changes but you will grant us and to recipients of software distributed by us a perpetual, worldwide, non-exclusive, no-charge, royalty-free, irrevocable copyright license to reproduce, prepare derivative works of, publicly display, publicly perform, sublicense, and distribute Your Contributions and such derivative works. 

License
-------
The ImmotionRoom SDK Samples are licensed under the MIT License (MIT).

Copyright (C) 2014-2017 Immotionar, a division of Beps Engineering.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.