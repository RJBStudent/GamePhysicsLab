using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisAlignedBoundingBox2D : CollisionHull2D
{

    public AxisAlignedBoundingBox2D() : base(CollisionHull2D.HullType.AABB) { }

    [Range(0.1f, 100.0f)]
    public float length;
    [Range(0.1f, 100.0f)]
    public float hieght;

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
        //pass if, for all axes, max extent of A is greater than the min extent of B

        // 1) Get positions of colliders
        Vector2 otherPos = other.particle.position;


        // 2) Get Extent values in space by first halving height and width

        // 3) With the halved values add them to partocle position to get max extent

        // 4) subtract for min extents

        // 5) compare max 0 with min 1 on x axis

        // 6) compare max 1 with min 0 on x axis

        // 7) compare to see if x axis test is true

        // 8) compare max 0 with min 1 on y axis

        // 9) compare max 1 with min 0 on y axis

        // 10) Compare y axis test to see if true

        // 11) return true if both axis test are true



        //Get the min and max extents of 

        return false;
    }


    public override bool TestCollisionVsOBB(ObjectBoundingBox2D other)
    {
        // Same as above twice:
        // first find max extents of OBB, do AABB vs this box
        // then transform this box into OBB space find max extents, repeat
        // 1. .........

        return false;
    }

    public override bool TestCollisionVsCircle(CircleCollision other)
    {

        //see circle
        return false;
    }

}
