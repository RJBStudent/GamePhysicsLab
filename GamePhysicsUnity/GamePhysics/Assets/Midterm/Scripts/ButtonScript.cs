using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{


    public void StartButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Tommy_GameScene");
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
