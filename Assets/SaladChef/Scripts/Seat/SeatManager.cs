using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Monobehaviour based Manager class for managing seats.
/// </summary>
public class SeatManager : MonoBehaviour
{
    /// <summary>
    /// All seats with SeatController classes attached to them from the scene.
    /// </summary>
	[SerializeField]
	private SeatController[] seats;

    /// <summary>
    /// Actions of a customer leaving and player to be gifted.
    /// Handled by GameManager.
    /// </summary>
    public Action<PlayerID, int> LeavingCustomer;
    public Action<PlayerID> GiftPlayer;

    /// <summary>
    /// Default score to be increased per correct order, or decreased per incorrect order.
    /// </summary>
    public int score = 30;

    /// <summary>
    /// Checks all seats and returns if a seat is empty.
    /// </summary>
    /// <returns></returns>
	public bool IsASeatVacant ()
	{
		for (int i = 0; i < seats.Length; i++)
		{
			if (seats[i].IsEmpty())
			{
				return true;
			}
		}

		return false;
	}

    /// <summary>
    /// Assigns new CustomerController to an empty seat.
    /// </summary>
    /// <param name="customer"></param>
	public void AssignSeat (CustomerController customer)
	{
		for (int i = 0; i < seats.Length; i++)
		{
			if (seats[i].IsEmpty ())
			{
				seats[i].AssignCustomer (customer);
				break;
			}
		}
	}

    /// <summary>
    /// Implementation of decisions to be taken when a customer leaves.
    /// Takes into account if the customer was satisfied and left angry or not.
    /// Also passes the servingPlayer's ID if the customer was served correct/incorrect order.
    /// </summary>
    /// <param name="wasSatisfied"></param>
    /// <param name="wasAngry"></param>
    /// <param name="servingPlayer"></param>
    public void CustomerLeft ( bool wasSatisfied, bool wasAngry, PlayerID servingPlayer )
    {
        if ( wasSatisfied )
        {
            LeavingCustomer.Invoke ( servingPlayer, score );
        }
        else
        {
            if ( !wasAngry )
            {
                LeavingCustomer.Invoke ( PlayerID.None, -score );
            }
            else
            {
                LeavingCustomer.Invoke ( servingPlayer, -(score * 2) );
            }
        }
    }

    /// <summary>
    /// Function to call GiftPlayer Action that's handled by GameManager.
    /// </summary>
    /// <param name="servingPlayer">Takes the serving player's ID to make it eligible to use gift.</param>
    public void GiftPickup ( PlayerID servingPlayer )
    {
        GiftPlayer.Invoke ( servingPlayer );
    }

    /// <summary>
    /// Empties all seats.
    /// </summary>
    public void FlushSeats ( )
    {
        for ( int i = 0; i < seats.Length; i++ )
        {
            if ( seats [ i ] != null && seats[i].GetCustomer() != null)
                Destroy ( seats [ i ].GetCustomer ( ).gameObject );
        }
    }
}