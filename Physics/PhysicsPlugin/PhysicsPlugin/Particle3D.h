#pragma once
#include <math.h>


class Particle3D
{
public:
	Particle3D();
	~Particle3D();

	

private:
	



};

class Vector3
{
public :
	Vector3(int X = 0, int Y = 0, int Z = 0) : x(X), y(Y), z(Z) {}

	float x, y, z;

	Vector3 operator + (Vector3 const &left)
	{
		Vector3 out;
		out.x = x + left.x;
		out.y = y + left.y;
		out.z = z + left.z;
		return out;
	}


	Vector3 operator - (Vector3 const& left)
	{
		Vector3 out;
		out.x = x - left.x;
		out.y = y - left.y;
		out.z = z - left.z;
		return out;
	}

	Vector3 operator * (float const& left)
	{
		Vector3 out;
		out.x = x * left;
		out.y = y * left;
		out.z = z * left;
		return out;
	}

	float Dot(Vector3 const& left)
	{
		float out;
		out = ((x * left.x) + (y * left.y) + (z * left.z));
		return out;
	}

	float Magnitude()
	{
		return sqrtf((x * x) + (y * y) + (z * z));
	}

	Vector3 normal()
	{
		float m = sqrtf((x * x) + (y * y) + (z * z));
		Vector3 out;
		out.x = x / m;
		out.y = y / m;
		out.z = z / m;
		return out;

	}

	void Normalize()
	{
		float m = sqrtf((x * x) + (y * y) + (z * z));
		x /= m;
		y /= m;
		z /= m;
	}


};