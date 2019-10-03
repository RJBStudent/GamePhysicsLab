using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollisionHull2D : MonoBehaviour
{
    public struct BoxData
    {
        public Vector2 pos;
        public float width;
        public float height;
        public float rotation;
    }

    public class Collision
    {       
        public struct Contact
        {
            Vector2 point;
            Vector2 normal;
            float restitution;
            float collisionDepth;
        }

        public CollisionHull2D a = null, b = null;
        public Contact[] contacts = new Contact[4];
        public int contactCount = 0;
        public bool status = false;
        public bool resovled = false;

        public Vector2 closingVelocity = Vector2.zero;
    }

    public enum HullType
    {
        CIRCLE,
        AABB,
        OBB
    }

    private HullType type { get; }

    protected CollisionHull2D(HullType t)
    {
        type = t;
    }

    public Particle particle;

    public bool colliding;


    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<Particle>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public static bool TestCollision(CollisionHull2D a, CollisionHull2D b, ref Collision c)
    {
        //see types and pick function
        bool collision = false;
        
        switch(b.type)
        {
            case HullType.AABB:
                {
                    AxisAlignedBoundingBox2D AABB = (AxisAlignedBoundingBox2D)b;
                     collision = a.TestCollisionVsAABB(AABB,ref c);

                }
                    break;
            case HullType.CIRCLE:
                {
                    CircleCollision CIRCLE = (CircleCollision)b;
                    collision=  a.TestCollisionVsCircle(CIRCLE, ref c);
                }
                    break;
            case HullType.OBB:
                {
                    ObjectBoundingBox2D OBB = (ObjectBoundingBox2D)b;
                     collision =  a.TestCollisionVsOBB(OBB, ref c);
                }
                    break;
            default:
                break;
        }
        
        return collision;
    }


    public abstract bool TestCollisionVsCircle(CircleCollision other, ref Collision c);

    public abstract bool TestCollisionVsOBB(ObjectBoundingBox2D other, ref Collision c);

    public abstract bool TestCollisionVsAABB(AxisAlignedBoundingBox2D other, ref Collision c);
}
