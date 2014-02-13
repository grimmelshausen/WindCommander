using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(ShipController))]
public class CannonController : MonoBehaviour {

	// The cannonball prefab
	public Rigidbody prefabCannonBall; 

	[Range(0, 1000)]
	public float velocity = 500.0f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Fire1"))
        {
			FireCannon();
        }
	}



    void FireCannon()
    {
		Rigidbody ballClone = (Rigidbody)Instantiate(prefabCannonBall, this.transform.position, this.transform.rotation);
		ballClone.AddForce(this.transform.forward * velocity);
		Destroy(ballClone);
        
    }


}
