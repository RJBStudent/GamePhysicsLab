﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBoundingBox2D : CollisionHull2D
{
    [Range(0.1f, 100.0f)]
    public float width;
     [Range(0.1f, 100.0f)]
    public float height;

    public ObjectBoundingBox2D() : base(CollisionHull2D.HullType.OBB) { }

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

    public override bool TestCollisionVsAABB(AxisAlignedBoundingBox2D other)
    {
        //see AABB

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

        // 8) Get Min X as lowest X Extent value

        // 9) Get Max X as Highest X Extent  value

        // 10) Get Min Y as Highest Y Extent value

        // 11) Get Max Y as Highest Y Extent value

        // 12) Max Extent is Highest X and Highest Y

        // 13) Min Extent is Lowest X and lowest Y

        // 14) Get Extents of this AABB with half width and half height added and subtracted to the position

        // 15) compare max OBB with min ABB on x axis

        // 16 ) compare max ABB with min OBB on x axis

        // 17) compare to see if x axis test is true

        // 18) compare max OBB with min AABB on y axis

        // 19) compare max AABB with min OBB on y axis

        // 20) Compare y axis test to see if true

        // 21) if both are true continue else return false

        // 22) rotate the AABB to be in the OBB world space 

        // 22/1) Get 4 Extents of AABB (topleft/topRight/bottomLeft/bottomRight)

        // 22/2) multiply AABB position and extents by rotation matrix

        // 23) Get new AABB extents with the highest and lowest X and Y values

        // 24) Do AABB comparision  step 16 - 21

        // (1) 
        Vector2 AABBpos = other.particle.position;

        // (2) 
        Vector2 OBBpos = particle.position;

        // (5)
        float AABBWidth = other.width;
        float AABBHeight = other.height;

        // (6/1)
        Vector2 OBBTopRight = new Vector2(+(width / 2), +(height / 2));

        // (6/2)
        Vector2 OBBTopLeft = new Vector2(-(width / 2), +(height / 2));

        // (6/3)
        Vector2 OBBBottomRight = new Vector2(+(width / 2), -(height / 2));

        // (6/4)
        Vector2 OBBBottomLeft = new Vector2(-(width / 2), -(height / 2));

        // (7)
        Quaternion newQuat = Quaternion.Euler(0, 0, -particle.rotation);
        Matrix4x4 rotationMat = Matrix4x4.Rotate(newQuat);

        OBBTopRight = rotationMat.MultiplyPoint3x4(OBBTopRight);
        OBBTopLeft = rotationMat.MultiplyPoint3x4(OBBTopLeft);
        OBBBottomRight = rotationMat.MultiplyPoint3x4(OBBBottomRight);
        OBBBottomLeft = rotationMat.MultiplyPoint3x4(OBBBottomLeft);


        // (8 - 11)

        float minX = Mathf.Infinity, maxX = 0, minY = Mathf.Infinity, maxY = 0;

        Vector2[] corners = new Vector2[] { OBBTopRight, OBBTopLeft, OBBBottomRight, OBBBottomLeft };

        for (int i = 0; i < corners.Length; i++)
        {
            // (8)
            if (corners[i].x < minX)
            {
                minX = corners[i].x;
            }

            // (9)
            if (corners[i].x > maxX)
            {
                maxX = corners[i].x;
            }

            // (10)
            if (corners[i].y < minY)
            {
                minY = corners[i].y;
            }

            // (11)
            if (corners[i].y > maxY)
            {
                maxY = corners[i].y;
            }

        }


        // (12)
        Vector2 maxOBBExtent = new Vector2(OBBpos.x + maxX, OBBpos.y + maxY);

        // (13)
        Vector2 minOBBExtent = new Vector2(OBBpos.x + minX, OBBpos.y + minY);

        // (14)
        Vector2 maxAABBExtent = new Vector2(AABBpos.x + (AABBWidth / 2), AABBpos.y + (AABBHeight / 2));

        // (15)
        Vector2 minAABBExtent = new Vector2(AABBpos.x - (AABBWidth / 2), AABBpos.y - (AABBHeight / 2));


        bool xTest, yTest;

        // (16)                                   (17)
        if (maxOBBExtent.x >= minAABBExtent.x && maxAABBExtent.x >= minOBBExtent.x)
        {
            // (17)
            xTest = true;

        }
        else
        {
            // (17)
            xTest = false;
        }


        // (18)                                     (19)
        if (maxOBBExtent.y >= minAABBExtent.y && maxAABBExtent.y >= minOBBExtent.y)
        {
            // (20)
            yTest = true; 
        }
        else
        {
            // (20)
            yTest = false;
        }

        // (21)
        if (xTest == false || yTest == false)
        {
            return false;
        }

        // (22)
        maxOBBExtent = new Vector2(width / 2, height / 2);
        minOBBExtent = new Vector2(-width / 2, -height / 2);

        // (22/1)
        Vector2 AABBTopRight = new Vector2((AABBpos.x + (AABBWidth / 2) - OBBpos.x), (AABBpos.y + (AABBHeight / 2)) - OBBpos.y);
        Vector2 AABBTopLeft = new Vector2((AABBpos.x - (AABBWidth / 2) - OBBpos.x), (AABBpos.y + (AABBHeight / 2)) - OBBpos.y);
        Vector2 AABBBottomRight = new Vector2((AABBpos.x + (AABBWidth / 2) - OBBpos.x), (AABBpos.y - (AABBHeight / 2)) - OBBpos.y);
        Vector2 AABBBottomLeft = new Vector2((AABBpos.x - (AABBWidth / 2) - OBBpos.x), (AABBpos.y - (AABBHeight / 2)) - OBBpos.y);


        // (22/2)
        AABBTopRight = rotationMat.MultiplyPoint3x4(AABBTopRight);
        AABBTopLeft = rotationMat.MultiplyPoint3x4(AABBTopLeft);
        AABBBottomRight = rotationMat.MultiplyPoint3x4(AABBBottomRight);
        AABBBottomLeft = rotationMat.MultiplyPoint3x4(AABBBottomLeft);

        //(23)
        minX = Mathf.Infinity;
        maxX = 0;
        minY = Mathf.Infinity;
        maxY = 0;

        corners = new Vector2[] { AABBTopRight, AABBTopLeft, AABBBottomRight, AABBBottomLeft };

        for (int i = 0; i < corners.Length; i++)
        {
            if (corners[i].x < minX)
            {
                minX = corners[i].x;
            }

            // (9)
            if (corners[i].x > maxX)
            {
                maxX = corners[i].x;
            }

            // (10)
            if (corners[i].y < minY)
            {
                minY = corners[i].y;
            }

            // (11)
            if (corners[i].y > maxY)
            {
                maxY = corners[i].y;
            }

        }

        maxAABBExtent = new Vector2(maxX, maxY);
        minAABBExtent = new Vector2(minX, minY);

        // (24)
        if (maxOBBExtent.x >= minAABBExtent.x && maxAABBExtent.x >= minOBBExtent.x)
        {
            xTest = true;
        }
        else
        {
            xTest = false;
        }


        if (maxOBBExtent.y >= minAABBExtent.y && maxAABBExtent.y >= minOBBExtent.y)
        {
            yTest = true;
        }
        else
        {
            yTest = false;
        }

        if (xTest == false || yTest == false)
        {
            return false;
        }
        else
        {
            return true;
        }
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
        Vector2 pos = other.transform.position;
        //  1. Get z-rotation of OBB
        float zRot = particle.rotation;
        //  2. Rotate OBB by -Z

        zRot *= Mathf.Deg2Rad;
        //  3. Transform circles position by mat2x2 {cos -sin;   sin cos} * {circlePos - boxPos}
        Vector2 top = new Vector2(Mathf.Cos(-zRot), -Mathf.Sin(-zRot));
        Vector2 bottom = new Vector2(Mathf.Sin(-zRot), Mathf.Cos(-zRot));

        pos = pos - particle.position;
        pos.x = pos.x * top.x + pos.y * top.y;
        pos.y = pos.x * bottom.x + pos.y * bottom.y;


        //  4. Clamp circle pos by the extents of the box

        Vector2 clampedPos = Vector2.zero;
        clampedPos.x = Mathf.Clamp(pos.x, -.5f * width, .5f * width);
        clampedPos.y = Mathf.Clamp(pos.y, -.5f * height, .5f *height);

        //  5. Compare clamped position against circles radius
        if ((pos - clampedPos).magnitude <= other.radius)
        {
            return true;
        }
        else
        {
            return false;
        }

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
