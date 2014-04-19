using UnityEngine;
using System.Collections;

public class ArrowWind : TogglableHelpGUI {

	public Vector3 offset;
	public Transform ship;
	public WindController wind;


	float normalScale = 20;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = ship.transform.position + offset;
		this.transform.rotation = wind.transform.rotation;
		this.transform.Rotate(Vector3.up, 180); // Yup. Rotate 180 deg because the arrow looks backwards
		this.transform.localScale = new Vector3(normalScale, normalScale, normalScale * wind.speed / 4);

	}


}
