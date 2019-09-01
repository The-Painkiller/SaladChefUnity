using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MVC Controller to implement Customer.
/// </summary>
public class CustomerController : MonoBehaviour
{
    /// <summary>
    /// Action for invoking leaving customer related decisions.
    /// Passes the flags _wasSatisfied, _wasAngry and PlayerID of the Player that served it last.
    /// If none did, the the PlayerID remains set to None.
    /// </summary>
    public Action<bool, bool, PlayerID> CustomerLeft;

    /// <summary>
    /// Action to invoke gifting something to the player with PlayerID that served it real quick(70% time remaining).
    /// </summary>
    public Action<PlayerID> GiftPlayer;

    /// <summary>
    /// Multiplier for increasing the Customer time decrease faster.
    /// </summary>
    public float _timeDecreasePunishment = 1.5f;

    /// <summary>
    /// Variable for keeping the percent of time that should remain if the customer is supposed to be gifted.
    /// </summary>
    public float _percentOfTimeForGifting = 70;

    private CustomerModel _customerModel;
    private CustomerView _customerView;
    private SeatManager _seatManager;

    private float _originalTime;

    /// <summary>
    /// Default time decrease speed of the customer.
    /// </summary>
	private float _timerDecreaseSpeed = 1f;

    /// <summary>
    /// Called by GameManager on instantiating.
    /// Sets its model, view and seatManager, and initializes basic model and view properties.
    /// </summary>
    /// <param name="model">Takes CustomerModel instance</param>
    /// <param name="view">Takes CustomerView instance</param>
    /// <param name="seatManager">Takes SeatManager instance</param>
    public void Init ( CustomerModel model, CustomerView view, SeatManager seatManager )
    {
        _customerModel = model;
        _customerView = view;
		_seatManager = seatManager;
        _customerView.Init ( _customerModel._inventory );
        _customerModel._servingPlayer = PlayerID.None;
        _originalTime = _customerModel._time;
    }

    /// <summary>
    /// Returns the Veggie inventory that the customer has.
    /// </summary>
    /// <returns></returns>
	public Veggie [] GetCustomerOrder ()
	{
		if (_customerModel._inventory.Length == 0)
			return null;
		else
		return _customerModel._inventory;
	}

    /// <summary>
    /// Final implementation of ServeOrder.
    /// Checks whether the served order was correct or not and leaves or gets angry respectively.
    /// </summary>
    /// <param name="order">Takes the passed by the Player</param>
    /// <param name="servingPlayer">Takes the PlayerID of the serving player</param>
    public void ServeOrder ( Veggie [] order, PlayerID servingPlayer )
    {
        _customerModel._servingPlayer = servingPlayer;

        for ( int i = 0; i < GetCustomerOrder ( ).Length; i++ )
        {
            if ( GetCustomerOrder ( ) [ i ]._veggieDetails._veggieType != order [ i ]._veggieDetails._veggieType )
            {
                Anger ( );
                return ;
            }

            if ( !GetCustomerOrder ( ) [ i ]._veggieDetails._isReady )
            {
                Anger ( );
                return;
            }
        }
        Leave();
    }

    private void Update ( )
    {
        ///Decrease time if > 0 else Leave
        if ( !_customerModel._hasFinished )
        {
            if ( _customerModel._time > 0 )
            {
                _customerModel._time -= Time.deltaTime * _timerDecreaseSpeed;
                _customerView.UpdateTimer ( _customerModel._time );
            }
            else
            {
                Leave ( );
            }
        }
    }

    /// <summary>
    /// Implements Anger by flagging _IsAngry as true, setting the _timerDecreaseSpeed higher and looking visibly angry in the view.
    /// </summary>
    private void Anger ( )
    {
        _customerModel._isAngry = true;
		_timerDecreaseSpeed = _timeDecreasePunishment;
        _customerView.LookAngry ( );
    }

    /// <summary>
    /// Implements leaving of the customer.
    /// Sets _hasFinished to true.
    /// Sets _wasSatisfied to true or false depending on whether or not the time ran out.
    /// Also see if the player is supposed to be happy if the customer was served correct order and to gift if the time remaining was more than 70%
    /// Lastly invokes CustomerLeft action read by SeatController to which it was referenced.
    /// </summary>
    private void Leave ( )
    {
        _customerModel._hasFinished = true;
        _customerModel._wasSatisfied = _customerModel._time > 0;

        if ( _customerModel._wasSatisfied )
        {
            _customerView.LookHappy ( );
        }
        if ( ( _customerModel._time / _originalTime ) >= (_percentOfTimeForGifting/100) )
        {
            GiftPlayer.Invoke ( _customerModel._servingPlayer );
        }

        CustomerLeft.Invoke ( _customerModel._wasSatisfied, _customerModel._isAngry, _customerModel._servingPlayer );
        StartCoroutine ( StartLeaving ( ) );
    }

    /// <summary>
    /// Simple coroutine to add a small delay before customer leaves(destroyed)
    /// </summary>
    /// <returns></returns>
    IEnumerator StartLeaving ( )
    {
        yield return new WaitForSeconds ( 1f );
        Destroy ( gameObject );
    }
}
