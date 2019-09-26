using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCollision : CollisionHull2D
{

    public CircleCollision() : base(CollisionHull2D.HullType.CIRCLE) { }

    [Range(0.0f, 100.0f)]
    public float radius;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool TestCollisionVsCircle(CircleCollision other)
    {

        // Stepo 2
        //Collision if distance between centers is less or equal than sum of radii
        //optimized collision if (distance between centers) squared is less than or equal (the sum of radii) squared
       

        // 1) Get positions
        Vector2 thisPosition = particle.position;
        Vector2 otherPosition = other.particle.position;

        // 2) differnce of the two
        Vector2 distance = thisPosition - otherPosition;

        // 3) distance squared
        float distanceSq = Vector2.Dot(distance, distance);

        // 4) Sum of Radii
        float sumOfRadii = radius + other.radius;

        // 5) square the sum
        float sumSq = sumOfRadii * sumOfRadii;

        // 6) TEST: distanceSq <= sumSq

        if(distanceSq <= sumSq)
        {
            return true;
        }

        return false;
    }

    public override bool TestCollisionVsAABB(AxisAlignedBoundingBox2D other)
    {
        //Find the closest point to the circle on the box, take the center and the closest point
        // (done by clamping center of circle to be within box dimensions)
        // if closest point is within circle, pass (do point vs circle test)
        
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

    public override bool TestCollisionVsOBB(ObjectBoundingBox2D other)
    {

        //Same as above, but first...
        // multiply circle center by box world matrix inverse



        return false;
    }



}
