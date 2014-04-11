using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WindController : MonoBehaviour {

	[Range(-10,10)]
	public float rotateSpeed = 1;


	public float speed = 4;

	// Use this for initialization
	void Start () {
        
	}

	void Update()
	{
		this.particleSystem.startSpeed = speed;
		this.transform.Rotate (0, rotateSpeed, 0);

		if (Input.GetKeyDown (KeyCode.O))
						this.transform.Rotate (0, 3, 0);

		if (Input.GetKeyDown (KeyCode.P))
						this.transform.Rotate (0, -3, 0);


	}

	public Vector3 Wind()
	{
		return this.transform.forward * speed;
	}
}
