using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce = 10f;
    public float gravityModifier;
    private bool isOnGround;
    public bool gameOver;

    private Animator playerAnim;

    public ParticleSystem explosionParticle;

    public ParticleSystem dustParticle;

    public ParticleSystem fireworksParticle;

    public AudioClip jumpSound;

    public AudioClip crashSound;

    public AudioClip moneySound;

    private AudioSource playerAudio;

    private GameManager gameManagerScript;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        Physics.gravity *= gravityModifier;

        playerAnim = GetComponent<Animator>();

        playerAudio = GetComponent<AudioSource>();

        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnim.SetTrigger("Jump_trig");
            isOnGround = false;
            dustParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("On Ground !");
            isOnGround = true;
            dustParticle.Play();

        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over !");
            gameOver = true;

            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);

            explosionParticle.Play();

            dustParticle.Stop();

            playerAudio.PlayOneShot(crashSound, 1.0f);

        }
        else if (collision.gameObject.CompareTag("Money"))
        {
            Debug.Log("Score Added");

            fireworksParticle.Play();

            playerAudio.PlayOneShot(moneySound, 1.0f);

            gameManagerScript.AddScore(5);

            Destroy(collision.gameObject);

        }
        if (gameOver)
        {
            gameManagerScript.GameOver();
        }
    }

 
}

