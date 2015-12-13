using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the FriendWheel user interface.
/// </summary>
public class FriendWheel : MonoBehaviour {

	/// <summary>
	/// References to the party
	/// </summary>
	[SerializeField] private Party party;

	/// <summary>
	/// References to the friend buttons.
	/// </summary>
	[SerializeField] private RectTransform northButton, eastButton, southButton, westButton;

	/// <summary>
	/// The color that represents each color direction.
	/// </summary>
	[SerializeField] private Color northColor, eastColor, southColor, westColor;

	/// <summary>
	/// Time it takes to rotate the buttons.
	/// </summary>
	[SerializeField] private float rotateTime = 0.1f;

	/// <summary>
	/// Distance buttons spread out visually.
	/// </summary>
	[SerializeField] private int buttonSpread = 40;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () {

		// Assign initial colors.
		northButton.GetComponent<Image> ().color = northColor;
		eastButton.GetComponent<Image> ().color = eastColor;
		southButton.GetComponent<Image> ().color = southColor;
		westButton.GetComponent<Image> ().color = westColor;

		// Initialize button positioning.
		northButton.GetComponent<RectTransformLerper> ().MoveTo (
			DirectionUtil.GetDirectionVector (Direction.North, buttonSpread), 0);
		eastButton.GetComponent<RectTransformLerper> ().MoveTo (
			DirectionUtil.GetDirectionVector (Direction.East, buttonSpread), 0);
		southButton.GetComponent<RectTransformLerper> ().MoveTo (
			DirectionUtil.GetDirectionVector (Direction.South, buttonSpread), 0);
		westButton.GetComponent<RectTransformLerper> ().MoveTo (
			DirectionUtil.GetDirectionVector (Direction.West, buttonSpread), 0);

		// Hook controls.
		Controls.RotateLeft.AddListener (OnRotateLeft);
		Controls.RotateRight.AddListener (OnRotateRight);

		// Hook Friend status monitoring.
		party.GetFriend (Direction.North).GetComponent<HealthPool> ().OnHealthModified.AddListener (UpdateAllFriends);
		party.GetFriend (Direction.East).GetComponent<HealthPool> ().OnHealthModified.AddListener (UpdateAllFriends);
		party.GetFriend (Direction.South).GetComponent<HealthPool> ().OnHealthModified.AddListener (UpdateAllFriends);
		party.GetFriend (Direction.West).GetComponent<HealthPool> ().OnHealthModified.AddListener (UpdateAllFriends);
	}

	/// <summary>
	/// Update the health pools! I'm lazy.
	/// </summary>
	/// <param name="uselessInteger">Useless integer doesn't get used except in event hook.</param>
	private void UpdateAllFriends (int uselessInteger = 0) {
		northButton.GetComponent<Image> ().fillAmount = party.GetFriend (Direction.North).GetComponent<HealthPool> ().PercentHealth;
		eastButton.GetComponent<Image> ().fillAmount = party.GetFriend (Direction.East).GetComponent<HealthPool> ().PercentHealth;
		southButton.GetComponent<Image> ().fillAmount = party.GetFriend (Direction.South).GetComponent<HealthPool> ().PercentHealth;
		westButton.GetComponent<Image> ().fillAmount = party.GetFriend (Direction.West).GetComponent<HealthPool> ().PercentHealth;
	}

	/// <summary>
	/// Rotate the party to the left.
	/// </summary>
	private void OnRotateLeft () {

		// North -> West
		northButton.GetComponent<RectTransformLerper> ().MoveTo (
			DirectionUtil.GetDirectionVector (Direction.West, buttonSpread), rotateTime);
		northButton.GetComponent<Image> ().color = westColor;

		// West -> South
		westButton.GetComponent<RectTransformLerper> ().MoveTo (
			DirectionUtil.GetDirectionVector (Direction.South, buttonSpread), rotateTime);
		westButton.GetComponent<Image> ().color = southColor;

		// South -> East
		southButton.GetComponent<RectTransformLerper> ().MoveTo (
			DirectionUtil.GetDirectionVector (Direction.East, buttonSpread), rotateTime);
		southButton.GetComponent<Image> ().color = eastColor;

		// East -> North
		eastButton.GetComponent<RectTransformLerper> ().MoveTo (
			DirectionUtil.GetDirectionVector (Direction.North, buttonSpread), rotateTime);
		eastButton.GetComponent<Image> ().color = northColor;

		// Move references.
		RectTransform cachedButton = northButton;
		northButton = eastButton;
		eastButton = southButton;
		southButton = westButton;
		westButton = cachedButton;
	}

	/// <summary>
	/// Rotate the party to the right.
	/// </summary>
	private void OnRotateRight () {

		// North -> East
		northButton.GetComponent<RectTransformLerper> ().MoveTo (
			DirectionUtil.GetDirectionVector (Direction.East, buttonSpread), rotateTime);
		northButton.GetComponent<Image> ().color = eastColor;

		// West -> North
		westButton.GetComponent<RectTransformLerper> ().MoveTo (
			DirectionUtil.GetDirectionVector (Direction.North, buttonSpread), rotateTime);
		westButton.GetComponent<Image> ().color = northColor;

		// South -> West
		southButton.GetComponent<RectTransformLerper> ().MoveTo (
			DirectionUtil.GetDirectionVector (Direction.West, buttonSpread), rotateTime);
		southButton.GetComponent<Image> ().color = westColor;

		// East -> South
		eastButton.GetComponent<RectTransformLerper> ().MoveTo (
			DirectionUtil.GetDirectionVector (Direction.South, buttonSpread), rotateTime);
		eastButton.GetComponent<Image> ().color = southColor;

		// Move references.
		RectTransform cachedButton = northButton;
		northButton = westButton;
		westButton = southButton;
		southButton = eastButton;
		eastButton = cachedButton;
	}
}
