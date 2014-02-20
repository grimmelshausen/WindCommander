using UnityEngine;
using System.Collections;

public class Mathhelp {

	public static Vector3 Clamp(Vector3 v, float min, float max)
	{
		return new Vector3(Mathf.Clamp(v.x, min, max),
		          		  Mathf.Clamp(v.y, min, max),
		          		  Mathf.Clamp(v.z, min, max));
	}
}
