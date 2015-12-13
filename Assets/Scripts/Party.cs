using UnityEngine;

/// <summary>
/// Manages controls that affect the whole party.
/// </summary>
public class Party : MonoBehaviour {

	/// <summary>
	/// References to each of the party members.
	/// </summary>
	[SerializeField] private Friend northFriend, eastFriend, southFriend, westFriend;

	/// <summary>
	/// The distance that the party spreads out.
	/// </summary>
	[SerializeField] private float partySpread = 1f;

	/// <summary>
	/// The speed that the party rotates.
	/// </summary>
	[SerializeField] private float rotateTime = 0.1f;

	/// <summary>
	/// Movement speed of the party.
	/// </summary>
	[SerializeField] private float partyMovementSpeed = 10f;

	/// <summary>
	/// Initialize this component.
	/// </summary>
	void Start () {

		// Initialize party positioning.
		northFriend.SetDirection (Direction.North, partySpread);
		eastFriend.SetDirection (Direction.East, partySpread);
		southFriend.SetDirection (Direction.South, partySpread);
		westFriend.SetDirection (Direction.West, partySpread);

		// Hook control events.
		Controls.RotateLeft.AddListener (OnRotateLeft);
		Controls.RotateRight.AddListener (OnRotateRight);
	}

	/// <summary>
	/// Update this component.
	/// </summary>
	void Update () {
		Controls.Update ();

		// Calculate movement speed this frame.
		Vector2 velocity = new Vector2 (Controls.HorizontalAxis, Controls.VerticalAxis);
		velocity.Normalize ();
		velocity *= partyMovementSpeed;
		velocity *= Time.deltaTime;

		// Set attacking.
		northFriend.GetComponent<AttackController> ().Attacking = Controls.FireNorth;
		eastFriend.GetComponent<AttackController> ().Attacking = Controls.FireEast;
		southFriend.GetComponent<AttackController> ().Attacking = Controls.FireSouth;
		westFriend.GetComponent<AttackController> ().Attacking = Controls.FireWest;

		// Apply movement.
		transform.Translate (velocity);
	}

	/// <summary>
	/// Get a reference to the friend in the specified direction.
	/// </summary>
	/// <param name="direction">Direction.</param>
	public Friend GetFriend (Direction direction) {
		switch (direction) {
		case Direction.North:
			return northFriend;
		case Direction.East:
			return eastFriend;
		case Direction.South:
			return southFriend;
		case Direction.West:
			return westFriend;
		}
		return null;
	}

	/// <summary>
	/// Heal the entire party by the specified amount (each).
	/// </summary>
	/// <param name="healing">Healing.</param>
	public void HealParty (int healing) {
		northFriend.GetComponent<HealthPool> ().Heal (healing);
		eastFriend.GetComponent<HealthPool> ().Heal (healing);
		southFriend.GetComponent<HealthPool> ().Heal (healing);
		westFriend.GetComponent<HealthPool> ().Heal (healing);
	}

	/// <summary>
	/// Rotate the party to the left.
	/// </summary>
	private void OnRotateLeft () {
		northFriend.SetDirection (Direction.West, partySpread, rotateTime);
		westFriend.SetDirection (Direction.South, partySpread, rotateTime);
		southFriend.SetDirection (Direction.East, partySpread, rotateTime);
		eastFriend.SetDirection (Direction.North, partySpread, rotateTime);

		Friend cachedFriend = northFriend;
		northFriend = eastFriend;
		eastFriend = southFriend;
		southFriend = westFriend;
		westFriend = cachedFriend;
	}

	/// <summary>
	/// Rotate the party to the right.
	/// </summary>
	private void OnRotateRight () {
		northFriend.SetDirection (Direction.East, partySpread, rotateTime);
		westFriend.SetDirection (Direction.North, partySpread, rotateTime);
		southFriend.SetDirection (Direction.West, partySpread, rotateTime);
		eastFriend.SetDirection (Direction.South, partySpread, rotateTime);

		Friend cachedFriend = northFriend;
		northFriend = westFriend;
		westFriend = southFriend;
		southFriend = eastFriend;
		eastFriend = cachedFriend;
	}
}
