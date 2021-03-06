﻿using UnityEngine;
using System.Collections;

public class RudderController : MonoBehaviour
{

    public enum RudderControlMode
    {
        Incremental,
        Flipper,
    }

    [Range(1, 90)]
    public float
        maxRudderAngle = 60;
    [Range(0.01f, 5)]
    public float
        rudderMoveSpeed = 0.2f;
    public RudderControlMode rudderControlMode;
    private float rudderPercent;

    //public string debugText = "empty";

    // Use this for initialization
    void Start()
    {
        rudderPercent = 0.5f;
    }
    
    // Update is called once per frame
    void Update()
    {
        float input = 0;
        if (Input.touchCount == 1)
        {
            Touch t = Input.GetTouch(0);
            // debugText = "Phase " + t.phase.ToString() + ", DeltaPos " + t.deltaPosition + ", FingerId " + t.fingerId + ", Pos " + t.position + ", TapCount" + t.tapCount;
            if (t.phase == TouchPhase.Moved)
            {
                input = -t.deltaPosition.x * 0.03f;
            }
        } else
        {
            input = -Input.GetAxis("Rudder");
        }

        if (this.rudderControlMode == RudderControlMode.Incremental)
        {
            rudderPercent += input * Time.deltaTime * rudderMoveSpeed; //calculate percentage of rudder 0 to 1 (0 is all left, 1 is all right)
        } else if (this.rudderControlMode == RudderControlMode.Flipper)
        {            
            if (input != 0) //if a key is pressed, slowly move to this position
            {
                rudderPercent = Mathf.Lerp(rudderPercent, input + 0.5f, Time.deltaTime * rudderMoveSpeed);
            } else // if key is released go to center very quickly
            {
                rudderPercent = Mathf.Lerp(rudderPercent, input + 0.5f, Time.deltaTime * rudderMoveSpeed*10);
            }
        }
           rudderPercent = Mathf.Clamp01(rudderPercent); //make sure its not over 1 or below 0
        float r = Mathf.SmoothStep(-maxRudderAngle, maxRudderAngle, rudderPercent); //smoothly map 0 to 1 to -maxAngle to +maxAngle
        this.transform.localRotation = Quaternion.Euler(0, r, 0); //set rotation to hinge   

    }
      


}

