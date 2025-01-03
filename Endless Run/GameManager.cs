using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public Button playAgainButton;

    int score = 0;

    public Text scoreText;

    private PlayerController playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        // Hide the Play Again button at the start
        playAgainButton.gameObject.SetActive(false);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void GameOver()
    {
        playAgainButton.gameObject.SetActive(true);
    }

    public void PlayAgain()
    {
        playAgainButton.gameObject.SetActive(false);
        SceneManager.LoadScene("Prototype 3"); // loads the scene of index 0
        playerControllerScript.gameOver = false;

        score = 0;
        UpdateScoreText();
    }

    public void AddScore(int value)
    {
        score += value;
        Debug.Log("Score = " + score);
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}
