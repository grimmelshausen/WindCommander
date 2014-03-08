using UnityEngine;
using System.Collections;


/*
 * Segel bergen/streichen (einholen)
 */

public class FurlSails : MonoBehaviour {

	public bool sailsFurled;
	public Transform sails;


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
			sails.renderer.material.SetFloat("_Cutoff", 1);
		else
			sails.renderer.material.SetFloat("_Cutoff", 0);
	}

	
}
