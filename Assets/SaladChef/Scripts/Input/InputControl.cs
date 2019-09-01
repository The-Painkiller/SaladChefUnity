using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// InputControl class extending from IInputControl Interface.
/// Used to map keyboard keys passed from GameManager, to different actions.
/// </summary>
public class InputControl : MonoBehaviour, IInputControl
{
    public Action ForwardPressed { get; set; }
    public Action BackwardPressed { get; set; }
    public Action LeftPressed { get; set; }
    public Action RightPressed { get; set; }
    public Action InteractionPressed { get; set; }

	private KeyCode _forwardKey;
	private KeyCode _backwardKey;
	private KeyCode _leftKey;
	private KeyCode _rightKey;
	private KeyCode _interactionKey;

    /// <summary>
    /// Called to assign input keyboard keys for different actions.
    /// </summary>
    /// <param name="fwd">Key for forward movement.</param>
    /// <param name="bck">Key for backward movement.</param>
    /// <param name="rgt">Key for right movement.</param>
    /// <param name="lft">Key for left movement</param>
    /// <param name="intrct">Key for interaction.</param>
	public void AssignInputs (KeyCode fwd, KeyCode bck, KeyCode rgt, KeyCode lft, KeyCode intrct)
	{
		_forwardKey = fwd;
		_backwardKey = bck;
		_leftKey = lft;
		_rightKey = rgt;
		_interactionKey = intrct;
	}

	void Update()
    {
        if ( Input.GetKeyDown ( _forwardKey ) )
        {
            if ( ForwardPressed != null )
            {
                ForwardPressed.Invoke ( );
            }
        }

        if ( Input.GetKeyDown ( _backwardKey ) )
        {
            if ( BackwardPressed != null )
            {
                BackwardPressed.Invoke ( );
            }
        }

        if ( Input.GetKeyDown ( _leftKey ) )
        {
            if ( LeftPressed != null )
            {
                LeftPressed.Invoke ( );
            }
        }

        if ( Input.GetKeyDown ( _rightKey ) )
        {
            if ( RightPressed != null )
            {
                RightPressed.Invoke ( );
            }
        }

        if ( Input.GetKeyDown ( _interactionKey ) )
        {
            if ( InteractionPressed != null )
            {
                InteractionPressed.Invoke ( );
            }
        }

    }
}
