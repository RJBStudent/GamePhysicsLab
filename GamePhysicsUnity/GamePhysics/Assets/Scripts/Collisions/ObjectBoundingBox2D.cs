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

        // 3) rotate the OBB into its world space normals
        
        // 3) Get the x normal of OBB by (+cos(particle.rotation), +sin(particle.rotation))

        // 4) Get the y normal of OBB by (-sin(particle.rotation), +cos(particle.rotation))

        // 4) get extents of this rotated box by adding half width and height then subtracting half width half height

        // 5) get extents of other OBB

        // 6) rotate other extents to be in their own world space(particle current rotation)

        // 7) Rotate other extents to be in this OBB rotation

        // 8) get Min and Max of X and Y of other OBB

        // 9) using max X and Y get the Maximum extent
        
        // 10) using min X and Y get the minimum extent 

        // 11) Do AABB vs AABB with the new extents of the other OBB

        return false;
    }

    public override bool TestCollisionVsCircle(CircleCollision other)
    {
               //see circle

        return false;
    }
}
