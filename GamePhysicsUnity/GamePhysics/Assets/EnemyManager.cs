using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("FLyee bois")]

    [SerializeField]
    GameObject FlyingEnemies;
    [SerializeField]
    int flyingCount;
    int currentFlyingIndex = 0;
    [SerializeField]
    Transform mCamera;

    List<GameObject> flyingList = new List<GameObject>();

    [SerializeField]
    float spawnTime;
    float currentTime;

    void Start()
    {
        for (int i = 0; i < flyingCount; i++)
        {
            GameObject newFlyBoy = (GameObject)Instantiate(FlyingEnemies);
            flyingList.Add(newFlyBoy);
            newFlyBoy.SetActive(false);
            newFlyBoy.GetComponent<EnemyScript>().mCamera = mCamera;
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime > spawnTime)
        {
            if (!flyingList[(currentFlyingIndex + 1) % flyingCount].activeInHierarchy)
            {
                currentFlyingIndex = (currentFlyingIndex + 1) % flyingCount;
                flyingList[currentFlyingIndex].SetActive(true);
                flyingList[currentFlyingIndex].GetComponent<EnemyScript>().SpawnEnemy();
                currentTime = 0f;
            }
        }
    }
    

}
