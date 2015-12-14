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
	/// The number of projectiles fired with each shot.
	/// </summary>
	[SerializeField] private int numProjectiles = 1;

	/// <summary>
	/// Interval of time between shots.
	/// </summary>
	[SerializeField] private float attackInterval = 3f;

	/// <summary>
	/// If set to true, direction for attacks don't matter, instead projectiles fire in circles.
	/// </summary>
	[SerializeField] private bool circularAttack = false;

	/// <summary>
	/// Sound that plays on attack.
	/// </summary>
	[SerializeField] private AudioClip attackSound;

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
				Attack ();
				attackCooldownTimer -= attackInterval;
			}
			else {
				attackCooldownTimer = attackInterval;
			}
		}
	}

	/// <summary>
	/// Fire the attack!
	/// </summary>
	private void Attack () {
		if (numProjectiles > 0) {
			if (circularAttack) {
				float angleInterval = 360f / numProjectiles;
				float angle = 0;
				for (int i = 0; i < numProjectiles; i++) {
					Vector2 direction = MathUtil.Vector2FromMagnitudeAndAngle (1, angle * Mathf.Deg2Rad);
					FireProjectile (direction);
					angle += angleInterval;
				}
			}
			else {
				FireProjectile (Direction);
			}

			// Play audio.
			if (attackSound != null) {
				AudioSource source = GetComponent<AudioSource> ();
				source.PlayOneShot (attackSound);
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
