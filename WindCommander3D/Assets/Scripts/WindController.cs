using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WindController : MonoBehaviour {
	
	public Transform windDirectionAndStrengthIndicator;

	// Use this for initialization
	void Start () {
        this.particleSystem.startSpeed = GetWind().magnitude;
	}

	public Vector3 GetWind()
	{
		return windDirectionAndStrengthIndicator.position  - this.transform.position;
	}

}
