using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      Debug.Log(PhysicsPlugin.InitParticleManager()); 
    }

    // Update is called once per frame
    void Update()
    {
        PhysicsPlugin.UpdateParticleManager(Time.deltaTime);
    }
}
