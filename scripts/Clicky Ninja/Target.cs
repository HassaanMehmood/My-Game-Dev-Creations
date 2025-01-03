using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;

    private float minSpeed = 9f;
    private float maxSpeed = 12f;
    private float maxToeque = 10f;
    private float xRange = 4f;
    private float ySpawnPos = -0.5f;
    public int scorePointValue;

    public ParticleSystem explosionParticle;

    private GameManager gameManagerScript;


    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();

        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = RandomSPawnPos();

        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
    }
    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(maxToeque, -maxToeque);
    }

    Vector3 RandomSPawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos, 0);
    }

    private void OnMouseDown()
    {
        if (gameManagerScript.isGameActive && gameManagerScript.paused == false)
        {
            gameManagerScript.clickAudio.PlayOneShot(gameManagerScript.clickSound, 1.0f);
            Destroy(gameObject);
            gameManagerScript.UpdateScore(scorePointValue);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        // so only good item when hit to senser then game over will appear
        if (!gameObject.CompareTag("Bad"))
        {
            gameManagerScript.UpdateLives(-1);

            if (gameManagerScript.lives == 0)
            {
                gameManagerScript.GameOver();
            }
        }
    }

}
