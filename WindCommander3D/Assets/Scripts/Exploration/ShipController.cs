﻿using UnityEngine;

[RequireComponent(typeof(GameShip))]
[AddComponentMenu("Exploration/Ship Controller")]
public class ShipController : MonoBehaviour
{


	//public Vector3 vel;
	//public float velMag;

	// Whether this ship is controlled by player input
	public bool controlledByInput = false;

	/// <summary>
	/// Raycast points used to determine if the ship has hit shallow water.
	/// </summary>

	public Transform[] raycastPoints;

	/// <summary>
	/// Mask to use when raycasting.
	/// </summary>

	public LayerMask raycastMask;

	// Left/right, acceleration
	Vector2 mInput = Vector2.zero;
	Vector2 mSensitivity = new Vector2(6f, 1f);

	float mSpeed = 0f;
	float mSteering = 0f;
	float mTargetSpeed = 0f;
	float mTargetSteering = 0f;

	Transform mTrans;
	GameShip mStats;
	Cannon[] mCannons;

	public WindController windController;


	/// <summary>
	/// For controlling the ship via external means (such as AI)
	/// </summary>

	public Vector2 input { get { return mInput; } set { mInput = value; } }

	/// <summary>
	/// Current speed (0-1 range)
	/// </summary>

	public float speed { get { return mSpeed; } }

	/// <summary>
	/// Current steering value (-1 to 1 range)
	/// </summary>

	public float steering { get { return mSteering; } }

	/// <summary>
	/// Helper function that finds the ship control script that contains the specified child in its transform hierarchy.
	/// </summary>

	static public ShipController Find (Transform trans)
	{
		while (trans != null)
		{
			ShipController ctrl = trans.GetComponent<ShipController>();
			if (ctrl != null) return ctrl;
			trans = trans.parent;
		}
		return null;
	}

	/// <summary>
	/// Cache the transform
	/// </summary>

	void Start ()
	{
		mTrans = transform;
		mStats = GetComponent<GameShip>();
		mCannons = GetComponentsInChildren<Cannon>();

        
	}


	void Update ()
	{
		// Update the input values if controlled by the player
		if (controlledByInput) UpdateInput();

		bool shallowWater = false;

		// Determine if the ship has hit shallow water
		if (raycastPoints != null)
		{
			foreach (Transform point in raycastPoints)
			{
				if (Physics.Raycast(point.position + Vector3.up * 10f, Vector3.down, 10f, raycastMask))
				{
					shallowWater = true;
					break;
				}
			}
		}

	}

	/// <summary>
	/// Update the input (used when ship is controlled by the player).
	/// </summary>

	void UpdateInput ()
	{
		mInput.y = Mathf.Clamp01(Input.GetAxis("Vertical"));
		mInput.x = Input.GetAxis("Horizontal");


		// Fire the cannons
		if (mCannons != null && (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.JoystickButton0)))
		{
			Vector3 dir = GameCamera.flatDirection;
			Vector3 left = mTrans.rotation * Vector3.left;
			Vector3 right = mTrans.rotation * Vector3.right;

			left.y = 0f;
			right.y = 0f;
			
			left.Normalize();
			right.Normalize();

			// Calculate the maximum firing range using the best available cannon
			float maxRange = 1f;

			foreach (Cannon cannon in mCannons)
			{
				float range = cannon.maxRange;
				if (range > maxRange) maxRange = range;
			}

			// Aim and fire the cannons on each side of the ship, force-firing if the camera is looking that way
			AimAndFire(left, maxRange, Vector3.Angle(dir, left) < 60f);
			AimAndFire(right, maxRange, Vector3.Angle(dir, right) < 60f);
		}
	}

	/// <summary>
	/// Aim and fire the cannons given the specified direction and maximum range.
	/// </summary>

	void AimAndFire (Vector3 dir, float maxRange, bool forceFire)
	{
		float distance = maxRange * 1.2f;
		GameUnit gu = GameUnit.Find(mStats, dir, distance, 60f);

		// If a unit was found, override the direction and angle
		if (gu != null)
		{
			dir = gu.transform.position - mTrans.position;
			distance = dir.magnitude;
			if (distance > 0f) dir *= 1.0f / distance;
			else distance = maxRange;
			
			// Fire in the target direction
			Fire(dir, distance);
		}
		else if (forceFire)
		{
			// No target found -- only fire if asked to
			Fire(dir, distance);
		}
	}

	/// <summary>
	/// Fire the ship's cannons in the specified direction.
	/// </summary>

	public void Fire (Vector3 dir, float distance)
	{
		if (mCannons != null)
		{
			foreach (Cannon cannon in mCannons)
			{
				cannon.Fire(dir, distance);
			}
		}
	}
}