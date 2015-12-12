using UnityEngine;

/// <summary>
/// Abstracts and manages interpolated transform movement.
/// </summary>
public class TransformLerper : MonoBehaviour {

	/// <summary>
	/// Positions used for interpolation.
	/// </summary>
	private Vector2 initialPosition, targetPosition;

	/// <summary>
	/// Interpolation trackers.
	/// </summary>
	private float lerpTime = 0, t = 0;

	/// <summary>
	/// Flag for still interpolating for efficiency.
	/// </summary>
	private bool interpolating = false;

	/// <summary>
	/// Update this component.
	/// </summary>
	void Update () {
		if (interpolating) {
			t += Time.deltaTime;

			if (t >= lerpTime) {
				t = lerpTime;
				interpolating = false;
				transform.localPosition = targetPosition;
			}
			else {
				transform.localPosition = Vector2.Lerp (initialPosition, targetPosition, t / lerpTime);
			}
		}
	}

	/// <summary>
	/// Move to the specified position in the specified time.
	/// </summary>
	/// <param name="position">Position.</param>
	/// <param name="time">Time.</param>
	public void MoveTo (Vector2 position, float time) {
		initialPosition = transform.localPosition;
		targetPosition = position;
		lerpTime = time;
		t = 0;
		interpolating = true;
	}
}
