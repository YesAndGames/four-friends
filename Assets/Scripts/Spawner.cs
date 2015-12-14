using UnityEngine;

/// <summary>
/// A persistent spawner that spawns things.
/// </summary>
public class Spawner : MonoBehaviour {

	/// <summary>
	/// The prefab that gets spawned.
	/// </summary>
	[SerializeField] private GameObject spawnPrefab;

	/// <summary>
	/// If set to true, spawns the prefabs as long as it is alive.
	/// </summary>
	[SerializeField] private bool spawnsInfinitely = true;

	/// <summary>
	/// If set to true, the party needs to trigger this portal.
	/// </summary>
	[SerializeField] private bool requiresTrigger = false;

	/// <summary>
	/// If this doesn't spawn infinitely, spawn this many things.
	/// </summary>
	[SerializeField] private int numSpawns = 5;

	/// <summary>
	/// Spawn a thing every spawnInterval seconds.
	/// </summary>
	[SerializeField] private float spawnInterval = 2f;

	/// <summary>
	/// Whether other not this spawner is spawning currently.
	/// </summary>
	private bool triggered = false;

	/// <summary>
	/// Time counter.
	/// </summary>
	private float t = 0;

	/// <summary>
	/// Initialize this component.
	/// </summary>
	void Start () {
		t = 0;
		triggered = !requiresTrigger;
	}

	/// <summary>
	/// Update this component.
	/// </summary>
	void Update () {
		if (triggered) {
			t += Time.deltaTime;
			while (t >= spawnInterval) {
				Spawn ();
				t -= spawnInterval;
			}
		}
	}

	/// <summary>
	/// Fires when another trigger enters this one.
	/// </summary>
	/// <param name="other">Other collider.</param>
	void OnTriggerEnter2D (Collider2D other) {
		Party party = other.GetComponent<Party> ();
		if (party != null) {
			triggered = true;
		}
	}

	/// <summary>
	/// Spawn a thing.
	/// </summary>
	private void Spawn () {
		GameObject spawned = Instantiate (spawnPrefab, transform.position, Quaternion.identity) as GameObject;
		spawned.transform.SetParent (transform.parent);

		// Decrement remaining spawns.
		if (!spawnsInfinitely) {
			numSpawns--;
			if (numSpawns <= 0) {
				DestroySelf ();
			}
		}
	}

	/// <summary>
	/// Destroy this spawner thing.
	/// </summary>
	private void DestroySelf () {
		Destroy (gameObject);
	}
}
