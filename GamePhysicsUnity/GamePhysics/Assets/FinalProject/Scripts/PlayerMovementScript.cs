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

    [SerializeField]
    Particle3D thisParticle;


    float xInput = 0;
    float yInput = 0;

    Transform thisTransform;
    [SerializeField]
    Transform thePlanet;
    [SerializeField]
    float offsetFromPlanetCenter;

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
        RotateAroundPlanet();
    }

    void UpdateInput()
    {

        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        

    }

    Vector3 force = new Vector3();
    void UpdatePosition()
    {
        force.x = xInput;
        force.y = 0;
        force.z = yInput;
        
        thisParticle.AddForce(thisTransform.TransformDirection(force));

        //thisParticle.AddTorque(new Vector3(0, xInput * 100, 0))
       // thisParticle.angularVelocity = new Vector3(0, xInput * 100, 0);
    }

    void RotateAroundPlanet()
    {
        Vector3 difference = (thisParticle.position - thePlanet.position).normalized;
        Vector3 newPosition = difference * offsetFromPlanetCenter;

        //Set rotation with difference and set forward/ up

        // set transform.position to newPosition;
        //thisParticle.position = newPosition;
        thisParticle.AddForce(difference* -5);

        thisTransform.up = difference;
        //Quaternion targetRotation = Quaternion.FromToRotation(thisTransform.up, difference) * thisTransform.rotation;
        //thisTransform.rotation = Quaternion.Slerp(thisTransform.rotation, targetRotation, 50 * Time.deltaTime);
    }
}
