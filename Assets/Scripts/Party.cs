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
	[SerializeField] private float rotateSpeed = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
