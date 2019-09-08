using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{

    public Vector2 position, velocity, acceleration;

    public float rotation, angularVelocity, angularAcceleration;

    //  step 2
    void updatePositionExplicitEuler(float dt)
    {
        //  x(t+dt) = x(t) + v(t)dt
        //  Euler:
        //  F(t+dt) = F(t) + f(t)dt

        position += velocity * dt;

        // v(t+dt) = v(t) + a(t)dt
        velocity += acceleration * dt;
    }

    void updatePositionKinematic(float dt)
    {
        position += velocity * dt + acceleration * dt * dt * .5f;

        velocity += acceleration * dt;
    }

    void updateRotationEulerExplicit(float dt)
    {
        rotation += angularVelocity * dt;
        angularVelocity += angularAcceleration * dt;
    }
    void updateRotationKinematic(float dt)
    {
        rotation += angularVelocity * dt + angularAcceleration * dt * dt * .5f;
        angularVelocity += angularAcceleration * dt;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    void FixedUpdate()
    {
        //  step 3
        //  choose integrator
        //updatePositionExplicitEuler(Time.fixedDeltaTime);
        updatePositionKinematic(Time.fixedDeltaTime);

        updateRotationEulerExplicit(Time.fixedDeltaTime);

        // update transform
        transform.position = position;
        transform.rotation = Quaternion.Euler(0, 0, rotation);
        //  step 4
        //  test by faking motion along a curve
        acceleration.x = -Mathf.Sin(Time.time);
        angularAcceleration = Mathf.Sin(Time.time)*10f;
    }
}
