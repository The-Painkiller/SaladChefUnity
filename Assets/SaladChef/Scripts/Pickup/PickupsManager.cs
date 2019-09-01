using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enum to determine the type of the Pickup.
/// Speeder: Increases player's movement for some time.
/// Timer: Increases overall time of the player.
/// Scorer: Increases player's score.
/// </summary>
public enum PickupType
{
    Speeder,
    Timer,
    Scorer
}

/// <summary>
/// Manager class to manage pickups, while interacting with GameManager.
/// </summary>
public class PickupsManager : MonoBehaviour
{
    /// <summary>
    /// Array of pickup prefabs that can be instantiated.
    /// </summary>
    public PickupController [] pickups;

    /// <summary>
    /// Instantiates a random prefab to be used by a specific PlayerID only.
    /// </summary>
    /// <param name="player">PlayerID of the player eligible</param>
    /// <returns></returns>
    public PickupController GetRandomPickup ( PlayerID player)
    {
        PickupController currentPickup;
        int randomIndex = Random.Range ( 0, pickups.Length );
        currentPickup = GameObject.Instantiate<PickupController> ( pickups [ randomIndex ] );
        currentPickup.Init ( MoveableAreaConstants.GetRandomPlacement ( ), player );
        return currentPickup;
    }
}
