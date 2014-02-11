using UnityEngine;
using System.Collections;

public class ShipControllerScript : MonoBehaviour {

	public GameObject windObject;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float moveHorizontal = -Input.GetAxis ("Horizontal");
		float moveVertical = -Input.GetAxis ("Vertical");
		Transform sail = transform.FindChild("Sail");

//		transform.Rotate(new Vector3(0,0,moveHorizontal));


		updateSailPosition(sail, moveVertical);
		applyKeel();
		applyDrag(sail);
		applyRudderForce(moveHorizontal);
//		applyAirfoil();
//		applyFriction();
	}

	void applyRudderForce(float amount)
	{
		Vector2 keelPerp = new Vector2(1, 0);
		Vector2 rudderPosition = new Vector2(0, -0.5f);
		float shipAngle = Mathf.Deg2Rad*transform.rotation.eulerAngles.z;
		Vector2 rudderDirection = new Vector2(
			keelPerp.x * Mathf.Cos(shipAngle) - keelPerp.y * Mathf.Sin(shipAngle),
			keelPerp.x * Mathf.Sin(shipAngle) + keelPerp.y * Mathf.Cos(shipAngle));
		rudderPosition = new Vector2(
			rudderPosition.x * Mathf.Cos(shipAngle) - rudderPosition.y * Mathf.Sin(shipAngle),
			rudderPosition.x * Mathf.Sin(shipAngle) + rudderPosition.y * Mathf.Cos(shipAngle));
		rigidbody2D.AddForceAtPosition((0.1f*amount)*rudderDirection, new Vector2(transform.position.x, transform.position.y)+rudderPosition);

	}

	void applyKeel()
	{
		Vector2 keel = new Vector2(-1, 0);
		Vector3 projection = Vector3.Project(new Vector3(rigidbody2D.velocity.x, rigidbody2D.velocity.y), new Vector3(keel.x, keel.y, 0));
		Vector2 keelForce = -(rigidbody2D.velocity-new Vector2(projection.x, projection.y));
		rigidbody2D.AddForce(2*keelForce);
	}

	void applyFriction()
	{
//		Vector2 keel = new Vector2(0, -1);
//		float shipAngle = Mathf.Deg2Rad*transform.rotation.eulerAngles.z;
//		Vector2 keelDirection = new Vector2(
//			keel.x * Mathf.Cos(shipAngle) - keel.y * Mathf.Sin(shipAngle),
//			keel.x * Mathf.Sin(shipAngle) + keel.y * Mathf.Cos(shipAngle));
	}

	void applyDrag(Transform sail)
	{
		float windAngle = Mathf.Deg2Rad*windObject.transform.eulerAngles.z;
		Vector2 wind = new Vector2(Mathf.Cos(windAngle), Mathf.Sin(windAngle));
		Vector2 sailNormal = new Vector2(0,-1);
		Vector2 keel = new Vector2(0, -1);
		float sailAngle = Mathf.Deg2Rad*sail.rotation.eulerAngles.z;
		Vector2 sailDirection = new Vector2(
			sailNormal.x * Mathf.Cos(sailAngle) - sailNormal.y * Mathf.Sin(sailAngle),
			sailNormal.x * Mathf.Sin(sailAngle) + sailNormal.y * Mathf.Cos(sailAngle));

		float dragAngle =  Mathf.Deg2Rad *(Vector2.Angle(wind, sailDirection));

		float shipAngle = Mathf.Deg2Rad*transform.rotation.eulerAngles.z;
		Vector2 keelDirection = new Vector2(
			keel.x * Mathf.Cos(shipAngle) - keel.y * Mathf.Sin(shipAngle),
			keel.x * Mathf.Sin(shipAngle) + keel.y * Mathf.Cos(shipAngle));

		float keelAngle = Mathf.Deg2Rad*Vector2.Angle(sailDirection, keelDirection);

		Vector2 force = Mathf.Cos(dragAngle) * Mathf.Cos(keelAngle) * new Vector2(Mathf.Cos(shipAngle-Mathf.PI/2), Mathf.Sin(shipAngle-Mathf.PI/2));


		rigidbody2D.AddForce(0.5f*force);
	}

	void applyAirfoil()
	{
	}

	void updateSailPosition(Transform sail, float deltaAngle)
	{
		float angle = sail.localRotation.eulerAngles.z;
		float destinationAngle = angle + deltaAngle;
		if (destinationAngle > 90 && destinationAngle < 180)
		{
			destinationAngle = 90;
		}
		if (destinationAngle < 270 && destinationAngle > 180)
		{
			destinationAngle = 270;
		}
		
		float newMove = destinationAngle - angle;
		sail.Rotate(new Vector3(0,0,newMove));
	}

}
