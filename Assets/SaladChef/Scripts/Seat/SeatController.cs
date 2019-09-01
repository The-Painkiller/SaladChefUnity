using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MVC Controller to implement Customer Seats that act in general as a bridge between Player and actual Customer.
/// </summary>
public class SeatController : MonoBehaviour
{
	private SeatModel _model;
	private SeatView _view;

	[SerializeField]
	private SeatManager _seatManager;

	private void Awake ()
	{
		_model = GetComponent<SeatModel> ();
		_view = GetComponent<SeatView> ();
	}

    /// <summary>
    /// Gets customer.
    /// Assigns seat coordinate to passed customer.
    /// Takes customer order and adds handlers to customer's CustomerLeft and GiftPlayer Actions.
    /// </summary>
    /// <param name="customer"></param>
	public void AssignCustomer (CustomerController customer)
	{
        customer.transform.position = _view._seatCoordinate.position;
		_model._customer = customer;
		_model._customerOrder = _model._customer.GetCustomerOrder ();

        _model._customer.CustomerLeft += OnCustomerLeft;
        _model._customer.GiftPlayer += OnGiftPlayer;
    }

    /// <summary>
    /// Handler for Customer's CustomerLeft Action.
    /// Removes customer from itself and passes data to SeatManager regarding whether the customer was angry and satisied along with serving PlayerID.
    /// </summary>
    /// <param name="wasSatisfied">Takes bool for whether customer left satisfied.</param>
    /// <param name="wasAngry">Takes bool for whether customer left angry.</param>
    /// <param name="servingPlayer">Takes PlayerID for servingPlayer.</param>
    private void OnCustomerLeft ( bool wasSatisfied, bool wasAngry, PlayerID servingPlayer )
    {
        RemoveCustomer ( );
        _seatManager.CustomerLeft ( wasSatisfied, wasAngry, servingPlayer );
    }

    /// <summary>
    /// Removes Action Handlers and sets customer properties of its model back to null.
    /// </summary>
    public void RemoveCustomer ()
	{
        _model._customer.CustomerLeft -= OnCustomerLeft;
        _model._customer.GiftPlayer -= OnGiftPlayer;
        _model._customer = null;
		_model._customerOrder = null;
	}

    /// <summary>
    /// Handler for Customer's GiftPlayer Action.
    /// Passes ID of serving player to SeatManager.
    /// </summary>
    /// <param name="servingPlayer">Takes PlayerID of serving player</param>
    private void OnGiftPlayer ( PlayerID servingPlayer )
    {
        _seatManager.GiftPlayer ( servingPlayer );
    }

    /// <summary>
    /// Returns the current Customer's CustomerModel
    /// </summary>
    /// <returns></returns>
    public CustomerController GetCustomer ( )
    {
        if ( _model._customer != null )
            return _model._customer;
        else
            return null;
    }

    /// <summary>
    /// Returns whether or not this seat is empty.
    /// </summary>
    /// <returns></returns>
	public bool IsEmpty ()
	{
		return _model._customer == null;
	}

    /// <summary>
    /// Serves order to the Customer it is referencing.
    /// Passes Veggie array order and PlayerID of the serving player.
    /// Called from PlayerController.
    /// </summary>
    /// <param name="order">Takes order of Veggie array</param>
    /// <param name="player">Takes PlayerID of serving player</param>
    public void ServeOrder ( Veggie [] order, PlayerID player )
    {
        if ( _model._customer == null ) return;

        _model._customer.ServeOrder ( order, player );
    }
}
