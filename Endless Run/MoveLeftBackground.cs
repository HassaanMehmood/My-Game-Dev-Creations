using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftBackground : MonoBehaviour
{
    private float speed = 10f;

    public float leftBound = -10f;

    private PlayerController playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver == false)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if(transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
        if (transform.position.x < leftBound && gameObject.CompareTag("Money"))
        {
            Destroy(gameObject);
        }
    }
}