using UnityEngine;
using System.Collections;

public class WaterFrictionForce : MonoBehaviour {

	public float waterFriction = 1;

	Rigidbody r;

	// Use this for initialization
	void Start () {
		r = this.rigidbody;
	}
	
	// Update is called once per frame
	void FixedUpdate () {			
		/*
		 * This force is the backward drag of the water displacement. The faster the ship the F=~v^2 the drag.
		 * 
		 */ 
		
		
		//Drag
		float velocityMagnitude = r.velocity.sqrMagnitude;
		r.AddForce(-r.velocity.normalized*velocityMagnitude*waterFriction);
	}
}
