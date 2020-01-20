A locally hosted REST API exposing your installed games across all launchers.

Application is a dotnet core self hosted http api, which also hosts a number of background threads which look for fresh content and update library data.  API is exposed with swagger- the idea being the end user can launch a simple web based viewer for all their local content.  Developers can focus on creating cool ui's using any technology they wish and hook into the api exposed by GPI.
