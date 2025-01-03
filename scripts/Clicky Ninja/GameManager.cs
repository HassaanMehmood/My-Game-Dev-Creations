using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting.Antlr3.Runtime;

public class GameManager : MonoBehaviour
{
    public List<GameObject> target1; // List
    private float spawnRate = 0.9f;
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public int lives;
    public TextMeshProUGUI livesText;


    public bool isGameActive;

    public Button restartButton;

    public GameObject titleScreen;

    public GameObject pauseScreen;
    public bool paused;

    public AudioSource clickAudio;

    public AudioClip clickSound;



    // Start is called before the first frame update
    void Start()
    {
        clickAudio = GetComponent<AudioSource>();
    }

    public void StartGame (int difficulty)
    {
            isGameActive = true;
            StartCoroutine(SpawnTarget());
            score = 0;
            UpdateScore(0);
            UpdateLives(3);
            spawnRate /= difficulty;
            titleScreen.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(true);
            livesText.gameObject.SetActive(true);
    }

    void ChangePaused()
    {
        if (!paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
  
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);

            Time.timeScale = 1;
        }
    }


    // Update is called once per frame
    void Update()
    {
        //Check if the user has pressed the P key
        if (Input.GetKeyDown(KeyCode.Tab) && isGameActive == true)
        {
            ChangePaused();
        }
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, target1.Count);
            Instantiate(target1[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
            score += scoreToAdd;
            scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int livesToChange)
    {
            lives += livesToChange;
            livesText.text = "Lives: " + lives;
            if (lives <= 0)
            {
                GameOver();
            }
    }
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
