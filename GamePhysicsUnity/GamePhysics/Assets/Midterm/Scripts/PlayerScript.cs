using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    float projectileSpeed;

    [SerializeField]
    Text scoreText;

    [SerializeField]
    Text liveText;

    int score = 0;

    int currentLives = 3;


    // Start is called before the first frame update
    void Start()
    {


        playerForce = new Vector2();
        direction = new Vector2();
        bullets = new List<GameObject>();
        for(int i = 0; i < bulletCount; i++)
        {
            GameObject newBullet = Instantiate(playerBullet, transform.position, Quaternion.identity) as GameObject;
            bullets.Add(newBullet);
            newBullet.SetActive(false);
        }

        GetComponent<CollisionHull2D>().callMethod = OnCollisionEvent;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        UpdateUI();

        GetInput();
        
        thisParticle.AddTorque(rotationForce);


        thisParticle.AddForce(playerForce);

        Shoot();
    }

    void UpdateUI()
    {
        scoreText.text = "Score : " + score.ToString();

        liveText.text = "Lives : ";
        for(int i = 0; i < currentLives; i++)
        {
            liveText.text = liveText.text + " O ";
        }

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
            // bullets[currentBulletIndex].GetComponent<Particle>().position = thisParticle.position;
            bullets[currentBulletIndex].transform.position = thisParticle.position + (direction.normalized * 2f);
            bullets[currentBulletIndex].GetComponent<Particle>().position = bullets[currentBulletIndex].transform.position;
            bullets[currentBulletIndex].GetComponent<CollisionHull2D>().enabled = true;
            bullets[currentBulletIndex].GetComponent<Particle>().AddForce(direction.normalized * projectileSpeed);
            bullets[currentBulletIndex].GetComponent<Particle>().velocity = Vector2.zero;
        }
    }

    void OnCollisionEvent(CollisionHull2D col)
    {
        Debug.Log("Player");
        if (col.gameObject.tag == "PlayerBullet")
        {
            col.gameObject.SetActive(false);
        }
    }

 }                        
