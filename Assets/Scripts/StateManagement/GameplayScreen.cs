using UnityEngine;

/// <summary>
/// Manages gameplay screen functions.
/// </summary>
public class GameplayScreen : IGameState {

	/// <summary>
	/// Initialize this game state.
	/// </summary>
	public override void OnInitializeState () {
		base.OnInitializeState ();

		// Hook game over event.
		FindObjectOfType<Party> ().PartyDies.AddListener (OnGameOver);
	}

	/// <summary>
	/// Clean up loose projectiles in scene.
	/// </summary>
	void OnDestroy () {
		base.OnExitState ();

		Projectile[] looseProjectiles = FindObjectsOfType<Projectile> ();
		for (int i = 0; i < looseProjectiles.Length; i++) {
			Destroy (looseProjectiles [i].gameObject);
		}
	}

	/// <summary>
	/// Call to invoke game over.
	/// </summary>
	private void OnGameOver () {
		PushState ("Game Over");
	}
}
