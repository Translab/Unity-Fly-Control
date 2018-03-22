# Unity-Fly-Control
A simple set of scripts for fly navigation in Unity based on VRTK library.

#### Environment Required:
Unity 2017 2.0f or above
VRTK plugin 3.2

#### How to use:
1. Attach controller_fly.cs onto one of your VRTK controller script alias (either left or right hand).

2. On your controller script alias, choose if you are using "simulator or not", and choose if you are using "lefthand or not".

3. Link SteamVR CameraRig object and VRSimulatorCameraRig object to your controller_fly script.

4. Link PlayArea script alias object to the script.

5. On your PlayArea script alias object, attach Collision_detect.cs onto it.

6. Add "VRTK_HeadsetCollision" script to PlayArea script alias.

#### Libraries Reference:
[VRTK plugin](https://vrtoolkit.readme.io/)