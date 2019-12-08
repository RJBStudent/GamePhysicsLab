using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManagerScript : MonoBehaviour
{

    public static ScoreManagerScript Instance;
    

    [SerializeField]
    Text scoreText;

    [SerializeField]
    Text liveText;

    [SerializeField]
    Text gameOverText;

    public int score = 0;

    public int currentLives = 3;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        gameOverText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        scoreText.text = "Score : " + score.ToString();


    }

    public void GameOver()
    {
        StartCoroutine(GameOverCourutine());
    }

    IEnumerator GameOverCourutine()
    {
        Time.timeScale = 0f;
        gameOverText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(5);
        SceneManager.LoadScene("StartScreen");
    }
}
