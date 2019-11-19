#pragma once


class Vector3 {};
class Particle2D
{
public:
	Particle2D() {}
	~Particle2D() {}

	
	Vector3 position;
	Vector3 velocity;
	Vector3 acceleration;
	float rotation = 0;
	float angularVelocity = 0;
	float angularAcceleration = 0;

	float mass = 0;
	float massInv = 0;

	void Update();


private:
	float inertia = 0;

	void SetInertia();
	void SetMass();
	Vector3 force;
	void AddForce();

	float torque = 0;
	void AddTorque();

	void UpdateAcceleration();
	void UpdatePosition();

	void UpdateRotation();
	void UpdateAngularAcceleration();

	void ClampRotation();


};
