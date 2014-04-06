using UnityEngine;
using System.Collections;

/**
 * The rule is: Given B (vector in opposide boat direction), W (wind direction), S (sail direction): S must always be between B and W and not more than 90 deg from B away
 * 
 */ 
public class SailRopeContollerTwo : MonoBehaviour {


	public WindController wind;
	public float angleSailWind;
	public float angleShipWind;
	public float angleSailShip;
	public bool isInIrons;
	public ShipController ship;

	private float inIronsTolerance = 10;
	public float rot;

	public string msg;

	// Use this for initialization
	void Start () {
	
	}

	void OnGUI()
	{
		GUI.Label(new Rect(10,10, 100, 20), msg);
	}
	
	// Update is called once per frame
	void Update () {
	
	 float input = Input.GetAxis("SailRope")*5; //input>0 means ease off sheets, input<0 means shorten sails
	
		// The wind relative to the ship's velocity
		Vector3 relWind = wind.Wind() - ship.rigidbody.velocity;
		
		
		/*
		 * Calculate angle between wind and sail
		 */
		angleSailWind = Mathhelp.AngleY(this.transform.forward, relWind);//Mathhelp.MinAngleY(relWind, this.transform.forward);
		angleShipWind = Mathhelp.AngleY(-ship.transform.forward, relWind);//Mathhelp.MinAngleY(relWind, this.transform.forward);
		angleSailShip = Mathhelp.AngleY(-ship.transform.forward, this.transform.forward);

		//Check if we are in irons
		if (Mathf.Abs(angleSailWind) <= inIronsTolerance || Mathf.Abs(angleSailWind) >= 180 - inIronsTolerance)
		{
			isInIrons = true;
		}
		else
		{
			isInIrons = false;
		}


		//Two cases: wind is right (starboard) or left (portbord) of boat
		rot = 0;
		if (angleShipWind > 0) // wind is right side
		{

		}
		else // wind from left side
		{
			rot = -input;
			if (angleSailWind >= 10)
			{
				if (rot < 0)
					rot = 0;
			}

			if (Mathf.Abs(angleSailShip) <= 5)
			{
				if (rot > 0)
					rot = 0;
			}
		}
		this.transform.Rotate(new Vector3(0, rot, 0));

	}
}
