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
	/// Gets the position a party member should be at given a specific direction.
	/// </summary>
	/// <returns>The party position.</returns>
	/// <param name="direction">Direction.</param>
	/// <param name="spreadDistance">Spread distance.</param>
	public static Vector2 GetPartyPosition (Direction direction, float spreadDistance) {
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
		final *= spreadDistance;
		return final;
	}
}