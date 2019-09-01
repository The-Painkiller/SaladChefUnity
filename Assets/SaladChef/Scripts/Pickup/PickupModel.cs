using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MVC Model class to implement Pickups.
/// Takes a PickupType property and PlayerID of eligible player.
/// </summary>
public class PickupModel : InteractableObject
{
    public PickupType _pickupType;
    public PlayerID _PlayerEligible;
}
