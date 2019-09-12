using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Lab 2 Force Generator step 3
public class ForceGenerator
{

    public static Vector2 GenerateForce_Gravity(float particleMass, float gravitationalConstant, Vector2 worldUp)
    {
        //f = mg
        Vector2 f_gravity = particleMass * gravitationalConstant * worldUp;
        return f_gravity;
    }

    public static Vector2 GenerateForce_normal(Vector2 f_gravity, Vector2 surfaceNormal_unit)
    {
        

        return surfaceNormal_unit;
    }
    
    public static Vector2 GenerateForce_sliding(Vector2 f_gravity, Vector2 f_normal)
    {
        // f_sliding = f_gravity + f_normal
        Vector2 f_sliding = f_gravity + f_normal;
        return f_sliding;
    }

    public static Vector2 GenerateForce_friction_static(Vector2 f_normal, Vector2 f_opposing, Vector2 unitVelocity, float frictionCoefficient_static)
    {
        // f_friction_k = -coeff*|f_normal| * unit(vel)
        //Vector2 f_friction_k = -frictionCoefficient_static *| f_normal | * unitVelocity;
        return Vector2.zero;
    }

    public static Vector2 GenerateForce_friction_kinetic(Vector2 f_normal, Vector2 particleVelocity, float frictionCoefficient_kinetic)
    {
        // f_drag = (p * u^2 * area * coeff)/2
        return Vector2.zero;

    }

    public static Vector2 GenerateForce_drag(Vector2 particleVelocity, Vector2 fluidVelocity, float fluidDensity, float objectArea_crossSection, float objectDragCoefficient)
    {
        // f_spring = -coeff*(spring length - spring resting length)
        return Vector2.zero;

    }

    public static Vector2 GenerateForce_spring(Vector2 particlePosition, Vector2 anchorPosition, float springRestingLength, float springStiffnessCoefficient)
    {
        return Vector2.zero;

    }
}
