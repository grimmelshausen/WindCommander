using UnityEngine;


public class ShipCamera : MonoBehaviour
{

	public AnimationCurve distance;
	public AnimationCurve angle;

	Transform mTrans;

	void Start ()
	{
		mTrans = transform;
	}

	void Update ()
	{
		float speed = 0; //??

		Quaternion rot = Quaternion.Euler(angle.Evaluate(speed), 0f, 0f);
		mTrans.localPosition = rot * Vector3.back * distance.Evaluate(speed);
		mTrans.localRotation = rot;

	}
}