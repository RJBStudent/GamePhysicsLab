using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Particle3D))]
public class RotateAroundPlanetScrpt : MonoBehaviour
{
    [SerializeField]
    Transform thePlanet;

    [SerializeField]
    float offsetFromPlanetCenter;

    Vector3 difference;

    Vector3 newPosition;

    Particle3D thisParticle;

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
        difference = (transform.position - thePlanet.position).normalized;
        newPosition = difference * offsetFromPlanetCenter;

        //Set rotation with difference and set forward/ up

        // set transform.position to newPosition;
        thisParticle.position = newPosition;

        thisTransform.up = difference;
    }
}
