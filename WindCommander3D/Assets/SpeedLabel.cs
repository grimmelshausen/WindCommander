using UnityEngine;
using System.Collections;

//[RequireComponent UILabel]
public class SpeedLabel : MonoBehaviour {

	public Rigidbody ship;

	UILabel label;

	// Use this for initialization
	void Start () {
		this.label = this.gameObject.GetComponent<UILabel>();
	}
	
	// Update is called once per frame
	void Update () {
		this.label.text = "Speed " + Mathf.Round(ship.rigidbody.velocity.magnitude*10)/10;




	}
}
