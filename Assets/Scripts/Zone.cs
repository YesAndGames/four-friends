using UnityEngine;

/// <summary>
/// A zone impacts Friends when they enter it.
/// </summary>
public class Zone : MonoBehaviour {

	/// <summary>
	/// Amount of damage dealt to a Friend when they enter this Zone.
	/// </summary>
	[SerializeField] private int damageOnEnter = 0;

	/// <summary>
	/// Amount of force applied to the Party when a Friend takes damage from this Zone.
	/// </summary>
	[SerializeField] private float forceOnEnter = 0;

	/// <summary>
	/// Called when another trigger enters this one.
	/// </summary>
	/// <param name="other">Other collider.</param>
	void OnTriggerEnter2D (Collider2D other) {
		Friend friend = other.GetComponent<Friend> ();
		if (friend != null) {
			friend.GetComponent<HealthPool> ().Damage (damageOnEnter);
			Rigidbody2D body = friend.Party.GetComponent<Rigidbody2D> ();
			if (body != null) {
				body.AddForce ((body.transform.position - transform.position).normalized * forceOnEnter, ForceMode2D.Force);
			}
		}
	}
}
