using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody playerRb;
    private float speed = 500;
    private float speedBoost = 800;

    private GameObject focalPoint;

    public bool hasPowerup;
    public GameObject powerupIndicator;
    public int powerUpDuration = 8;

    private float normalStrength = 8; 
    private float powerupStrength = 12; 

    public ParticleSystem boostParticle;

    public bool inMotion;

    private AudioSource playerAudio;

    public AudioClip bumpAudio;


    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        playerAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        PlayerMovement();
        BoostPlayer();

        // Set powerup indicator position to beneath player
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    void PlayerMovement()
    {
        // Check if there is vertical input
        if (Input.GetAxis("Vertical") != 0)
        {
            float verticalInput = Input.GetAxis("Vertical");
            playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * Time.deltaTime);
            inMotion = true;
        }
        else
        {
            inMotion = false; 
        }
    }

    void BoostPlayer()
    {
        if (Input.GetKey(KeyCode.Space) && inMotion == true)
        {
            boostParticle.Play();
            playerRb.AddForce(focalPoint.transform.forward * speedBoost * Time.deltaTime);
        }
    }


    // If Player collides with powerup, activate powerup
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            powerupIndicator.SetActive(true);
            StartCoroutine(PowerupCooldown());
        }
    }

    // Coroutine to count down powerup duration
    IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    // If Player collides with enemy
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = other.transform.position - transform.position;




            if (hasPowerup) // if have powerup hit enemy with powerup force
            {
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
                playerAudio.PlayOneShot(bumpAudio, 1.0f);
            }
            else // if no powerup, hit enemy with normal strength 
            {
                enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
                playerAudio.PlayOneShot(bumpAudio, 1.0f);

            }
        }
    }

}
