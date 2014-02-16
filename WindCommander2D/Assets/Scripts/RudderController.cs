using UnityEngine;
using System.Collections;


public class RudderController : MonoBehaviour {

	public float maxRudderAngle = 60;
	public Rigidbody2D ship;
	float input;
	float rudderPercent;


	// Use this for initialization
	void Start () {
		rudderPercent = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {

        input = Input.GetAxis("Horizontal");

		//Rotate rudder sprite
		rudderPercent += input * Time.deltaTime; //calculate percentage of rudder 0 to 1 (0 is all left, 1 is all right)
		rudderPercent = Mathf.Clamp01(rudderPercent); //make sure its not over 1 or below 0
		float r = Mathf.SmoothStep(-maxRudderAngle, maxRudderAngle, rudderPercent);	//smoothly map 0 to 1 to -maxAngle to +maxAngle
		this.transform.localRotation = Quaternion.Euler(0, 0, r); //set rotation to hinge (SET Z FOR 2D, Y FOR 3D)

		// Maybe it makes sense to use the hinge rotation now to calculate forces?
	
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
		ship.AddForceAtPosition((0.1f * -input) * rudderDirection, new Vector2(transform.position.x, transform.position.y) + rudderPosition);
	}
}
