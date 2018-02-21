# Unity-Fly-Control
A simple set of scripts for fly navigation in Unity based on VRTK library.

#### Environment Required:
Unity 2017 2.0f or above
VRTK plugin 3.2

#### How to use:
1. Attach controller_fly.cs onto one of your VRTK controller script alias.

2. Link CameraRig object and actual pointing controller object to the script.

3. Link PlayArea script alias to the script

4. On your PlayArea script alias, attach Collision_detect.cs onto it.

5. Add "VRTK_HeadsetCollision" script to PlayArea script alias.

#### Libraries Reference:
[VRTK plugin](https://vrtoolkit.readme.io/)