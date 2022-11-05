# FoldersVersionsManager

Simple lib manager for folders' versions control / Простой менеджер для управления версиями папок

Хайлайты, общий обзор, детальный SbS: https://youtu.be/KxVK408OE3c

How to use: lib entry class - FVManager   
Использование: входная точка библиотеки - FVManager

## Example:
// Init  
FVManager vManager = new(WorkPath, SavePath);  
// Build WorkPath model and save it to SavePath  
vManager.Save(vManager.GetInfoModel());  
// Get last version data   
var x = vManager.GetVersionData(vManager.GetLastVersion());   
// Log data (if Console Project is used)   
Logger.Log(vManager.Compare(x, vManager.GetInfoModel()));   
