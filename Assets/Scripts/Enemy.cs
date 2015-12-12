using UnityEngine;

/// <summary>
/// Manages an enemy.
/// </summary>
public class Enemy : MonoBehaviour {

	/// <summary>
	/// Initialize this component.
	/// </summary>
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Kill this enemy.
	/// </summary>
	public void Die () {
		Destroy (gameObject);
	}
}
