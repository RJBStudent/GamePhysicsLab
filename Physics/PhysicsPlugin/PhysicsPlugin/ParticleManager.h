#pragma once

#include <vector>

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

	void AddParticle(ParticleWrapper particle);

	void Update();


	ParticleManager();
	~ParticleManager();

private:

	std::vector<Particle2D*> particleList;

};

