using System;

/// <summary>
/// Interface for InputControl.
/// Can be used to extend Input controllers for different platforms.
/// </summary>
public interface IInputControl
{
    Action ForwardPressed { get; set; }
    Action BackwardPressed { get; set; }
    Action LeftPressed { get; set; }
    Action RightPressed { get; set; }
    Action InteractionPressed { get; set; }
}