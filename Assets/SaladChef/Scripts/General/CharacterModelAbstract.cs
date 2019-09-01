/// <summary>
/// Abstract class that acts as a base class for Player and Customer classes.
/// </summary>
public abstract class CharacterModelAbstract
{
    /// <summary>
    /// Time property. 
    /// For player this represents its total time running out.
    /// For Customer it represents its waiting time running out.
    /// </summary>
    public abstract float _time { get; set; }

    /// <summary>
    /// Veggie Inventory property.
    /// </summary>
   // public Veggie [] _inventory { get; set; }

    /// <summary>
    /// Flag to define whether the character is locked/busy.
    /// Mainly used by Player.
    /// </summary>
    public bool _isLocked { get; set; }

    /// <summary>
    /// Flag to define whether the character has finished.
    /// </summary>
    public bool _hasFinished { get; set; }
}