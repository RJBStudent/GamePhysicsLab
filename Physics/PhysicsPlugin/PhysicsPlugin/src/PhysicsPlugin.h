#pragma once

#include "Lib.h"

#ifdef  __cplusplus
extern "C"
{
#else //  !__cplusplus

#endif //  __cplusplus


PHYSICSPLUGIN_SYMBOL int InitParticleManager();
PHYSICSPLUGIN_SYMBOL void UpdateParticleManager(float dt);
PHYSICSPLUGIN_SYMBOL int TermParticleManager();
PHYSICSPLUGIN_SYMBOL float* getParticleValues(int key);
PHYSICSPLUGIN_SYMBOL float getParticlePosX(int key);
PHYSICSPLUGIN_SYMBOL float getParticlePosY(int key);
PHYSICSPLUGIN_SYMBOL float getParticlePosZ(int key);
PHYSICSPLUGIN_SYMBOL float getParticleRotation(int key);
PHYSICSPLUGIN_SYMBOL int AddNewParticle(float posX, float posY, float posZ, float rot, float mass);
PHYSICSPLUGIN_SYMBOL void AddForce(float x, float y, int key);
PHYSICSPLUGIN_SYMBOL void AddTorque(float value, int key);


#ifdef  __cplusplus
}
#else //  !__cplusplus

#endif //  __cplusplus
