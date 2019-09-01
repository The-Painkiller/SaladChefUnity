using UnityEngine;

/// <summary>
/// Static class with basic moveable area coordinates.
/// </summary>
public static class MoveableAreaConstants
{
	public static readonly Vector2 _playerMoveableAreaMax = new Vector2 (5.5f, 1.5f);
	public static readonly Vector2 _playerMoveableAreaMin = new Vector2 (-5.5f, -2.5f);
    public static readonly Vector3 _firstPlayerSpawnPoint = new Vector3 ( -2.5f, -2.5f, -0.01f );
    public static readonly Vector3 _secondPlayerSpawnPoint = new Vector3 ( 2.5f, -2.5f, -0.01f );

    /// <summary>
    /// Mainly used by PickupsManager to get a random location in playable area to keep a pickup at.
    /// </summary>
    /// <returns></returns>
    public static Vector2 GetRandomPlacement ( )
    {
        Vector2 placement;

        float x = (int)Random.Range ( -4, 5 );
        float y = (int)Random.Range ( -1, 1 );

        x -= 0.5f;
        y -= 0.5f;
        placement = new Vector2 ( x, y );

        return placement;
    }
}
