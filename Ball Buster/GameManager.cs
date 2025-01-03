using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    // public Text scoreText;
    public Button playAgainButton;
    void Start()
    {
        UpdateScoreText(); // Initialize the text fields with the starting values
        playAgainButton.gameObject.SetActive(false); // Hide the Play Again button at the start
    }
  
    public void AddScore(int value)
    {
        score += value;
        Debug.Log("Score = " + score);
        UpdateScoreText();
    }
    void UpdateScoreText()
    {
        // scoreText.text = "Score: " + score;
    }
    public void GameOver()
    {
        playAgainButton.gameObject.SetActive(true); // Show the Play Again button
        Time.timeScale = 0; // Pause the game
    }
    public void PlayAgain()
    {
        // Hide the Play Again button
        playAgainButton.gameObject.SetActive(false);
        // Resume the game
        Time.timeScale = 1;
        // Reset the game state
        // score = 0;
        UpdateScoreText();
        SceneManager.LoadScene(0);
    }
}