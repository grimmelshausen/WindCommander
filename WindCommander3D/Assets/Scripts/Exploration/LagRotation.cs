using UnityEngine;

[AddComponentMenu("Exploration/Lag Rotation")]
public class LagRotation : MonoBehaviour
{
	public float speed = 1f;
	
    public Transform playerShip;

	Quaternion localRotation;
	Quaternion parentRotation;
	
	void Start()
	{
        localRotation = transform.localRotation;
        parentRotation = playerShip.rotation;
	}
	
	void LateUpdate()
	{
        parentRotation = Quaternion.Slerp(parentRotation, playerShip.rotation, Time.deltaTime * speed);
        transform.rotation = parentRotation * localRotation;
    }
}