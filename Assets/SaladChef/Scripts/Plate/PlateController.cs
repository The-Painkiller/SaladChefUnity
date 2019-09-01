using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MVC Controller for Plate implementation.
/// </summary>
public class PlateController : MonoBehaviour
{
    private PlateModel _model;
    private PlateView _view;

    private void Awake ( )
    {
        _model = GetComponent<PlateModel> ( );
        _view = GetComponent<PlateView> ( );
    }

    /// <summary>
    /// Places a Veggie on the plate.
    /// Assigns Veggie to its model and displays it through view.
    /// </summary>
    /// <param name="veg"></param>
    public void PlaceVeggie ( Veggie veg )
    {
        if ( _model._currentVeggie != null )
            return;

        _model._currentVeggie = veg;
        _view.PlaceVeggie ( veg );
    }

    /// <summary>
    /// Passes the veggie back to the interacting player, and removes it from itself at the same time.
    /// </summary>
    /// <returns></returns>
    public Veggie RemoveVeggie ( )
    {
        Veggie veg = _model._currentVeggie;
        veg._veggieDetails._veggieType = _model._currentVeggie._veggieDetails._veggieType;
        _model._currentVeggie = null;
        _view.RemoveVeggie ( );
        return veg;
    }

    /// <summary>
    /// Checks if Veggie is kept on it or not.
    /// </summary>
    /// <returns></returns>
    public bool IsVeggieAvailable ( )
    {
        return _model._currentVeggie != null;
    }
}
