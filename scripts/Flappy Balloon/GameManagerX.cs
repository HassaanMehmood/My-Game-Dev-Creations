using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class GameManagerX : MonoBehaviour
{
    public Button playAgainButton;

    int score = 0;

    public Text scoreText;

    private PlayerControllerX playerControllerXScript;
    // Start is called before the first frame update
    void Start()
    {
        // Hide the Play Again button at the start
        playAgainButton.gameObject.SetActive(false);
        playerControllerXScript = GameObject.Find("Player").GetComponent<PlayerControllerX>();
    }

    public void GameOver()
    {
        playAgainButton.gameObject.SetActive(true);
    }

    public void PlayAgain()
    {
        playAgainButton.gameObject.SetActive(false);
        SceneManager.LoadScene("Challenge 3"); // loads the scene of index 1
        playerControllerXScript.gameOver = false;

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
