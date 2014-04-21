using UnityEngine;
using System.Collections;

public class SpeedMap : TogglableHelpGUI {

    public Vector3 offset;
    public Transform ship;
    private WindController wind;


	// Use this for initialization
	void Start () {
        this.wind = GameObject.FindGameObjectWithTag("Wind").GetComponent<WindController>();
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = ship.transform.position + offset;
        this.transform.rotation = wind.transform.rotation;
       //this.transform.Rotate(Vector3.up, 180); // 
       
	}
}
