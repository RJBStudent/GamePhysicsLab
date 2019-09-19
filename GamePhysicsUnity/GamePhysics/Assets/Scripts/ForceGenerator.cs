using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Lab 2 Force Generator step 3
public class ForceGenerator
{

    const float cCoeff = 0.01f;

    public static Vector2 GenerateForce_Gravity(float particleMass, float gravitationalConstant, Vector2 worldUp)
    {
        //f = mg
        Vector2 f_gravity = particleMass * gravitationalConstant * worldUp;
        return f_gravity;
    }

    public static Vector2 GenerateForce_normal(Vector2 f_gravity, Vector2 surfaceNormal_unit)
    {
        // f_normal = proj(f_gravity, surfaceNormal_unit)
        Vector2 f_normal = f_gravity.magnitude * surfaceNormal_unit;
        return f_normal;
    }

    public static Vector2 GenerateForce_sliding(Vector2 f_gravity, Vector2 f_normal)
    {
        // f_sliding = f_gravity + f_normal
        Vector2 f_sliding = f_gravity + f_normal;
        return f_sliding;
    }

  public static Vector2 GenerateForce_friction_static(Vector2 f_normal, Vector2 f_opposing, float frictionCoefficient_static)
    {
        // f_friction_s = -f_opposing if less than max, else -coeff*f_normal (max amount is coeff*|f_normal|)
        float max = frictionCoefficient_static * f_normal.magnitude;
        Vector2 f_friction_s = (f_opposing.magnitude < max) ? -f_opposing : -frictionCoefficient_static*f_normal.magnitude*f_opposing.normalized;
        return f_friction_s;

    }

    public static Vector2 GenerateForce_friction_kinetic(Vector2 f_normal, Vector2 particleVelocity, float frictionCoefficient_kinetic)
    {
        
        // f_friction_k = -coeff*|f_normal| * unit(vel)
        Vector2 f_friction_k = f_normal.magnitude * particleVelocity.normalized * -frictionCoefficient_kinetic;
        return f_friction_k;

    }

    public static Vector2 GenerateForce_drag(Vector2 particleVelocity, Vector2 fluidVelocity, float fluidDensity, float objectArea_crossSection, float objectDragCoefficient)
    {
       
        // f_drag = (p * u^2 * area * coeff)/2
        Vector2 u = particleVelocity - fluidVelocity;
        Vector2 f_drag = (fluidDensity * ( u.magnitude*u) * objectArea_crossSection * objectDragCoefficient) * .5f;
        return f_drag;

    }

    public static Vector2 GenerateForce_spring(Vector2 particlePosition, Vector2 anchorPosition, float springRestingLength, float springStiffnessCoefficient)
    {

        //Vector2 f_spring = -springStiffnessCoefficient * (length - springRestingLength);
        Vector2 length = (anchorPosition - particlePosition);
           
        Vector2 lengthResting = -(length.normalized * springRestingLength);
        Vector2 f_spring = springStiffnessCoefficient * (length - lengthResting);

        return f_spring;
    }

    static Vector2 Abs(Vector2 v2) 
    {
        return new Vector2(Mathf.Abs(v2.x), Mathf.Abs(v2.y));
    }
}
