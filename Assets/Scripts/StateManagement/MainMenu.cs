using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Manages the main menu state.
/// </summary>
public class MainMenu : IGameState {

	/// <summary>
	/// Initialize this component.
	/// </summary>
	public override void OnInitializeState () {
		base.OnInitializeState ();

		// Register hideables.
		AddHideableElement ("Sqwad", false);
		AddHideableElement ("Controls");

		// Hook listeners for closing controls after shown.
		GetGUIElement ("Controls").OnShown.AddListener ((h) => {
			Controls.Select.AddListener (HideControls);
			Controls.Cancel.AddListener (HideControls);
		});

		// Initialize start selection.
		EventSystem.current.SetSelectedGameObject (GuiCanvas.transform.Find ("Buttons Container/Play Button").gameObject);
	}

	/// <summary>
	/// Called when the user presses the play button.
	/// </summary>
	public void OnPressPlayButton () {
		HideGUIElement ("Sqwad");
		EventSystem.current.SetSelectedGameObject (null);
		SetAllButtonsEnabled (false);
	}

	/// <summary>
	/// Called when the user presses the controls button.
	/// </summary>
	public void OnPressControlsButton () {
		ShowGUIElement ("Controls");
		SetAllButtonsEnabled (false);
	}

	/// <summary>
	/// Called when the user presses the other games button.
	/// </summary>
	public void OnPressOtherGamesButton () {
		Application.OpenURL ("http://yesandgames.com");
	}

	/// <summary>
	/// Called when play begins.
	/// </summary>
	public void OnBeginPlay () {
		Manager.PushState ("Gameplay");
	}

	/// <summary>
	/// Hides the controls screen.
	/// </summary>
	private void HideControls () {
		HideGUIElement ("Controls");
		SetAllButtonsEnabled (true);
		Controls.Select.RemoveListener (HideControls);
		Controls.Cancel.RemoveListener (HideControls);
	}

	/// <summary>
	/// Sets whether or not the buttons on this screen are enabled.
	/// </summary>
	private void SetAllButtonsEnabled (bool enabled) {
		Button[] buttons = GuiCanvas.gameObject.GetComponentsInChildren<Button> ();
		for (int i = 0; i < buttons.Length; i++) {
			buttons [i].interactable = enabled;
		}
	}
}
