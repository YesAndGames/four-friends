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

	/// <summary>
	/// Initialize this component.
	/// </summary>
	void Start () {
		
	}

	/// <summary>
	/// Sets the direction this friend is facing.
	/// </summary>
	/// <param name="direction">Direction.</param>
	/// <param name="spreadDistance">The distance the party members spread when the move around.</param>
	/// <param name="switchTime">Switch time.</param>
	public void SetDirection (Direction direction, float spreadDistance, float switchTime = 0) {

		// Set the spatial direction.
		Direction = direction;
		Vector2 targetPosition = DirectionUtil.GetPartyPosition (Direction, spreadDistance);
		GetComponentInChildren<TransformLerper> ().MoveTo (targetPosition, switchTime);

		// Set the sprite.
		SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer> ();
		Sprite sprite = spriteRenderer.sprite;
		switch (Direction) {
		case Direction.North:
			sprite = northSprite;
			break;
		case Direction.East:
			sprite = eastSprite;
			break;
		case Direction.South:
			sprite = southSprite;
			break;
		case Direction.West:
			sprite = westSprite;
			break;
		}

		spriteRenderer.sprite = sprite;
	}
}
