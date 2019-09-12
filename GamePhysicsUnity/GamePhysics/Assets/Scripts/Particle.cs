using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    [Header("Transform Values")]
    // lab 1 step 1
    public Vector2 position, velocity, acceleration;

    //lab 1 step 1
    public float rotation, angularVelocity, angularAcceleration;
    [Space]
    [Header("Mass Values")]

    //lab 2 step 1
    public float startingMass = 1.0f;
    float mass, massInv;

    //lab 2 step 1
    public void SetMass(float newMass)
    {
        //mass = (newMass > 0.0f) ? newMass : 0f;
        mass = Mathf.Max(0, 0f, newMass);
        massInv = mass > 0.0f ? 1.0f / mass : 0.0f;
    }


    //lab 2 step 1
    public float GetMass()
    {
        return mass;
    }

    //lab 2 step 2
    Vector2 force;
    public void AddForce(Vector2 newForce)
    {
        //D'Alembert
        force += newForce;
    }

    public void UpdateAcceleration()
    {
        //Newton 2
        acceleration = force * massInv;

        //REset because there are new forces coming next frame maybeee
        force = Vector2.zero;
    }

    // lab 1  step 2
    void updatePositionExplicitEuler(float dt)
    {
        //  x(t+dt) = x(t) + v(t)dt
        //  Euler:
        //  F(t+dt) = F(t) + f(t)dt

        
        position += velocity * dt;

        // v(t+dt) = v(t) + a(t)dt
        velocity += acceleration * dt;
    }

    //lab 1 step 2
    void updatePositionKinematic(float dt)
    {
        position += velocity * dt + acceleration * dt * dt * .5f;

        velocity += acceleration * dt;
    }

    //lab 1 step 2
    void updateRotationEulerExplicit(float dt)
    {
        rotation += angularVelocity * dt;
        angularVelocity += angularAcceleration * dt;
    }

    //lab 1 step 2
    void updateRotationKinematic(float dt)
    {
        rotation += angularVelocity * dt + angularAcceleration * dt * dt * .5f;
        angularVelocity += angularAcceleration * dt;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetMass(startingMass);
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
        
        //Lab 2 
        UpdateAcceleration();

        //Lab 1
        updateRotationEulerExplicit(Time.fixedDeltaTime);

        // lab 1:  update transform
        transform.position = position;
        transform.rotation = Quaternion.Euler(0, 0, rotation);


        // lab 1 step 4
        //  test by faking motion along a curve
        //  acceleration.x = -Mathf.Sin(Time.time);
        // angularAcceleration = Mathf.Sin(Time.time)*10f;

        //Lab 2 test: apply gravity: f = mg

       // AddForce(ForceGenerator.GenerateForce_Gravity(mass, -9.81f, Vector2.up));
        AddForce(ForceGenerator.GenerateForce_sliding(new Vector2(0, -9.81f), Vector2.up));
    }
}
