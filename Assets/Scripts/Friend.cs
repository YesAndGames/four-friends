using UnityEngine;

/// <summary>
/// Controls a single friend in the party.
/// </summary>
public class Friend : MonoBehaviour {

	/// <summary>
	/// Each of this Friend's directional sprites.
	/// </summary>
	[SerializeField] private Sprite northSprite, eastSprite, southSprite, westSprite;

	/// <summary>
	/// The current direction this Friend is facing.
	/// </summary>
	/// <value>Facing direction.</value>
	public Direction Direction { get; private set; }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Sets the direction this friend is facing.
	/// </summary>
	/// <param name="direction">Direction.</param>
	/// <param name="spreadDistance">The distance the party members spread when the move around.</param>
	/// <param name="switchTime">Switch time.</param>
	public void SetDirection (Direction direction, float spreadDistance, float switchTime = 0) {
		Direction = direction;
		transform.localPosition = DirectionUtil.GetPartyPosition (Direction, spreadDistance);
	}
}
