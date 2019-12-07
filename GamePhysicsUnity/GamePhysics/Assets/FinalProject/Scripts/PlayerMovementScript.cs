using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Particle3D))]
public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField]
    float xSpeed;

    [SerializeField]
    float ySpeed;

    public Vector3 maxSpeeds;
    public Vector3 defaultForwardForce;

    public Vector2 maxRotations;

    [SerializeField]
    Particle3D thisParticle;


    float xInput = 0;
    float yInput = 0;
    float spaceBar = 0;

    Transform thisTransform;

    // Start is called before the first frame update
    void Start()
    {
        thisParticle = GetComponent<Particle3D>();
        thisTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();
        UpdatePosition();
        ClampVelocity();
        ClampRotation();
    }

    void UpdateInput()
    {

        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        spaceBar = Input.GetAxisRaw("Jump");


    }

    void UpdatePosition()
    {
        thisParticle.AddTorque(new Vector3(yInput, xInput ,0));
        thisParticle.AddRelativeForce(defaultForwardForce);

    }

    void ClampVelocity()
    {
        Vector3 vel = thisParticle.velocity;

        vel.z = Mathf.Clamp(vel.z, 0, maxSpeeds.z);

        thisParticle.velocity = vel;
    }

    void ClampRotation()
    {
        float xRot = thisTransform.rotation.x * Mathf.Rad2Deg;
        float yRot = thisTransform.rotation.y * Mathf.Rad2Deg;
        float zRot = thisTransform.rotation.z * Mathf.Rad2Deg;

        //xRot = Mathf.Clamp(thisTransform.rotation.x * Mathf.Rad2Deg, -maxRotations.x, maxRotations.x);
        //yRot = Mathf.Clamp(thisTransform.rotation.y * Mathf.Rad2Deg, -maxRotations.x, maxRotations.y);


        //thisTransform.rotation = Quaternion.Euler(xRot, yRot, zRot);

        if (Mathf.Abs(thisTransform.rotation.x * Mathf.Rad2Deg) > maxRotations.x)
        {
            thisParticle.angularVelocity.x = 0;
        }

        if (Mathf.Abs(thisTransform.rotation.y * Mathf.Rad2Deg) > maxRotations.y)
        {
            thisParticle.angularVelocity.y = 0;
        }

        thisTransform.rotation = Quaternion.Euler(xRot, yRot, 0);
    }

}
