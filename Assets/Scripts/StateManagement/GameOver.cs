using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Game over screen.
/// </summary>
public class GameOver : IGameState {

	/// <summary>
	/// Initialize this game state.
	/// </summary>
	public override void OnInitializeState () {
		base.OnInitializeState ();

		AddHideableElement ("Sqwad", false);
		EventSystem.current.SetSelectedGameObject (GuiCanvas.transform.Find ("Buttons Container/Replay Button").gameObject);
	}

	/// <summary>
	/// Press the replay button.
	/// </summary>
	public void OnPressReplayButton () {
		HideGUIElement ("Sqwad");
	}

	/// <summary>
	/// Begin replay.
	/// </summary>
	public void OnBeginReplay () {
		PopState ();
	}

	/// <summary>
	/// Go to main menu.
	/// </summary>
	public void OnPressMainMenu () {
		Manager.PopToState ("Main Menu");
	}
}
