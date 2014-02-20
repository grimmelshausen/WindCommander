using UnityEngine; 
using System.Collections;

public class ShipRudderForce : MonoBehaviour {

	public float strength = 0.1f;
	public WindController wind;
	public Transform rudderHinge;

	Rigidbody r;
	


	// Use this for initialization
	void Start () {
		r = this.rigidbody;
	}
	
	void FixedUpdate () {
	
		/*
		 * Rudder force
		 * 
		 * The ship's velocity is projected on the rudder.right vector multiplied with some parameter. This force is applied at the rudder hinge		
         */

		Vector3 f = -strength * Vector3.Project(r.velocity, -rudderHinge.right);
		Vector3 p = rudderHinge.position;
		r.AddForceAtPosition(f, p);
		Debug.DrawLine(p, p + f);
	}
}
