using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingEnemy : MonoBehaviour
{

    Particle3D thisParticle;

    public Vector3 bounceForce;

    public bool canCollide = true;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CollisionHull3D>().callMethod = OnCollisionEvent;

        thisParticle = GetComponent<Particle3D>();
        //thisParticle.AddForce(bounceForce);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnCollisionEvent(CollisionHull3D col)
    {
        if(col.tag == "Ground" && canCollide)
        {
            //thisParticle.position.y = 1f;

            thisParticle.acceleration = Vector3.zero;
            thisParticle.AddForce(bounceForce);
            //StartCoroutine(WaitForCollisiion(1f));

            Debug.Log("Bounced");
        }
    }

    IEnumerator WaitForCollisiion(float time)
    {
        canCollide = false;
        yield return new WaitForSeconds(time);
        canCollide = true;
    }
}
