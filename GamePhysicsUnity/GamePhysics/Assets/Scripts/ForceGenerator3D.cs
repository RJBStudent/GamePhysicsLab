using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceGenerator3D
{
    public static Vector3 GenerateForce_Gravity(float particleMass, float gravitationalConstant, Vector3 worldUp)
    {
        Vector3 f_gravity = particleMass * gravitationalConstant * worldUp;
        return f_gravity;
    }

    public static Vector3 GenerateForce_normal(Vector3 f_gravity, Vector3 surfaceNormal_unit)
    {
        // f_normal = proj(f_gravity, surfaceNormal_unit)
        Vector3 f_normal = f_gravity.magnitude * surfaceNormal_unit;
        return f_normal;
    }


    public static Vector3 GenerateForce_friction_static(Vector3 f_normal, Vector3 f_opposing, float frictionCoefficient_static)
    {
        // f_friction_s = -f_opposing if less than max, else -coeff*f_normal (max amount is coeff*|f_normal|)
        float max = frictionCoefficient_static * Mathf.Abs(f_normal.magnitude);
        Vector3 f_friction_s = (f_opposing.magnitude < max) ? -f_opposing : -frictionCoefficient_static * f_normal.magnitude * f_opposing.normalized;
        return f_friction_s;

    }

    public static Vector3 GenerateForce_friction_kinetic(Vector3 f_normal, Vector3 particleVelocity, float frictionCoefficient_kinetic)
    {

        // f_friction_k = -coeff*|f_normal| * unit(vel)
        Vector3 f_friction_k = Mathf.Abs(f_normal.magnitude) * particleVelocity * -frictionCoefficient_kinetic;
        return f_friction_k;

    }

    public static Vector3 GenerateForce_drag(Vector3 particleVelocity, Vector3 fluidVelocity, float fluidDensity, float objectArea_crossSection, float objectDragCoefficient)
    {

        // f_drag = (p * u^2 * area * coeff)/2
        Vector3 u = particleVelocity - fluidVelocity;
        Vector3 f_drag = (fluidDensity * Vector3.Scale(u,  u) * objectArea_crossSection * objectDragCoefficient) * .5f;
        return f_drag;

    }

}
