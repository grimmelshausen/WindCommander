using UnityEngine;

[RequireComponent(typeof(ImprovedTrail))]
[AddComponentMenu("Exploration/Ship Trail")]
public class ShipTrail : MonoBehaviour
{
	public Transform playerShip;

	ImprovedTrail mTrail;

	void Start ()
	{
		mTrail = GetComponent<ImprovedTrail>();
	}

	void Update ()
	{

		mTrail.alpha = playerShip.rigidbody.velocity.magnitude;

	}
}