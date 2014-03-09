using UnityEngine;
using System.Collections;

/*
 */

public class SailRopeController : MonoBehaviour {


	public float maxRopeLength = 80; // in degrees
	public float currentRopeLength = 0;
	public float ropeMoveSpeed = 0.1f;

	public float currentRopeLengthPercent;
	public WindController wind;
	public ShipController ship;


	public Vector3 targetRotEuler;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		// The wind relative to the ship's velocity
		Vector3 relWind = wind.Wind() - ship.rigidbody.velocity;


		/*d
		 * First calculate the length of the rope, change with the user input. similar to SailController.cs
		 */

		float input = Input.GetAxis("SailRope");
		currentRopeLengthPercent += input * Time.deltaTime*ropeMoveSpeed;
		currentRopeLengthPercent = Mathf.Clamp01(currentRopeLengthPercent); //make sure its not over 1 or below 0
		currentRopeLength = currentRopeLengthPercent * maxRopeLength; // convert from 0 to 1 to 0 to 80

		/*
		 * Now we know how long the rope is (in degrees), =currentRopeLength. Now we 
		 * need to figure out if the sail is on the right or left side of the ship
		 * Basically, we need to know if the wind blows into the right or left side of the sail
		 * Take the Y component of the crossproduct (sail direction, real wind dir), if it's positive, the wind blows from one side, from the otherside otherwise.
		 * 
		 */
		Vector3 cross = Vector3.Cross(this.transform.forward, relWind);

		Quaternion targetRot; //this is how the sail should be rotated (but due to animation the sail might not be there yet)

		if (cross.y >= 0)
		{
			targetRot = Quaternion.Euler(0, currentRopeLength, 0);
		}
		else
		{
			targetRot = Quaternion.Euler(0, -currentRopeLength, 0); 
		}
		targetRotEuler = targetRot.eulerAngles;
		this.transform.localRotation = Quaternion.Slerp(this.transform.localRotation, targetRot, Time.deltaTime*2); //now make nice interploation beween is and should be

	}
}
