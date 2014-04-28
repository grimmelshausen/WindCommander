using UnityEngine;
using System.Collections.Generic;


/*
 * Segel bergen/streichen (einholen)
 */

public class FurlSails : MonoBehaviour {

	public bool sailsFurled;
    public List<Transform> sails;
    public ShipSailLutForce shipForce;


	// Use this for initialization
	void Start () {
		sailsFurled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
			ToggleFurlSails();
	}

	void ToggleFurlSails()
	{

		sailsFurled = !sailsFurled;
		if (sailsFurled)
            foreach(Transform t in sails)
			    t.renderer.material.SetFloat("_Cutoff", 1);
		else
            foreach(Transform t in sails)
			    t.renderer.material.SetFloat("_Cutoff", 0);

        shipForce.sailsFurled = sailsFurled;
	}

	
}
