#pragma once

#include "Lib.h"

#ifdef  __cplusplus
extern "C"
{
#else //  !__cplusplus

#endif //  __cplusplus

struct ParticleWrapper;

PHYSICSPLUGIN_SYMBOL int InitParticleManager();
PHYSICSPLUGIN_SYMBOL void UpdateParticleManager();
PHYSICSPLUGIN_SYMBOL int TermParticleManager();
PHYSICSPLUGIN_SYMBOL int AddNewParticle(ParticleWrapper particle);


#ifdef  __cplusplus
}
#else //  !__cplusplus

#endif //  __cplusplus
