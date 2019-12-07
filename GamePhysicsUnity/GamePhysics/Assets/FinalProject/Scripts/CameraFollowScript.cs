using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{

    public Transform FollowTarget;
    public Vector3 FollowOffset;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = FollowTarget.position + FollowOffset;
        //transform.position = Vector3.Lerp(transform.position, FollowTarget.position + FollowOffset, 1);
    }
}
