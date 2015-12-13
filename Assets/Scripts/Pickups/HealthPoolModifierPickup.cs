using UnityEngine;

/// <summary>
/// Health pool modifier pickup.
/// </summary>
public class HealthPoolModifierPickup : Pickup {

	/// <summary>
	/// Amount of health modified. Positive is healing, negative is damage.
	/// </summary>
	[SerializeField] private int modification;

	/// <summary>
	/// Called when this item is picked up by the given other collider.
	/// </summary>
	/// <param name="other">Thing that picked this thing up.</param>
	protected override void OnPickup (Collider2D other) {

		// Heal the party.
		Friend friend = other.GetComponent<Friend> ();
		if (friend != null) {
			friend.Party.HealParty (modification);
		}

		// Destroy self.
		base.OnPickup (other);
	}
}
