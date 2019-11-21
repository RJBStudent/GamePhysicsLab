#include "PhysicsPlugin.h"
#include "../ParticleManager.h"
#include "../Particle2D.h"

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


void UpdateParticleManager(float dt)
{
	if (inst)
	{
		inst->Update(dt);
	}
}


int TermParticleManager()
{
	if (inst)
	{
		delete inst;
		inst = NULL;
		return 1;
	}
	return 0;
}

float* getParticleValues(int key)
{
	if (inst)
	{
		Particle2D*  p = inst->getParticle(key);
		float particleData[4] = {p->position.x, p->position.y, p->position.z, p->rotation};

		return particleData;
	}
}

float getParticlePosX(int key)
{
	if (inst)
	{
		Particle2D* p = inst->getParticle(key);
		float particleData = p->mass;

		Vector3 pee = Vector3(1, 1, 1);
		Vector3 poo = Vector3(2, 2, 2);

		pee += poo;

		return pee.x;
	}
}

int AddNewParticle(float posX, float posY, float posZ, float rot, float mass)
{
	if (inst)
	{
		return inst->AddParticle(posX, posY, posZ, rot, mass);
	}
	return -1;
}

void AddForce(float x, float y, int key)
{
	if (inst)
	{
		Particle2D* p = inst->getParticle(key);
		p->AddForce(Vector3(x, y, 0));
	}
}

void AddTorque(float value, int key)
{
	if (inst)
	{
		Particle2D* p = inst->getParticle(key);
		p->AddTorque(value);
	}
}