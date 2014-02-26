using UnityEngine;

[RequireComponent(typeof(GameShip))]
[AddComponentMenu("Exploration/Ship Controller")]
public class ShipController : MonoBehaviour
{


	public Vector3 vel;
	public float velMag;

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

	
	public float waterFrictionStrength = 0.1f;
	public float rudderStrength = 0.001f;
	public Transform rudderHinge;
	public float waterLiftStrength = 100;
	public float v;
	public Transform buoyancyCenter;

	void FixedUpdate()
	{
		this.vel = this.rigidbody.velocity;
		this.velMag = this.rigidbody.velocity.sqrMagnitude;

		/*
		//Add water friction
		Vector3	waterFriction = -(this.rigidbody.velocity*this.rigidbody.velocity.magnitude)*waterFrictionStrength;
		this.rigidbody.AddForce(waterFriction);
	*/
	
		//Add wind force
		//Add full force projected on forward of ship direction
		//this.rigidbody.AddForce(Vector3.Project(windController.GetWindDirection(), this.transform.forward));
		//Add a little bit of "drift" in true wind direction
		//this.rigidbody.AddForce(windController.GetWind());
		/*

		//Add torgue to the ship depending on speed and rudder
		float rudderAngle = rudderHinge.localRotation.eulerAngles.y;
		if (rudderAngle > 180)
			rudderAngle = rudderAngle - 360;
		this.rigidbody.AddTorque(-this.transform.up * rudderStrength * rudderAngle * this.rigidbody.velocity.magnitude);

		//this.rigidbody.angularDrag = 5;
*/

		
		//Add water lift force
		float y = Mathf.Clamp(buoyancyCenter.position.y,-10, 0); //use only negative value of y position 
		//dampen the y-movement because bouncy bouncy bouncy
		this.rigidbody.AddForce(Vector3.up * y*y * waterLiftStrength); //make the force really strong
		this.rigidbody.AddForce(Vector3.up * - this.rigidbody.velocity.y * 1f); //dampen the y movement
	/*

		//Add wind flattening force
		//this.rigidbody.AddRelativeTorque(this.transform.forward * -Vector3.Project(this.windController.GetWindDirection(), this.transform.right).magnitude*0.1f);


		//Add upward force
	


		//Sail force: Very simple
		//First calculate the strengh of the wind on the sail (project 
		*/

	}


    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 20), "Player ship speed " + this.rigidbody.velocity.magnitude);
        GUI.Label(new Rect(10, 30, 200, 20), "Player ship speed direction " + Vector3.AngleBetween(Vector3.forward, this.rigidbody.velocity.normalized));
    }

	/// <summary>
	/// Update the input values, calculate the speed and steering, and move the transform.
	/// </summary>



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






		/*
		// Being in shallow water immediately cancels forward-driving input
		if (shallowWater) mInput.y = 0f;
		float delta = Time.deltaTime;

		// Slowly decay the speed and steering values over time and make sharp turns slow down the ship.
		mTargetSpeed = Mathf.Lerp(mTargetSpeed, 0f, delta * (0.5f + Mathf.Abs(mTargetSteering)));
		mTargetSteering = Mathf.Lerp(mTargetSteering, 0f, delta * 3f);

		// Calculate the input-modified speed
		mTargetSpeed = shallowWater ? 0f : Mathf.Clamp01(mTargetSpeed + delta * mSensitivity.y * mInput.y);
		mSpeed = Mathf.Lerp(mSpeed, mTargetSpeed, Mathf.Clamp01(delta * (shallowWater ? 8f : 5f)));

		// Steering is affected by speed -- the slower the ship moves, the less maneuverable is the ship
		mTargetSteering = Mathf.Clamp(mTargetSteering + delta * mSensitivity.x * mInput.x * (0.1f + 0.9f * mSpeed), -1f, 1f);
		mSteering = Mathf.Lerp(mSteering, mTargetSteering, delta * 5f);

		// Move the ship
		mTrans.localRotation = mTrans.localRotation * Quaternion.Euler(0f, mSteering * delta * mStats.turningSpeed, 0f);
		mTrans.localPosition = mTrans.localPosition + mTrans.localRotation * Vector3.forward * (mSpeed * delta * mStats.movementSpeed);
		*/
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