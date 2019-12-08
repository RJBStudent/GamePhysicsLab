using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGenerator : MonoBehaviour
{
    public GameObject[] BuidlingPrefab;
    public GameObject BridgePrefab;
    public Transform PlayerTransform;

    GameObject[] buildingObjects;
    GameObject[] bridgeObjects;

    public int numBuidlings;
    public int numBridges;

    public Vector2 xBuildingSpawnRange;
    public Vector2 zBuildingSpawnRange;

    public Vector2 zBridgeSpawnRange;

    public int indexToChange_Buidling = 0;
    public int lastIndex_Buidling = 9;

    public int indexToChange_Bridge = 0;
    public int lastIndex_Bridge = 9;

    // Start is called before the first frame update
    void Start()
    {
        buildingObjects = new GameObject[numBuidlings];
        lastIndex_Buidling = buildingObjects.Length - 1;
        bridgeObjects = new GameObject[numBridges];
        lastIndex_Bridge = bridgeObjects.Length - 1;

        for (int i = 0; i < bridgeObjects.Length; i++)
        {
            Vector3 spawnPos;

            bridgeObjects[i] = Instantiate(BridgePrefab, new Vector3(1000, -1000, 1000), Quaternion.identity);

            if (i == 0)
            {
                spawnPos = new Vector3(0, .5f * bridgeObjects[i].transform.localScale.y,
                                        Random.Range(zBridgeSpawnRange.x, zBridgeSpawnRange.y));
            }
            else
            {
                spawnPos = new Vector3(0, .5f * bridgeObjects[i].transform.localScale.y,
                                        bridgeObjects[i - 1].transform.position.z + Random.Range(zBridgeSpawnRange.x, zBridgeSpawnRange.y));
            }


            bridgeObjects[i].transform.position = spawnPos;
        }


        for (int i = 0; i < buildingObjects.Length; i++)
        {
            Vector3 spawnPos;

            buildingObjects[i] = Instantiate(BuidlingPrefab[Random.Range(0, BuidlingPrefab.Length)], new Vector3(1000, -1000, 1000), Quaternion.identity);

            if (i == 0)
            {
                spawnPos = new Vector3(Random.Range(xBuildingSpawnRange.x, xBuildingSpawnRange.y), .5f * buildingObjects[i].transform.localScale.y, 20);
            }
            else
            {
                spawnPos = new Vector3(Random.Range(xBuildingSpawnRange.x, xBuildingSpawnRange.y), .5f * buildingObjects[i].transform.localScale.y, 
                                        buildingObjects[i-1].transform.position.z + Random.Range(zBuildingSpawnRange.x, zBuildingSpawnRange.y));
            }


            buildingObjects[i].transform.position = spawnPos;
        }

    }

    // Update is called once per frame
    void Update()
    {
        generateBuidlings();
    }

    void generateBuidlings()
    {
        if (PlayerTransform.position.z - buildingObjects[indexToChange_Buidling].transform.position.z > 20)
        {

            Vector3 offset = new Vector3(Random.Range(xBuildingSpawnRange.x, xBuildingSpawnRange.y), 
                                                        .5f * buildingObjects[indexToChange_Buidling].transform.localScale.y, 
                                                        Random.Range(zBuildingSpawnRange.x, zBuildingSpawnRange.y));

            buildingObjects[indexToChange_Buidling].transform.position = Vector3.Cross(buildingObjects[lastIndex_Buidling].transform.position, new Vector3(1, 0 ,1)) + offset;


            lastIndex_Buidling = indexToChange_Buidling;

            indexToChange_Buidling++;
            indexToChange_Buidling = indexToChange_Buidling % buildingObjects.Length;
        }
    }
}
