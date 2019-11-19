#include "Particle2D.h"
#include "ParticleManager.h"
#include "Vector3.h"

void ParticleManager::AddParticle(ParticleWrapper particle)
{
	Particle2D* newP = new Particle2D();
	newP->position = Vector3(particle.x, particle.y, particle.z);
	newP->mass = particle.mass;
	newP->rotation = particle.rotation;

	//NEED THIS FUNCTION - initialize mass inverse and inertias
	//newP->Init();
	
	particleList.push_back(newP);
}

ParticleManager::ParticleManager()
{
	
}

ParticleManager::~ParticleManager()
{
	for (hiDan particle : particleList)
	{
		delete particle;
	}
	particleList.clear;
}

void ParticleManager::Update()
{
	for(hiDan a : particleList)
	{
		a.Update();

	}
}