using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    [SerializeField, Range(1, 10)]
    float xSpeed=1;
    [SerializeField, Range(1, 10)]
    float ySpeed=1;

    Vector2 playerForce;

    [SerializeField]
    Particle thisParticle;

    // Start is called before the first frame update
    void Start()
    {
        playerForce = new Vector2();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetInput();
        thisParticle.AddForce(playerForce);
    }

    void GetInput()
    {
        playerForce.x = Input.GetAxis("Horizontal") * xSpeed;
        playerForce.y = Input.GetAxis("Vertical") * ySpeed;
    }
}
