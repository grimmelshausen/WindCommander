using UnityEngine;
using System.Collections;

public class ShipWindHullForce : MonoBehaviour {

    public float shipHullWindForceStrength = 0.01f;
    public WindController wind;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // The wind relative to the ship's velocity
        Vector3 relWind = wind.Wind() - this.rigidbody.velocity;

        this.rigidbody.AddForce(relWind * shipHullWindForceStrength);
	}
}
