using UnityEngine;
using System.Collections;

public class ShipBuoyancyForce : MonoBehaviour {

	public float waterLiftStrength = 100;
	public Transform buoyancyCenter;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Add water lift force
		float y = Mathf.Clamp(buoyancyCenter.position.y,-10, 0); //use only negative value of y position 
		//dampen the y-movement because bouncy bouncy bouncy
		this.rigidbody.AddForce(Vector3.up * y*y * waterLiftStrength); //make the force really strong
		this.rigidbody.AddForce(Vector3.up * - this.rigidbody.velocity.y * 1f); //dampen the y movement
	}
}
