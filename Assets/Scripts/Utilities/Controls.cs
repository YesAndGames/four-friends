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
		VerticalAxis = Input.GetAxis ("Vertical");
		HorizontalAxis = Input.GetAxis ("Horizontal");
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
