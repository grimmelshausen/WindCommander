using UnityEngine; 
using System.Collections;

public class ShipRudderForce : MonoBehaviour {



	public float strength = 0.1f;

	public Transform rudderHinge;

    public Transform rudderForcePos;

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
		
		/*
		 * 12.04.2014: added a simplification: The rudder force is projected into the Y axis plane, so there is no force tiling/heeling the ship only turning. 
		 * This allows us to better it
		 * 
		 * old: Vector3 f = -strength * Vector3.Project(this.rigidbody.velocity, -rudderHinge.right);
		 */


		
		Vector3 f = -strength * Vector3.Project(this.rigidbody.velocity, -rudderHinge.right);
		f.y = 0; //project on the y plane
        Vector3 p = rudderForcePos.position;
		this.rigidbody.AddForceAtPosition(f, p);
		Debug.DrawLine(p, p + f);
	
	}
}
