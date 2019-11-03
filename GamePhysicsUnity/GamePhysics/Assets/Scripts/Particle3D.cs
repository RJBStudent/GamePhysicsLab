﻿using UnityEngine;

public enum Shape3D
{
    SOLID_SPHERE,
    HOLLOW_SPHERE,
    SOLID_BOX,
    HOLLOW_BOX,
    SOLID_CYLINDER,
    SOLID_CONE
}


public class Particle3D : MonoBehaviour
{
    [Header("Transform Values")]
    // lab 1 step 1
    public Vector3 position;
    public Vector3 velocity, acceleration;
    public Matrix4x4 worldTransformationMatrix;

    //lab 1 step 1
    public Quaternion rotation;
    public Vector3 angularVelocity, angularAcceleration;
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
    public Shape3D particleShape3D;
    public float length, radius;
    //Lab 03 step 2
    [SerializeField]
    Vector3 torque;

    [SerializeField]
    RotationalForce[] rotationalForce;
    [SerializeField]
    Vector3 centerOfMass;


    //Lab 03 step 1
    Matrix4x4 inertia;
    Matrix4x4 inverseInertiaTensor;
    Matrix4x4 scaleMat;
    Matrix4x4 inverseScaleMat;
    Matrix4x4 rotationMat;
    Matrix4x4 inverseRotationMat;
    Matrix4x4 localToWorldMatrix;
    Matrix4x4 worldToLocalMatrix;

    void Start()
    {
        SetMass(startingMass);
        position = transform.position;

        if (startSliding)
        {
            AddForce(ForceGenerator.GenerateForce_sliding(new Vector2(0, -9.81f), slopeNormal * 9.81f) * 70f);
        }

        //Lab 3 Add Torque test

        //lab 3 set inertia and torque
        SetInertia();
        torque = angularAcceleration;

        scaleMat = new Matrix4x4(new Vector4(transform.localScale.x, 0, 0, 0), new Vector4(0, transform.localScale.y, 0, 0),
            new Vector4(0, 0, transform.localScale.z, 0), new Vector4(0, 0, 0, 1));
        inverseScaleMat = new Matrix4x4(new Vector4(1 / transform.localScale.x, 0, 0, 0), new Vector4(0, 1 / transform.localScale.y, 0, 0),
            new Vector4(0, 0, 1 / transform.localScale.z, 0), new Vector4(0, 0, 0, 1));

        //lab 3 add torque force and set acceleration
        for (int i = 0; i < rotationalForce.Length; i++)
        {
            AddTorque(calculateTorque(rotationalForce[i].forcePosition, rotationalForce[i].forceDirection));
        }
        updateAngularAcceleration();

        rotation.w = 1;
    }

    void updateWorldTransformationMatrix()
    {
        Vector4 vec1 = new Vector4(rotation.w * rotation.w + rotation.x * rotation.x - rotation.y * rotation.y - rotation.z * rotation.z,
            2 * rotation.x * rotation.y - 2 * rotation.w * rotation.z, 2 * rotation.x * rotation.z + 2 * rotation.w * rotation.y, 0);

        Vector4 vec2 = new Vector4(2 * rotation.x * rotation.y + 2 * rotation.w * rotation.z,
            rotation.w * rotation.w - rotation.x * rotation.x + rotation.y * rotation.y - rotation.z * rotation.z, 2 * rotation.y * rotation.z + 2 * rotation.w * rotation.x, 0);

        Vector4 vec3 = new Vector4(2 * rotation.x * rotation.z - 2 * rotation.w * rotation.y,
            2 * rotation.y * rotation.z + 2 * rotation.w * rotation.x, rotation.w * rotation.w - rotation.x * rotation.x - rotation.y * rotation.y + rotation.z * rotation.z, 0);

        Vector4 vec4 = new Vector4(0, 0, 0, 1);

        rotationMat = new Matrix4x4(vec1, vec2, vec3, vec4);
        inverseRotationMat = rotationMat.transpose;

        Matrix4x4 positionMat = new Matrix4x4(new Vector4(1, 0, 0, position.x), new Vector4(0, 1, 0, position.y), new Vector4(0, 0, 1, position.z), new Vector4(0, 0, 0, 1));
        Matrix4x4 inversePositionMat = new Matrix4x4(new Vector4(1, 0, 0, -position.x), new Vector4(0, 1, 0, -position.y), new Vector4(0, 0, 1, -position.z), new Vector4(0, 0, 0, 1));


        localToWorldMatrix =  scaleMat * rotationMat * positionMat;
        worldToLocalMatrix = inverseScaleMat * inverseRotationMat * inversePositionMat;
    }

