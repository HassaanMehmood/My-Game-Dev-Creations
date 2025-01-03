using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ReverseTrafficMovement : MonoBehaviour
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
      Vector3 carPos2 = transform.position;
      if(carPos2.z <= -120) // ending position of z: -120
      {
        carPos2.z = 120; // starting position of z: 120
        transform.position = carPos2; // update the car position again to initial position = 120 
      }
    }
}
