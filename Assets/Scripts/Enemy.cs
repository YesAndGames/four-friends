using UnityEngine;

/// <summary>
/// Manages an enemy.
/// </summary>
public class Enemy : MonoBehaviour {

	/// <summary>
	/// The movement speed of this enemy.
	/// </summary>
	[SerializeField] private float movementSpeed = 1f;

	/// <summary>
	/// This enemy's target Friend.
	/// </summary>
	private Friend target;

	/// <summary>
	/// Initialize this component.
	/// </summary>
	void Start () {
		GetComponent<HealthPool> ().TakeDamage.AddListener (OnTakeDamage);
		GetComponent<AttackController> ().Attacking = true;
		ReevaluateBehavior ();
		InvokeRepeating ("ReevaluateBehavior", 0, 5f);
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {

			// Move towards the target.
			Vector2 direction = target.transform.position - transform.position;
			Vector2 velocity = direction.normalized * movementSpeed * Time.deltaTime;
			transform.Translate (velocity);

			// Set attacking direction.
			GetComponent<AttackController> ().Direction = direction;
		}
	}

	/// <summary>
	/// Kill this enemy.
	/// </summary>
	public void Die () {
		Destroy (gameObject);
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
