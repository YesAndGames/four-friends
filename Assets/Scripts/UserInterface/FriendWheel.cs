﻿using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the FriendWheel user interface.
/// </summary>
public class FriendWheel : MonoBehaviour {

	/// <summary>
	/// References to each friend so we can track their health.
	/// </summary>
	[SerializeField] private Friend northFriend, eastFriend, southFriend, westFriend;

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
		northFriend.GetComponent<HealthPool> ().TakeDamage.AddListener ((int amount) => UpdateFriend (northFriend.Direction));
		eastFriend.GetComponent<HealthPool> ().TakeDamage.AddListener ((int amount) => UpdateFriend (eastFriend.Direction));
		southFriend.GetComponent<HealthPool> ().TakeDamage.AddListener ((int amount) => UpdateFriend (southFriend.Direction));
		westFriend.GetComponent<HealthPool> ().TakeDamage.AddListener ((int amount) => UpdateFriend (westFriend.Direction));
	}

	/// <summary>
	/// Updates this friend's status on the UI.
	/// </summary>
	/// <param name="direction">Direction.</param>
	private void UpdateFriend (Direction direction) {
		Friend friend = null;
		RectTransform button = null;
		switch (direction) {
		case Direction.North:
			friend = northFriend;
			button = northButton;
			break;
		case Direction.East:
			friend = eastFriend;
			button = eastButton;
			break;
		case Direction.South:
			friend = southFriend;
			button = southButton;
			break;
		case Direction.West:
			friend = westFriend;
			button = westButton;
			break;
		}

		if (friend != null && button != null) {
			button.GetComponent<Image> ().fillAmount = friend.GetComponent<HealthPool> ().PercentHealth;
		}
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
