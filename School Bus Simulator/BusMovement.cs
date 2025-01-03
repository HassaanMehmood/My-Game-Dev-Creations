using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;
public class BusMovement : MonoBehaviour
{
    public float steering_rotation = 0.55f;
    public AudioClip HornAudio;
    public Text studentCountText;
    private int totalStudents = 0;

    // Flags to track if the bus is ready to load students
    private bool canLoadStudents = false; 
    private GameObject currentBusGround = null;
    private bool childrenExist = false; 
    float healthValue = 50;
    public Text healthText;

    void Start()
    {
        GetComponent<AudioSource>().clip = HornAudio;
        studentCountText.text = "Total Students: " + totalStudents.ToString();
        healthText.text = "Health: " + healthValue.ToString("F1");
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(0,0,0.22f);  // Move Forward
        }
        if(Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0,steering_rotation,0); // Right rotation
        }
        if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0,-steering_rotation,0); // Left rotation
        }
        if(Input.GetKey(KeyCode.S))
        {
            transform.Translate(0,0,-0.12f);  // Move Backword
        }
        if(Input.GetKey(KeyCode.H))
        {
        GetComponent<AudioSource>().PlayOneShot(HornAudio , 1.0f);
        }

        // Load the student in bus
        if (Input.GetKeyDown(KeyCode.Space) && canLoadStudents && currentBusGround != null)
        {
            // Loop to destroy all child objects of "BusGround"
            foreach (Transform child in currentBusGround.transform)
            {
                childrenExist = true;
                Destroy(child.gameObject);
            }
            if (childrenExist)
            {
            // Increment and update student count
            totalStudents += 10;
            studentCountText.text = "Total Students: " + totalStudents.ToString();
            }
            // Reset the flags and currentBusGround reference
            canLoadStudents = false;
            currentBusGround = null;
            childrenExist = false;
        }

        if (totalStudents == 30)
        {
           SceneManager.LoadScene("GameOver");
        }
        if (healthValue <= 0)
        {
           SceneManager.LoadScene("GameOver");
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name.StartsWith("BusGround"))
        {
            canLoadStudents = true; 
            currentBusGround = col.gameObject; // Keep reference to the collided BusGround object
        }
        if (col.gameObject.name.StartsWith("House"))
        {
            SceneManager.LoadScene("GameOver");
        }
        if (col.gameObject.name.StartsWith("Traffic"))
        {
            healthValue -= 10;
            healthText.text = "Health: " + healthValue.ToString("F1");
        }
    }
}

    // private void OnCollisionEnter(Collision col)
    // {
    // // Check if the collision is with the "BusGround" object
    // if (col.gameObject.name.StartsWith("BusGround"))
    // {
    //     // Get the "BusGround" object
    //     GameObject busGround = col.gameObject;
    //     // Loop through all child objects of "BusGround" and destroy them
    //     foreach (Transform child in busGround.transform)
    //     {
    //         Destroy(child.gameObject);
    //     }

    //     totalStudents += 5;
    //     studentCountText.text = "Total Students: " + totalStudents.ToString();
    // }
    // }
