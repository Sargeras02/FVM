# Folders' Versions Manager

Simple lib manager for folders' versions control  
- Build folders' hierarchical info model (recursively)
- Save info models as versions to data files
- Compare built versions
---
Простой менеджер для управления версиями папок  
- Построение иерархичной информационной модели папок (рекурсионно)
- Сохранение информационных моделей в качестве версий
- Сравнение построенных версий

## Example:
Lib's Entry Class / Входная точка библиотеки- FVManager

// Init  
FVManager vManager = new(WorkPath, SavePath);  
// Build WorkPath model and save it to SavePath  
vManager.Save(vManager.GetInfoModel());  
// Get last version data (Loaded from SavePath if exists)   
var x = vManager.GetVersionData(vManager.GetLastVersion());   
// Log data (if Console Project is used)   
Logger.Log(vManager.Compare(x, vManager.GetInfoModel()));   

## Media
Хайлайты, общий обзор, детальный SbS: https://youtu.be/KxVK408OE3c
