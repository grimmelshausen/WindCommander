﻿using UnityEngine;
using System.Collections;

public class SailBlow : MonoBehaviour {

    public enum SailType
    {
        Forward,
        Sideward,
    };

    private WindController wind;
    public float blowStrength = 0.05f;
    public Transform ship;
    public SailType sailType = SailType.Forward;    
   
    public Vector3 zeroPos = new Vector3(0, 2.411292f, -0.7563128f);

    public SailRopeControllerThree sailRopeContoller;

	public Vector3 blow;


	// Use this for initialization
	void Start () {
        this.wind = GameObject.FindGameObjectWithTag("Wind").GetComponent<WindController>();
	}
	
	// Update is called once per frame
	void Update () {


        // The wind relative to the ship's velocity
        Vector3 relWind = wind.Wind() - ship.rigidbody.velocity;


        Vector3 dir = Vector3.zero;
        switch (sailType)
        {
            case SailType.Forward:
                {
                    dir = this.transform.forward; break;
                }
            case SailType.Sideward:
                {
                    dir = this.transform.right; break;
                }
        }
        if (!sailRopeContoller.isInIrons)
        {

            blow = Vector3.Project(relWind, dir) * blowStrength;
            blow = Quaternion.AngleAxis(-this.transform.rotation.eulerAngles.y, Vector3.up) * blow; //this is a hack, it rotates the vector by something. I have no fucking clue why this is needed and I found it by trial&error :p shit happens
            this.transform.localPosition = blow + zeroPos;    
        }
        else
        {
            blow = dir * blowStrength * Mathf.Sin(Time.fixedTime*60);
            blow = Quaternion.AngleAxis(-this.transform.rotation.eulerAngles.y, Vector3.up) * blow; //this is a hack, it rotates the vector by something. I have no fucking clue why this is needed and I found it by trial&error :p shit happens
            this.transform.localPosition = blow + zeroPos; 
        }


	}
}
