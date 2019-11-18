#pragma once

#include <vector>

class Particle2D;

class ParticleManager
{
public:

	void AddParticle(ParticleWrapper particle);

	void Update();

private:
	ParticleManager();
	~ParticleManager();

	std::vector<Particle2D> particleList;

};

struct ParticleWrapper
{
	float x;
	float y;
	float z;
	float rotation;
	float mass;

};