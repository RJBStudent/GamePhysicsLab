using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollisionHull2D : MonoBehaviour
{

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

    public static bool TestCollision(CollisionHull2D a, CollisionHull2D b)
    {
        //see types and pick function
        bool collision = false;
        
        switch(b.type)
        {
            case HullType.AABB:
                {
                    AxisAlignedBoundingBox2D AABB = (AxisAlignedBoundingBox2D)b;
                     collision = a.TestCollisionVsAABB(AABB);

                    break;
                }
            case HullType.CIRCLE:
                {
                    CircleCollision CIRCLE = (CircleCollision)b;
                    collision=  a.TestCollisionVsCircle(CIRCLE);
                    break;
                }
            case HullType.OBB:
                {
                    ObjectBoundingBox2D OBB = (ObjectBoundingBox2D)b;
                     collision =  a.TestCollisionVsOBB(OBB);
                    break;
                }
            default:
                break;
        }
        
        return collision;
    }


    public abstract bool TestCollisionVsCircle(CircleCollision other);

    public abstract bool TestCollisionVsOBB(ObjectBoundingBox2D other);

    public abstract bool TestCollisionVsAABB(AxisAlignedBoundingBox2D other);
}
