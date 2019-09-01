using System.Collections.Generic;
/// <summary>
/// Enum for PlayerIDs.
/// </summary>
public enum PlayerID
{
    None,
    Player01,
    Player02
}

/// <summary>
/// MVC Model to implement Player.
/// </summary>
public class PlayerModel : CharacterModelAbstract
{
    /// <summary>
    /// Constructor instantiates new PlayerModel with the given timer.
    /// </summary>
    /// <param name="time"></param>
    public PlayerModel ( float time )
    {
        _time = time;
    }

    /// <summary>
    /// Timer value to serve customers within.
    /// </summary>
    public override float _time { get; set; }

    /// <summary>
    /// Veggie class List type inventory
    /// </summary>
    public List<Veggie> _inventory = new List<Veggie> ( );

    public int _score { get; set; }
    public PlayerID _playerID { get; set; }

    /// <summary>
    /// Size restriction for the player's inventory at a time.
    /// </summary>
    public int _inventorySizeRestriction = 2;
}
