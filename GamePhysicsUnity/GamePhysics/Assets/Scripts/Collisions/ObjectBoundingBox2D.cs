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

        // 1) Get this OBB position

        // 2) get other OBB position
        
        // 3) Get the x normal of OBB by (+cos(particle.rotation), +sin(particle.rotation))

        // 4) Get the y normal of OBB by (-sin(particle.rotation), +cos(particle.rotation))

        // 5) rotate the OBB into its world space X normal

        // 6) get extents of this rotated box by adding half width and height then subtracting half width half height

        // 7) get extents of other OBB

        // 8) rotate other extents to be in their own world space(particle current rotation)

        // 9) Rotate other extents to be in this OBB rotation

        // 10) get Min and Max of X and Y of other OBB

        // 11) using max X and Y get the Maximum extent

        // 12) using min X and Y get the minimum extent 

        // 13) Do AABB vs AABB with the new extents of the other OBB

        // 14) Go Back to step 4 and rotate the original positions to be its Y normal

        // 15) step 6 in Y normal world space
        
        // 16) step 7 - 13

        // 17) if all AABB are true return true

        return false;
    }

    public override bool TestCollisionVsCircle(CircleCollision other)
    {
               //see circle

        return false;
    }
}
