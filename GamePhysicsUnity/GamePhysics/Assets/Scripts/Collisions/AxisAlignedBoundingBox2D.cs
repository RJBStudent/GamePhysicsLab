﻿using System.Collections;
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

        // 1) Get position of this AABB

        // 2) Get position of OBB

        // 3) Get the x normal of OBB by (+cos(particle.rotation), +sin(particle.rotation))

        // 4) Get the y normal of OBB by (-sin(particle.rotation), +cos(particle.rotation))

        // 5) Get the height and width of the OBB

        // 6) Get the extents of all corners

            // 6/1) TopRightCorner add half of width and add half of heigth
        
            // 6/2) TopLeftCorner subtract half of width and add half of heigth
        
            // 6/3) BottomRightCorner add half of width and subtract half of heigth
            
            // 6/4) BottomLeftCorner subtract half of width and subtract half of heigth

        // 7) rotate the corners with rotation matrix




        return false;
    }

    public override bool TestCollisionVsCircle(CircleCollision other)
    {

        //see circle

        // 1) Get Circle position 

        // 2) Get position of AABB collider

        // 3) Get max and min Extents of X boxes

        // 5) Get max and min Extents of Y boxes

        // 6) Clamp circle X position 

        // 7) Clamp circle Y position

        // 8) Create vector2 for closest point on rectangle with clamped position

        // 9) TEST : see if new vector2 is within circle radius


        return false;
    }

}
