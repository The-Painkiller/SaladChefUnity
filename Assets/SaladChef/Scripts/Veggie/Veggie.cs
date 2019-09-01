using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enum to define Veggies.
/// </summary>
public enum VeggieTypes
{
    None,
    A,
    B,
    C,
    D,
    E,
    F
}

/// <summary>
/// Struct to describle some simple details for the Veggie class.
/// Sprite to show the Veggie visual.
/// VeggieTypes to define the type of veggie.
/// Bool to check if it is ready to serve or being served raw.
/// </summary>
[System.Serializable]
public struct VeggieDetails
{
	public Sprite _veggieSprite;
    public VeggieTypes _veggieType;
    public bool _isReady;

    public VeggieDetails (Sprite vegSprite, VeggieTypes vegType, bool isReady )
    {
        _veggieSprite = vegSprite;
        _veggieType = vegType;
        _isReady = isReady;
    }
}

/// <summary>
/// Simple InteractableObject child class to implement Veggies.
/// </summary>
public class Veggie : InteractableObject
{
    public VeggieDetails _veggieDetails  = new VeggieDetails(null, VeggieTypes.None, false);

	private void Awake ()
	{
        if ( gameObject != null )
        {
            _veggieDetails._veggieSprite = GetComponent<SpriteRenderer> ( ).sprite;
        }
        else
        {
            _veggieDetails._veggieType =VeggieTypes.None;
            _veggieDetails._veggieSprite = null;
        }
	}
}
