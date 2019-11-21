#include "Particle2D.h"
#include "ParticleManager.h"
#include "Vector3.h"


int ParticleManager::AddParticle(float posX, float posY, float posZ, float rot, float mass)
{
	Particle2D* newP = new Particle2D();
	newP->position = Vector3(posX, posY, posZ);
	
	newP->rotation = rot;

	//NEED THIS FUNCTION - initialize mass inverse and inertias
	//newP->Init();
	newP->SetMass(mass);
	newP->SetInertia(1,1);

	particleList.insert(particleList.begin(), std::pair<int, Particle2D*>(index, newP));
	index++;

	return index-1;
}

ParticleManager::ParticleManager()
{
	
}

Particle2D* ParticleManager::getParticle(int key)
{
	return particleList.at(key);
}

ParticleManager::~ParticleManager()
{
	for (std::map<int, Particle2D*>::iterator itr = particleList.begin(); itr != particleList.end(); itr++)
	{
		delete (itr->second);
	}
	particleList.clear();
	index = 0;
}

void ParticleManager::Update(float dt)
{
	for(hiDan a : particleList)
	{
		a.second->Update(dt);

	}
}