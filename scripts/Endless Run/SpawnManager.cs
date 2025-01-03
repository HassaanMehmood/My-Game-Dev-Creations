using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public GameObject coinPrefab;

    private Vector3 obstacleSpawnPosition = new Vector3(35,0,0);
    private Vector3 coinSpawnPosition = new Vector3(35, 3, 0);


    public float startDelay = 2f;
    public float repeatRate = 2f;

    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle" , startDelay, repeatRate);
        InvokeRepeating("SpawnCoin", startDelay, repeatRate);

        // Fetching Script
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void SpawnObstacle()
    {
        // Using Script bool variable here
        if (playerControllerScript.gameOver == false)
        {
            Instantiate(obstaclePrefab, obstacleSpawnPosition, obstaclePrefab.transform.rotation);
        }
    }
    void SpawnCoin()
    {
        // Using Script bool variable here
        if (playerControllerScript.gameOver == false)
        {
            Instantiate(coinPrefab, coinSpawnPosition, obstaclePrefab.transform.rotation);
        }
    }
}
