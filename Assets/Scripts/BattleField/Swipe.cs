using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    
     Vector3 start;
     Vector3 end;
     float deltaX = 0f;
     float deltaY = 0f;
     [SerializeField]
     Camera mainCamera;

    void Update () {
        if (Input.GetMouseButtonDown (0)) {
            start = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp (0)) {
            end = Input.mousePosition;
            deltaX = (start - end).x;
             deltaY = (start - end).y;
        }
        mainCamera.transform.Translate (deltaX * Time.deltaTime, deltaY * Time.deltaTime, 0f);
        deltaX *= 0.95f;
        deltaY *= 0.95f;
    }

}
