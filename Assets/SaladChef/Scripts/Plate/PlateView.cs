using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MVC View for implementing Plate.
/// </summary>
public class PlateView : MonoBehaviour
{
    public SpriteRenderer _currentVeggieSprite;
    public TextMesh _currentVeggieName;

    /// <summary>
    /// Displays Veggie on screen.
    /// </summary>
    /// <param name="veg">Takes a veggie passed to it to display.</param>
    public void PlaceVeggie ( Veggie veg )
    {
        _currentVeggieName.text = veg._veggieDetails._veggieType.ToString();
        _currentVeggieSprite.sprite = veg._veggieDetails._veggieSprite;
    }

    /// <summary>
    /// Removes veggie from the screen.
    /// </summary>
    public void RemoveVeggie ( )
    {
        _currentVeggieSprite.sprite = null;
        _currentVeggieName.text = string.Empty;
    }
}
