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
	/// If this doesn't spawn infinitely, spawn this many things.
	/// </summary>
	[SerializeField] private int numSpawns = 5;

	/// <summary>
	/// Spawn a thing every spawnInterval seconds.
	/// </summary>
	[SerializeField] private float spawnInterval = 2f;

	/// <summary>
	/// Time counter.
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
		t += Time.deltaTime;
		while (t >= spawnInterval) {
			Spawn ();
			t -= spawnInterval;
		}
	}

	/// <summary>
	/// Spawn a thing.
	/// </summary>
	private void Spawn () {
		Instantiate (spawnPrefab, transform.position, Quaternion.identity);

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
