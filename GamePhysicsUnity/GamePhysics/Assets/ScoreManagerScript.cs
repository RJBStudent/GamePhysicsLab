using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        liveText.text = "Lives : ";
        for (int i = 0; i < currentLives; i++)
        {
            liveText.text = liveText.text + " O ";
        }

    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverText.gameObject.SetActive(true);
    }
}
