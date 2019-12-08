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
    public Vector3 defaultForwardSpeed;

    public Vector3 reticleLengths;
    public float turnSpeed;


    [SerializeField]
    Particle3D thisParticle;


    float xInput = 0;
    float yInput = 0;
    float spaceBar = 0;

    Transform thisTransform;

    public Vector3 direction;

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
        //ClampVelocity();
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
        thisParticle.SetRelativeVelocity(defaultForwardSpeed);

    }

    void ClampVelocity()
    {
        Vector3 vel = thisParticle.velocity;

        vel.z = Mathf.Clamp(vel.z, 0, maxSpeeds.z);

        thisParticle.velocity = vel;
    }

    void UpdateRotation()
    {
        if(xInput != 0 )
        {
            direction.x += xInput * turnSpeed;
        }
        else
        {
            direction.x = Mathf.Lerp(direction.x, 0, .1f);
        }
        if(yInput != 0)
        {
            direction.y -= yInput * turnSpeed;
        }
        else
        {
            direction.y = Mathf.Lerp(direction.y, 0, .1f);
        }
        


        direction.x = Mathf.Clamp(direction.x, -reticleLengths.x, reticleLengths.x);
        direction.y = Mathf.Clamp(direction.y, -reticleLengths.y, reticleLengths.y);
        direction.z = reticleLengths.z;

        thisParticle.rotation.SetLookRotation(direction);
    }

}
