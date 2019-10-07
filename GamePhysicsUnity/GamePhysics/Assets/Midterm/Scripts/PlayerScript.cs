using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    [SerializeField, Range(1, 100)]
    float forwardSpeed=1;
    [SerializeField, Range(1, 100)]
    float rotationSpeed=1;

    Vector2 playerForce;
    float rotationForce;

    Vector2 direction;

    [SerializeField]
    Particle thisParticle;

    float spaceInput = 0.0f;
    float lastSpaceInput = 0.0f;

    [Space]
    [Header ("Bullets and Shooting")]
    List<GameObject> bullets;

    [SerializeField]
    GameObject playerBullet;
    [SerializeField]
    int bulletCount;

    int currentBulletIndex;

    // Start is called before the first frame update
    void Start()
    {
        playerForce = new Vector2();
        direction = new Vector2();
        bullets = new List<GameObject>();
        for(int i = 0; i < bulletCount; i++)
        {
            GameObject newBullet = Instantiate(playerBullet, Vector3.zero, Quaternion.identity) as GameObject;
            bullets.Add(newBullet);

            bullets[currentBulletIndex].GetComponent<Particle>().position = transform.position;
            newBullet.SetActive(false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetInput();
        thisParticle.AddTorque(rotationForce);
        thisParticle.AddForce(playerForce);

        Shoot();
    }

    void GetInput()
    {
        //playerForce.x = Input.GetAxis("Horizontal") * xSpeed;
        playerForce.x = Input.GetAxis("Vertical") * forwardSpeed;

        rotationForce = -Input.GetAxis("Horizontal") * rotationSpeed;

        direction.x = Mathf.Cos(thisParticle.rotation * Mathf.Deg2Rad);
        direction.y = Mathf.Sin(thisParticle.rotation * Mathf.Deg2Rad);


        playerForce = direction * playerForce.x;

        lastSpaceInput = spaceInput;
        spaceInput = Input.GetAxisRaw("Shoot");
        
    }

    void Shoot()
    {
        if(spaceInput == 1f && lastSpaceInput != 1.0f)
        {
            Debug.Log("Shoot : " + currentBulletIndex);
            currentBulletIndex = (currentBulletIndex+1) % bulletCount;
            bullets[currentBulletIndex].SetActive(true);
            //bullets[currentBulletIndex].transform.position = transform.position;

        }
    }
}
