using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Particle))]
public class ScreenWrap : MonoBehaviour
{

    [SerializeField] float yWrapPos;
    [SerializeField] float xWrapPos;

    Particle thisParticle;


    // Start is called before the first frame update
    void Start()
    {
        thisParticle = GetComponent<Particle>();        
    }

    // Update is called once per frame
    void Update()
    {
        if(thisParticle.position.x < -xWrapPos)
        {
            thisParticle.position.x = xWrapPos;
        }
        else if(thisParticle.position.x > xWrapPos)
        {
            thisParticle.position.x = -xWrapPos;
        }

        if (thisParticle.position.y < -yWrapPos)
        {                         
            thisParticle.position.y = yWrapPos;
        }
        else if (thisParticle.position.y > yWrapPos)
        {
            thisParticle.position.y = -yWrapPos;
        }
    }
}
