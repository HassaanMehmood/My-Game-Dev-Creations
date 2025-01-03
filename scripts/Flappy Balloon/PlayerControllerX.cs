using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;
    public bool isOnGround;

    public float floatForce;
    private float gravityModifier = 1.5f;
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip jumpSound;

    private GameManagerX gameManagerXScript;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        playerRb.AddForce(Vector3.up * floatForce, ForceMode.Impulse);

        Physics.gravity = new Vector3(0, -9.81f, 0); // Reset gravity to default

        Physics.gravity *= gravityModifier;

        playerAudio = GetComponent<AudioSource>();

        gameManagerXScript = GameObject.Find("GameManagerX").GetComponent<GameManagerX>();
    }

    // Update is called once per frame
    void Update()
    {
        // While space is pressed and player is low enough, float up
        if (Input.GetKeyDown(KeyCode.Space) && !isOnGround &&!gameOver)
        {
            playerRb.AddForce(Vector3.up * floatForce, ForceMode.Impulse);
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
            gameManagerXScript.GameOver();
            
        }
        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Debug.Log("Money Added!");
            Destroy(other.gameObject);
            gameManagerXScript.AddScore(5);
        }
        // if player collides with ground, apply upward force once
        else if (other.gameObject.CompareTag("Ground") && !gameOver)
        {
            isOnGround = true;
            // ApplyUpwardForceOnce();

            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            gameManagerXScript.GameOver();
        }
    
    }

   /* private void ApplyUpwardForceOnce()
    {
        if (isOnGround)
        {
            playerRb.AddForce(Vector3.up * floatForce, ForceMode.Impulse);
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            isOnGround = false;
        }
    }*/
}
