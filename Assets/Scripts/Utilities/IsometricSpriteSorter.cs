using UnityEngine;

[ExecuteInEditMode]
public class IsometricSpriteSorter : MonoBehaviour {
	
	/// <summary>
	/// Update this instance; set the sorting order based on height.
	/// </summary>
	void Update () {
		this.GetComponent<Renderer> ().sortingOrder = (int)(transform.position.y * -10);
	}
}
