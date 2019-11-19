#include "PhysicsPlugin.h"
#include "../ParticleManager.h"

ParticleManager* inst = NULL;


int InitParticleManager()
{
	if (!inst)
	{
		inst = new ParticleManager();
		return 1;
	}
	return 0;

}


void UpdateParticleManager()
{
	if (inst)
	{
		inst->Update();
		
	}
}


int TermParticleManager()
{
	if (inst)
	{
		delete inst;
		return 1;
	}
	return 0;
}

int AddNewParticle(ParticleWrapper particle)
{
	if (inst)
	{
		inst->AddParticle(particle);
		return 1;
	}
	return 0;
}