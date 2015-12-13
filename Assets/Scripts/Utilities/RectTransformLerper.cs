using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Abstracts and manages interpolated rect transform movement.
/// </summary>
public class RectTransformLerper : MonoBehaviour {

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
				GetComponent<RectTransform> ().anchoredPosition = targetPosition;
			}
			else {
				GetComponent<RectTransform> ().anchoredPosition = Vector2.Lerp (initialPosition, targetPosition, t / lerpTime);
			}
		}
	}

	/// <summary>
	/// Move to the specified position in the specified time.
	/// </summary>
	/// <param name="position">Position.</param>
	/// <param name="time">Time.</param>
	public void MoveTo (Vector2 position, float time) {
		initialPosition = GetComponent<RectTransform> ().anchoredPosition;
		targetPosition = position;
		lerpTime = time;
		t = 0;
		interpolating = true;
	}
}
