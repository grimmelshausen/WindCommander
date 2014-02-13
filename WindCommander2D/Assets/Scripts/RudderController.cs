using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class RudderController : MonoBehaviour {


    public float parameter = 1.2f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        float input = -Input.GetAxis("Horizontal");


        // Calculate forces and apply to rigidbody
        Vector2 keelPerp = new Vector2(1, 0);
        Vector2 rudderPosition = new Vector2(0, -0.5f);
        float shipAngle = Mathf.Deg2Rad * transform.rotation.eulerAngles.z;
        Vector2 rudderDirection = new Vector2(
            keelPerp.x * Mathf.Cos(shipAngle) - keelPerp.y * Mathf.Sin(shipAngle),
            keelPerp.x * Mathf.Sin(shipAngle) + keelPerp.y * Mathf.Cos(shipAngle));
        rudderPosition = new Vector2(
            rudderPosition.x * Mathf.Cos(shipAngle) - rudderPosition.y * Mathf.Sin(shipAngle),
            rudderPosition.x * Mathf.Sin(shipAngle) + rudderPosition.y * Mathf.Cos(shipAngle));
        this.rigidbody2D.AddForceAtPosition((0.1f * input) * rudderDirection, new Vector2(transform.position.x, transform.position.y) + rudderPosition);
	}
}
