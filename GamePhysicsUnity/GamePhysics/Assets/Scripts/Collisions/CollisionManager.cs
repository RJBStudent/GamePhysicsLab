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
    }

    public void AddCollision(CollisionHull2D newCol)
    {
        collisionList.Add(newCol);
    }

    public void RemoveCollision(CollisionHull2D newCol)
    {
        collisionList.Remove(newCol);
    }

    void ResolveCollisions()
    {
        foreach(CollisionHull2D.Collision col in currentCollisions)
        {
            //resove?
            //if not resolved keep in list else delete from list
        }
    }
}
