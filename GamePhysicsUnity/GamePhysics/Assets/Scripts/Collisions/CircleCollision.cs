﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCollision : CollisionHull2D
{

    public CircleCollision() : base(CollisionHull2D.HullType.CIRCLE) { }

    [Range(0.0f, 100.0f)]
    public float radius;

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

    public override bool TestCollisionVsCircle(CircleCollision other, ref Collision c)
    {

        // Stepo 2
        //Collision if distance between centers is less or equal than sum of radii
        //optimized collision if (distance between centers) squared is less than or equal (the sum of radii) squared
       

        // 1) Get positions
        Vector2 thisPosition = particle.position;
        Vector2 otherPosition = other.particle.position;

        // 2) differnce of the two
        Vector2 distance = thisPosition - otherPosition;


        // 3) distance squared
        float distanceSq = Vector2.Dot(distance, distance);

        // 4) Sum of Radii
        float sumOfRadii = radius + other.radius;

        // 5) square the sum
        float sumSq = sumOfRadii * sumOfRadii;

        // 6) TEST: distanceSq <= sumSq

        if (distanceSq <= sumSq)
        {
            //Collision Depth
            c.contacts[0].collisionDepth = (other.radius + radius) - distance.magnitude;

            //Collision Normal 
            c.contacts[0].normal = distance / distance.magnitude;

            c.contacts[0].point = c.contacts[0].normal.normalized * other.radius + other.particle.position;

            c.contacts[0].restitution = 0.0001f;

            c.contactCount = 1;

            return true;            
        }

        return false;
    }

    public override bool TestCollisionVsAABB(AxisAlignedBoundingBox2D other, ref Collision c)
    {
        //Find the closest point to the circle on the box, take the center and the closest point
        // (done by clamping center of circle to be within box dimensions)
        // if closest point is within circle, pass (do point vs circle test)

        // 1) Get Circle position 

        // 2) Get position of AABB collider

        // 3) Get max and min Extents of X boxes

        // 4) Get minimum and max x and y positions for clamping

        // 5) Clamp circle X position 

        // 6) Clamp circle Y position

        // 7) Create vector2 for closest point on rectangle with clamped position

        // 8) TEST : see if new vector2 is within circle radius

        // (1)
        Vector2 pos = transform.position;

        pos = pos - other.particle.position;

        Vector2 clampedPos = Vector2.zero;
        clampedPos.x = Mathf.Clamp(pos.x, -.5f * other.width, .5f * other.width);
        clampedPos.y = Mathf.Clamp(pos.y, -.5f * other.height, .5f * other.height);

        //  5. Compare clamped position against circles radius
        if ((pos - clampedPos).magnitude <= radius)
        {
            c.contacts[0].normal = clampedPos - pos;

            c.contacts[0].collisionDepth = c.contacts[0].normal.magnitude;

            c.contacts[0].point = particle.position + (c.contacts[0].normal.normalized * radius);

            c.contactCount = 1;

            return true;
        }
        else
        {
            return false;
        }

    }

    public override bool TestCollisionVsOBB(ObjectBoundingBox2D other, ref Collision c)
    {
        Vector2 pos = transform.position;
        //  1. Get z-rotation of OBB
        float zRot = other.particle.rotation;
        //  2. Rotate OBB by -Z

        zRot *= Mathf.Deg2Rad;
        //  3. Transform circles position by mat2x2 {cos -sin;   sin cos} * {circlePos - boxPos}
        Vector2 top = new Vector2(Mathf.Cos(-zRot), -Mathf.Sin(-zRot));
        Vector2 bottom = new Vector2(Mathf.Sin(-zRot), Mathf.Cos(-zRot));

        Vector2 localPos = pos - other.particle.position;
        pos.x = localPos.x * top.x + localPos.y * top.y;
        pos.y = localPos.x * bottom.x + localPos.y * bottom.y;


        //  4. Clamp circle pos by the extents of the box

        Vector2 clampedPos = Vector2.zero;
        clampedPos.x = Mathf.Clamp(pos.x, -.5f * other.width, .5f * other.width);
        clampedPos.y = Mathf.Clamp(pos.y, -.5f * other.height, .5f * other.height);
        
        //  5. Compare clamped position against circles radius
        if ((pos - clampedPos).magnitude <= radius)
        {
            top = new Vector2(Mathf.Cos(-zRot), Mathf.Sin(-zRot));
            bottom = new Vector2(-Mathf.Sin(-zRot), Mathf.Cos(-zRot));


            c.contacts[0].normal = clampedPos - pos;

            c.contacts[0].normal.x = c.contacts[0].normal.x * top.x + c.contacts[0].normal.y * top.y;
            c.contacts[0].normal.y = c.contacts[0].normal.x * bottom.x + c.contacts[0].normal.y * bottom.y;

            c.contacts[0].collisionDepth = c.contacts[0].normal.magnitude;

            c.contacts[0].point = particle.position + (c.contacts[0].normal.normalized * radius);

            c.contactCount = 1;
            return true;
        }
        else
        {
            return false;
        }
    }



}
