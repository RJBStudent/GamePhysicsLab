using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{

    public AxisAlignedBoundingBox3D boundingBox;
    // Start is called before the first frame update
    void Start()
    {
        boundingBox = GetComponent<AxisAlignedBoundingBox3D>();

        boundingBox.width = transform.localScale.x;
        boundingBox.height = transform.localScale.y;
        boundingBox.depth = transform.localScale.z;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
