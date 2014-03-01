using UnityEngine;
using System.Collections;

public class ShipMainSailForce : MonoBehaviour {
	
	public WindController wind;
	public Transform mainSailHinge;
	public Transform windMainSailForcePos;

	public float mainSailDragStrength = 1f;
	public float mainSailLiftStrength = 1f;
	Rigidbody r;


	// Use this for initialization
	void Start () {
		this.r = this.rigidbody;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		/*
		 * The forces of the sail are twofold: A drag force perpendicular to the sail if the wind blows 
		 * perpendicular into the sail and a lift force (bernoulli) also perpendicular to the sail if 
		 * the wind blows across the sail (aligned) (but only if the sail is *filled with wind*)
		 */

        // The wind relative to the ship's velocity
		Vector3 relWind = wind.Wind() - r.velocity;

		//Drag force
		//Project rel wind onto sail.right. The force is not applied at the hinge but at windMainSailForcePos, so you can move it around to make it look nice (higher up will make the ship tilt more)
		Vector3 f = Vector3.Project(relWind, this.mainSailHinge.right)*this.mainSailDragStrength;
		Vector3 p = this.windMainSailForcePos.position;
		r.AddForceAtPosition(f, p);
		Debug.DrawLine(mainSailHinge.position, mainSailHinge.position+(f*3), Color.green, 0, false);        


		//Lift force
		// Project rel wind onto sail.forward for strength, but then apply force perpendicular
		//TODO: Add condition that for lift force, sail musst be *filled with wind*
		//TODO: Find correct term for *filled with wind*
		f = Vector3.Project(relWind, this.mainSailHinge.forward).magnitude*this.mainSailHinge.right*this.mainSailLiftStrength;
		r.AddForceAtPosition(f, p);
        Debug.DrawLine(mainSailHinge.position, mainSailHinge.position + f, Color.red, 0, false);

	}
}
