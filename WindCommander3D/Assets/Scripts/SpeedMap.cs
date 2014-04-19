using UnityEngine;
using System.Collections;

public class SpeedMap : TogglableHelpGUI {

    public Vector3 offset;
    public Transform ship;
    public WindController wind;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = ship.transform.position + offset;
        this.transform.rotation = wind.transform.rotation;
       //this.transform.Rotate(Vector3.up, 180); // 
       
	}
}
