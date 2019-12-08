using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisAlignedBoundingBox3D : CollisionHull3D
{

    public AxisAlignedBoundingBox3D() : base(CollisionHull3D.HullType.AABB_3D) { }

    [Range(0.1f, 100.0f)]
    public float width;
    [Range(0.1f, 100.0f)]
    public float height;
    [Range(0.1f, 100.0f)]
    public float depth;

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
        Vector3 otherPos = other.particle.position + other.offset;
        Vector3 thisPos = particle.position + offset;

        Vector3 distance = thisPos - otherPos;

        // (2)
        float otherWidth = other.width;
        float otherHeight = other.height;
        float otherDepth = other.depth;

        // (3)
        Vector3 thisMaxExtent = new Vector3(thisPos.x + (width / 2), thisPos.y + (height / 2), thisPos.z + (depth / 2));
        Vector3 otherMaxExtent = new Vector3(otherPos.x + (otherWidth / 2), otherPos.y + (otherHeight / 2), otherPos.z + (otherDepth / 2));


        // (4)
        Vector3 thisMinExtent = new Vector3(thisPos.x - (width / 2), thisPos.y - (height / 2), thisPos.z - (depth / 2));
        Vector3 otherMinExtent = new Vector3(otherPos.x - (otherWidth / 2), otherPos.y - (otherHeight / 2), otherPos.z - (otherDepth / 2));

        // lab 5 collision response
        Vector3 thisExtent = new Vector3((thisMaxExtent.x - thisMinExtent.x) / 2, (thisMaxExtent.y - thisMinExtent.y) / 2,  (thisMaxExtent.z - thisMinExtent.z) / 2);
        Vector3 otherExtent = new Vector3((otherMaxExtent.x - otherMinExtent.x) / 2, (otherMaxExtent.y - otherMinExtent.y) / 2, (otherMaxExtent.z - otherMinExtent.z) / 2);

        float xOverlap = thisExtent.x + otherExtent.x - Mathf.Abs(distance.x);
        float yOverlap = thisExtent.y + otherExtent.y - Mathf.Abs(distance.y);
        float zOverlap = thisExtent.z + otherExtent.z - Mathf.Abs(distance.z);

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


            c.contacts[0].point.x = Mathf.Max(thisMinExtent.x, otherMinExtent.x);
            c.contacts[0].point.y = Mathf.Max(thisMinExtent.y, otherMinExtent.y);
            c.contacts[0].point.z = Mathf.Max(thisMinExtent.z, otherMinExtent.z);
            
            
                c.contacts[1].point.x = Mathf.Min(thisMaxExtent.x, otherMaxExtent.x);
                c.contacts[1].point.y = Mathf.Min(thisMaxExtent.y, otherMaxExtent.y);
                c.contacts[1].point.z = Mathf.Min(thisMaxExtent.z, otherMaxExtent.z);





            if (xOverlap>yOverlap && xOverlap>zOverlap)
            {
                c.contacts[0].normal = distance.x < 0 ? new Vector3(-1, 0, 0) : new Vector3(1, 0, 0);
                c.contacts[1].normal = distance.x < 0 ? new Vector3(-1, 0, 0) : new Vector3(1, 0, 0);
                c.contacts[0].collisionDepth = xOverlap;
                c.contacts[1].collisionDepth = xOverlap;
            }
            else if(yOverlap > xOverlap && yOverlap > zOverlap)
            { 
                c.contacts[0].normal = distance.y < 0 ? new Vector3(0, -1, 0) : new Vector3(0, 1, 0);
                c.contacts[1].normal = distance.y < 0 ? new Vector3(0, -1, 0) : new Vector3(0, 1, 0);
                c.contacts[0].collisionDepth = yOverlap;
                c.contacts[1].collisionDepth = yOverlap;
            }
            else if (zOverlap > xOverlap && zOverlap > yOverlap)
            {
                c.contacts[0].normal = distance.z < 0 ? new Vector3(0, 0, -1) : new Vector3(0, 0, 1);
                c.contacts[1].normal = distance.z < 0 ? new Vector3(0, 0, -1) : new Vector3(0, 0, 1);
                c.contacts[0].collisionDepth = zOverlap;
                c.contacts[1].collisionDepth = zOverlap;
            }
            else
            {
                Debug.LogError("BREAKING");
            }


            c.contacts[0].restitution = 0.0001f;
            c.contacts[1].restitution = 0.0001f;
            c.contactCount = 1;
            

            return true;
        }
        else
        {
            return false;
        }
    }
    


    public override bool TestCollisionVsOBB_3D(ObjectBoundingBox3D other, ref Collision3D c)
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
        

        BoxData3D box1;
        box1.pos = particle.position;
        box1.rotation = particle.rotation;
        box1.dimensions.x = width;
        box1.dimensions.y = height;
        box1.dimensions.z = depth;
        box1.particleRef = particle;
        box1.transformRef= transform;

        BoxData3D box2;
        box2.pos = other.particle.position;
        box2.rotation = other.particle.rotation;
        box2.dimensions.x = other.width;
        box2.dimensions.y = other.height;
        box2.dimensions.z = other.depth;
        box2.particleRef = other.particle;
        box2.transformRef = other.transform;

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

    public override bool TestCollisionVsSphere_3D(SphereCollision3D other, ref Collision3D c)
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
        Vector3 pos = other.particle.position + other.offset;

        pos = pos - (particle.position + offset);

        Vector3 clampedPos = Vector2.zero;
        clampedPos.x = Mathf.Clamp(pos.x, -.5f * width, .5f * width);
        clampedPos.y = Mathf.Clamp(pos.y, -.5f * height, .5f * height);
        clampedPos.z = Mathf.Clamp(pos.z, -.5f * depth, .5f * depth);
            
        //  5. Compare clamped position against circles radius
        if ((pos - clampedPos).magnitude <= other.radius)
        {
            c.contacts[0].normal = clampedPos - pos;
            
            c.contacts[0].collisionDepth = c.contacts[0].normal.magnitude;
            c.contacts[0].normal.Normalize();
            c.contacts[0].point = (other.particle.position + other.offset) + (c.contacts[0].normal.normalized * other.radius);
            
            c.contacts[0].restitution = 0.0001f;

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

    Vector3 thisMaxExtent = new Vector3();
    Vector3 thisMinExtent = new Vector3();
    Vector3 otherMaxExtent = new Vector3();
    Vector3 otherMinExtent = new Vector3();

    Vector3[] points;
    List<Vector3> drawPoints = new List<Vector3>();
    List<Vector3> drawExtents = new List<Vector3>();

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
            //points[i] = box1.transformRef.worldToLocalMatrix.MultiplyPoint3x4(points[i]);

            drawPoints.Add(points[i]);
        }


        //Vector3 otherRotatedPos;
        //if (box1.rotation.eulerAngles.magnitude != 0)
        //{
        //    // otherRotatedPos = rotateAroundPoint(otherPos, thisPos, -thisRot);
        //    otherRotatedPos = box1.transformRef.worldToLocalMatrix.MultiplyPoint3x4(otherPos); 
        //    //otherRotatedPos -= thisPos;
        //}
        //else
        //{
        //    otherRotatedPos = otherPos;
        //}


        //float otherRot = box2.rotation;
        //otherRot -= thisRot;


        float otherMaxX = Mathf.Max(points[0].x, points[1].x, points[2].x, points[3].x, points[4].x, points[5].x, points[6].x, points[7].x);
        float otherMinX = Mathf.Min(points[0].x, points[1].x, points[2].x, points[3].x, points[4].x, points[5].x, points[6].x, points[7].x);
        float otherMaxY = Mathf.Max(points[0].y, points[1].y, points[2].y, points[3].y, points[4].y, points[5].y, points[6].y, points[7].y);
        float otherMinY = Mathf.Min(points[0].y, points[1].y, points[2].y, points[3].y, points[4].y, points[5].y, points[6].y, points[7].y);
        float otherMaxZ = Mathf.Max(points[0].z, points[1].z, points[2].z, points[3].z, points[4].z, points[5].z, points[6].z, points[7].z);
        float otherMinZ = Mathf.Min(points[0].z, points[1].z, points[2].z, points[3].z, points[4].z, points[5].z, points[6].z, points[7].z);


        otherMinExtent = new Vector3(otherMinX, otherMinY, otherMinZ);
        otherMaxExtent = new Vector3(otherMaxX, otherMaxY, otherMaxZ);
        drawExtents.Add(otherMinExtent);
        drawExtents.Add(otherMaxExtent);


        otherMinExtent = box1.transformRef.worldToLocalMatrix.MultiplyPoint3x4(otherMinExtent);
        otherMaxExtent = box1.transformRef.worldToLocalMatrix.MultiplyPoint3x4(otherMaxExtent);
        

        float thisMaxX = width * .5f;
        float thisMinX = -width * .5f;
        float thisMaxY = height * .5f;
        float thisMinY = -height * .5f;
        float thisMaxZ = depth * .5f;
        float thisMinZ = -depth * .5f;


        thisMinExtent = new Vector3(thisMinX, thisMinY,thisMinZ);
        thisMaxExtent= new Vector3(thisMaxX, thisMaxY,thisMaxZ);
        
        
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
        //return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        foreach( Vector3 v in drawPoints)
        {
            Gizmos.DrawSphere(v, 0.1f);
        }
        drawPoints.Clear();

        Gizmos.color = Color.red;
        foreach (Vector3 v in drawExtents)
        {
            Gizmos.DrawSphere(v, 0.2f);
        }
        drawExtents.Clear();
    }


    public override void AbstractCollisionEvent(CollisionHull3D col) { }
}
