using UnityEngine;

/// <summary>
/// Manages attacks and all the wacky things you can do with them.
/// </summary>
public class AttackController : MonoBehaviour {

	/// <summary>
	/// If set to true, voluntarily attacking.
	/// </summary>
	/// <value><c>true</c> if attacking; otherwise, <c>false</c>.</value>
	public bool Attacking { get; set; }

	/// <summary>
	/// Sets the attacking direction.
	/// </summary>
	/// <value>The attacking direction.</value>
	public Vector2 Direction { get; set; }

	/// <summary>
	/// The projectile prefab.
	/// </summary>
	[SerializeField] private Projectile projectilePrefab;

	/// <summary>
	/// Interval of time between shots.
	/// </summary>
	[SerializeField] private float attackInterval = 3f;

	/// <summary>
	/// Time for projectile shots.
	/// </summary>
	private float attackCooldownTimer;

	/// <summary>
	/// Initialize this component.
	/// </summary>
	void Start () {
		attackCooldownTimer = attackInterval;
	}

	/// <summary>
	/// Update this component.
	/// </summary>
	void Update () {
		// Shoot when you can.
		attackCooldownTimer += Time.deltaTime;
		if (attackCooldownTimer >= attackInterval) {
			if (Attacking) {
				FireProjectile (Direction);
				attackCooldownTimer -= attackInterval;
			}
			else {
				attackCooldownTimer = attackInterval;
			}
		}
	}

	/// <summary>
	/// Fire a projectile at the target.
	/// </summary>
	private void FireProjectile (Vector2 direction) {
		Projectile projectile = Instantiate (projectilePrefab, transform.position, Quaternion.identity) as Projectile;
		projectile.gameObject.layer = gameObject.layer;
		projectile.Fire (direction);
	}
}
