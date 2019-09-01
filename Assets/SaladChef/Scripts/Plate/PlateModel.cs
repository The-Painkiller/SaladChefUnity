using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MVC Model implementation for Plate.
/// Simply takes 1 Veggie type property.
/// </summary>
public class PlateModel : InteractableObject
{
    public Veggie _currentVeggie { get; set; }
}
