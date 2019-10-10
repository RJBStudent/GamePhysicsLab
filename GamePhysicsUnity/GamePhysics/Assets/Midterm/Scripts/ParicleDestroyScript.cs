using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParicleDestroyScript : MonoBehaviour
{

    [SerializeField]
    float destroyTime;

    float currentTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
    }

    private void Awake()
    {
        currentTime = 0;
    }

    private void OnEnable()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(currentTime >= destroyTime)
        {
            gameObject.SetActive(false);
        }
        currentTime += Time.deltaTime;

    }
}
