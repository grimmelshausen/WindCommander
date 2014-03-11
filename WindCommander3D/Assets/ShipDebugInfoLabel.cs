using UnityEngine;
using System.Collections;

public class ShipDebugInfoLabel : MonoBehaviour {

	public ShipMainSailForce mainSailForce;
	
	UILabel label;
	
	// Use this for initialization
	void Start () {
		this.label = this.gameObject.GetComponent<UILabel>();
	}
	
	// Update is called once per frame
	void Update () {
		this.label.text = "Forward lift: " + Mathf.Round(mainSailForce.liftForward.magnitude*100)/100 + 
			"\n Forward drag: " + + Mathf.Round(mainSailForce.dragForward.magnitude*100)/100;
	}
}
