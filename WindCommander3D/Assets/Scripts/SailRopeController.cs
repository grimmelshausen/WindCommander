using UnityEngine;
using System.Collections;

/*
 */

public class SailRopeController : MonoBehaviour {


	public float maxRopeLength = 85; // in degrees
	public float targetRopeLength = 0;
	public float ropeMoveSpeed = 0.1f;

	public float targetRopeLengthPercent;
	private WindController wind;
	public ShipController ship;


	public Vector3 targetRotEuler;


	public float debugAngleSailWind;

	public bool debugIsInIrons  = false;

	private float inIronsTolerance = 10;

	public float debugInput;
	public bool debugWindBlowsFromRight= false;

	// Use this for initialization
	void Start () {
        this.wind = GameObject.FindGameObjectWithTag("Wind").GetComponent<WindController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		float input = Input.GetAxis("SailRope"); //input>0 means ease off sheets, input<0 means shorten sails



		// The wind relative to the ship's velocity
		Vector3 relWind = wind.Wind() - ship.rigidbody.velocity;


		/*
		 * Check if sail is in irons. If it is, don't calculate stuff
		 */
		debugAngleSailWind = 	(Quaternion.FromToRotation(relWind, -this.transform.forward).eulerAngles.y - 180);

		if (debugAngleSailWind < inIronsTolerance || debugAngleSailWind > 180 - inIronsTolerance)
			
		{
			debugIsInIrons = true;

		}
		else
		{
			debugIsInIrons = false;
		}


		/*
		 * 
		 * First calculate if wind is blowing. Now we 
		 * need to figure out if the sail is on the right or left side of the ship
		 * Basically, we need to know if the wind blows into the right or left side of the sail
		 * Take the Y component of the crossproduct (sail direction, real wind dir), if it's positive, the wind blows from one side, from the otherside otherwise
		 * */
		Vector3 cross = Vector3.Cross(this.transform.forward, relWind);



		if (cross.y >= 0) //wind blows from right
		{
			debugWindBlowsFromRight = true;
			if (debugAngleSailWind >= inIronsTolerance)
			{
				if (input < 0)
					input = 0;
			}

		}
		else
		{
			debugWindBlowsFromRight = false;
			if (debugAngleSailWind <= inIronsTolerance)
			{
				if (input > 0)
					input = 0;
			}
		}

		/*
		 *  calculate the length (in percent) of the rope, change with the user input. similar to SailController.cs
		 */
		targetRopeLengthPercent += input * Time.deltaTime*ropeMoveSpeed; //it can only change as fast as defined with ropeMoveSpeed
		targetRopeLengthPercent = Mathf.Clamp01(targetRopeLengthPercent); //make sure its not over 1 or below 0 
		targetRopeLength = targetRopeLengthPercent * maxRopeLength; // map from percent to actualy degrees, from 0 to 1 to 0 to 80

		/*
		 * Now we know how long the rope should be (in degrees), =targetRopeLength. 
		 */


		Quaternion targetRot; //this is how the sail should be rotated (but due to animation the sail might not be there yet)

		if (cross.y >= 0)
		{
			targetRot = Quaternion.Euler(0, targetRopeLength, 0);
		}
		else
		{
			targetRot = Quaternion.Euler(0, -targetRopeLength, 0); 
		}
		targetRotEuler = targetRot.eulerAngles;
		this.transform.localRotation = targetRot;//Quaternion.Slerp(this.transform.localRotation, targetRot, Time.deltaTime); //now make nice interploation beween is and should be

		debugInput = input;
	}
}
