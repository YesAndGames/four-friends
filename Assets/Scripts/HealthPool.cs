using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Event that applies to the health pool.
/// </summary>
public class HealthEvent : UnityEvent<int> { }

/// <summary>
/// Manages an entity's health pool.
/// </summary>
public class HealthPool : MonoBehaviour {

	/// <summary>
	/// Fires when this health pool takes damage.
	/// </summary>
	public HealthEvent TakeDamage = new HealthEvent ();

	/// <summary>
	/// Fires when this health pool takes damage.
	/// </summary>
	public HealthEvent TakeHealing = new HealthEvent ();

	/// <summary>
	/// Fires when this health pool is modified at all.
	/// </summary>
	public HealthEvent OnHealthModified = new HealthEvent ();

	/// <summary>
	/// Fires when this health pool indicates death.
	/// </summary>
	public UnityEvent Death = new UnityEvent ();

	/// <summary>
	/// Calculates the percent health this health pool has currently.
	/// </summary>
	/// <value>The percent health.</value>
	public float PercentHealth { get { return (float)health / (float)maxHealth; } }

	/// <summary>
	/// Maximum health in the health pool.
	/// </summary>
	[SerializeField] private int maxHealth = 100;

	/// <summary>
	/// Current health.
	/// </summary>
	private int health;

	/// <summary>
	/// Initialize this component.
	/// </summary>
	void Start () {
		health = maxHealth;
	}

	/// <summary>
	/// Apply damage to this health pool
	/// </summary>
	/// <param name="amount">Amount of damage.</param>
	public void Damage (int amount) {
		TakeDamage.Invoke (amount);
		Modify (-amount);
	}

	/// <summary>
	/// Heal damage to the health pool
	/// </summary>
	/// <param name="amount">Amount of damage.</param>
	public void Heal (int amount) {
		TakeHealing.Invoke (amount);
		Modify (amount);
	}

	/// <summary>
	/// Modify the health pool.
	/// </summary>
	/// <param name="amount">Modification.</param>
	private void Modify (int amount) {
		health += amount;
		health = Mathf.Clamp (health, 0, maxHealth);
		OnHealthModified.Invoke (amount);

		// Check death condition.
		if (health == 0) {
			Death.Invoke ();
		}
	}
}
