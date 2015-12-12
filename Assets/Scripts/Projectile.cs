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
			Destroy (gameObject);
		}
	}

	/// <summary>
	/// Fires this projectile in the specified direction.
	/// </summary>
	/// <param name="direction">Direction.</param>
	public void Fire (Vector2 direction) {
		velocity = direction * speed;
	}
}
