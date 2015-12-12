using UnityEngine;

/// <summary>
/// Enum that represents the cardinal directions.
/// </summary>
public enum Direction {
	North,
	East,
	South,
	West,
}

/// <summary>
/// Utilities for calculating stuff using Directions.
/// </summary>
public static class DirectionUtil {

	/// <summary>
	/// Gets a direction vector from a direction and a magnitude
	/// </summary>
	/// <returns>The party position.</returns>
	/// <param name="direction">Direction.</param>
	/// <param name="magnitude">Magnitude.</param>
	public static Vector2 GetDirectionVector (Direction direction, float magnitude = 1) {
		Vector2 final = Vector2.zero;
		switch (direction) {
		case Direction.North:
			final = Vector2.up;
			break;
		case Direction.East:
			final = Vector2.right;
			break;
		case Direction.South:
			final = Vector2.down;
			break;
		case Direction.West:
			final = Vector2.left;
			break;
		}
		final.Normalize ();
		final *= magnitude;
		return final;
	}
}