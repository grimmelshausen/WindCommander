using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(ShipController))]
public class CannonController : MonoBehaviour {

	// The cannonball prefab
	public Rigidbody2D prefabCannonBall; 

	[Range(0, 10000)]
	public float velocity = 5000.0f;


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
        // Spawn new ball and destroy after 1 sec
		Rigidbody2D ballClone = (Rigidbody2D)Instantiate(prefabCannonBall, this.transform.position, this.transform.rotation);
		ballClone.AddForce(this.transform.forward * velocity);
		Destroy(ballClone, 1.0f);        
    }
}
