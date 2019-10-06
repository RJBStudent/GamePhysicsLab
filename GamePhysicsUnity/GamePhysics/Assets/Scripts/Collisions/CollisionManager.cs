using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class Created by RJ
public class CollisionManager : MonoBehaviour
{

    public static CollisionManager Instance;

    List<CollisionHull2D> collisionList;
    List<CollisionHull2D> thingsThatAreCollidingList;

    List<CollisionHull2D.Collision> currentCollisions;

  

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        collisionList = new List<CollisionHull2D>();
        thingsThatAreCollidingList = new List<CollisionHull2D>();
        currentCollisions = new List<CollisionHull2D.Collision>();

    }

    //Update is called once per frame
    void Update()
    {
        thingsThatAreCollidingList.Clear();

        foreach (CollisionHull2D firstCol in collisionList)
        {
            firstCol.colliding = false;
            foreach (CollisionHull2D secondCol in collisionList)
            {
                if (firstCol.gameObject != secondCol.gameObject)                
                {
                    CollisionHull2D.Collision newCol = new CollisionHull2D.Collision();
                    newCol.a = firstCol;
                    newCol.b = secondCol;
                    if (CollisionHull2D.TestCollision(firstCol, secondCol, ref newCol))
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

        foreach (CollisionHull2D firstCol in thingsThatAreCollidingList)
        {
            firstCol.colliding = true;
        }

        //ResolveCollisions();
    }

    public void AddCollision(CollisionHull2D newCol)
    {
        collisionList.Add(newCol);
    }

    public void RemoveCollision(CollisionHull2D newCol)
    {
        collisionList.Remove(newCol);
    }

    //TEMP GIZMO POSITION
    Vector3 draw;

    void ResolveCollisions()
    {
        foreach(CollisionHull2D.Collision col in currentCollisions)
        {
            //resove?
            //if not resolved keep in list else delete from list

            
            //calculate seperating velocity

            // Check whether there is a collision to solve in other words if it is stationary

            //calculate new seperating velcocity with restitution

            //get the change in the seperating velocities

            //Apply the change in velocity to each object proprotionate to inverse mass

            //add the two inverse masses together

            // impulse = change in seperating velocites / total inverse mass
            
            //the ammount of impulses per mass is the contact normal * impulse

            //new velocity = old velocity + impulses per mass * inverse mass;

            // the other particle goes om the inverse direction (negate inverse mass)

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        if(currentCollisions == null)
        {
            return;
        }
        foreach (CollisionHull2D.Collision col in currentCollisions)
        {
            draw = col.contacts[0].point;
            draw.z = 5;
            Gizmos.DrawSphere(draw, 0.1f);
        }
    }
}
