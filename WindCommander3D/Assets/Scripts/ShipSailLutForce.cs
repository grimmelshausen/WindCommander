using UnityEngine;
using System.Collections;

public class ShipSailLutForce : MonoBehaviour {

	public Transform sailHinge; //sail angle
	public Transform wind; //wind angle


	public int relWindAngle;
	public int sailAngle;

	public float strength = 1;
	public float sailDrive;

	// Use this for initialization
	void Start () {
	
	}

	/**
	 * Currently the wind strengh is ignored. We just need the rel wind angle and the sail angle
	 */ 
	void Update ()
	{


		relWindAngle = Mathf.Abs(Mathf.RoundToInt(Quaternion.FromToRotation(wind.forward, this.transform.forward).eulerAngles.y) - 180); //angle from wind forward to the boat forward
		sailAngle = Mathf.RoundToInt(Mathhelp.AbsAngleY(-this.transform.forward, this.sailHinge.transform.forward)); //angle from the boat back to the sail direction
		sailDrive = SpeedLUT.Instance.Speed(relWindAngle, sailAngle);
		this.rigidbody.AddForce(sailDrive * this.transform.forward * strength);

	}
}
