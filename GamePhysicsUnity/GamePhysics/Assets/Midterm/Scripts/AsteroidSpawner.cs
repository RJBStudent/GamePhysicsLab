using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{

    public Vector2 spawnTimeRange;
    public GameObject asteroidPrefab;
    float currentSpawnTime;
    float deltaTime = 0;

    public float spawnForce;

    public int maxAsteroids;

    // Start is called before the first frame update
    void Start()
    {
        currentSpawnTime = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime += Time.deltaTime;
        Spawn();
    }

    void Spawn()
    {
        if(deltaTime > currentSpawnTime)
        {
            GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");

            int collectiveAsteroidMass = 0;
            for(int i = 0; i < asteroids.Length; i++)
            {
                collectiveAsteroidMass += asteroids[i].GetComponent<Asterroid>().size;
            }

            if(collectiveAsteroidMass < maxAsteroids)
            {
                GameObject asteroid = Instantiate(asteroidPrefab, transform.position, transform.rotation);


                Vector2 shootDir = -(Vector2)transform.position + new Vector2(Random.RandomRange(-5, 5), Random.RandomRange(-5, 5));
                asteroid.GetComponent<Particle>().AddForce(shootDir * spawnForce);

                deltaTime = 0;
                currentSpawnTime = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
            }
        }
    }
}
