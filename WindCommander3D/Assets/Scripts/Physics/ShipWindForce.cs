using UnityEngine;
using System.Collections;

public class ShipWindForce : MonoBehaviour {


	public float windOnHullStrength = 0.5f;
	public Transform windShipForcePos;
	public WindController windController;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		/*
		 * This force is just the wind that blows on the ship, the hull, a bit the sails from whatever directions. It should be really smaller than the sail drag and lift
		 * The force is applied a bit above the hull so the ship is tilted a bit
		 * 
		 */ 

		this.rigidbody.AddForceAtPosition((windController.Wind() - this.rigidbody.velocity)*windOnHullStrength, windShipForcePos.position);

	}
}
