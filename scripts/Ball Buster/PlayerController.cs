using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 4f;

    public float verticalInput;

    public GameObject foaclPoint;

    public Rigidbody playerRb;

    public bool hasPowerup;

    private float powerupAbility = 12f;

    public GameObject powerupIndicator;

    public GameManager gameManagerScript;

    public AudioClip bumpAudio;

    private AudioSource playerAudio;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        playerRb = GetComponent<Rigidbody>();
        foaclPoint = GameObject.Find("Focal Point");

        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        
        // playerRb.AddForce(Vector3.forward * speed * verticalInput);
        playerRb.AddForce(foaclPoint.transform.forward * speed * verticalInput);

        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);


        if (transform.position.y < -10)
        {
            gameManagerScript.GameOver();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            powerupIndicator.gameObject.SetActive(true);
            StartCoroutine(PowerupCountDownRoutine());
        }
    }

    IEnumerator PowerupCountDownRoutine()
    {
        yield return new WaitForSeconds(7);

        hasPowerup = false;

        powerupIndicator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup )
        {

            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.transform.position - transform.position);

            playerAudio.PlayOneShot(bumpAudio, 1.0f);


            Debug.Log(" Collided with " + collision.gameObject.name + " with Powerup set to " + hasPowerup);
            enemyRb.AddForce(awayFromPlayer * powerupAbility , ForceMode.Impulse);
        }
        else if(collision.gameObject.CompareTag("Enemy"))
        {
            playerAudio.PlayOneShot(bumpAudio, 1.0f);
        }
    }

}
