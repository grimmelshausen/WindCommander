using UnityEngine;
using System.Collections;

public class ShipKeelForce : MonoBehaviour {

	public float straightenUpStrength = 1;
	public float sidewaysRetardationStrength = 1;
	public Transform shipTop;
	public Transform shipBottom;

	Rigidbody r;

	// Use this for initialization
	void Start () {
		r = this.rigidbody;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		/*
		 * The keel does two things: First it almost removes the sideways movement and second it straightens up the ship
		 * 
		 * Sideways movement retardation: Just add a sidewards force quadratic to the sidewards speed, that is the velocity projected on ship.right
		 * 
		 * Straightening up: is just a force at the top of the ship pulling upwards and a force at the bottom of the ship pulling downwards. I think that should work.
		 */ 


		//Retardation
		r.AddForce(-Vector3.Project(r.velocity, this.transform.right)*sidewaysRetardationStrength);
		Debug.DrawLine(shipTop.position, shipTop.position - Vector3.Project(r.velocity, this.transform.right), Color.red);

		//Straightening up
		r.AddForceAtPosition(straightenUpStrength * Vector3.up, this.shipTop.position);
		r.AddForceAtPosition(straightenUpStrength * Vector3.down, this.shipBottom.position);

		//And some hack++ The euler x rotation should always be 0 because the ship can relly not tilt forward or backwars
		this.transform.rotation = Quaternion.Euler(0, this.transform.rotation.eulerAngles.y, this.transform.rotation.eulerAngles.z);

	}
}
