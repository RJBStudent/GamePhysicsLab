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

    public override bool TestCollisionVsAABB(AxisAlignedBoundingBox2D other)
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

        // (2)
        float otherWidth = other.width;
        float otherHeight = other.height;

        // (3)
        Vector2 thisMaxExtent = new Vector2(particle.position.x + (width / 2), particle.position.y + (width / 2));
        Vector2 otherMaxExtent = new Vector2(otherPos.x + (otherWidth / 2), otherPos.y  + (otherHeight/ 2));

        // (4)
        Vector2 thisMinExtent = new Vector2(particle.position.x - (width / 2), particle.position.y - (height/ 2));
        Vector2 otherMinExtent = new Vector2(otherPos.x - (otherWidth / 2), otherPos.y - (otherHeight/ 2));

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
        return (xTest && yTest);
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
        Vector2 AABBpos = particle.position;

        // (2) 
        Vector2 OBBpos = other.particle.position;

        // (3)
        Vector2 xNormal = new Vector2(Mathf.Cos(other.particle.rotation * Mathf.Deg2Rad), Mathf.Sin(other.particle.rotation * Mathf.Deg2Rad));

        // (4)
        Vector2 yNormal = new Vector2(-Mathf.Sin(other.particle.rotation * Mathf.Deg2Rad), Mathf.Cos(other.particle.rotation * Mathf.Deg2Rad));

        // (5)
        float OBBWidth = other.width;
        float OBBHeight = other.height;

        // (6/1)
        Vector2 OBBTopRight = new Vector2( + (OBBWidth/2), + (OBBHeight / 2));

        // (6/2)
        Vector2 OBBTopLeft = new Vector2(- (OBBWidth / 2), + (OBBHeight / 2));

        // (6/3)
        Vector2 OBBBottomRight = new Vector2(+ (OBBWidth / 2), - (OBBHeight / 2));

        // (6/4)
        Vector2 OBBBottomLeft = new Vector2(- (OBBWidth / 2), - (OBBHeight / 2));

        // (7)
        Quaternion newQuat = Quaternion.Euler(0, 0, -other.particle.rotation);
        Matrix4x4 rotationMat = Matrix4x4.Rotate(newQuat);

        OBBTopRight = rotationMat.MultiplyPoint3x4(OBBTopRight);
        OBBTopLeft = rotationMat.MultiplyPoint3x4(OBBTopLeft);
        OBBBottomRight = rotationMat.MultiplyPoint3x4(OBBBottomRight);
        OBBBottomLeft = rotationMat.MultiplyPoint3x4(OBBBottomLeft);

        
        // (8 - 11)

        float minX = Mathf.Infinity, maxX = 0, minY = Mathf.Infinity, maxY = 0;

        Vector2[] corners = new Vector2[] { OBBTopRight, OBBTopLeft, OBBBottomRight, OBBBottomLeft};
        
        for (int i = 0; i < corners.Length; i++)
        {
            // (8)
            if(corners[i].x < minX)
            {
                minX = corners[i].x;
            }

            // (9)
            if (corners[i].x > maxX)
            {
                maxX = corners[i].x;
            }

            // (10)
            if ( corners[i].y < minY )
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
        Vector2 maxOBBExtent = new Vector2(OBBpos.x + maxX,OBBpos.y +  maxY);

        // (13)
        Vector2 minOBBExtent = new Vector2(OBBpos.x + minX, OBBpos.y + minY);

        // (14)
        Vector2 maxAABBExtent = new Vector2(AABBpos.x + (width / 2), AABBpos.y + (height / 2));

        // (15)
        Vector2 minAABBExtent = new Vector2(AABBpos.x - (width / 2), AABBpos.y - (height / 2));


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
        if(xTest == false || yTest == false)
        {
            return false;
        }

        // (22)
        maxOBBExtent = new Vector2(other.width/2, other.height/2);
        minOBBExtent = new Vector2(-other.width/2, -other.height/2);

        // (22/1)
        Vector2 AABBTopRight = new Vector2((particle.position.x +(width / 2) - OBBpos.x), (particle.position.y + (height / 2)) - OBBpos.y);
        Vector2 AABBTopLeft = new Vector2((particle.position.x -(width / 2) - OBBpos.x), (particle.position.y + (height / 2)) - OBBpos.y);
        Vector2 AABBBottomRight = new Vector2((particle.position.x +(width / 2) - OBBpos.x), (particle.position.y - (height / 2)) - OBBpos.y);
        Vector2 AABBBottomLeft = new Vector2((particle.position.x -(width / 2) - OBBpos.x), (particle.position.y - (height / 2)) - OBBpos.y);


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

       corners = new Vector2[] { AABBTopRight, AABBTopLeft, AABBBottomRight, AABBBottomLeft};

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

    public override bool TestCollisionVsCircle(CircleCollision other)
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
        Vector2 otherPos = other.particle.position;

        // (2)
        Vector2 thisPos = particle.position;

       
        // (4)
        float maxX = thisPos.x + (width / 2);
        float minX = thisPos.x - (width / 2);

        float maxY = thisPos.y - (height / 2);
        float minY = thisPos.y - (height / 2);

        // (5)
        float xClosest = otherPos.x;
        xClosest = Mathf.Clamp(xClosest, minX, maxX);

        // (6)
        float yClosest = otherPos.y;
        yClosest = Mathf.Clamp(yClosest, minY, maxY);

        // (7)
        Vector2 closestPoint = new Vector2(xClosest, yClosest);

        // (8)
        if((thisPos - closestPoint).magnitude < other.radius)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

}
