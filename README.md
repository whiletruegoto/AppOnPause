**Struggle saving data when the game gets closed because OnApplicationQuit not working as expected? Check this repo!**

This simple Unity project was created for visual demonstration of OnApplicationFocus, OnApplicationPause and OnApplicationQuit methods. 
They behave differently on platforms.

To get OnApplicationPause called in editor, uncheck **Run in Background** and **Visible in Background** in Player Settings/Resolution and Presentation 
as [docs suggest](https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnApplicationPause.html).

Logs are printed to the UI panel to test on mobile device right off the bat. Build and run on device, then try to switch between apps. 
Timer is included to see if coroutine gets aborted or not. Also you can delete prefs from the file menu, see **AppOnPause/Delete Save**.

[Download apk](https://github.com/cococatus/AppOnPause/releases/download/v1.0/AppOnPause.apk)
