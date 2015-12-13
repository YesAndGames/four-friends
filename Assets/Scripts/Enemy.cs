using UnityEngine;

/// <summary>
/// Manages an enemy.
/// </summary>
public class Enemy : MonoBehaviour {

	/// <summary>
	/// Describes an Enemy's chance to drop a particular drop.
	/// </summary>
	[System.Serializable]
	private class ChanceToDropEntry {

		/// <summary>
		/// The thing that gets dropped.
		/// </summary>
		public GameObject drop;

		/// <summary>
		/// The chance this thing will drop.
		/// </summary>
		public float chance = 0.5f;
	}

	/// <summary>
	/// The movement speed of this enemy.
	/// </summary>
	[SerializeField] private float movementSpeed = 1f;

	/// <summary>
	/// If set to true, dies on contact with a friend.
	/// </summary>
	[SerializeField] private bool diesOnContact = false;

	/// <summary>
	/// List of GameObjects that always drop when this enemy is killed.
	/// </summary>
	[SerializeField] private GameObject[] alwaysDropOnDeath;

	/// <summary>
	/// List of chance to drop entries for when this enemy is killed.
	/// </summary>
	[SerializeField] private ChanceToDropEntry[] chanceToDropOnDeath;

	/// <summary>
	/// Force applied to a thing on drop.
	/// </summary>
	[SerializeField] private float dropForce = 100f;

	/// <summary>
	/// This enemy's target Friend.
	/// </summary>
	private Friend target;

	/// <summary>
	/// Initialize this component.
	/// </summary>
	void Start () {
		GetComponent<HealthPool> ().TakeDamage.AddListener (OnTakeDamage);

		// Enable attacking all the time, if this Enemy attacks.
		AttackController attack = GetComponent<AttackController> ();
		if (attack != null) {
			attack.Attacking = true;
		}

		// Begin AI routines.
		ReevaluateBehavior ();
		InvokeRepeating ("ReevaluateBehavior", 0, 5f);
	}

	/// <summary>
	/// Update personal income.
	/// </summary>
	void Update () {
		if (target != null) {

			// Move towards the target.
			Vector2 direction = target.transform.position - transform.position;
			Vector2 velocity = direction.normalized * movementSpeed * Time.deltaTime;
			transform.Translate (velocity);

			// Set attacking direction.
			AttackController attack = GetComponent<AttackController> ();
			if (attack != null) {
				GetComponent<AttackController> ().Direction = direction;
			}
		}
	}

	/// <summary>
	/// Called when another trigger enters this one.
	/// </summary>
	void OnTriggerEnter2D (Collider2D other) {

		// Friend contact.
		Friend friend = other.GetComponent<Friend> ();
		if (friend != null) {

			// Death on contact.
			if (diesOnContact) {
				Die ();
			}
		}
	}

	/// <summary>
	/// Kill this enemy.
	/// </summary>
	public void Die () {
		int i;

		// Drop all the guaranteed drops.
		for (i = 0; i < alwaysDropOnDeath.Length; i++) {
			DropThing (alwaysDropOnDeath [i]);
		}

		// Figure out potential drops based on random rolls.
		for (i = 0; i < chanceToDropOnDeath.Length; i++) {
			float roll = Random.value;
			ChanceToDropEntry entry = chanceToDropOnDeath [i];
			if (roll >= entry.chance) {
				DropThing (entry.drop);
			}
		}

		// Die.
		Destroy (gameObject);
	}

	/// <summary>
	/// Drops the thing.
	/// </summary>
	/// <param name="thing">Thing.</param>
	private void DropThing (GameObject prefab) {
		GameObject thing = Instantiate (prefab, transform.position, Quaternion.identity) as GameObject;

		// Apply force maybe.
		Rigidbody2D body = thing.GetComponent <Rigidbody2D> ();
		if (body != null) {
			float angle = Random.Range (0, Mathf.PI * 2);
			Vector2 force = MathUtil.Vector2FromMagnitudeAndAngle (dropForce, angle);
			body.AddForce (force);
		}
	}

	/// <summary>
	/// Reevaluates this enemy's behavior.
	/// </summary>
	protected void ReevaluateBehavior () {
		Friend[] potentialTargets = FindObjectsOfType<Friend> ();
		float distance = 0;
		if (potentialTargets.Length > 0) {
			target = potentialTargets [0];
			distance = Vector2.Distance (transform.position, target.transform.position);
			for (int i = 1; i < potentialTargets.Length; i++) {
				Friend potentialTarget = potentialTargets [i];
				float potentialDistance = Vector2.Distance (transform.position, potentialTarget.transform.position);
				if (potentialDistance < distance) {
					target = potentialTarget;
					distance = potentialDistance;
				}
			}
		}
		else {
			target = null;
		}
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
}
