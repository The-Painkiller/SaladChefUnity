using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MVC View class for implementing Pickups.
/// </summary>
public class PickupView : MonoBehaviour
{
    /// <summary>
    /// Places the pickup it is attached to, at a given location.
    /// </summary>
    /// <param name="location"></param>
    public void PlacePickup ( Vector3 location )
    {
        gameObject.transform.position = location;
    }
}
