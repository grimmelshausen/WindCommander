using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(ShipController))]
public class CannonController : MonoBehaviour {

	// The cannonball prefab
	public GameObject prefabCannonBall; 

	[Range(0, 500)]
	public float velocity = 120.0f;


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
		GameObject ballClone; 
        // Spawn new ball and destroy after 1 sec
		ballClone = (GameObject) Instantiate (prefabCannonBall, this.transform.position, this.transform.rotation);
		ballClone.rigidbody2D.AddForce(this.transform.up * velocity);
		GameObject.Destroy(ballClone, 2.0f);        
    }
}
