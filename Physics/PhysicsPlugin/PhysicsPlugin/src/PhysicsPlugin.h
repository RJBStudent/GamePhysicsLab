#pragma once

#include "Lib.h"

#ifdef  __cplusplus
extern "C"
{
#else //  !__cplusplus

#endif //  __cplusplus

PHYSICSPLUGIN_SYMBOL int InitFoo(int f_new);
PHYSICSPLUGIN_SYMBOL int DoFoo(int bar);
PHYSICSPLUGIN_SYMBOL int TermFoo();

#ifdef  __cplusplus
}
#else //  !__cplusplus

#endif //  __cplusplus
