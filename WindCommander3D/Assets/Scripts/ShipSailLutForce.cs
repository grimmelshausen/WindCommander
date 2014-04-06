using UnityEngine;
using System.Collections;

public class ShipSailLutForce : MonoBehaviour {

	public Transform sailHinge; //sail angle
	public Transform wind; //wind angle


	public int relWindAngle;
	public int sailAngle;

	// Use this for initialization
	void Start () {
	
	}

	/**
	 * Currently the wind strengh is ignored. We just need the rel wind angle and the sail angle
	 */ 
	void Update ()
	{


		relWindAngle = Mathf.Abs(Mathf.RoundToInt(Quaternion.FromToRotation(wind.forward, this.transform.forward).eulerAngles.y) - 180);

		//this.rigidbody.AddForce(SpeedLUT.Instance.Speed(windAngle, sailAngle);


	
	}
}
