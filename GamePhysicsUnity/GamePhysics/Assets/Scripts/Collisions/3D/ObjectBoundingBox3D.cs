using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBoundingBox3D : CollisionHull3D
{
    [Range(0.1f, 100.0f)]
    public float width;
     [Range(0.1f, 100.0f)]
    public float height;
    [Range(0.1f, 100.0f)]
    public float depth;

    public ObjectBoundingBox3D() : base(CollisionHull3D.HullType.OBB_3D) { }

    MeshRenderer meshRen;

   
    
    // Start is called before the first frame update
    void Start()
    {
        if (callMethod == null)
        {
            callMethod = AbstractCollisionEvent;
        }

         CollisionManager3D.Instance.AddCollision(this);

        meshRen = GetComponent<MeshRenderer>();
    }


    private void OnEnable()
    {
        if (CollisionManager3D.Instance)
           CollisionManager3D.Instance.AddCollision(this);
    }

    private void OnDisable()
    {
         CollisionManager3D.Instance.RemoveCollision(this);
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
            meshRen.material.color = Color.yellow;
        }
        
    }

    public override bool TestCollisionVsAABB_3D(AxisAlignedBoundingBox3D other, ref Collision3D c)
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

        BoxData3D box1;
        box1.pos = particle.position;
        box1.rotation = particle.rotation;
        box1.dimensions.x = width;
        box1.dimensions.y = height;
        box1.dimensions.z = depth;
        box1.particleRef = particle;
        box1.transformRef = transform;

        BoxData3D box2;
        box2.pos = other.particle.position;
        box2.rotation = other.particle.rotation;
        box2.dimensions.x = other.width;
        box2.dimensions.y = other.height;
        box2.dimensions.z = other.depth;
        box2.particleRef = particle;
        box2.transformRef = transform;

        bool check1 = checkBoundingBox(box1, box2);
        bool check2 = checkBoundingBox(box2, box1);

        return (check1 && check2);
    }


    public override bool TestCollisionVsOBB_3D(ObjectBoundingBox3D other, ref Collision3D c)
    {
        //same as AABB-OBB part 2, twice

        // 1) Get this OBB position

        BoxData3D box1;
        box1.pos = particle.position;
        box1.rotation = particle.rotation;
        box1.dimensions.x = width;
        box1.dimensions.y = height;
        box1.dimensions.z = depth;
        box1.particleRef = particle;
        box1.transformRef = transform;

        BoxData3D box2;
        box2.pos = other.particle.position;
        box2.rotation = other.particle.rotation;
        box2.dimensions.x = other.width;
        box2.dimensions.y = other.height;
        box2.dimensions.z = other.depth;
        box2.particleRef = particle;
        box2.transformRef = transform;

        bool check1 = checkBoundingBox(box1, box2);
        bool check2 = checkBoundingBox(box2, box1);

        return (check1 && check2);

        // DO IT AGAIN!!

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

    }

    public override bool TestCollisionVsSphere_3D(SphereCollision3D other, ref Collision3D c)
    {
        Vector3 pos = other.transform.position;
        ////  1. Get z-rotation of OBB
        //float zRot = particle.rotation;
        ////  2. Rotate OBB by -Z
        //zRot *= Mathf.Deg2Rad;
        ////  3. Transform circles position by mat2x2 {cos -sin;   sin cos} * {circlePos - boxPos}
        //Vector2 top = new Vector2(Mathf.Cos(-zRot), -Mathf.Sin(-zRot));
        //Vector2 bottom = new Vector2(Mathf.Sin(-zRot), Mathf.Cos(-zRot));
        //
        //
        //Vector2 localPos = pos - particle.position;
        Vector3 posInOBBSpace = transform.worldToLocalMatrix.MultiplyPoint3x4(pos);

        //
        //pos.x = localPos.x * top.x + localPos.y * top.y;
        //pos.y = localPos.x * bottom.x + localPos.y * bottom.y;
        //
        ////  4. Clamp circle pos by the extents of the box
        //
        Vector3 clampedPos = Vector2.zero;
        clampedPos.x = Mathf.Clamp(pos.x, -.5f * width, .5f * width );
        clampedPos.y = Mathf.Clamp(pos.y, -.5f * height, .5f * height);
        clampedPos.z = Mathf.Clamp(pos.z, -.5f * depth, .5f * depth);
        //
        ////  5. Compare clamped position against circles radius
        if ((posInOBBSpace - clampedPos).magnitude <= other.radius)
        {
        //
        //    top = new Vector2(Mathf.Cos(-zRot), Mathf.Sin(-zRot));
        //    bottom = new Vector2(-Mathf.Sin(-zRot), Mathf.Cos(-zRot));
        //
        //
        //    c.contacts[0].normal = clampedPos - pos;
        //
        //    c.contacts[0].normal.x = c.contacts[0].normal.x * top.x + c.contacts[0].normal.y * top.y;
        //    c.contacts[0].normal.y = c.contacts[0].normal.x * bottom.x + c.contacts[0].normal.y * bottom.y;
        //
        //    c.contacts[0].collisionDepth = c.contacts[0].normal.magnitude;
        //
        //    c.contacts[0].point = other.particle.position + (c.contacts[0].normal.normalized * other.radius);
        //
        //    c.contactCount = 1;
            return true;
        }
        else
        { 
            return false;
        }

        //return false;
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

    Vector3 thisMaxExtent = new Vector3();
    Vector3 thisMinExtent = new Vector3();
    Vector3 otherMaxExtent = new Vector3();
    Vector3 otherMinExtent = new Vector3();

    Vector3[] points;

    bool checkBoundingBox(BoxData3D box1, BoxData3D box2)
    {
        Vector3 thisPos = box1.pos;
        //// 2) get other OBB position
        Vector3 otherPos = box2.pos;

        //float thisRot = box1.rotation;

        points = new Vector3[8];

        points[0] = .5f * box2.dimensions.x * box2.transformRef.right + .5f * box2.dimensions.y * box2.transformRef.up + .5f * box2.dimensions.z * box2.transformRef.forward + otherPos;
        points[1] = .5f * box2.dimensions.x * box2.transformRef.right + .5f * box2.dimensions.y * box2.transformRef.up - .5f * box2.dimensions.z * box2.transformRef.forward + otherPos;
        points[2] = .5f * box2.dimensions.x * box2.transformRef.right - .5f * box2.dimensions.y * box2.transformRef.up + .5f * box2.dimensions.z * box2.transformRef.forward + otherPos;
        points[3] = .5f * box2.dimensions.x * box2.transformRef.right - .5f * box2.dimensions.y * box2.transformRef.up - .5f * box2.dimensions.z * box2.transformRef.forward + otherPos;
        points[4] = -.5f * box2.dimensions.x * box2.transformRef.right + .5f * box2.dimensions.y * box2.transformRef.up + .5f * box2.dimensions.z * box2.transformRef.forward + otherPos;
        points[5] = -.5f * box2.dimensions.x * box2.transformRef.right + .5f * box2.dimensions.y * box2.transformRef.up - .5f * box2.dimensions.z * box2.transformRef.forward + otherPos;
        points[6] = -.5f * box2.dimensions.x * box2.transformRef.right - .5f * box2.dimensions.y * box2.transformRef.up + .5f * box2.dimensions.z * box2.transformRef.forward + otherPos;
        points[7] = -.5f * box2.dimensions.x * box2.transformRef.right - .5f * box2.dimensions.y * box2.transformRef.up - .5f * box2.dimensions.z * box2.transformRef.forward + otherPos;

        for (int i = 0; i < points.Length; i++)
        {
            //points[i] = rotateAroundPoint(points[i], new Vector2(0, 0), otherRot);
           // points[i] = box1.transformRef.worldToLocalMatrix.MultiplyPoint3x4(points[i]);
        }


        float otherMaxX = Mathf.Max(points[0].x, points[1].x, points[2].x, points[3].x, points[4].x, points[5].x, points[6].x, points[7].x);
        float otherMinX = Mathf.Min(points[0].x, points[1].x, points[2].x, points[3].x, points[4].x, points[5].x, points[6].x, points[7].x);
        float otherMaxY = Mathf.Max(points[0].y, points[1].y, points[2].y, points[3].y, points[4].y, points[5].y, points[6].y, points[7].y);
        float otherMinY = Mathf.Min(points[0].y, points[1].y, points[2].y, points[3].y, points[4].y, points[5].y, points[6].y, points[7].y);
        float otherMaxZ = Mathf.Max(points[0].z, points[1].z, points[2].z, points[3].z, points[4].z, points[5].z, points[6].z, points[7].z);
        float otherMinZ = Mathf.Min(points[0].z, points[1].z, points[2].z, points[3].z, points[4].z, points[5].z, points[6].z, points[7].z);


        otherMinExtent = box1.transformRef.worldToLocalMatrix.MultiplyPoint3x4(new Vector3(otherMinX, otherMinY, otherMinZ));
        otherMaxExtent = box1.transformRef.worldToLocalMatrix.MultiplyPoint3x4(new Vector3(otherMaxX, otherMaxY, otherMaxZ));
        //Debug.Log(box2.transformRef.gameObject + " " + otherMaxX + " " + otherMinX + " " + otherMinY + " " + otherMaxY + " " + otherMinZ + " " + otherMaxZ);

        float thisMaxX = width * .5f;
        float thisMinX = -width * .5f;
        float thisMaxY = height * .5f;
        float thisMinY = -height * .5f;
        float thisMaxZ = depth * .5f;
        float thisMinZ = -depth * .5f;



        thisMinExtent = new Vector3(thisMinX, thisMinY, thisMinZ);
        thisMaxExtent = new Vector3(thisMaxX, thisMaxY, thisMaxZ);

        bool xTest = false;
        bool yTest = false;
        bool zTest = false;

        // (5)                                     (6)
        if (thisMaxExtent.x >= otherMinExtent.x && otherMaxExtent.x >= thisMinExtent.x)
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

        if (thisMaxExtent.z >= otherMinExtent.z && otherMaxExtent.z >= thisMinExtent.z)
        {
            // (10)
            zTest = true;
        }
        else
        {
            // (10)
            zTest = false;
        }

        // (11)
        if (xTest && yTest && zTest)
        {

            return true;
        }
        else
        {
            return false;
        }
        
    }


    public override void AbstractCollisionEvent(CollisionHull3D col) { }

}
