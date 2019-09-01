using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

/// <summary>
/// MVC View class to implement Player.
/// Attached to player and looks for Triggers entering and exiting in the game area.
/// </summary>
public class PlayerView : MonoBehaviour
{
    /// <summary>
    /// Actions to invoke on entering or exiting InteractableObject triggers.
    /// </summary>
    public Action<GameObject> InteractableObjectEntered;
    public Action<GameObject> InteractableObjectExited;

    /// <summary>
    /// Get property for returning where the player is in the game area.
    /// </summary>
    public Transform _playerTransform { get { return transform; } }

    /// <summary>
    /// Visuals for player.
    /// </summary>
    public SpriteRenderer _playerSprite;
    public TextMesh _playerNameMesh;
    public TextMesh _playerInventory;

    /// <summary>
    /// UI elements for showing player's stats.
    /// </summary>
    private Text _scoreText;
    private Text _timerText;

    private void OnTriggerEnter ( Collider triggeringObject )
    {
        InteractableObjectEntered.Invoke ( triggeringObject.gameObject );
    }

    private void OnTriggerExit ( Collider triggeringObject )
    {
        InteractableObjectExited.Invoke ( triggeringObject.gameObject );
    }

    /// <summary>
    /// To be called on instantiating Model.
    /// Assigns player's color, name and UI elements.
    /// </summary>
    /// <param name="playerColor">Takes Color to assign to player</param>
    /// <param name="playerName">Takes string to assign name to player</param>
    /// <param name="scoreText">Takes UI Text to assign as player's score display</param>
    /// <param name="timerText">Takes UI Text to assign as player's timer display</param>
    public void AssignPlayerViewDetails ( Color playerColor, string playerName, Text scoreText, Text timerText)
    {
        _playerSprite.color = playerColor;
        _playerNameMesh.text = playerName;
        _scoreText = scoreText;
        _timerText = timerText;
    }

    /// <summary>
    ///  Displays player's inventory by going through each element in the array and showing in in a new line in TextMesh
    /// </summary>
    /// <param name="inventory">Takes the array of Veggies to display</param>
    public void DisplayInventory ( Veggie [] inventory )
    {
        _playerInventory.text = string.Empty;
        foreach ( Veggie v in inventory )
        {
			if(v != null)
				_playerInventory.text += "\n" + v._veggieDetails._veggieType;
        }
    }

    /// <summary>
    /// Displays player's timer on UI.
    /// </summary>
    /// <param name="timer">Takes float timer value.</param>
    public void DisplayTimer ( float timer )
    {
        _timerText.text = ( ( int ) timer ).ToString ( );
    }

    /// <summary>
    /// Displays player score on UI
    /// </summary>
    /// <param name="score">Takes int score value</param>
    public void DisplayScore ( int score )
    {
        _scoreText.text = score.ToString ( );
    }
}
