using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MVC Model to implement Seats.
/// Takes CustomerController of the customer that is assigned to it.
/// Takes customer order of type Veggie array.
/// </summary>
public class SeatModel : InteractableObject
{
	public CustomerController _customer { get; set; }
	public Veggie [] _customerOrder { get; set; }
}
