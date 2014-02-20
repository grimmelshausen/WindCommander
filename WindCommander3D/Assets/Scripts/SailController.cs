using UnityEngine;
using System.Collections;

public class SailController : MonoBehaviour {

	[Range(1, 90)]
	public float maxMainSailAngle = 86;

	[Range(0.01f, 1)]
	public float mainSailMoveSpeed = 0.4f;
	float mainSailPercent;
	
	
	// Use ths for initialization
	void Start () {
		mainSailPercent = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		float input= -Input.GetAxis("Vertical");
		
		mainSailPercent += input * Time.deltaTime*mainSailMoveSpeed; //calculate percentage of rudder 0 to 1 (0 is all left, 1 is all right)
		mainSailPercent = Mathf.Clamp01(mainSailPercent); //make sure its not over 1 or below 0
		float r = Mathf.SmoothStep(-maxMainSailAngle, maxMainSailAngle, mainSailPercent);	//smoothly map 0 to 1 to -maxAngle to +maxAngle
		this.transform.localRotation = Quaternion.Euler(0, r, 0); //set rotation to hinge	
	}
}
