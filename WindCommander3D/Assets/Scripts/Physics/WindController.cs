using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WindController : MonoBehaviour {

	[Range(0,10)]
	public float windMagnitude = 1;

	// Use this for initialization
	void Start () {
        
	}

	void Update()
	{
		this.particleSystem.startSpeed = windMagnitude;

	}

	public Vector3 Wind()
	{
		return this.transform.forward * windMagnitude;
	}
}
