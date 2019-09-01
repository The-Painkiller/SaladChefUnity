using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Enum for single or two player options.
/// </summary>
enum ActivePlayers
{
    OnePlayer,
    TwoPlayer
}

/// <summary>
/// Main gameplay class.
/// This class instantiates players, customers and pickups with help of their Controllers/Manager.
/// This class also interacts with SeatManager class to pass suitable information between itself and the seats.
/// It also interacts with EndUIManager to display end screen at a particular time when both player times have finished.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Player Models array
    /// </summary>
	private PlayerModel[] _playerModels;

    /// <summary>
    /// Player Controllers array
    /// </summary>
	private PlayerController[] _playerControllers;

    /// <summary>
    /// Player Views array
    /// </summary>
	private PlayerView[] _playerViews;

    /// <summary>
    /// Input Controls array
    /// </summary>
	private InputControl[] _playerInputControls;

    /// <summary>
    /// The timer after running of which to 0, a new customer could be generated. Depends if there is a seat empty or not.
    /// </summary>
    private float _customerGenerationTimer;

    /// <summary>
    /// Checks if the game has ended.
    /// </summary>
    private bool _hasGameEnded;

    /// <summary>
    /// Prefab for the Player
    /// </summary>
    public PlayerView _playerPrefab;

    /// <summary>
    /// Prefab for the Customer
    /// </summary>
    public CustomerView _customerPrefab;

    /// <summary>
    /// UI elements for players' score and timer Texts.
    /// </summary>
    public Text _scoreTextPlayer01;
    public Text _timeTextPlayer01;
    public Text _scoreTextPlayer02;
    public Text _timeTextPlayer02;
    
    /// <summary>
    /// Initial time given to players.
    /// </summary>
	public float _playerStartTimeInSeconds = 60f;

    /// <summary>
    /// Min and Max random frequency caps for generating a new customer.
    /// </summary>
    public float _minCustomerFrequencyInSeconds = 20f;
    public float _maxCustomerFrequencyInSeconds = 30f;

    /// <summary>
    /// Min and Max veggie counts in the salad being ordered by the customer.
    /// </summary>
    public int _minVeggiesCountInSalad = 2;
    public int _maxVeggiesCountInSalad = 3;

    /// <summary>
    /// Waiting time for the customer depending on small or big orders.
    /// </summary>
    public float _timeForSmallOrder = 60;
    public float _timeForBiggerOrder = 90;

    
    /// <summary>
    /// Array for veggies in the scene.
    /// </summary>
    public Veggie [] _veggies;

    /// <summary>
    /// Other Manager classes.
    /// </summary>
    public SeatManager _seatManager;
    public PickupsManager _pickupsManager;
    public EndUIManager _endUIManager;

	private void Start ()
	{
        _seatManager.LeavingCustomer += OnLeavingCustomer;
        _seatManager.GiftPlayer += OnGiftingPlayer;

        //Passed OnePlayer or TwoPlayer depending on how many players you want in the game.
        InitializePlayers (ActivePlayers.TwoPlayer);

        //Initial Customer generation.
        GenerateCustomer ( );
	}



    private void OnDestroy ( )
    {
        _seatManager.LeavingCustomer -= OnLeavingCustomer;
        _seatManager.GiftPlayer -= OnGiftingPlayer;
    }

    /// <summary>
    /// Initializes Players arrays and Input array depending on the number of players in the game.
    /// First player always gets WSADE keymap and red color indication, whereas second gets Arrow-0 keymap and green color indication.
    /// Both players instantiate in their own predefined locations.
    /// </summary>
    /// <param name="_numberOfPlayers"></param>
    private void InitializePlayers (ActivePlayers _numberOfPlayers )
	{
		int numOfPlayers = _numberOfPlayers == ActivePlayers.OnePlayer ? 1 : 2;

		_playerModels = new PlayerModel[numOfPlayers];
		_playerControllers = new PlayerController[numOfPlayers];
		_playerViews = new PlayerView[numOfPlayers];
        _playerInputControls = new InputControl [ numOfPlayers ];
        for (int i = 0; i < numOfPlayers; i++)
		{
            _playerViews [ i ] = GameObject.Instantiate<PlayerView> ( _playerPrefab );
            _playerInputControls [ i ] = _playerViews [ i ].gameObject.AddComponent<InputControl> ( );
            _playerModels [ i ] = new PlayerModel ( _playerStartTimeInSeconds );
            if ( i == 0 )
            {
                _playerViews [ i ].transform.localPosition = MoveableAreaConstants._firstPlayerSpawnPoint;
                _playerViews [ i ].AssignPlayerViewDetails ( Color.red, "Player1", _scoreTextPlayer01, _timeTextPlayer01 );
                _playerInputControls [ i ].AssignInputs ( KeyCode.W, KeyCode.S, KeyCode.D, KeyCode.A, KeyCode.E );
                _playerModels [ i ]._playerID = PlayerID.Player01;
            }
            else
            {
                _playerViews [ i ].transform.localPosition = MoveableAreaConstants._secondPlayerSpawnPoint;
                _playerViews [ i ].AssignPlayerViewDetails ( Color.green, "Player2", _scoreTextPlayer02, _timeTextPlayer02 );
                _playerInputControls [ i ].AssignInputs ( KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.Keypad0);
                _playerModels [ i ]._playerID = PlayerID.Player02;
            }
            _playerControllers [ i ] = _playerViews [ i ].gameObject.AddComponent<PlayerController> ( );
            _playerControllers [ i ].Init ( _playerModels [ i ], _playerViews [ i ], _playerInputControls [ i ] );
		}
	}

    /// <summary>
    /// Handler for GiftPlayer action coming from SeatManager.
    /// Passes PlayerID to PickupsManager, which then does its job.
    /// </summary>
    /// <param name="servingPlayer">Takes the player that's supposed to be gifted.</param>
    private void OnGiftingPlayer ( PlayerID servingPlayer )
    {
        _pickupsManager.GetRandomPickup ( servingPlayer );
    }

    /// <summary>
    /// Generates a customer whenever called, with a simple random salad combination with help of GenerateRandomSaladOrder().
    /// Only goes forth if a seat is empty, otherwise the timer is reset back to a random time.
    /// </summary>
    private void GenerateCustomer ( )
    {
        _customerGenerationTimer = UnityEngine.Random.Range ( _minCustomerFrequencyInSeconds, _maxCustomerFrequencyInSeconds );
        if ( _seatManager.IsASeatVacant ( ) )
        {
            Veggie [] saladOrder;
          
            float orderTime;
            GenerateRandomSaladOrder ( out saladOrder, out orderTime );
            CustomerModel customerModel = new CustomerModel ( orderTime, saladOrder );
            CustomerView customerView = GameObject.Instantiate<CustomerView> ( _customerPrefab );
            CustomerController customerControl = customerView.gameObject.AddComponent<CustomerController> ( );
            customerControl.Init ( customerModel, customerView, _seatManager );

            _seatManager.AssignSeat ( customerControl );
        }
    }

    /// <summary>
    /// Generates and outputs a random salad order for GenerateCustomer().
    /// </summary>
    /// <param name="salad">Outputs randomly generated salad combination.</param>
    /// <param name="time">Outputs a wait time for customer depending on the number of veggies in the salad combination.</param>
    private void GenerateRandomSaladOrder ( out Veggie [] salad, out float time )
    {
        int randomVeggiesCount = UnityEngine.Random.Range ( _minVeggiesCountInSalad, _maxVeggiesCountInSalad + 1);
        salad = new Veggie [randomVeggiesCount];
       
        for ( int i = 0; i < salad.Length; i++ )
        {
            salad [ i ] = _veggies [ UnityEngine.Random.Range ( 0, _veggies.Length ) ];
        }

        if ( randomVeggiesCount == _minVeggiesCountInSalad )
        {
            time = _timeForSmallOrder;
        }
        else
        {
            time = _timeForBiggerOrder;
        }
    }

    /// <summary>
    /// Handler for SeatManager's LeavingCustomer action.
    /// Passes the score coming from SeatManager(which can be positive or negative), to the suitable or both PlayerControllers.
    /// </summary>
    /// <param name="servingPlayer"></param>
    /// <param name="score"></param>
    private void OnLeavingCustomer ( PlayerID servingPlayer, int score )
    {
        foreach ( PlayerController p in _playerControllers )
        {
            if ( servingPlayer == PlayerID.None )
            {
                p.ReceiveScore ( score );
            }
            else
            {
                if ( p.GetPlayerID ( ) == servingPlayer )
                {
                    p.ReceiveScore ( score );
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Checks whether all the players have ran out of times and returns bool.
    /// </summary>
    /// <returns></returns>
    private bool HaveAllPlayersFinished ( )
    {
        foreach ( PlayerController p in _playerControllers )
        {
            if ( !p.HasPlayerTimeFinished ( ) )
                return false;
        }

        return true;
    }

    /// <summary>
    /// Run in Update. Keeps checking whether players have ran out of time.
    /// If not, and customer generation timer has ran out, then generates a customer.
    /// Otherwise flushes all seats, gets high score and calls for end screen.
    /// </summary>
    private void ManageEndGame ( )
    {
        if ( HaveAllPlayersFinished ( ) )
        {
            _hasGameEnded = true;
            int highScore = 0;
            string playerName = "";

            for ( int i = 0; i < _playerControllers.Length; i++ )
            {
                if ( highScore < _playerControllers [ i ].GetScore ( ) )
                {
                    highScore = _playerControllers [ i ].GetScore ( );
                    playerName = _playerControllers [ i ].GetPlayerID ( ).ToString ( );
                }
            }
            _seatManager.FlushSeats ( );
            _endUIManager.DisplayEndGame ( highScore, playerName );
            return;
        }
        if ( _customerGenerationTimer < 0 )
        {
            GenerateCustomer ( );
        }

    }

    private void Update ( )
    {
        if ( _hasGameEnded )
            return;

        _customerGenerationTimer -= Time.deltaTime;
        ManageEndGame ( );
    }
}
