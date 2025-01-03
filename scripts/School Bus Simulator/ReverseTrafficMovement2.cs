using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReverseTrafficMovement2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      transform.Translate(0,0,0.16f);
    
      // move the car from ending to starting position again
      Vector3 carPos = transform.position;
      if(carPos.x <= -120) // ending position of z: 80
      {
        carPos.x = 120; // starting position of z: -90
        transform.position = carPos; // update the car position again to initial position = -90 
      }
    }
}
