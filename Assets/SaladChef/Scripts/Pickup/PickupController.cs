using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MVC Controller class for implementing Pickups.
/// </summary>
public class PickupController : MonoBehaviour
{
    private PickupModel _model;
    private PickupView _view;

    private void Awake ( )
    {
        _model = GetComponent<PickupModel> ( );
        _view = GetComponent<PickupView> ( );
    }

    /// <summary>
    /// On being called, initializes the pickup by passing a location and the PlayerID of the player eligible to use it.
    /// </summary>
    /// <param name="location"></param>
    /// <param name="player"></param>
    public void Init ( Vector3 location, PlayerID player )
    {
        _view.PlacePickup ( location );
        _model._PlayerEligible = player;
    }

    /// <summary>
    /// Checks if the player interacting with it is eligible to use it or not.
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public bool IsPlayerEligible ( PlayerID player )
    {
        return _model._PlayerEligible == player;
    }

    /// <summary>
    /// Returns the PickupType of the Pickup it is attached to.
    /// </summary>
    /// <returns></returns>
    public PickupType GetPickupType ( )
    {
        return _model._pickupType;
    }
}
