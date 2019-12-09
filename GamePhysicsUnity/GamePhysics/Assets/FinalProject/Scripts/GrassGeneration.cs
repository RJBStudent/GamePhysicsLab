using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassGeneration : MonoBehaviour
{

    public GameObject GrassPrefab;
    public Transform PlayerTransform;

    GameObject[] grassObjects;

    public int indexToChange = 0;
    public int lastIndex = 9;

    // Start is called before the first frame update
    void Start()
    {
        grassObjects = new GameObject[10];
        lastIndex = grassObjects.Length - 1;

        for (int i = 0; i < grassObjects.Length; i++)
        {
            grassObjects[i] = Instantiate(GrassPrefab, new Vector3(0, 0, 30f * i), Quaternion.identity);
            grassObjects[i].GetComponent<Particle3D>().position = new Vector3(0, 0, 30f * i);
        }

    }

    // Update is called once per frame
    void Update()
    {
        generateGrass();
    }

    void generateGrass()
    {
        if(PlayerTransform.position.z - grassObjects[indexToChange].transform.position.z > 20)
        {
            grassObjects[indexToChange].transform.position = grassObjects[lastIndex].transform.position + new Vector3(0, 0, 30);

            grassObjects[indexToChange].GetComponent<Particle3D>().position = grassObjects[lastIndex].transform.position + new Vector3(0, 0, 30);

            lastIndex = indexToChange;

            indexToChange++;
            indexToChange = indexToChange % grassObjects.Length;
        }
    }
}
