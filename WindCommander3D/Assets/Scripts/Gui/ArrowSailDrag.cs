using UnityEngine;
using System.Collections;

public class ArrowSailDrag : MonoBehaviour {

	public Vector3 offset;
	public Transform ship;
	public ShipMainSailForce sailForce;

	public float normalScale = 9;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = ship.transform.position + offset;
		this.transform.rotation = Quaternion.FromToRotation(Vector3.back, sailForce.drag); //it's back because I fucked up the direction of the arrow in maya (it's backward, you guessed right. bravo.)
		this.transform.localScale = new Vector3(normalScale, normalScale, normalScale * sailForce.drag.magnitude*2);
	}
}
