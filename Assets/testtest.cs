using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testtest : MonoBehaviour
{
    
  private Vector3 velocity = Vector3.zero;
    
  void Update()
  {
         transform.position = Vector3.SmoothDamp(transform.position, new Vector3(0,3,0),ref velocity, 1f);
  }
}
