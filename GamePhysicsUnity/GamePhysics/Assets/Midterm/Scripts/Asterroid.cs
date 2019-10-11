using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asterroid : MonoBehaviour
{
    public int size;

    public GameObject asteroidPrefab;

    public float separationForce;

    Particle particle;
    ScreenWrap screenWrap;

    public float maxSpeed;


    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<Particle>();
        screenWrap = GetComponent<ScreenWrap>();
        bool xGood = particle.position.x < screenWrap.xWrapPos && particle.position.x > -screenWrap.xWrapPos;
        bool yGood = particle.position.y < screenWrap.yWrapPos && particle.position.y > -screenWrap.yWrapPos;
        if (xGood && yGood)
        {
            screenWrap.enabled = true;
        }
        else
        {
            screenWrap.enabled = false;
        }
        GetComponent<CollisionHull2D>().callMethod = OnCollisionEvent;
    }

    // Update is called once per frame
    void Update()
    {
        if(screenWrap.enabled == false)
        {
            bool xGood =  particle.position.x < screenWrap.xWrapPos && particle.position.x > -screenWrap.xWrapPos;
            bool yGood = particle.position.y < screenWrap.yWrapPos && particle.position.y > -screenWrap.yWrapPos;

            if (xGood && yGood)
            {
                screenWrap.enabled = true;
            }
        }

        particle.velocity = Vector2.ClampMagnitude(particle.velocity, maxSpeed);
    }

    void BreakApart()
    {
        if(size == 1)
        {
            Destroy(gameObject);
        }
        else
        {
            int asteroidOneSize = Random.Range(1, size - 1);
            float asteroidOneRadius = .5f * asteroidOneSize;

            Vector2 randomDirection = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
            randomDirection.Normalize();

            GameObject asteroidOne = Instantiate(asteroidPrefab, transform.position + asteroidOneRadius * (Vector3)randomDirection, transform.rotation);
            asteroidOne.GetComponent<CircleCollision>().radius = asteroidOneRadius;
            asteroidOne.GetComponent<Asterroid>().size = asteroidOneSize;
            asteroidOne.transform.localScale = new Vector3(asteroidOneSize, asteroidOneSize, asteroidOneSize);
            asteroidOne.GetComponent<Particle>().AddForce(randomDirection * separationForce);

            int asteroidTwoSize = size - asteroidOneSize;
            float asteroidTwoRadius = .5f * asteroidTwoSize;

            GameObject asteroidTwo = Instantiate(asteroidPrefab, transform.position - asteroidTwoRadius * (Vector3)randomDirection, transform.rotation);
            asteroidTwo.GetComponent<CircleCollision>().radius = asteroidTwoRadius;
            asteroidTwo.GetComponent<Asterroid>().size = asteroidTwoSize;
            asteroidTwo.transform.localScale = new Vector3(asteroidTwoSize, asteroidTwoSize, asteroidTwoSize);
            asteroidTwo.GetComponent<Particle>().AddForce(-randomDirection * separationForce);

            Destroy(gameObject);
        }
    }

    void OnCollisionEvent(CollisionHull2D col)
    {
        if (col.gameObject.tag == "PlayerBullet")
        {
            col.gameObject.SetActive(false);
            BreakApart();
            ScoreManagerScript.Instance.score += 100;
            Debug.Log("Col Asteroid");
        }
    }
}
