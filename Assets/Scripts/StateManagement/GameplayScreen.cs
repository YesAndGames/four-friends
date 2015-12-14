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
	/// Call to invoke game over.
	/// </summary>
	private void OnGameOver () {
		Debug.LogWarning ("game over");
	}
}
