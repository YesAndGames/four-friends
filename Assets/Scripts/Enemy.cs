using UnityEngine;

/// <summary>
/// Manages an enemy.
/// </summary>
public class Enemy : MonoBehaviour {

	/// <summary>
	/// The projectile this enemy fires.
	/// </summary>
	[SerializeField] private Projectile projectilePrefab;

	/// <summary>
	/// Interval of time between shots.
	/// </summary>
	[SerializeField] private float projectileFireInterval = 3f;

	/// <summary>
	/// Time for projectile shots.
	/// </summary>
	private float projectileFireTimer = 0;

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

			// Shoot when you can.
			projectileFireTimer += Time.deltaTime;
			while (projectileFireTimer >= projectileFireInterval) {
				FireProjectile ();
				projectileFireTimer -= projectileFireInterval;
			}
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
	/// Fire a projectile at the target.
	/// </summary>
	private void FireProjectile () {
		Vector2 direction = target.transform.position - transform.position;
		Projectile projectile = Instantiate (projectilePrefab, transform.position, Quaternion.identity) as Projectile;
		projectile.gameObject.layer = gameObject.layer;
		projectile.Fire (direction);
	}
}
