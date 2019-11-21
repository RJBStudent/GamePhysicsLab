#include "Particle2D.h"
#include <math.h>

Particle2D::Particle2D()
{
	position = Vector3((float)0, (float)0, (float)0);
	rotation = 0;
	velocity = Vector3((float)0, (float)0, (float)0);
	acceleration = Vector3((float)0, (float)0, (float)0);
}

void Particle2D::Update(float dt)
{
	UpdatePosition(dt);
	UpdateAcceleration();
	UpdateRotation(dt);
	UpdateAngularAcceleration();
}

void Particle2D::UpdateAcceleration()
{
	acceleration = force * massInv;

	force = Vector3((float)0, (float)0, (float)0);
}

void Particle2D::UpdateAngularAcceleration()
{
	angularAcceleration = ((float)1 / inertia) * torque;

	torque = 0;
}

void Particle2D::UpdatePosition(float dt)
{
	position += velocity * dt + acceleration * dt * dt * .5f;

	velocity += acceleration * dt;
}

void Particle2D::UpdateRotation(float dt)
{

	rotation += angularVelocity * dt;
	angularVelocity += angularAcceleration * dt;
}

void Particle2D::SetInertia(float width, float height)
{
	inertia = .0833f * mass * (width * width + height * height);
}

void Particle2D::SetMass(float newMass)
{
	mass = fmax(0, newMass);
	massInv = mass > 0.0f ? 1.0f / mass : 0.0f;
}

void Particle2D::AddForce(Vector3 newForce)
{
	//D'Alembert
	force += newForce;
}

void Particle2D::AddTorque(float newTorque)
{
	//D'Alembert
	torque += newTorque;
}