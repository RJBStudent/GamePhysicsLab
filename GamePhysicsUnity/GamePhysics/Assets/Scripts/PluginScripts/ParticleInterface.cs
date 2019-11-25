using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleInterface : MonoBehaviour
{
    int key;
    Vector3 newPos = new Vector3();
    
    // Start is called before the first frame update
    void Start()
    {
        key = PhysicsPlugin.AddNewParticle(0, 0, 0, 0, 1);
        Debug.Log(key);
        PhysicsPlugin.AddForce(-8, 16, key);
        PhysicsPlugin.AddTorque(20, key);

    }

    // Update is called once per frame
    void Update()
    {
        
        float xPos = PhysicsPlugin.getParticlePosX(key);
        float yPos = PhysicsPlugin.getParticlePosY(key);
        float rotation = PhysicsPlugin.getParticleRotation(key);

        transform.position = new Vector3(xPos, yPos, 0);

        transform.rotation = Quaternion.Euler(0f, 0f, rotation);


    }
}
