using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// MVC Controller class to implement Player.
/// Instantiated and attached to Player prefab by GameManager.
/// </summary>
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Its model and view instances.
    /// </summary>
    private PlayerModel _playerModel;
    private PlayerView _playerView;

    /// <summary>
    /// Its InputControl instance. It can be passed any class extending from IInputControl depending on platforms.
    /// </summary>
    private IInputControl _playerInputControl;

    /// <summary>
    /// Current interactable GameObject its interacting with.
    /// </summary>
    private GameObject _currentInteractingGameObject;

    /// <summary>
    /// Its chopping board's controller.
    /// </summary>
    private ChoppingBoardController _choppingBoardControl;

    /// <summary>
    /// Its plate's controller.
    /// </summary>
    private PlateController _plateControl;

    /// <summary>
    /// Movement speed multiplier. It changes to higher value when Player gets a Speeder Pickup.
    /// </summary>
    private float _movementMultiplier;

    /// <summary>
    /// Initializes PlayerController, linking its references and adding handlers to PlayerView and InputControl's Actions.
    /// </summary>
    /// <param name="model">Takes PlayerModel instance</param>
    /// <param name="view">Takes PlayerView instance.</param>
    /// <param name="inputControl">Takes IInputControl instance</param>
    public void Init ( PlayerModel model, PlayerView view, IInputControl inputControl )
    {
        ///This changes to a higher value if Speeder pickup is used.
        _movementMultiplier = 0.5f;

        _playerModel = model;
        _playerView = view;
        _playerInputControl = inputControl;
        
        _playerView.InteractableObjectEntered += OnInteractableObjectEntered;
        _playerView.InteractableObjectExited += OnInteractableObjectExited;
        _playerInputControl.BackwardPressed += OnBackPressed;
        _playerInputControl.ForwardPressed += OnForwardPressed;
        _playerInputControl.LeftPressed += OnLeftPressed;
        _playerInputControl.RightPressed += OnRightPressed;
        _playerInputControl.InteractionPressed += OnInteractionPressed;
    }

    /// <summary>
    /// Receives negative or positive score from GameManager and adds up to its model's score.
    /// </summary>
    /// <param name="score"></param>
    public void ReceiveScore ( int score )
    {
        _playerModel._score += score;
        _playerView.DisplayScore ( _playerModel._score );
    }

    /// <summary>
    /// Returns its PlayerID.
    /// </summary>
    /// <returns></returns>
    public PlayerID GetPlayerID ( )
    {
        return _playerModel._playerID;
    }

    /// <summary>
    /// Sets its player time.
    /// </summary>
    /// <param name="time"></param>
    public void SetTime ( float time )
    {
        _playerModel._time += time;
    }

    /// <summary>
    /// Returns whether or not the player's time has run out.
    /// </summary>
    /// <returns></returns>
    public bool HasPlayerTimeFinished ( )
    {
        return _playerModel._hasFinished;
    }

    /// <summary>
    /// Returns player score.
    /// </summary>
    /// <returns></returns>
    public int GetScore ( )
    {
        return _playerModel._score;
    }

    private void OnDestroy ( )
    {
        _playerView.InteractableObjectEntered -= OnInteractableObjectEntered;
        _playerView.InteractableObjectExited -= OnInteractableObjectExited;
        _playerInputControl.BackwardPressed -= OnBackPressed;
        _playerInputControl.ForwardPressed -= OnForwardPressed;
        _playerInputControl.LeftPressed -= OnLeftPressed;
        _playerInputControl.RightPressed -= OnRightPressed;
        _playerInputControl.InteractionPressed -= OnInteractionPressed;
    }

    /// <summary>
    /// Handler for InputControl InteractionPressed.
    /// If player is busy or there's no interacting object available, do nothing.
    /// Calls Interaction() otherwise.
    /// </summary>
    private void OnInteractionPressed ( )
    {
        if ( _currentInteractingGameObject == null || _playerModel._isLocked)
			return;

		Interaction ( _currentInteractingGameObject );
    }

    /// <summary>
    /// Handler for InputControl RightPressed.
    /// Checks for moveable area and moves player as per movementMultiplier.
    /// </summary>
    private void OnRightPressed ( )
    {
		if (_playerModel._isLocked)
			return;

        if ( ( _playerView._playerTransform.localPosition + Vector3.right * _movementMultiplier ).x > MoveableAreaConstants._playerMoveableAreaMax.x )
            return;

        _playerView._playerTransform.localPosition += (Vector3.right * _movementMultiplier);
    }

    /// <summary>
    /// Handler for InputControl LeftPressed.
    /// Checks for moveable area and moves player as per movementMultiplier.
    /// </summary>
    private void OnLeftPressed ( )
    {
		if (_playerModel._isLocked)
			return;

		if ( ( _playerView._playerTransform.localPosition + Vector3.left * _movementMultiplier ).x < MoveableAreaConstants._playerMoveableAreaMin.x )
            return;

        _playerView._playerTransform.localPosition += (Vector3.left * _movementMultiplier);
    }

    /// <summary>
    /// Handler for InputControl ForwardPressed.
    /// Checks for moveable area and moves player as per movementMultiplier.
    /// </summary>
    private void OnForwardPressed ( )
    {
		if (_playerModel._isLocked)
			return;

		if ( ( _playerView._playerTransform.localPosition + Vector3.up * _movementMultiplier ).y > MoveableAreaConstants._playerMoveableAreaMax.y )
            return;
        _playerView._playerTransform.localPosition += (Vector3.up * _movementMultiplier);
    }

    /// <summary>
    /// Handler for InputControl BackwardPressed.
    /// Checks for moveable area and moves player as per movementMultiplier.
    /// </summary>
    private void OnBackPressed ( )
    {
		if (_playerModel._isLocked)
			return;

		if ( ( _playerView._playerTransform.localPosition + Vector3.down * _movementMultiplier ).y < MoveableAreaConstants._playerMoveableAreaMin.y )
            return;
        _playerView._playerTransform.localPosition += (Vector3.down * _movementMultiplier);
    }

    /// <summary>
    /// Called when PlayerView's OnTriggerEnter is triggered on an InteractableObject.
    /// </summary>
    /// <param name="interactingObject"></param>
    private void OnInteractableObjectEntered ( GameObject interactingObject )
    {
        _currentInteractingGameObject = interactingObject;
    }

    /// <summary>
    /// Called when PlayerView's OnTriggerExit is triggered on leaving an InteractableObject.
    /// </summary>
    /// <param name="interactingObject"></param>
    private void OnInteractableObjectExited ( GameObject interactingObject )
    {
        _currentInteractingGameObject = null;
    }

    /// <summary>
    /// Interaction implementation for different types of InteractableObject's .
    /// Takes the GameObject of the InteractableObject.
    /// </summary>
    /// <param name="obj">InteractableObject GameObject is passed here.</param>
    private void Interaction ( GameObject obj )
    {
        IInteractableObject interactingObject = obj.GetComponent<IInteractableObject> ( );

        switch ( interactingObject._ObjectType )
        {
            ///Interaction with Garbage Bin. If Player's inventory has something, set it to null.
            case InteractableObjects.Garbage:
                if ( GetPlayerInventoryEmpty ( ) )
                    return;

                RefreshInventory ( );
                break;

              ///Interaction with Chopping Board
            case InteractableObjects.ChoppingBoard:
                if ( obj.tag != "Setup" + _playerModel._playerID.ToString ( ) )
                    return;

				_choppingBoardControl = obj.GetComponent<ChoppingBoardController> ();
				ChoppingBoardInteraction ( );
				break;

            ///Interaction with Plate.
            case InteractableObjects.Plate:
                if ( obj.tag != "Setup" + _playerModel._playerID.ToString ( ) )
                    return;

                _plateControl = obj.GetComponent<PlateController> ( );
                PlateInteraction ( );
                break;

              ///Interaction with Veggies A B C D E & F.
            case InteractableObjects.Veggies:
                if ( GetPlayerInventoryFull ( ) )
                    return;

				Veggie veg = obj.GetComponent<Veggie>();
                _playerModel._inventory.Add ( veg );
                _playerView.DisplayInventory ( _playerModel._inventory.ToArray() );
                break;

              ///Interaction with customer's seat.
              ///Serves the order to the customer then empties the player inventory.
            case InteractableObjects.Seat:
                if ( GetPlayerInventoryEmpty ( ) )
                    return;

                SeatController seatController = obj.GetComponent<SeatController> ( );
                seatController.ServeOrder ( _playerModel._inventory.ToArray(), _playerModel._playerID );
                RefreshInventory ( );
                break;

              ///Interaction with Pickup.
            case InteractableObjects.Pickup:
                PickupController pickup = obj.GetComponent<PickupController> ( );
                if ( !pickup.IsPlayerEligible ( _playerModel._playerID ) )
                    return;

                PickupInteraction ( pickup.GetPickupType ( ) );
                Destroy ( obj.gameObject );
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Implementation of Pickup's interaction.
    /// Score, speed, and time changed as per the pickup used.
    /// </summary>
    /// <param name="pickupType">Type of the Pickup that was used.</param>
    private void PickupInteraction ( PickupType pickupType )
    {
        switch ( pickupType )
        {
            case PickupType.Scorer:
                _playerModel._score += 20;
                _playerView.DisplayScore ( _playerModel._score );
                break;

            case PickupType.Speeder:
                _movementMultiplier = 1;
                break;

            case PickupType.Timer:
                SetTime ( 15 );
                break;
        }
    }

    /// <summary>
    /// Removes all Veggie class instances and sets the inventory members back to null.
    /// </summary>
    private void RefreshInventory ( )
    {
        _playerModel._inventory.Clear ( );
        _playerView.DisplayInventory ( _playerModel._inventory.ToArray ( ) );
    }

    /// <summary>
    /// Plate interaction implementation.
    /// If plate is empty, and player has a Veggie, put the veggie to the plate.
    /// If plate is not empty and player has Veggies, do nothing.
    /// If plate is not empty and player has no veggie, put the Veggie back on to the player.
    /// </summary>
    private void PlateInteraction ( )
    {
        if ( _plateControl.IsVeggieAvailable ( ) && !GetPlayerInventoryFull ( ))
        {
            _playerModel._inventory.Add ( _plateControl.RemoveVeggie ( ) );
            _playerView.DisplayInventory ( _playerModel._inventory.ToArray() );
        }
        else
        {
            if ( !GetPlayerInventoryEmpty ( ) )
            {
                _plateControl.PlaceVeggie ( _playerModel._inventory [ 0 ] );
                _playerModel._inventory.RemoveAt ( 0 );
                _playerView.DisplayInventory ( _playerModel._inventory.ToArray ( ) );
            }
        }
    }

    /// <summary>
    /// Interaction implementation for Chopping board.
    /// If player has nothing and chopping board has a salad, put it on to the Player.
    /// If player has nothing and there's nothing on chopping board, do nothing.
    /// If player has a Veggie and chopping board is free, lock player, lock chopping board and start chopping.
    /// </summary>
	private void ChoppingBoardInteraction ()
	{
		if (GetPlayerInventoryEmpty() && _choppingBoardControl.IsSaladAvailable() && !GetPlayerInventoryFull())
		{
			_playerModel._inventory = _choppingBoardControl.GetReadiedSalad ();
			_playerView.DisplayInventory (_playerModel._inventory.ToArray());
            return;
		}
		else
		{
            if ( GetPlayerInventoryEmpty ( ) )
                return;

			_playerModel._isLocked = true;
            _choppingBoardControl.ChopNextVeggie ( _playerModel._inventory [ 0 ] );
            _playerModel._inventory.RemoveAt ( 0 );
			
			_playerView.DisplayInventory (_playerModel._inventory.ToArray());
            StartCoroutine ( PlayerBusy ( ) );
		}
	}

    /// <summary>
    /// Returns if Player's inventory is empty.
    /// </summary>
    /// <returns></returns>
    bool GetPlayerInventoryEmpty ( )
    {
        return _playerModel._inventory.Count == 0;
    }

    /// <summary>
    /// Returns if Player's inventory is full.
    /// </summary>
    /// <returns></returns>
    bool GetPlayerInventoryFull ( )
    {
        return _playerModel._inventory.Count == _playerModel._inventorySizeRestriction;
    }

    /// <summary>
    /// Coroutine to keep player busy for given time, to indicate chopping process.
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayerBusy ( )
    {
        yield return new WaitForSeconds ( 5f );
        _choppingBoardControl.SetReadiedVeggie ( );
        _playerModel._isLocked = false;
    }

    /// <summary>
    /// Speeder pickup effect is disabled here after a given time.
    /// </summary>
    /// <returns></returns>
    IEnumerator DisableSpeeder ( )
    {
        yield return new WaitForSeconds ( 10f );
        _movementMultiplier = 0.5f;
    }

    private void Update ( )
    {
        if ( _playerModel._hasFinished )
            return;

        _playerModel._time -= Time.deltaTime;
        _playerView.DisplayTimer ( _playerModel._time );

        ///If the Player's time has run out, it can't do anything more. Set its _hasFinished flag to True.
        if ( _playerModel._time <= 0 )
        {
            _playerModel._isLocked = true;
            _playerModel._hasFinished = true;
        }
    }
}
