using UnityEngine;


public class ShipHull : MonoBehaviour
{
	public GameShip gameShip;

	/// <summary>
	/// Cache the stats.
	/// </summary>

	void Start () {  }

	/// <summary>
	/// React to the ship being hit by cannon fire.
	/// </summary>

	void OnCollisionEnter (Collision col)
	{
		Cannonball cb = col.collider.GetComponent<Cannonball>();

		if (cb != null && cb.damage > 0f)
		{
			// Damage the hull
			float damage = gameShip.ApplyDamage(cb.damage, cb.owner);

			// Print the damage text over the hull
			if (damage > 0f) ScrollingCombatText.Print(gameObject, "-" + Mathf.RoundToInt(damage), Color.red);
		}
	}
}