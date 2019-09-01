using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MVC View for implementing Chopping Board.
/// </summary>
public class ChoppingBoardView : MonoBehaviour
{
	public SpriteRenderer _ongoingVeggieSprite;
	public SpriteRenderer _readiedVeggieSprite;
	public TextMesh _ongoingVeggieName;
	public TextMesh _readiedVeggieName;

    /// <summary>
    /// Displays the ongoing Veggie on the board.
    /// </summary>
    /// <param name="veg">Veggie passed to ChoppingBoard for beginning to chop.</param>
	public void ChopNextVeggie (Veggie veg)
	{
		_ongoingVeggieSprite.sprite = veg._veggieDetails._veggieSprite;
		_ongoingVeggieName.text = veg._veggieDetails._veggieType.ToString();
	}

    /// <summary>
    /// Displays the list of readied Veggies.
    /// </summary>
    /// <param name="veg"></param>
	public void SetReadiedVeggieAside (Veggie [] veg)
	{
		_readiedVeggieSprite.sprite = veg[0]._veggieDetails._veggieSprite;
        _readiedVeggieName.text = "";
        foreach ( Veggie v in veg )
        {
            _readiedVeggieName.text += v._veggieDetails._veggieType.ToString ( ) + "\n";
        }
		
		EmptyChopper ();
	}

    /// <summary>
    /// Used for emptying the whole chopping board view.
    /// </summary>
	public void EmptyChoppingBoard ()
	{
		_ongoingVeggieSprite.sprite = null;
		_readiedVeggieSprite.sprite = null;
		_ongoingVeggieName.text = string.Empty;
		_readiedVeggieName.text = string.Empty;
	}

    /// <summary>
    /// Sets only the ongoing veggie view to empty. Called by SetReadiedVeggieAside().
    /// </summary>
	private void EmptyChopper ()
	{
		_ongoingVeggieName.text = string.Empty;
		_ongoingVeggieSprite.sprite = null;
	}
}
