using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBoundingBox2D : CollisionHull2D
{

    public ObjectBoundingBox2D() : base(CollisionHull2D.HullType.OBB) { }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool TestCollisionVsAABB(AxisAlignedBoundingBox2D other)
    {
        //see AABB

        return false;
    }


    public override bool TestCollisionVsOBB(ObjectBoundingBox2D other)
    {
        //same as AABB-OBB part 2, twice
        // 1. ..............

        return false;
    }

    public override bool TestCollisionVsCircle(CircleCollision other)
    {
               //see circle

        return false;
    }
}
