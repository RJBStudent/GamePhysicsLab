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


    // Start is called before the first frame update
    void Start()
    {
        thisParticle = GetComponent<Particle3D>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();
        UpdatePosition();
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
        
        thisParticle.AddLocalForce(force);

        //thisParticle.AddTorque(new Vector3(0, xInput * 100, 0))
       // thisParticle.angularVelocity = new Vector3(0, xInput * 100, 0);
    }
}
