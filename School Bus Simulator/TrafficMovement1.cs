using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TrafficMovement1 : MonoBehaviour
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
      if(carPos.z >= 120) // ending position of z: 120
      {
        carPos.z = -93; // starting position of z: -120
        transform.position = carPos; // update the car position again to initial position = -120 
      }
    }
}
