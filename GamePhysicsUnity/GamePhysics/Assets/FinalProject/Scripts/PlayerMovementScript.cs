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

    public Vector3 reticleLengths;
    public float turnSpeed;


    [SerializeField]
    Particle3D thisParticle;


    float xInput = 0;
    float yInput = 0;
    float spaceBar = 0;

    Transform thisTransform;

    Vector3 direction;

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
        UpdateRotation();
    }

    void UpdateInput()
    {

        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        spaceBar = Input.GetAxisRaw("Jump");


    }

    void UpdatePosition()
    {
        //thisParticle.AddTorque(new Vector3(yInput, xInput ,0));
        thisParticle.AddRelativeForce(defaultForwardForce);

    }

    void ClampVelocity()
    {
        Vector3 vel = thisParticle.velocity;

        vel.z = Mathf.Clamp(vel.z, 0, maxSpeeds.z);

        thisParticle.velocity = vel;
    }

    void UpdateRotation()
    {
        direction.x += xInput * turnSpeed;
        direction.y -= yInput * turnSpeed;
        direction.z = reticleLengths.z;

        direction.x = Mathf.Clamp(direction.x, -reticleLengths.x, reticleLengths.x);
        direction.y = Mathf.Clamp(direction.y, -reticleLengths.y, reticleLengths.y);

        thisParticle.rotation.SetLookRotation(direction);
    }

}
