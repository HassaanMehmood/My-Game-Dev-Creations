using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    private int lives = 3;

    public Text scoreText;
    public Text liveText;
    public Button playAgainButton;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the text fields with the starting values
        UpdateScoreText();
        UpdateLivesText();
        playAgainButton.gameObject.SetActive(false); // Hide the Play Again button at the start
    }

    public void AddLives(int value)
    {
        lives += value;
        if (lives <= 0)
        {
            Debug.Log("Game Over");
            lives = 0;
            GameOver();
        }
        Debug.Log("Lives = " + lives);
        UpdateLivesText();
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

    void UpdateLivesText()
    {
        liveText.text = "Lives: " + lives;
    }

    void GameOver()
    {
        playAgainButton.gameObject.SetActive(true); // Show the Play Again button

        Time.timeScale = 0; // Pause the game
    }

    public void PlayAgain()
    {
        // Hide the Play Again button
        playAgainButton.gameObject.SetActive(false);

        // Reset the game state
        score = 0;
        lives = 3;
        UpdateScoreText();
        UpdateLivesText();

        // Resume the game
        Time.timeScale = 1;
    }
}
