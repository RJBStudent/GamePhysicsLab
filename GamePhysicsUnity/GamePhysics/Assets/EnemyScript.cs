using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    
    public Transform mCamera;



    [SerializeField]
    float offset;
    Vector3 targetPosition;

    Vector3 offsetPosition;

    Particle3D thisParticle;

    bool onScreen = false;

    // Start is called before the first frame update
    void Start()
    {
        thisParticle = GetComponent<Particle3D>();
      //  offsetPosition = transform.position;

        GetComponent<CollisionHull3D>().callMethod = OnCollisionEvent;
    }

    public void SpawnEnemy()
    {

        thisParticle = GetComponent<Particle3D>();
        
        StartCoroutine( flyInScreen());
    }
    Vector3 newScreenPosition = new Vector3();

    Vector3 offScreenPosition = new Vector3();
    IEnumerator flyInScreen()
    {
        
        newScreenPosition.x = Random.Range(-10, 10);
        newScreenPosition.y = Random.Range(-5, 5);
        newScreenPosition.z = offset;

        offScreenPosition.x = Random.Range(-20, 20);
        offScreenPosition.y = Random.Range(-10, 10);
        offScreenPosition.z = Random.Range(30, 50);

        thisParticle.position = offScreenPosition;

        offsetPosition = newScreenPosition;
        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForSeconds(0.02f);
            targetPosition = offsetPosition + (mCamera.position + mCamera.forward * offset);
            thisParticle.position = Vector3.Lerp(thisParticle.position, targetPosition, 10f* Time.deltaTime);
        }
        onScreen = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!onScreen)
            return;
        targetPosition = offsetPosition + (mCamera.position + mCamera.forward * offset);

        thisParticle.position = targetPosition;
    }

    void OnCollisionEvent(CollisionHull3D col)
    {
        if(col.tag == "PlayerBullet")
        {
            onScreen = false;
            gameObject.SetActive(false);
        }
    }
}
