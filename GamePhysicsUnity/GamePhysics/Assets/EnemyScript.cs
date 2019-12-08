using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    
    public Transform mCamera;

    [SerializeField]
    int totalHealth = 10;

    int currentHealth = 0;

    [SerializeField]
    SkinnedMeshRenderer thisMesh;
    bool hitStun;

    [SerializeField]
    float offset;
    Vector3 targetPosition;

    Vector3 offsetPosition;

    Particle3D thisParticle;

    bool onScreen = false;

    [SerializeField]
    GameObject EnemyBullet;

    [SerializeField]
    float shootTime;

    float currentTime;

    public Transform player;

    [SerializeField]
    int bulletCount;
    int currentBulletIndex;

    List<GameObject> bullet = new List<GameObject>();

    [SerializeField]
    GameObject scorePrefab;
    GameObject score;


    // Start is called before the first frame update
    void Start()
    {

        thisMesh.material.color = Color.white;
        thisParticle = GetComponent<Particle3D>();
      //  offsetPosition = transform.position;

        GetComponent<CollisionHull3D>().callMethod = OnCollisionEvent;
        for (int i = 0; i <= bulletCount; i++)
        {
            GameObject nBullet = (GameObject)Instantiate(EnemyBullet);
            bullet.Add(nBullet);
            nBullet.tag = "EnemyBullet";
            nBullet.GetComponent<MeshRenderer>().material.color = Color.red;
            nBullet.SetActive(false);
        }
        score = (GameObject)Instantiate(scorePrefab);
        score.SetActive(false);
    }

    public void SpawnEnemy()
    {
        currentHealth = totalHealth;
        thisParticle = GetComponent<Particle3D>();
        
        StartCoroutine( flyInScreen());
    }
    Vector3 newScreenPosition = new Vector3();

    Vector3 offScreenPosition = new Vector3();
    IEnumerator flyInScreen()
    {
        
        newScreenPosition.x = Random.Range(-20, 20);
        newScreenPosition.y = Random.Range(3, 20);
        newScreenPosition.z = offset;

        offScreenPosition.x = Random.Range(-20, 20);
        offScreenPosition.y = Random.Range(15, 20);
        offScreenPosition.z = Random.Range(30, 50);

        thisParticle.position = offScreenPosition + mCamera.position;

        offsetPosition = newScreenPosition;
        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForEndOfFrame();
            offsetCameraPosition.x = offsetPosition.x;
            offsetCameraPosition.z = mCamera.position.z;
            offsetCameraPosition.y = offsetPosition.y;
            targetPosition = offsetPosition + (offsetCameraPosition + mCamera.forward * offset);
            thisParticle.position = Vector3.Lerp(thisParticle.position, targetPosition, ((float)i)/50f);
            transform.position = thisParticle.position;
        }
        onScreen = true;
    }

    Vector3 offsetCameraPosition = new Vector3();
    // Update is called once per frame
    void Update()
    {
        if (!onScreen)
            return;
        offsetCameraPosition.x = offsetPosition.x;
        offsetCameraPosition.z = mCamera.position.z;
        offsetCameraPosition.y = offsetPosition.y;

        targetPosition = offsetPosition + (offsetCameraPosition + mCamera.forward * offset);

        thisParticle.position = targetPosition;

        if (hitStun)
            return;

        currentTime += Time.deltaTime;
        
        eyeColor.r = 1 - currentTime/shootTime;
        eyeColor.b = currentTime/shootTime;
        eyeColor.g = 1 - currentTime/shootTime;


        thisMesh.materials[0].color = eyeColor;

        if(currentTime >= shootTime)
        {
            currentTime = 0;

            currentBulletIndex = (currentBulletIndex + 1) % bulletCount;
            Vector3 dir = (player.position + mCamera.forward * 5f) - thisParticle.position;
            dir.Normalize();
            bullet[currentBulletIndex].SetActive(true);
            bullet[currentBulletIndex].transform.position = thisParticle.position + (dir * 10f);
            bullet[currentBulletIndex].GetComponent<Particle3D>().position = bullet[currentBulletIndex].transform.position;
            bullet[currentBulletIndex].GetComponent<Particle3D>().velocity = Vector3.zero;
            bullet[currentBulletIndex].GetComponent<Particle3D>().AddForce(dir * 1000f);
        }

    }
    Color eyeColor = Color.white;

    void OnCollisionEvent(CollisionHull3D col)
    {
        if (hitStun)
            return;
        if(col.tag == "PlayerBullet")
        {
            currentHealth--;
            if (currentHealth <= 0)
            {
                score.transform.position = thisParticle.position;
                score.GetComponent<Particle3D>().position = score.transform.position;
                score.GetComponent<Particle3D>().velocity = Vector3.zero;
                score.GetComponent<Particle3D>().AddForce(new Vector3(Random.Range(-1f, 1f), 5f, 0f));
                score.SetActive(true);
                gameObject.SetActive(false);
                ScoreManagerScript.Instance.score += 100;
                onScreen = false;
            }
            else
            {
            StartCoroutine(hitStunTime());

            }
        }
    }

    IEnumerator hitStunTime()
    {
        thisMesh.material.color = Color.red;
        hitStun = true;
        yield return new WaitForSecondsRealtime(0.5f);
        thisMesh.material.color = eyeColor;
        hitStun = false;
    }

}
