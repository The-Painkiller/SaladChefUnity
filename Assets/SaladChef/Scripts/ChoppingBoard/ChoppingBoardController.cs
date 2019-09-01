using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MVC Controller for implementing Chopping Board.
/// Interacts with PlayerController and its own ChoppingBoardModel and ChoppingBoardView classes
/// </summary>
public class ChoppingBoardController : MonoBehaviour
{
    [SerializeField]
	private ChoppingBoardModel _model;
    [SerializeField]
    private ChoppingBoardView _view;

    /// <summary>
    /// Frees ChoppingBoard to allow next veggie and sets aside the already attached veggie 
    /// by adding it up in its ReadiedVeggies property.
    /// </summary>
	public void SetReadiedVeggie ()
	{
		_model._isChoppingBoardBusy = false;

		if (_model._ongoingVeggie == null)
			return;

        _model._ongoingVeggie._veggieDetails._isReady = true;
        _model._ReadiedVeggies.Add ( _model._ongoingVeggie );
        _view.SetReadiedVeggieAside ( _model._ReadiedVeggies.ToArray() );
        _model._ongoingVeggie = null;
	}

    /// <summary>
    /// Locks ChoppingBoard.
    /// Sets the Veggie passed to it as ongoing.
    /// </summary>
    /// <param name="veg"></param>
	public void ChopNextVeggie (Veggie veg)
	{
		if (_model._isChoppingBoardBusy)
			return;

        _model._ongoingVeggie = new Veggie ( );
		_model._ongoingVeggie = veg;
		_view.ChopNextVeggie (veg);
		_model._isChoppingBoardBusy = true;
	}

    /// <summary>
    /// Returns whether there is a ready salad available(ReadiedVeggies count 0 or not) 
    /// </summary>
    /// <returns></returns>
    public bool IsSaladAvailable ( )
    {
        return _model._ReadiedVeggies.Count != 0;
    }

    /// <summary>
    /// Returns the list of Veggies inside ReadiedVeggies property.
    /// Then empties the ChoppingBoard.
    /// </summary>
    /// <returns></returns>
	public List<Veggie> GetReadiedSalad ()
	{
		List<Veggie> salad = new List<Veggie>(_model._ReadiedVeggies);
		_model._ongoingVeggie = null;
		_model._ReadiedVeggies.Clear();
		_view.EmptyChoppingBoard ();
        return salad;
    }
}
