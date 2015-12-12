using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manages cross platform controls and fires events.
/// </summary>
public static class Controls {

	/// <summary>
	/// Fires when left rotate control is triggered.
	/// </summary>
	public static UnityEvent RotateLeft = new UnityEvent ();

	/// <summary>
	/// Fires when right rotate control is triggered.
	/// </summary>
	public static UnityEvent RotateRight = new UnityEvent ();

	/// <summary>
	/// Fires when north fire control is triggered.
	/// </summary>
	public static UnityEvent FireNorth = new UnityEvent ();

	/// <summary>
	/// Fires when east fire control is triggered.
	/// </summary>
	public static UnityEvent FireEast = new UnityEvent ();

	/// <summary>
	/// Fires when south fire control is triggered.
	/// </summary>
	public static UnityEvent FireSouth = new UnityEvent ();

	/// <summary>
	/// Fires when west fire control is triggered.
	/// </summary>
	public static UnityEvent FireWest = new UnityEvent ();

	/// <summary>
	/// Cardinal motion axes.
	/// </summary>
	public static float HorizontalAxis = 0, VerticalAxis = 0;

	/// <summary>
	/// Rotation controls are ready.
	/// </summary>
	private static bool rotateReady = false;

	/// <summary>
	/// The treshold for an axis being released from hold.
	/// </summary>
	private static float axisReleaseThreshold = 0.1f;

	/// <summary>
	/// Update the controls.
	/// </summary>
	public static void Update () {

		// Movement controls.
		VerticalAxis = Input.GetAxis ("Vertical");
		HorizontalAxis = Input.GetAxis ("Horizontal");

		// Fire controls.
		if (Input.GetButtonUp ("Fire North")) {
			FireNorth.Invoke ();
		}
		if (Input.GetButtonUp ("Fire East")) {
			FireEast.Invoke ();
		}
		if (Input.GetButtonUp ("Fire South")) {
			FireSouth.Invoke ();
		}
		if (Input.GetButtonUp ("Fire West")) {
			FireWest.Invoke ();
		}

		// Rotate controls.
		float rotateAxis = Input.GetAxis ("Rotate");

		if (rotateReady) {
			if (rotateAxis < -1 + axisReleaseThreshold) {
				rotateReady = false;
				RotateLeft.Invoke ();
			}
			else if (rotateAxis > 1 - axisReleaseThreshold) {
				rotateReady = false;
				RotateRight.Invoke ();
			}
		}
		else if (rotateAxis < axisReleaseThreshold && rotateAxis > -axisReleaseThreshold) {
			rotateReady = true;
		}
	}
}