    void SetInertia()
    {

        inertia = new Matrix4x4();
        switch (particleShape3D)
        {
            case Shape3D.SOLID_SPHERE:
                inertia.SetRow(0, new Vector4(.4f * mass * (radius * radius), 0f, 0f, 0f));
                inertia.SetRow(1, new Vector4(0f, .4f * mass * (radius * radius), 0f, 0f));
                inertia.SetRow(2, new Vector4(0f, 0f, .4f * mass * (radius * radius), 0f));
                inertia.SetRow(3, new Vector4(0f, 0f, 0f, 1f));
                break;
            case Shape3D.HOLLOW_SPHERE:
                inertia.SetRow(0, new Vector4(.666f * mass * (radius * radius), 0f, 0f, 0f));
                inertia.SetRow(1, new Vector4(0f, .666f * mass * (radius * radius), 0f, 0f));
                inertia.SetRow(2, new Vector4(0f, 0f, .666f * mass * (radius * radius), 0f));
                inertia.SetRow(3, new Vector4(0f, 0f, 0f, 1f));
                break;
            case Shape3D.SOLID_BOX:
                inertia.SetRow(0, new Vector4(.083f * mass * (transform.localScale.y* transform.localScale.y + transform.localScale.z * transform.localScale.z), 0f, 0f, 0f));
                inertia.SetRow(1, new Vector4(0f, .083f * mass * (transform.localScale.z * transform.localScale.z + transform.localScale.x * transform.localScale.x), 0f, 0f));
                inertia.SetRow(2, new Vector4(0f, 0f, .083f * mass * (transform.localScale.x * transform.localScale.x + transform.localScale.y * transform.localScale.y), 0f));
                inertia.SetRow(3, new Vector4(0f, 0f, 0f, 1f));
                break;
            case Shape3D.HOLLOW_BOX:
                inertia.SetRow(0, new Vector4(1.66f * mass * (transform.localScale.y * transform.localScale.y + transform.localScale.z * transform.localScale.z), 0f, 0f, 0f));
                inertia.SetRow(1, new Vector4(0f, 1.66f * mass * (transform.localScale.z * transform.localScale.z + transform.localScale.x * transform.localScale.x), 0f, 0f));
                inertia.SetRow(2, new Vector4(0f, 0f, 1.66f * mass * (transform.localScale.x * transform.localScale.x + transform.localScale.y * transform.localScale.y), 0f));
                inertia.SetRow(3, new Vector4(0f, 0f, 0f, 1f));
                break;
            case Shape3D.SOLID_CYLINDER:
                inertia.SetRow(0, new Vector4(1.66f * mass * (3*(radius * radius) + transform.localScale.y * transform.localScale.y), 0f, 0f, 0f));
                inertia.SetRow(1, new Vector4(0f, 1.66f * mass * (3 * (radius * radius) + transform.localScale.y * transform.localScale.y), 0f, 0f));
                inertia.SetRow(2, new Vector4(0f, 0f, 1.66f * mass * (radius * radius), 0f));
                inertia.SetRow(3, new Vector4(0f, 0f, 0f, 1f));
                break;
            case Shape3D.SOLID_CONE:
                inertia.SetRow(0, new Vector4((0.6f * mass * (transform.localScale.y * transform.localScale.y))+(.15f*mass*(radius*radius)), 0f, 0f, 0f));
                inertia.SetRow(1, new Vector4(0f, (0.6f * mass * (transform.localScale.y * transform.localScale.y)) + (.15f * mass * (radius * radius)), 0f, 0f));
                inertia.SetRow(2, new Vector4(0f, 0f, 0.3f * mass * (radius * radius), 0f));
                inertia.SetRow(3, new Vector4(0f, 0f, 0f, 1f));
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

    public void AddTorque(Vector3 newTorque)
    {
        //D'Alembert
        torque += newTorque;
    }

    Vector3 calculateTorque(Vector3 pointOfForce, Vector3 force)
    {
        pointOfForce = pointOfForce - centerOfMass;
        return Vector3.Cross(pointOfForce, force);
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
        // newQuat = oldQuat + (.5 * dt * angularVelocity * oldQuat)


        Quaternion velXrotation = vectorXquaternion(angularVelocity, rotation);
        //velXrotation.Normalize();

        rotation.w += .5f * dt * velXrotation.w;
        rotation.x += .5f * dt * velXrotation.x;
        rotation.y += .5f * dt * velXrotation.y;
        rotation.z += .5f * dt * velXrotation.z;

        rotation.Normalize();

        //rotation += angularVelocity * dt;
        angularVelocity += angularAcceleration * dt;
    }

    //Lab 06
    Quaternion vectorXquaternion(Vector3 lhs, Quaternion rhs)
    {
        Vector3 rhsVector = new Vector3(rhs.x, rhs.y, rhs.z);
        float newQuatsW = -Vector3.Dot(lhs, rhsVector);
        Vector3 newQuatsVector = (rhs.w* lhs) + Vector3.Cross(lhs, rhsVector);

        Quaternion newQuat = new Quaternion();
        newQuat.w = newQuatsW;
        newQuat.x = newQuatsVector.x;
        newQuat.y = newQuatsVector.y;
        newQuat.z = newQuatsVector.z;

        return newQuat;
    }

    //lab 1 step 2
    void updateRotationKinematic(float dt)
    {
        //rotation += angularVelocity * dt + angularAcceleration * dt * dt * .5f;
        angularVelocity += angularAcceleration * dt;
    }

    void updateAngularAcceleration()
    {
        //angularAcceleration = (1f / inertia) * torque;
        updateInverseInertiaTensor();
        angularAcceleration = inverseInertiaTensor * (Vector4)torque;
        torque = Vector3.zero;
    }

    void updateInverseInertiaTensor()
    {
        //inverseInertiaTensor = transform.worldToLocalMatrix * inertia.inverse * transform.localToWorldMatrix;
        updateWorldTransformationMatrix();
        
        inverseInertiaTensor = worldToLocalMatrix * inertia.inverse * localToWorldMatrix;
    }

    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {

    }


    void FixedUpdate()
    {
        //  step 3
        //  choose integrator
        updatePositionExplicitEuler(Time.fixedDeltaTime);
        //updatePositionKinematic(Time.fixedDeltaTime);

        //Lab 2 
        UpdateAcceleration();

        //Lab 1
        updateRotationEulerExplicit(Time.fixedDeltaTime);

        //Lab 3
        updateAngularAcceleration();


        // lab 1:  update transform
        //position = position + deltaPosition;
        transform.position = new Vector3(position.x, position.y, position.z);

        ClampRotation();
        transform.rotation = rotation;

        updateWorldTransformationMatrix();

        // lab 1 step 4
        //  test by faking motion along a curve
        //  acceleration.x = -Mathf.Sin(Time.time);
        // angularAcceleration = Mathf.Sin(Time.time)*10f;

        //lab 3 test


        //Lab 2 test: apply gravity: f = mg

        if (gravity)
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
        if (drag)
        {
            AddForce(ForceGenerator.GenerateForce_drag(velocity, new Vector2(0, 3f), .5f, 1, 1.05f));
        }
        if (spring)
        {
            AddForce(ForceGenerator.GenerateForce_spring(position, springTransform.position, 3f, 3f));
        }
        if (frictionStatic)
        {
            AddForce(ForceGenerator.GenerateForce_friction_static(slopeNormal, acceleration * mass, 5f));
        }
    }

    void ClampRotation()
    {
        //if (rotation > 360)
        //{
        //    rotation = -360;
        //}
        //else if (rotation < -360)
        //{
        //    rotation = 360;
        //}
    }
}
