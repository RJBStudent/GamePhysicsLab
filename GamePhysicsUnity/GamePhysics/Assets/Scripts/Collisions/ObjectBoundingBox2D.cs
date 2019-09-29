using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBoundingBox2D : CollisionHull2D
{
    [Range(0.1f, 100.0f)]
    public float width;
     [Range(0.1f, 100.0f)]
    public float height;

    public ObjectBoundingBox2D() : base(CollisionHull2D.HullType.OBB) { }

 
  
    public ObjectBoundingBox2D obb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TestCollisionVsOBB(obb);
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
        Vector2 thisPos = GetComponent<Particle>().position;
        // 2) get other OBB position
        Vector2 otherPos = other.GetComponent<Particle>().position;

        float thisRot = particle.rotation;

        Vector2 otherRotatedPos = rotateAroundPoint(otherPos, thisPos, -thisRot);
        otherRotatedPos -= thisPos;

        float otherRot = other.particle.rotation;
        otherRot -= thisRot;

        Vector2[] points = new Vector2[4];

        points[0] = new Vector2(-.5f * other.width, -.5f * other.height);
        points[1] = new Vector2(-.5f * other.width, .5f * other.height);
        points[2] = new Vector2(.5f * other.width, -.5f * other.height);
        points[3] = new Vector2(.5f * other.width, .5f * other.height);

        for (int i = 0; i < 4; i++)
        {
            points[i] = rotateAroundPoint(points[i], new Vector2(0, 0), otherRot);
        }

        float otherMaxX = Mathf.Max(points[0].x, points[1].x, points[2].x, points[3].x) + otherRotatedPos.x;
        float otherMinX = Mathf.Min(points[0].x, points[1].x, points[2].x, points[3].x) + otherRotatedPos.x;
        float otherMaxY = Mathf.Max(points[0].y, points[1].y, points[2].y, points[3].y) + otherRotatedPos.y;
        float otherMinY = Mathf.Min(points[0].y, points[1].y, points[2].y, points[3].y) + otherRotatedPos.y;

        

        float thisMaxX = width * .5f;
        float thisMinX = -width * .5f;
        float thisMaxY = height * .5f;
        float thisMinY = -height * .5f;

        if(name == "OBB_1")
        {
            //Debug.Log(otherMinX + "    " + otherMaxX + "    " + otherMinY + "    " + otherMaxY);
        }

        bool check1 = false;
        if ((thisMaxX > otherMinX && thisMaxY > otherMinY) && (otherMaxX > thisMinX && otherMaxY > thisMinY))
        {
            check1 = true;
            Debug.Log(check1);
        }

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

    Vector2 rotateAroundPoint(Vector2 point, Vector2 center, float degree)
    {
        degree *= Mathf.Deg2Rad;
        Vector2 top = new Vector2(Mathf.Cos(degree), -Mathf.Sin(degree));
        Vector2 bottom = new Vector2(Mathf.Sin(degree), Mathf.Cos(degree));

        point = point - center;
        point.x = point.x * top.x + point.y * top.y;
        point.y = point.x * bottom.x + point.y * bottom.y;

        point += center;
        return point;
    }
}
