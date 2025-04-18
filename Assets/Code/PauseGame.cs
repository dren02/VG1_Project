using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseGame : MonoBehaviour
{
    public TMP_Text pointsText;

    void Start()
    {
        gameObject.SetActive(false); 
    }

    public void Setup(int score)
    {
        gameObject.SetActive(true);
        pointsText.text = score.ToString() + " POINTS";
    }
    public void ResumeButton()
    {
        Time.timeScale = 1; // Unpause the game
        gameObject.SetActive(false);
    }

    public void ExitButton()
    {
        Time.timeScale = 1; // Unpause the game
        SceneManager.LoadScene("MenuScene");
    }
}