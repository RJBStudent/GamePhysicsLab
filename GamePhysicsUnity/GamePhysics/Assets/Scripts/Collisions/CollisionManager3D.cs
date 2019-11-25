using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class Created by RJ
public class CollisionManager3D : MonoBehaviour
{

    public static CollisionManager3D Instance;

    List<CollisionHull3D> collisionList;
    List<CollisionHull3D> thingsThatAreCollidingList;

    List<CollisionHull3D.Collision3D> currentCollisions;

  

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        collisionList = new List<CollisionHull3D>();
        thingsThatAreCollidingList = new List<CollisionHull3D>();
        currentCollisions = new List<CollisionHull3D.Collision3D>();

    }

    //Update is called once per frame
    void Update()
    {
        thingsThatAreCollidingList.Clear();

        foreach (CollisionHull3D firstCol in collisionList)
        {
            firstCol.colliding = false;
            foreach (CollisionHull3D secondCol in collisionList)
            {
                if (firstCol.gameObject != secondCol.gameObject)                
                {
                    CollisionHull3D.Collision3D newCol = new CollisionHull3D.Collision3D();
                    newCol.a = firstCol;
                    newCol.b = secondCol;
                    if (CollisionHull3D.TestCollision(firstCol, secondCol, ref newCol))
                    {
                        newCol.status = true;

                        //firstCol.colliding = true;
                        //secondCol.colliding = true;

                        currentCollisions.Add(newCol);

                        thingsThatAreCollidingList.Add(firstCol);
                        thingsThatAreCollidingList.Add(secondCol);
                    }
                    

                }
            }
        }

        foreach (CollisionHull3D firstCol in thingsThatAreCollidingList)
        {
            firstCol.colliding = true;
        }

        ResolveCollisions();
    }

    public void AddCollision(CollisionHull3D newCol)
    {
        if(!collisionList.Contains(newCol))
            collisionList.Add(newCol);
    }

    public void RemoveCollision(CollisionHull3D newCol)
    {

        if (collisionList.Contains(newCol))
            collisionList.Remove(newCol);


    }

    //TEMP GIZMO POSITION
    Vector3 draw;

    void ResolveCollisions()
    {
        foreach(CollisionHull3D.Collision3D col in currentCollisions)
        {
            //resove?
            //if not resolved keep in list else delete from list


            //calculate seperating velocity
            ContactResolve(col);
            //RestingContactResolution(col);
            ResolveInterpentration(col);


            if(col.a.generateCollisionEvent)
                col.a.CollisionDelegate(col.a.callMethod, col.b);
            //col.b.CollisionDelegate(col.b.callMethod, col.a);

        }
        currentCollisions.Clear();
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.white;
        if(currentCollisions == null)
        {
            return;
        }
        foreach (CollisionHull3D.Collision3D col in currentCollisions)
        {

            for(int i = 0; i < col.contactCount; i++)
            {
                    draw = col.contacts[i].point;
                    draw.z = -3;
                    Gizmos.DrawSphere(draw, 0.1f);
            }
        }

        currentCollisions.Clear();
    }

    void ContactResolve(CollisionHull3D.Collision3D col)
    {
        Vector3 relativeVelocity = col.a.particle.velocity - col.b.particle.velocity;
        float sepVelocity = Vector3.Cross(relativeVelocity, col.contacts[0].normal.normalized).magnitude;

        //// Check whether there is a collision to solve in other words if it is stationary


        ////calculate new seperating velcocity with restitution
        float newSepVelocity = -sepVelocity * col.contacts[0].restitution;

        ////get the change in the seperating velocities


        float deltaVelocity = newSepVelocity - sepVelocity;


        ////Apply the change in velocity to each object proprotionate to inverse mass

        ////add the two inverse masses together

        float totalInverseMass = (1 / col.a.particle.GetMass()) + (1 / col.b.particle.GetMass());


        //// impulse = change in seperating velocites / total inverse mass

        float impulse = deltaVelocity / totalInverseMass;

        ////the ammount of impulses per mass is the contact normal * impulse

        Vector3 impulsePerMass = col.contacts[0].normal * impulse;

        ////new velocity = old velocity + impulses per mass * inverse mass;

        col.a.particle.velocity = col.a.particle.velocity + impulsePerMass * -(1 / col.a.particle.GetMass());



        //// the other particle goes om the inverse direction (negate inverse mass)

        col.b.particle.velocity = col.b.particle.velocity + impulsePerMass * (1 / col.b.particle.GetMass());
    }

    void ResolveInterpentration(CollisionHull3D.Collision3D col)
    {
        Vector2 relativeVelocity = col.a.particle.velocity - col.b.particle.velocity;
        relativeVelocity *= col.contacts[0].normal.normalized;
        //
        //
        ////if there is no penetratione return
        if (col.contacts[0].collisionDepth <= 0)
        {
            return;
        }
        //
        float totalInverseMass = (1 / col.a.particle.GetMass()) + (1 / col.b.particle.GetMass());
        //
        if (totalInverseMass <= 0)
        {
            return;
        }
        //
        ////fond ammout of penetration per unity of inverse mass
        Vector3 movePerIMass = col.contacts[0].normal * (-col.contacts[0].collisionDepth / totalInverseMass);
        //
        ////Apply penetration resolution
        col.a.particle.position = col.a.particle.position + movePerIMass * (1 / col.a.particle.GetMass());
        //
        //
        col.b.particle.position = col.b.particle.position + movePerIMass * (1 / col.b.particle.GetMass());

    }

}
