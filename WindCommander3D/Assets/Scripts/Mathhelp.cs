using UnityEngine;
using System.Collections;

public class Mathhelp {

	//Diese yeile ist neu auf buero
	public static Vector3 Clamp(Vector3 v, float min, float max)
	{
		return new Vector3(Mathf.Clamp(v.x, min, max),
		          		  Mathf.Clamp(v.y, min, max),
		          		  Mathf.Clamp(v.z, min, max)); //diese zeile ist modified auf buero
	}

	public static float AbsAngleY(Vector3 v1, Vector3 v2)
	{
		float a = (Quaternion.FromToRotation(v1, v2).eulerAngles.y);
		float b = (Quaternion.FromToRotation(v2, v1).eulerAngles.y);

		if (a < b)
			return a;
		else
			return b;
	}

	//CCW will be negative, CW will be positive
	public static float AngleY(Vector3 v1, Vector3 v2)
	{
		float a = (Quaternion.FromToRotation(v1, v2).eulerAngles.y);

		if(a > 180)
		{
			return a-360;
		}
		else
		{
			return a;
		}
	}


	//project vector on plane (given the plane's normal n)
	public static Vector3 ProjectOnPlane(Vector3 v1, Vector3 n)
	{
		return v1 - Vector3.Dot(v1, n) * n;
	}
}
