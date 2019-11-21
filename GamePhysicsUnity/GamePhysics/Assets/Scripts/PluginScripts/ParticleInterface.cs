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
        PhysicsPlugin.AddForce(-1, 2, key);
        PhysicsPlugin.AddTorque(20, key);

    }

    // Update is called once per frame
    void Update()
    {
        float[] values= PhysicsPlugin.getParticleValues(key);
        Debug.Log(values[0] + " " + values[1]);
        newPos.x = values[0];
        newPos.y = values[1];
        //newPos.z = values[2];

        
        float xPos = PhysicsPlugin.getParticlePosX(key);
        Debug.Log("xPos " +xPos);
        transform.position = new Vector3(xPos, xPos, 0);

        transform.rotation = Quaternion.Euler(0f, 0f, (float)values[3]);


    }
}
