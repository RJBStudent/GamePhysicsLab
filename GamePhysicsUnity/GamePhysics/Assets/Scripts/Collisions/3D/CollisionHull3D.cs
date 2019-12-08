using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollisionHull3D : MonoBehaviour
{
    public struct BoxData3D
    {
        public Vector3 pos;
        public Vector3 dimensions;
        public Quaternion rotation;
        public Particle3D particleRef;
        [Tooltip("Delete This")]
        public Transform transformRef;

    }

    public class Collision3D
    {       
        public struct Contact3D
        {
           public Vector3 point;
           public Vector3 normal;
           public float restitution;
           public float collisionDepth;
        }

        public CollisionHull3D a = null, b = null;
        public bool enter = false, exit = false, stay = false;
        public Contact3D[] contacts = new Contact3D[8];
        public int contactCount = 0;
        public bool status = false;
        public bool resolved = false;

        public Vector3 closingVelocity = Vector3.zero;
    }
    

    public enum HullType
    {
        SPHERE_3D,
        AABB_3D,
        OBB_3D
    }

    private HullType type { get; }

    protected CollisionHull3D(HullType t)
    {
        type = t;
    }

    public Particle3D particle;

    public Vector3 offset;

    public bool colliding;

    public delegate void CollisionEvent(CollisionHull3D col);
    public CollisionEvent callMethod;

    public bool generateCollisionEvent = true;
    public bool isStatic = false;

    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<Particle3D>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public static bool TestCollision(CollisionHull3D a, CollisionHull3D b, ref Collision3D c)
    {
        //see types and pick function
        bool collision = false;
        
        switch(b.type)
        {
            case HullType.AABB_3D:
                {
                    AxisAlignedBoundingBox3D AABB_3D = (AxisAlignedBoundingBox3D)b;
                     collision = a.TestCollisionVsAABB_3D(AABB_3D,ref c);

                }
                    break;
            case HullType.SPHERE_3D:
                {
                    SphereCollision3D SPHERE_3D = (SphereCollision3D)b;
                    collision=  a.TestCollisionVsSphere_3D(SPHERE_3D, ref c);
                }
                    break;
            case HullType.OBB_3D:
                {
                    ObjectBoundingBox3D OBB_3D = (ObjectBoundingBox3D)b;
                    collision =  a.TestCollisionVsOBB_3D(OBB_3D, ref c);
                }
                    break;
            default:
                break;
        }
        
        return collision;
    }


    public abstract bool TestCollisionVsSphere_3D(SphereCollision3D other, ref Collision3D c);

    public abstract bool TestCollisionVsOBB_3D(ObjectBoundingBox3D other, ref Collision3D c);

    public abstract bool TestCollisionVsAABB_3D(AxisAlignedBoundingBox3D other, ref Collision3D c);

    public void CollisionDelegate(CollisionEvent collisionEvent, CollisionHull3D col)
    {
        collisionEvent(col);
    }

    public abstract void AbstractCollisionEvent(CollisionHull3D col);

    public Vector3 CalculateClosingVelocity(Particle3D a, Particle3D b) 
    {
        //negate if opposite
        // closing velocity = (scalar position a - scalar position b) * (position a - position b)

        return (a.position.magnitude - b.position.magnitude) * Vector3.Scale(a.position, b.position);
        //return (a.position.magnitude - b.position.magnitude) * (a.position * b.position);
    }
}
