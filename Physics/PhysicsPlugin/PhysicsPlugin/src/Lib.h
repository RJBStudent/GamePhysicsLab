#pragma once

#ifdef PHYSICSPLUGIN_EXPORT
#define PHYSICSPLUGIN_SYMBOL __declspec(dllexport)
#else // !PHYSICSPLUGIN_EXPORT
#ifdef PHYSICSPLUGIN_IMPORT
#define PHYSICSPLUGIN_SYMBOL __declspec(dllimport)
#else // !PHYSICSPLUGIN_IMPORT
#define PHYSICSPLUGIN_IMPORT 
#endif // PHYSICSPLUGIN_IMPORT
#endif // PHYSICSPLUGIN_EXPORT
