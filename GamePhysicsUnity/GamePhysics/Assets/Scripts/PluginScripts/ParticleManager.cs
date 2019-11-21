using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    void Start()
    {
      Debug.Log(PhysicsPlugin.InitParticleManager()); 
    }

    // Update is called once per frame
    void Update()
    {
        PhysicsPlugin.UpdateParticleManager(Time.deltaTime);
    }

    private void OnDestroy()
    {
        Debug.Log("TERM : "+PhysicsPlugin.TermParticleManager());
    }
}
