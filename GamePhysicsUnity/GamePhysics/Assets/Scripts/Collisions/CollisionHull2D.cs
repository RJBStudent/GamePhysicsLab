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
           public Vector2 point;
           public Vector2 normal;
           public float restitution;
           public float collisionDepth;
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


    public delegate void CollisionEvent(CollisionHull2D col);
    public CollisionEvent callMethod;


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

    public void CollisionDelegate(CollisionEvent collisionEvent, CollisionHull2D col)
    {
        collisionEvent(col);
    }

    public abstract void AbstractCollisionEvent(CollisionHull2D col);

    public Vector2 CalculateClosingVelocity(Particle a, Particle b) 
    {
        //negate if opposite
        // closing velocity = (scalar position a - scalar position b) * (position a - position b)

        return (a.position.magnitude - b.position.magnitude) * (a.position * b.position);
    }
}
