using UnityEngine;

/// <summary>
/// Manages a door that moves the party somewhere and is maybe locked.
/// </summary>
public class Door : MonoBehaviour {

	/// <summary>
	/// Sprites that indicate locked state of the door.
	/// </summary>
	[SerializeField] private Sprite lockedSprite, unlockedSprite;

	/// <summary>
	/// If set to true, this door is locked.
	/// </summary>
	public bool locked = false;

	/// <summary>
	/// The place this door takes you to.
	/// </summary>
	[SerializeField] private Transform otherSide;

	/// <summary>
	/// Initialize this component.
	/// </summary>
	void Start () {
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer> ();
		if (spriteRenderer != null) {
			spriteRenderer.sprite = locked ? lockedSprite : unlockedSprite;
		}
	}

	/// <summary>
	/// Fires when a trigger enters this trigger.
	/// </summary>
	/// <param name="other">Other collider.</param>
	void OnTriggerEnter2D (Collider2D other) {
		Party party = other.GetComponent<Party> ();
		if (party != null) {
			Controls.Select.AddListener (() => OnPartyEnters (party));
		}
	}

	/// <summary>
	/// Fires when a trigger exits this trigger.
	/// </summary>
	/// <param name="other">Other collider.</param>
	void OnTriggerExit2D (Collider2D other) {
		Party party = other.GetComponent<Party> ();
		if (party != null) {
			Controls.Select.RemoveAllListeners ();
		}
	}

	/// <summary>
	/// When called, the party enters this door.
	/// </summary>
	private void OnPartyEnters (Party party) {
		if (!locked) {
			party.transform.position = otherSide.transform.position;
		}
	}
}
