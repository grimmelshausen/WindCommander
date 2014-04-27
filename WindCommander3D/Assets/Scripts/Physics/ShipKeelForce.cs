using UnityEngine;
using System.Collections;

public class ShipKeelForce : MonoBehaviour {

	[Range(0, 50)]
	public float straightenUpStrengthZ = 2;
	[Range(0, 50)]
	public float straightenUpStrengthX= 2;
	[Range(0, 100)]
	public float sidewaysRetardationStrength = 10;
	//public Transform shipTop;
	//public Transform shipBottom;

	//Rigidbody r;

	// Use this for initialization
	void Start () {
		//r = this.rigidbody;
	}
	
	// Update is called once per frame
	void Update () {


		//OLD

		/*
		 * The keel does two things: First it almost removes the sideways movement and second it straightens up the ship
		 * 
		 * Sideways movement retardation: Just add a sidewards force quadratic to the sidewards speed, that is the velocity projected on ship.right
		 * 
		 * Straightening up: is just a force at the top of the ship pulling upwards and a force at the bottom of the ship pulling downwards. I think that should work.
		 */ 


		//Retardation
		//r.AddForce(-Vector3.Project(r.velocity, this.transform.right)*sidewaysRetardationStrength);
		//Debug.DrawLine(shipTop.position, shipTop.position - Vector3.Project(r.velocity, this.transform.right), Color.red);

		//Straightening up
		//float straightenUpForce = straightenUpStrength* Mathf.Abs(this.transform.rotation.eulerAngles.z) + 5;
		//r.AddForceAtPosition(straightenUpForce * Vector3.up, this.shipTop.position);
		//r.AddForceAtPosition(straightenUpForce * Vector3.down, this.shipBottom.position);
		//r.AddRelativeTorque(-r.angularVelocity.z * Vector3.forward *straightenUpStrength); //dampen the straighning up

		//And some hack++ The euler x rotation should always be 0 because the ship can relly not tilt forward or backwars
		//this.transform.rotation = Quaternion.Euler(0, this.transform.rotation.eulerAngles.y, this.transform.rotation.eulerAngles.z);



		//NEW
        /*
		 * Just slowly rotate the object into original rotation, keeping the y rotation
		 * 
		 */
        // straight up in Z dir
		Quaternion r = Quaternion.Slerp(this.transform.rotation, Quaternion.identity, Time.deltaTime*straightenUpStrengthZ);
		this.transform.rotation = Quaternion.Euler(new Vector3(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y, r.eulerAngles.z));

		// straight up in X dir
		//r = Quaternion.Slerp(this.transform.rotation, Quaternion.identity, Time.deltaTime*straightenUpStrengthX);
		//this.transform.rotation = Quaternion.Euler(new Vector3(r.eulerAngles.x, this.transform.rotation.eulerAngles.y, this.transform.rotation.eulerAngles.z));
		this.transform.rotation = Quaternion.Euler(0, this.transform.rotation.eulerAngles.y, this.transform.rotation.eulerAngles.z); //the hack works better

        //Retardation
        this.rigidbody.AddForce(-Vector3.Project(this.rigidbody.velocity, this.transform.right)*sidewaysRetardationStrength);
        //Debug.DrawLine(shipTop.position, shipTop.position - Vector3.Project(this.rigidbody.velocity, this.transform.right), Color.red);



	}
}
