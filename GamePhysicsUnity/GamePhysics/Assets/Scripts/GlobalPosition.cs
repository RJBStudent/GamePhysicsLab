using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalPosition : MonoBehaviour
{
    public Vector3 globalPos;

    Vector2 clampPos;
    Vector2 rotPos;
    float dist;
    public ObjectBoundingBox2D obb;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        globalPos = transform.position;
        
    }
}
