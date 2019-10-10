using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Particle))]
public class ClampParticleSpeed : MonoBehaviour
{

    Particle thisParticle;


    [SerializeField]
    float maxSpeed;

    [SerializeField]
    float maxRotation;

    // Start is called before the first frame update
    void Start()
    {
        thisParticle = GetComponent<Particle>();
    }

    // Update is called once per frame
    void Update()
    {
        thisParticle.angularVelocity = Mathf.Clamp(thisParticle.angularVelocity, -maxRotation, maxRotation);


        thisParticle.velocity.x = Mathf.Clamp(thisParticle.velocity.x, -maxSpeed, maxSpeed);
        thisParticle.velocity.y = Mathf.Clamp(thisParticle.velocity.y, -maxSpeed, maxSpeed);
    }
}
