using UnityEngine;
using System.Collections;

public class ShipBuoyancyForce : MonoBehaviour {

	public float waterLiftStrength = 100;
	public float damper = 1f;
	public Transform buoyancyCenter;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		float y = buoyancyCenter.position.y;

		if (y < 0)  //only inside water
		{
			this.rigidbody.AddForce(Vector3.up * -y * waterLiftStrength); //push upwards (the water lift is not quadratic)
			this.rigidbody.AddForce(Vector3.up * - this.rigidbody.velocity.y * damper);//dampen the y-movement because bouncy bouncy bouncy
		}
	}
}
