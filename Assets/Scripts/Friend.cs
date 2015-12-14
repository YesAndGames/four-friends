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
	/// Gets a reference to the Party this Friend is a member of.
	/// </summary>
	/// <value>This friend's party.</value>
	public Party Party { get; private set; }

	/// <summary>
	/// The current direction this Friend is facing.
	/// </summary>
	/// <value>Facing direction.</value>
	public Direction Direction { get; private set; }

	/// <summary>
	/// Initialize this component.
	/// </summary>
	void Start () {
		Party = transform.parent.GetComponent<Party> ();
		GetComponent<HealthPool> ().TakeDamage.AddListener (OnTakeDamage);
		GetComponent<HealthPool> ().TakeHealing.AddListener (OnHealDamage);
		GetComponent<HealthPool> ().Death.AddListener (OnDie);
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
		Vector2 targetPosition = DirectionUtil.GetDirectionVector (Direction, spreadDistance);
		GetComponentInChildren<TransformLerper> ().MoveTo (targetPosition, switchTime);
		GetComponent<AttackController> ().Direction = targetPosition.normalized;

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

	/// <summary>
	/// Called when this entity's health pool takes damage.
	/// </summary>
	/// <param name="damage">Damage.</param>
	private void OnTakeDamage (int damage) {
		Animator anim = GetComponent<Animator> ();
		if (anim != null) {
			anim.Play ("Hurt", -1, 0);
		}
	}

	/// <summary>
	/// Called when this entity's health pool is healed.
	/// </summary>
	/// <param name="heal">Heal amount.</param>
	private void OnHealDamage (int heal) {
		Animator anim = GetComponent<Animator> ();
		if (anim != null) {
			anim.Play ("Healed", -1, 0);
		}

		// Revive this Friend.
		if (heal > 0) {
			SetAlive (true);
		}
	}

	/// <summary>
	/// This Friend dies.
	/// </summary>
	private void OnDie () {
		SetAlive (false);
	}

	/// <summary>
	/// Set a flag indicating whether or not this Friend is dead or alive.
	/// </summary>
	private void SetAlive (bool alive) {

		// Turn attack on/off.
		GetComponent<AttackController> ().enabled = alive;

		// If dead, play that animation.
		if (!alive) {
			GetComponent<Animator> ().Play ("Dead", -1, 0);
		}
	}
}
