using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle2D : MonoBehaviour
{
    //Step 1
    public Vector2 position, velocity, acceleration;

    public float rotation, angularVelocity, angularAcceleration;
    
    //Step 2
    void updatePositionExplicitEuler(float dt)
    {
        // x(t+dt) = x(t) + v(t)dt
        // Euler :
        // F(t+dt) = F(t) + f(t)dt
        //                + (dF/dt) dt
        position += velocity * dt;

        // v(t+dt) = v(t) + a(t)dt
        // add dampling
        velocity += acceleration *dt;

    }

    //Step 2
    void updateRotationExplicitEuler(float dt)
    {
        // r(t+dt) = r(t) + av(t)dt
        // Euler :
        // F(t+dt) = F(t) + f(t)dt
        //                + (dF/dt) dt
        rotation += angularVelocity * dt;

        // av(t+dt) = av(t) + aa(t)dt
        // add dampling
         angularVelocity += angularAcceleration * dt;

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Step 3
        // chose intergrator
       // updatePositionExplicitEuler(Time.fixedDeltaTime);
        updateRotationExplicitEuler(Time.fixedDeltaTime);

        // update position
        transform.position = position;
        transform.rotation = Quaternion.Euler(0, 0, rotation);

        // Step 4
        // test by faking motion along a curve
        acceleration.x = -Mathf.Sin(Time.time);
        angularAcceleration = -Mathf.Sin(Time.time*0.01f);
    }
}
