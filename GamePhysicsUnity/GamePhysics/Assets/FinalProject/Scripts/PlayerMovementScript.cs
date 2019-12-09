using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Particle3D))]
public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField]
    float xSpeed;

    [SerializeField]
    float ySpeed;

    public Vector3 maxSpeeds;
    public Vector3 defaultForwardSpeed;

    public Vector3 reticleLengths;
    public float turnSpeed;


    [SerializeField]
    Particle3D thisParticle;


    float xInput = 0;
    float yInput = 0;
    bool spaceBar = false;

    Transform thisTransform;

    public Vector3 direction;

    public float StartingHealth = 10;
    public float CurrentHealth = 10;

    public Slider HealthBar;
    private float healthBarValue = 1;

    public bool canCollide = true;
    // Start is called before the first frame update
    void Start()
    {
        thisParticle = GetComponent<Particle3D>();
        thisTransform = GetComponent<Transform>();

        CurrentHealth = StartingHealth;

        GetComponent<CollisionHull3D>().callMethod = OnCollisionEvent;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();
        UpdatePosition();
        //ClampVelocity();
        UpdateRotation();
        UpdateUI();
    }

    void UpdateInput()
    {

        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        spaceBar = Input.GetButtonDown("Fire1");
        
    }

    void UpdatePosition()
    {
        //thisParticle.AddTorque(new Vector3(yInput, xInput ,0));
        thisParticle.SetRelativeVelocity(defaultForwardSpeed);

    }

    void ClampVelocity()
    {
        Vector3 vel = thisParticle.velocity;

        vel.z = Mathf.Clamp(vel.z, 0, maxSpeeds.z);

        thisParticle.velocity = vel;
    }

    void UpdateRotation()
    {
        if(xInput != 0 )
        {
            direction.x += xInput * turnSpeed;
        }
        else
        {
            direction.x = Mathf.Lerp(direction.x, 0, .1f);
        }
        if(yInput != 0)
        {
            direction.y -= yInput * turnSpeed;
        }
        else
        {
            direction.y = Mathf.Lerp(direction.y, 0, .1f);
        }
        


        direction.x = Mathf.Clamp(direction.x, -reticleLengths.x, reticleLengths.x);
        direction.y = Mathf.Clamp(direction.y, -reticleLengths.y, reticleLengths.y);
        direction.z = reticleLengths.z;

        thisParticle.rotation.SetLookRotation(direction);
    }

    void UpdateUI()
    {
        HealthBar.value = Mathf.Lerp(HealthBar.value, healthBarValue, .1f);
    }

    public void AddHealth(float value)
    {
        CurrentHealth += value;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, StartingHealth);

        healthBarValue = CurrentHealth / StartingHealth;

        if(CurrentHealth == 0)
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    void OnCollisionEvent(CollisionHull3D col)
    {
        if ((col.gameObject.tag == "Building"  || col.gameObject.tag == "EnemyBullet") && canCollide)
        {
            Debug.Log(col.gameObject.name);
            AddHealth(-1);
            StartCoroutine(WaitForCollisiion(3f));
        }
        
    }

    IEnumerator WaitForCollisiion(float time)
    {
        canCollide = false;
        yield return new WaitForSeconds(time);
        canCollide = true;
    }
}
