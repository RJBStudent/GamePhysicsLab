using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisAlignedBoundingBox2D : CollisionHull2D
{

    public AxisAlignedBoundingBox2D() : base(CollisionHull2D.HullType.AABB) { }

    [Range(0.1f, 100.0f)]
    public float width;
    [Range(0.1f, 100.0f)]
    public float height;

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

    public override bool TestCollisionVsAABB(AxisAlignedBoundingBox2D other, ref Collision c)
    {
        //pass if, for all axes, max extent of A is greater than the min extent of B

        // 1) Get positions of colliders

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

        // (1)
        Vector2 otherPos = other.particle.position;
        Vector2 thisPos = particle.position;

        Vector2 distance = thisPos - otherPos;

        // (2)
        float otherWidth = other.width;
        float otherHeight = other.height;

        // (3)
        Vector2 thisMaxExtent = new Vector2(particle.position.x + (width / 2), particle.position.y + (width / 2));
        Vector2 otherMaxExtent = new Vector2(otherPos.x + (otherWidth / 2), otherPos.y  + (otherHeight/ 2));
        

        // (4)
        Vector2 thisMinExtent = new Vector2(particle.position.x - (width / 2), particle.position.y - (height/ 2));
        Vector2 otherMinExtent = new Vector2(otherPos.x - (otherWidth / 2), otherPos.y - (otherHeight/ 2));

        // lab 5 collision response
        Vector2 thisExtent = new Vector2((thisMaxExtent.x - thisMinExtent.x)/2, (thisMaxExtent.y - thisMinExtent.y)/2);
        Vector2 otherExtent = new Vector2((otherMaxExtent.x - otherMinExtent.x)/2, (otherMaxExtent.y - otherMinExtent.y)/2);

        float xOverlap = thisExtent.x + otherExtent.x - Mathf.Abs(distance.x);
        float yOverlap = thisExtent.y + otherExtent.y - Mathf.Abs(distance.y);

        bool xTest = false;
        bool yTest = false;

        // (5)                                     (6)
        if(thisMaxExtent.x >= otherMinExtent.x && otherMaxExtent.x >= thisMinExtent.x)
        {
            // (7)
            xTest = true;
        }
        else
        {
            // (7)
            xTest = false;
        }


        // (8)                                     (9)
        if (thisMaxExtent.y >= otherMinExtent.y && otherMaxExtent.y >= thisMinExtent.y)
        {
            // (10)
            yTest = true;
        }
        else
        {
            // (10)
            yTest = false;
        }

        // (11)
        if (xTest && yTest)
        {


                c.contacts[0].point.x = Mathf.Max(thisMinExtent.x, otherMinExtent.x);
                c.contacts[0].point.y = Mathf.Max(thisMinExtent.y, otherMinExtent.y);

                c.contacts[1].point.x = Mathf.Min(thisMaxExtent.x, otherMaxExtent.x);
                c.contacts[1].point.y = Mathf.Min(thisMaxExtent.y, otherMaxExtent.y);



            if (xOverlap>yOverlap)
            {


                c.contacts[0].normal = distance.x < 0 ? new Vector2(1, 0) : new Vector2(-1, 0);
                c.contacts[1].normal = distance.x < 0 ? new Vector2(1, 0) : new Vector2(-1, 0);
                c.contacts[0].collisionDepth = xOverlap;
                c.contacts[1].collisionDepth = xOverlap;
            }
            else
            { 
                c.contacts[0].normal = distance.y < 0 ? new Vector2(0, 1) : new Vector2(0, -1);
                c.contacts[1].normal = distance.y < 0 ? new Vector2(0, 1) : new Vector2(0, -1);
                c.contacts[0].collisionDepth = yOverlap;
                c.contacts[1].collisionDepth = yOverlap;
            }

            c.contactCount = 2;

            return true;
        }
        else
        {
            return false;
        }
    }


    public override bool TestCollisionVsOBB(ObjectBoundingBox2D other, ref Collision c)
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
        

        BoxData box1;
        box1.pos = particle.position;
        box1.rotation = particle.rotation;
        box1.width = width;
        box1.height = height;

        BoxData box2;
        box2.pos = other.particle.position;
        box2.rotation = other.particle.rotation;
        box2.width = other.width;
        box2.height = other.height;

        bool check1 = checkBoundingBox(box1, box2);
        bool check2 = checkBoundingBox(box2, box1);

        if(check1 && check2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override bool TestCollisionVsCircle(CircleCollision other, ref Collision c)
    {

        //see circle

        // 1) Get Circle position 

        // 2) Get position of AABB collider

        // 3) Get max and min Extents box

        // 4) Get minimum and max x and y positions for clamping

        // 5) Clamp circle X position 

        // 6) Clamp circle Y position

        // 7) Create vector2 for closest point on rectangle with clamped position

        // 8) TEST : see if new vector2 is within circle radius

        // (1)
        Vector2 pos = other.particle.position;

        pos = pos - particle.position;

        Vector2 clampedPos = Vector2.zero;
        clampedPos.x = Mathf.Clamp(pos.x, -.5f * width, .5f * width);
        clampedPos.y = Mathf.Clamp(pos.y, -.5f * height, .5f * height);

        //  5. Compare clamped position against circles radius
        if ((pos - clampedPos).magnitude <= other.radius)
        {
            c.contacts[0].normal = clampedPos - pos;

            c.contacts[0].collisionDepth = c.contacts[0].normal.magnitude;

            c.contacts[0].point = other.particle.position - (pos.normalized * other.radius);

            c.contactCount = 1;

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

        Vector2 localPos = point - center;
        point.x = localPos.x * top.x + localPos.y * top.y;
        point.y = localPos.x * bottom.x + localPos.y * bottom.y;

        point += center;
        return point;
    }

    bool checkBoundingBox(BoxData box1, BoxData box2)
    {
        Vector2 thisPos = box1.pos;
        // 2) get other OBB position
        Vector2 otherPos = box2.pos;

        float thisRot = box1.rotation;

        Vector2 otherRotatedPos;
        if (box1.rotation != 0)
        {
            otherRotatedPos = rotateAroundPoint(otherPos, thisPos, -thisRot);
            otherRotatedPos -= thisPos;
        }
        else
        {
            otherRotatedPos = otherPos;
        }
        

        float otherRot = box2.rotation;
        otherRot -= thisRot;

        Vector2[] points = new Vector2[4];

        points[0] = new Vector2(-.5f * box2.width, -.5f * box2.height);
        points[1] = new Vector2(-.5f * box2.width, .5f * box2.height);
        points[2] = new Vector2(.5f * box2.width, -.5f * box2.height);
        points[3] = new Vector2(.5f * box2.width, .5f * box2.height);

        Quaternion newQuat = Quaternion.Euler(0, 0, otherRot);
        Matrix4x4 rotationMat = Matrix4x4.Rotate(newQuat);

        for (int i = 0; i < 4; i++)
        {
            //points[i] = rotateAroundPoint(points[i], new Vector2(0, 0), otherRot);
            points[i] = rotationMat.MultiplyPoint3x4(points[i]);
        }

        float otherMaxX = Mathf.Max(points[0].x, points[1].x, points[2].x, points[3].x) + otherRotatedPos.x;
        float otherMinX = Mathf.Min(points[0].x, points[1].x, points[2].x, points[3].x) + otherRotatedPos.x;
        float otherMaxY = Mathf.Max(points[0].y, points[1].y, points[2].y, points[3].y) + otherRotatedPos.y;
        float otherMinY = Mathf.Min(points[0].y, points[1].y, points[2].y, points[3].y) + otherRotatedPos.y;



        float thisMaxX = width * .5f;
        float thisMinX = -width * .5f;
        float thisMaxY = height * .5f;
        float thisMinY = -height * .5f;

        bool check1 = false;
        if ((thisMaxX > otherMinX && thisMaxY > otherMinY) && (otherMaxX > thisMinX && otherMaxY > thisMinY))
        {
            check1 = true;
        }

        return check1;
    }
}
