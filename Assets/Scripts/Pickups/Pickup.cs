using UnityEngine;

/// <summary>
/// An abstract pickup item.
/// </summary>
public class Pickup : MonoBehaviour {

	/// <summary>
	/// Called when this trigger enters another trigger.
	/// </summary>
	void OnTriggerEnter2D (Collider2D other) {
		OnPickup (other);
	}

	/// <summary>
	/// Called when this item is picked up by the given other collider.
	/// </summary>
	/// <param name="other">Thing that picked this thing up.</param>
	protected virtual void OnPickup (Collider2D other) {

		// Self-destruct.
		Destroy (gameObject);
	}
}
