using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class Created by RJ
public class CollisionManager : MonoBehaviour
{

    public static CollisionManager Instance;

    List<CollisionHull2D> collisionList;

  

  

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        collisionList = new List<CollisionHull2D>();

    }

    // Update is called once per frame
    void Update()
    {
        foreach (CollisionHull2D firstCol in collisionList)
        {
            foreach (CollisionHull2D secondCol in collisionList)
            {
                if(firstCol.gameObject == secondCol.gameObject)
                {
                    
                }
                else
                {
                    if(CollisionHull2D.TestCollision(firstCol, secondCol))
                    {
                        Debug.Log("true");
                        firstCol.colliding = true;
                    }
                    else
                    {
                      //  firstCol.colliding = false;
                    }

                }
            }
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
}
