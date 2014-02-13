using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class SailController : MonoBehaviour {

    public WindController windController;
    public Transform sail;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


        float input = -Input.GetAxis("Vertical");
        UpdateRotation(input);


        // Calculate forces and apply to rigidbody
        float windAngle = Mathf.Deg2Rad * this.windController.transform.eulerAngles.z;
        Vector2 wind = new Vector2(Mathf.Cos(windAngle), Mathf.Sin(windAngle));
        Vector2 sailNormal = new Vector2(0, -1);
        Vector2 keel = new Vector2(0, -1);
        float sailAngle = Mathf.Deg2Rad * this.sail.rotation.eulerAngles.z;
        Vector2 sailDirection = new Vector2(
            sailNormal.x * Mathf.Cos(sailAngle) - sailNormal.y * Mathf.Sin(sailAngle),
            sailNormal.x * Mathf.Sin(sailAngle) + sailNormal.y * Mathf.Cos(sailAngle));

        float dragAngle = Mathf.Deg2Rad * (Vector2.Angle(wind, sailDirection));

        float shipAngle = Mathf.Deg2Rad * transform.rotation.eulerAngles.z;
        Vector2 keelDirection = new Vector2(
            keel.x * Mathf.Cos(shipAngle) - keel.y * Mathf.Sin(shipAngle),
            keel.x * Mathf.Sin(shipAngle) + keel.y * Mathf.Cos(shipAngle));

        float keelAngle = Mathf.Deg2Rad * Vector2.Angle(sailDirection, keelDirection);

        Vector2 force = Mathf.Cos(dragAngle) * Mathf.Cos(keelAngle) * new Vector2(Mathf.Cos(shipAngle - Mathf.PI / 2), Mathf.Sin(shipAngle - Mathf.PI / 2));


        this.rigidbody2D.AddForce(0.5f * force);
	}

    void UpdateRotation(float deltaAngle)
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
        this.sail.Rotate(new Vector3(0, 0, newMove));
    }
}
