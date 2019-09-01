using System.Collections.Generic;
/// <summary>
/// MVC Model class for implementing Customer.
/// Extends from CharacterModelAbstract  class.
/// </summary>
public class CustomerModel : CharacterModelAbstract
{
    /// <summary>
    /// Constructor for initializing the Model instance.
    /// Resriction of keeping _inventory array of CharacterModelAbstract to length 1.
    /// </summary>
    /// <param name="time">Passes time in float to _time property of model</param>
    /// <param name="inventory">Passes Veggie inventory to _inventory property.</param>
    public CustomerModel ( float time, Veggie [] inventory )
    {
        _time = time;
        _inventory = inventory;
    }

    /// <summary>
    /// Time that customer has before leaving.
    /// </summary>
    public override float _time { get; set; }

    /// <summary>
    /// Customer's order inventory.
    /// </summary>
    public Veggie[] _inventory { get; set; }

    /// <summary>
    /// Whether it is angry.
    /// </summary>
    public bool _isAngry { get; set; }

    /// <summary>
    /// Whether it was satisfied by correct order.
    /// </summary>
    public bool _wasSatisfied { get; set; }

    /// <summary>
    /// Which player served it last.
    /// </summary>
    public PlayerID _servingPlayer { get; set; }
}
