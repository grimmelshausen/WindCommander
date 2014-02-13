using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class KeelController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        // Calculate forces and apply to rigidbody
        Vector2 keel = new Vector2(-1, 0);
        Vector3 projection = Vector3.Project(new Vector3(rigidbody2D.velocity.x, rigidbody2D.velocity.y), new Vector3(keel.x, keel.y, 0));
        Vector2 keelForce = -(rigidbody2D.velocity - new Vector2(projection.x, projection.y));
        this.rigidbody2D.AddForce(2 * keelForce);
	}
}
