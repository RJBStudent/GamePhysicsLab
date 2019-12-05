using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Particle3D))]
public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField]
    float xSpeed;

    [SerializeField]
    float ySpeed;

    [SerializeField]
    Particle3D thisParticle;


    float xInput = 0;
    float yInput = 0;



    float shootInput = 0;
    float lastShootInput = 0;
    

    [SerializeField]
    int bulletCount;
    [SerializeField]
    GameObject playerBullet;
    List<GameObject> bullets;
    [SerializeField]
    float projectileSpeed;

    int currentBulletIndex = 0;

    Vector3 direction;


    // Start is called before the first frame update
    void Start()
    {
        thisParticle = GetComponent<Particle3D>();

        bullets = new List<GameObject>();
        for (int i = 0; i < bulletCount; i++)
        {
            GameObject newBullet = Instantiate(playerBullet, transform.position, Quaternion.identity) as GameObject;
            bullets.Add(newBullet);
            newBullet.GetComponent<RotateAroundPlanetScrpt>().thePlanet = GetComponent<RotateAroundPlanetScrpt>().thePlanet;
            newBullet.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();
        UpdatePosition();
        ShootProjectile();
    }

    void UpdateInput()
    {

        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        lastShootInput = shootInput;
        shootInput = Input.GetAxisRaw("Shoot");
       

    }

    Vector3 force = new Vector3();
    void UpdatePosition()
    {
        force.x = xInput * xSpeed;
        force.y = 0;
        force.z = yInput * ySpeed;
        
        thisParticle.AddLocalForce(force);

        direction = transform.eulerAngles;
        
        
        //transform.rotation = Quaternion.Euler(force);
        
        //thisParticle.AddTorque(new Vector3(0, xInput * 100, 0))
       // thisParticle.angularVelocity = new Vector3(0, xInput * 100, 0);
    }

    void ShootProjectile()
    {
        if(shootInput == 1f && lastShootInput != 1f)
        {
            currentBulletIndex = (currentBulletIndex + 1) % bulletCount;
            bullets[currentBulletIndex].SetActive(true);
            // bullets[currentBulletIndex].GetComponent<Particle>().position = thisParticle.position;
            bullets[currentBulletIndex].transform.position = thisParticle.position + (direction.normalized * 1.2f);
            bullets[currentBulletIndex].GetComponent<Particle3D>().position = bullets[currentBulletIndex].transform.position;
            bullets[currentBulletIndex].GetComponent<CollisionHull3D>().enabled = true;
            bullets[currentBulletIndex].GetComponent<Particle3D>().AddForce(direction.normalized * projectileSpeed);
           // bullets[currentBulletIndex].GetComponent<Particle3D>().velocity = Vector2.zero;
        }
    }
}
