using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootScript : MonoBehaviour
{

    [SerializeField]
    Particle3D thisParticle;

    float spaceInput = 0.0f;
    float lastSpaceInput = 0.0f;

    [Header("Bullets and Shooting")]
    List<GameObject> bullets;

    [SerializeField]
    GameObject playerBullet;
    [SerializeField]
    int bulletCount;

    int currentBulletIndex;

    [SerializeField]
    float projectileSpeed;

    Vector3 direction;

    [SerializeField]
    Transform reticlePos; 

    // Start is called before the first frame update
    void Start()
    {
        thisParticle = GetComponent<Particle3D>();
        bullets = new List<GameObject>();
        for (int i = 0; i < bulletCount; i++)
        {
            GameObject newBullet = Instantiate(playerBullet, transform.position, Quaternion.identity) as GameObject;
            bullets.Add(newBullet);
            newBullet.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        direction =  reticlePos.position - transform.position;
        direction.Normalize();
        lastSpaceInput = spaceInput;
        spaceInput = Input.GetAxisRaw("Shoot");
        Shoot();
    }

    void Shoot()
    {
        if (spaceInput == 1f && lastSpaceInput != 1.0f)
        {
            currentBulletIndex = (currentBulletIndex + 1) % bulletCount;
            bullets[currentBulletIndex].SetActive(true);
            // bullets[currentBulletIndex].GetComponent<Particle>().position = thisParticle.position;
            bullets[currentBulletIndex].transform.position = thisParticle.position + (direction.normalized * 5f);
            bullets[currentBulletIndex].GetComponent<Particle3D>().position = bullets[currentBulletIndex].transform.position;
            bullets[currentBulletIndex].GetComponent<CollisionHull3D>().enabled = true;
            bullets[currentBulletIndex].GetComponent<Particle3D>().AddForce(direction.normalized * projectileSpeed);
            bullets[currentBulletIndex].GetComponent<Particle3D>().velocity = Vector3.zero;
        }
    }
}
