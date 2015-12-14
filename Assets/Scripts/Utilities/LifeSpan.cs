using UnityEngine;

/// <summary>
/// Component that destroys its own GameObject after a given amount of time.
/// </summary>
public class LifeSpan : MonoBehaviour {

	/// <summary>
	/// The life span of this object.
	/// </summary>
	[SerializeField] private float lifeSpan = 10f;

	/// <summary>
	/// Time passed.
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
		if (t >= lifeSpan) {
			DestroySelf ();
		}
	}

	/// <summary>
	/// Destroy this game object.
	/// </summary>
	private void DestroySelf () {
		Destroy (gameObject);
	}
}
