#pragma once
#include "Vector3.h"

class Particle2D
{
public:
	Particle2D();
	~Particle2D() {}

	
	Vector3 position;
	Vector3 velocity;
	Vector3 acceleration;
	float rotation = 0;
	float angularVelocity = 0;
	float angularAcceleration = 0;

	float mass = 0;
	float massInv = 0;

	void Update(float dt);

	void AddForce(Vector3 newForce);
	void AddTorque(float newTorque);

private:
	float inertia = 0;

	void SetInertia(float width, float height);
	void SetMass(float newMass);
	Vector3 force;

	float torque = 0;

	void UpdateAcceleration();
	void UpdatePosition(float dt);

	void UpdateRotation(float dt);
	void UpdateAngularAcceleration();

	void ClampRotation();


};
