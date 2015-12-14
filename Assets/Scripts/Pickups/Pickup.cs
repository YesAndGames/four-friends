using UnityEngine;

/// <summary>
/// An abstract pickup item.
/// </summary>
public class Pickup : MonoBehaviour {

	/// <summary>
	/// Play this clip on pickup.
	/// </summary>
	[SerializeField] private AudioClip pickupSound;

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

		// Play the sound on the collider.
		if (pickupSound != null) {
			AudioSource source = other.GetComponentInChildren<AudioSource> ();
			if (source != null) {
				source.PlayOneShot (pickupSound);
			}
		}

		// Self-destruct.
		Destroy (gameObject);
	}
}
