using UnityEngine;
using System.Collections;
using System.Reflection;

public class ShipHeelingForce : MonoBehaviour {


    WindController wind;
    public Transform mainSailHinge;
    public float heelStrength = 1;
    public FurlSails furlSails;
	public Transform ship;

    public Transform heelForceTransform;



	// Use this for initialization
	void Start () {
        this.wind = GameObject.FindGameObjectWithTag("Wind").GetComponent<WindController>();

//		const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static;
//		FieldInfo[] fields = this.GetType().GetFields(flags);
//		foreach (FieldInfo fieldInfo in fields)
//		{
//			Debug.Log("Obj: " + this.name + ", Field: " + fieldInfo.Name);
//		}
	


	}
	
	// Update is called once per frame
    void FixedUpdate()
    {

        if (!furlSails.sailsFurled)
        {
            // The wind relative to the ship's velocity
            Vector3 relWind = wind.Wind() - this.rigidbody.velocity;
       
			Vector3 heelForce = Vector3.Project(relWind, this.mainSailHinge.right);
			Debug.DrawLine(heelForceTransform.position, heelForceTransform.position+heelForce, Color.red);
			heelForce = Vector3.Project(heelForce, ship.right);
			Debug.DrawLine(heelForceTransform.position, heelForceTransform.position+heelForce, Color.green);

            this.rigidbody.AddForceAtPosition(heelForce*heelStrength, heelForceTransform.position);
            
        }
	}
}
