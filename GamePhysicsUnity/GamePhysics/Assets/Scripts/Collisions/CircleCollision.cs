using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCollision : CollisionHull2D
{

    public CircleCollision() : base(CollisionHull2D.HullType.CIRCLE) { }

    [Range(0.0f, 100.0f)]
    public float radius;

    MeshRenderer meshRen;

    // Start is called before the first frame update
    void Start()
    {
      
        CollisionManager.Instance.AddCollision(this);

        meshRen = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (colliding)
        {
            meshRen.material.color = Color.red;
        }
        else
        {
            meshRen.material.color = Color.blue;
        }
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

        if (distanceSq <= sumSq)
        {
            return true;            
        }
        Debug.Log("false");
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

        // 4) Get minimum and max x and y positions for clamping

        // 5) Clamp circle X position 

        // 6) Clamp circle Y position

        // 7) Create vector2 for closest point on rectangle with clamped position

        // 8) TEST : see if new vector2 is within circle radius

        // (1)
        Vector2 otherPos = other.particle.position;

        // (2)
        Vector2 thisPos = particle.position;

        float otherWidth = other.width;
        float otherHeight = other.height;

        // (4)
        float maxX = otherPos.x + (otherWidth / 2);
        float minX = otherPos.x - (otherWidth / 2);
                     
        float maxY = otherPos.y - (otherHeight / 2);
        float minY = otherPos.y - (otherHeight / 2);

        // (5)
        float xClosest = thisPos.x;
        xClosest = Mathf.Clamp(xClosest, minX, maxX);

        // (6)
        float yClosest = thisPos.y;
        yClosest = Mathf.Clamp(yClosest, minY, maxY);

        // (7)
        Vector2 closestPoint = new Vector2(xClosest, yClosest);

        // (8)
        if ((otherPos - closestPoint).magnitude < radius)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public override bool TestCollisionVsOBB(ObjectBoundingBox2D other)
    {
        Vector2 pos = transform.position;
        //  1. Get z-rotation of OBB
        float zRot = other.particle.rotation;
        //  2. Rotate OBB by -Z

        zRot *= Mathf.Deg2Rad;
        //  3. Transform circles position by mat2x2 {cos -sin;   sin cos} * {circlePos - boxPos}
        Vector2 top = new Vector2(Mathf.Cos(-zRot), -Mathf.Sin(-zRot));
        Vector2 bottom = new Vector2(Mathf.Sin(-zRot), Mathf.Cos(-zRot));

        pos = pos - other.particle.position;
        pos.x = pos.x * top.x + pos.y * top.y;
        pos.y = pos.x * bottom.x + pos.y * bottom.y;


        //  4. Clamp circle pos by the extents of the box

        Vector2 clampedPos = Vector2.zero;
        clampedPos.x = Mathf.Clamp(pos.x, -.5f * other.width, .5f * other.width);
        clampedPos.y = Mathf.Clamp(pos.y, -.5f * other.height, .5f * other.height);
        
        //  5. Compare clamped position against circles radius
        if ((pos - clampedPos).magnitude <= radius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }



}
