using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGenerator : MonoBehaviour
{
    public Transform PlayerTransform;

    public GameObject[] cityObjects;

    public Vector2 xCitySpawnRange;

    public int indexToChange = 0;
    public int lastIndex = 9;

    // Start is called before the first frame update
    void Start()
    {
        lastIndex = cityObjects.Length - 1;


    }

    // Update is called once per frame
    void Update()
    {
        generateBuidlings();
    }

    void generateBuidlings()
    {
        if (PlayerTransform.position.z - cityObjects[indexToChange].transform.position.z > 35)
        {
            Debug.Log("AAAAAAA");
            cityObjects[indexToChange].transform.position = cityObjects[lastIndex].transform.position + new Vector3(0, 0, 35);

            
            //cityObjects[indexToChange].GetComponent<Particle3D>().position = cityObjects[lastIndex].transform.position + new Vector3(0, 0, 35);

            lastIndex = indexToChange;

            indexToChange++;
            indexToChange = indexToChange % cityObjects.Length;
        }
    }
}
