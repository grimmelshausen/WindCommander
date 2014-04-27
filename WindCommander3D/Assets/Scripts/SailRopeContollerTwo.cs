//using UnityEngine;
//using System.Collections;
//
///**
// * The rule is: Given B (vector in opposide boat direction), W (wind direction), S (sail direction): S must always be between B and W and not more than 90 deg from B away
// * 
// */ 
//public class SailRopeContollerTwo : MonoBehaviour {
//
//
//	private WindController wind;
//	public float angleSailWind;
//	public float angleShipWind;
//	public float angleSailShip;
//	public bool isInIrons;
//	public ShipController ship;
//
//	private float inIronsTolerance = 10;
//	public float inputRotSailDeg;
//	public float autoRotSailDeg;
//
//	public string msg;
//
//	// Use this for initialization
//	void Start () {
//        this.wind = GameObject.FindGameObjectWithTag("Wind").GetComponent<WindController>();
//	}
//
//	void OnGUI()
//	{
//		GUI.Label(new Rect(10,10, 100, 20), msg);
//	}
//	
//	// Update is called once per frame
//	void Update () {
//	
//	 float input = Input.GetAxis("SailRope")*5; //input>0 means ease off sheets, input<0 means shorten sails
//	
//		// The wind relative to the ship's velocity
//		Vector3 relWind = wind.Wind() - ship.rigidbody.velocity;
//		
//		
//		/*
//		 * Calculate angles between wind and sail and boat
//		 */
//		angleSailWind = Mathhelp.AngleY(this.transform.forward, relWind);//Mathhelp.MinAngleY(relWind, this.transform.forward);
//		angleShipWind = Mathhelp.AngleY(-ship.transform.forward, relWind);//Mathhelp.MinAngleY(relWind, this.transform.forward);
//		angleSailShip = Mathhelp.AngleY(-ship.transform.forward, this.transform.forward);
//
//		//Check if we are in irons
//		if (Mathf.Abs(angleSailWind) <= inIronsTolerance || Mathf.Abs(angleSailWind) >= 180 - inIronsTolerance)
//		{
//			isInIrons = true;
//		}
//		else
//		{
//			isInIrons = false;
//		}
//
//
//		/**
//		 * Process the input: The approach is not so nice. Idea: get the user input, then see go through all cases 
//		 * where the sail is relative to boat and wind and then restrict the input (sail is then 
//		 * only in one or the other direction movable)
//		 * 
//		 */ 
//		inputRotSailDeg = 0;
//		if (angleShipWind > 0) // wind is right side starboard of boat
//		{
//			inputRotSailDeg = input;
//		}
//		else // wind from left side port side of boat
//		{
//			inputRotSailDeg = -input;
//			/*
//			if (angleSailWind >= 10) //sail cannot rotate more than in irons
//			{
//				if (inputRotSailDeg < 0)
//					inputRotSailDeg = 0;
//			}
//
//			if (Mathf.Abs(angleSailShip) <= 5) //sail cannot rotate more than to the middle of the boat (fully shortened)
//			{
//				if (inputRotSailDeg > 0)
//					inputRotSailDeg = 0;
//			}
//
//			if (Mathf.Abs(angleSailShip) >= 90) //sail cannot rotate more than 90 deg away from ship (sheets eased fully off)
//			{
//				if (inputRotSailDeg > 0)
//					inputRotSailDeg = 0;
//			}*/
//
//
//		}
//		this.transform.Rotate(new Vector3(0, inputRotSailDeg, 0)); //rotate the sail with the corrected user input
//
//
//		/*
//		 * Final step: Move sail automatically if wind blows from bad direction. This happens if 
//		 * the wind or the boat changes direction
//		 */
//
//		autoRotSailDeg = 0;
//		
//		if (angleShipWind > 0) { // wind is right side starboard of boat
//
//
//		} else // wind from left side port side of boat 
//		{
//			if(angleSailWind > 0)
//				autoRotSailDeg = 1;
//		}
//		this.transform.Rotate(new Vector3(0, autoRotSailDeg, 0)); //rotate the sail with the corrected user input
//
//
//	}
//	
//}
