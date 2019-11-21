#pragma once

#include <map>

class Particle2D;

#define hiDan auto

struct ParticleWrapper
{
	float x;
	float y;
	float z;
	float rotation;
	float mass;

};

class ParticleManager
{
public:

	int AddParticle(float posX, float posY, float posZ, float rot, float mass);

	void Update(float dt);


	ParticleManager();
	~ParticleManager();

	Particle2D* getParticle(int key);

private:

	int index = 0;

	std::map<int, Particle2D*> particleList;

};

