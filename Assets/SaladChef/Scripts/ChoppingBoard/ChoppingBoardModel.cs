using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MVC Model for implementing Chopping Board.
/// </summary>
public class ChoppingBoardModel : InteractableObject
{
    /// <summary>
    /// Veggie class type variable for keeping ongoing(being chopped) veggie.
    /// </summary>
    public Veggie _ongoingVeggie { get; set; }

   /// <summary>
   /// List of readied Veggies on the table.
   /// </summary>
    public List<Veggie> _ReadiedVeggies = new List<Veggie>();
 
    /// <summary>
    /// public scope flag to see if the ChoppingBoard is busy or not.
    /// </summary>
    public bool _isChoppingBoardBusy = false;
}
