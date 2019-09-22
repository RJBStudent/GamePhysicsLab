using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum shape
{
   DISK,
   RECTANGLE,
   LINE_SEGMENT,
   RING
}

[System.Serializable]
public struct RotationalForce
{
    public Vector2 forcePosition;
    public Vector2 forceDirection;
}


public class Particle : MonoBehaviour
{
    

    [Header("Transform Values")]
    // lab 1 step 1
    public Vector2 position;
    public Vector2 velocity, acceleration;
    private Vector3 startingPos;

    //lab 1 step 1
    public float rotation, angularVelocity, angularAcceleration;
    [Space]
    [Header("Mass Values")]

    //lab 2 step 1
    public float startingMass = 1.0f;
    float mass, massInv;

    [Header("Force Types")]
    public bool gravity;
    public bool sliding;
    public bool frictionStatic;
    public bool frictionKinetic;
    public bool drag;
    public bool spring;

    public bool startSliding;

    [Header("Testing Variables")]
    public bool resetPosition;
    public bool resetData;
    public float resetTime;
    private float currentTime = 0;
    public Vector2 slopeNormal = new Vector2(-0.259f, 0.93f);
    public Transform springTransform;


    //Lab 03 step 1
    [Header("Torque Stuff")]
    public shape particleShape;
    public float inner, outer, diskRadius;
    //Lab 03 step 2
    [SerializeField]
    float torque;

    [SerializeField]
    RotationalForce[] rotationalForce;
    [SerializeField]
    Vector2 centerOfMass;


    //Lab 03 step 1
    float inertia;
    void SetInertia()
    {
        switch(particleShape)
        {
            case shape.DISK:
                inertia = .5f * mass * (diskRadius * .5f) * (diskRadius * .5f);
                break;
            case shape.RECTANGLE:
                inertia = .0833f * mass * (transform.localScale.x * transform.localScale.x + transform.localScale.y * transform.localScale.y);
                break;
            case shape.LINE_SEGMENT:
                inertia = .0833f * mass * (transform.localScale.y) * (transform.localScale.y);
                break;
            case shape.RING:
                inertia = .0833f * mass * (inner * inner + outer * outer);
                break;
            default:
                break;
        }
    }

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

    public void AddTorque(float newTorque)
    {
        //D'Alembert
        torque += newTorque;
    }

    float calculateTorque(Vector2 pointOfForce, Vector2 force)
    {
        pointOfForce = pointOfForce - centerOfMass;
        return (pointOfForce.x * force.y - pointOfForce.y * force.x);
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

    void updateAngularAcceleration()
    {
        angularAcceleration = (1f / inertia) * torque;

        torque = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetMass(startingMass);
        position = transform.position;

        startingPos = transform.position;

        if(startSliding)
        {
            AddForce(ForceGenerator.GenerateForce_sliding(new Vector2(0, -9.81f), slopeNormal * 9.81f)*70f);
        }

        //Lab 3 Add Torque test

        //lab 3 set inertia and torque
        SetInertia();
        torque = 0;

        //lab 3 add torque force and set acceleration
        for(int i = 0; i < rotationalForce.Length; i++)
        {
            AddTorque(calculateTorque(rotationalForce[i].forcePosition, rotationalForce[i].forceDirection));
        }
        updateAngularAcceleration();

    }

    // Update is called once per frame
    void Update()
    {
        if(resetPosition)
        {
            ResetPositions();
        }
        

    }

    void ResetPositions()
    {
        currentTime += Time.deltaTime;
        if(currentTime > resetTime)
        {
            position = startingPos;
            currentTime = 0f;
            if(resetData)
            {
                velocity = Vector2.zero;
                acceleration = Vector2.zero;
            }
        }
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

        //Lab 3
        updateAngularAcceleration();


        // lab 1:  update transform
        transform.position = new Vector3(position.x, position.y, startingPos.z);
        transform.rotation = Quaternion.Euler(0f, 0f, (float)rotation);


        // lab 1 step 4
        //  test by faking motion along a curve
        //  acceleration.x = -Mathf.Sin(Time.time);
        // angularAcceleration = Mathf.Sin(Time.time)*10f;

        //lab 3 test


        //Lab 2 test: apply gravity: f = mg
        
        if(gravity)
        {
            AddForce(ForceGenerator.GenerateForce_Gravity(mass, -9.81f, Vector2.up));
        }
        if (sliding)
        {
            AddForce(ForceGenerator.GenerateForce_sliding(new Vector2(0, -9.81f), slopeNormal * 9.81f));
        }
        if (frictionKinetic)
        {
            AddForce(ForceGenerator.GenerateForce_friction_kinetic(slopeNormal, velocity, .5f));
        }
        if(drag)
        {
            AddForce(ForceGenerator.GenerateForce_drag(velocity, new Vector2(0, 3f), .5f, 1, 1.05f));
        }
        if(spring)
        {
            AddForce(ForceGenerator.GenerateForce_spring(position, springTransform.position, 3f, 3f));
        }
        if(frictionStatic)
        {
            AddForce(ForceGenerator.GenerateForce_friction_static(slopeNormal, acceleration * mass , 5f));
        }
    }
    
}
