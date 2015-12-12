using UnityEngine;

/// <summary>
/// Controls a single projectile.
/// </summary>
public class Projectile : MonoBehaviour {

	/// <summary>
	/// Time until this projectile dies out.
	/// </summary>
	[SerializeField] private float lifespan = 10f;

	/// <summary>
	/// Speed of the projectile.
	/// </summary>
	[SerializeField] private float speed = 10f;

	/// <summary>
	/// Amount of damage this projectile deals.
	/// </summary>
	[SerializeField] private int damage = 10;

	/// <summary>
	/// The force applied on hit.
	/// </summary>
	[SerializeField] private float force = 100f;

	/// <summary>
	/// If set to true, this projectile penetrates colliders.
	/// </summary>
	[SerializeField] private bool penetrating = false;

	/// <summary>
	/// Current velocity of this projectile.
	/// </summary>
	private Vector2 velocity;

	/// <summary>
	/// Time spent alive.
	/// </summary>
	private float t = 0;

	/// <summary>
	/// Initialize this component.
	/// </summary>
	void Start () {
		t = 0;
	}

	/// <summary>
	/// Update this component.
	/// </summary>
	void Update () {

		// Movement.
		transform.Translate (velocity * Time.deltaTime);

		// Life span.
		t += Time.deltaTime;

		if (t >= lifespan) {
			DestroySelf ();
		}
	}

	/// <summary>
	/// Fires when a trigger collides with this trigger.
	/// </summary>
	void OnTriggerEnter2D (Collider2D other) {

		// Apply damage.
		HealthPool healthPool = other.GetComponent<HealthPool> ();
		if (healthPool != null) {
			healthPool.Damage (damage);

			// Collision.
			if (!penetrating) {
				DestroySelf ();
			}
		}

		// Apply force.
		Rigidbody2D body = other.GetComponent<Rigidbody2D> ();
		if (body != null) {
			body.AddForce (velocity.normalized * force, ForceMode2D.Force);
		}
	}

	/// <summary>
	/// Fires this projectile in the specified direction.
	/// </summary>
	/// <param name="direction">Direction.</param>
	public void Fire (Vector2 direction) {
		velocity = direction.normalized * speed;
	}

	/// <summary>
	/// Destroys this projectile.
	/// </summary>
	private void DestroySelf () {
		Destroy (gameObject);
	}
}
