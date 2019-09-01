# Basic Overview
The project mainly uses an implementation of MVC for it’s main classes, such as Player, Customer, interactable objects, etc. However, wherever in a few instances of simple functionalities, basic components are written and attached to GameObjects in the scene.
The game’s left part, apart from customers and veggies, is dedicated to Player1 whereas right for Player2. This means, the extra plate and chopping board on the left can only be used by Player1, the score and timer UI on top left indicate the same for Player1, whereas on the right, the same goes for Player2.
Chopping board, extra plate, veggies, customer plates and pickups, all come under Interactable objects and can be interacted with the players’ respective interaction buttons.
When a player picks up a veggie, its name is displayed besides the player, to indicate that it was added to the player’s inventory. Same goes for the Salad, where a list of veggies is displayed together besides the player when it picks it up from the chopping board. On a technical note, in the full game, it can be easily extended to show a visual graphic of veggies as well as the salad, since on player’s interaction, the whole Veggie class is passed to the player. This class includes veggie type and sprite reference.
On Chopping board player locks for 5 seconds before it becomes free again. At this point even the veggie is transferred to another corner on the board, indicating that it is chopped or ready. As more veggies are added, they start adding up to this corner..
The customers turn red when angry, and green just before leaving, which indicates correct order.
The garbage bin turns the whole inventory of the player to 0.
Through an enum in GameManager, the game can be Single or Two Player.


# Controls
Player 1 movement happens with WSAD and E for interaction. 
Player 2 uses arrows for movement and Numpad 0 for interacting.


# Implementation Steps

## Enums
### InteractableObjectsEnum
 This stores names of interactable object types. This is used by PlayerController to differentiate the object its interacting with.
### ActivePlayers
 Used by GameManager to make the game 1 or 2 player.
### PickupType
 This enum has the types of pickups that a prefab can be chosen to be. This includes Speeder, Timer, and Scorer, with respective functionalities of Speeding up player movement for a few seconds, increasing its time and increasing its score.
### PlayerID
 This is used by a few functionalities in the game to differentiate between Players. PlayerID.None is mainly used by SeatManager to be able to punish bother players at once.
### VeggieTypes
 This enum is used by the struct, VeggieDetails in Veggie class to indicate a veggie type. In the scene it is also assigned to each veggie through inspector.

## General/Base Classes
### CharacterModelAbstract
This class is used as a parent class for Player as well as Customer classes. It has abstract properties of time and inventory, along with non-abstract flags of whether the character is locked and whether it has finished. The Abstract properties are useful for both Player and Customer, whereas lock flag is used for Player and finish flag for Customer.
### IInteractableObject/InteractableObject
This is a basic Interface with a single property of Enum InteractableObjects. It has a simple child class called InteractableObject. Every object Model in the game that can be interacted with, is extended with this child class. Currently this only holds one property, but can be extended further to have more data related properties. 
### IInputControl
IInputControl works as an interface for extending InputControl class. InputControl is then used for implementing Keyboard interactions. In future, based on different platforms more Input classes can be defined, extending from IInputControl Interface.
### GameManager:
GameManager is a normal Monobehaviour class that attaches to GameManager object in the scene. It initializes Players and Customers, keeps basic data used by the game overall, as well as interacts with other manager classes.
### SeatManager
SeatManager takes care of interacting with Customers, and acts as a bridge between Customers and GameManager with help of Actions invoking on certain conditions.
### PickupsManager
This class has prefabs of all the pickups that can be instantiated in the scene and generates a random pickup at a point in the game area, along with a possible link to a particular player.
### EndUIManager
This class displays the end screen, along with high scores, calls HighScoreManager to set new high scores and has the function to restart the game which is called by a button in the UI.
### HighScoreManager
HighScoreManager uses a class called PlayerPrefsX(courtesy wiki.unity3d.com) and stores high score data in PlayerPrefs. It even sets a high score after checking a condition that whether the passed current score is higher than any of the scores already in the data.
### MoveableAreaConstants
This is just a static class with some static readonly Vector values to keep in check the players’ moveable area as well as spawn point. It also generates a random placement coordinate for Pickups to spawn at.

# Commit Steps
### Scene Setup
The gameplay scene was created using basic planes for the white platform area, which also indicates the game’s moveable area. For veggies, chopping board, garbage bin, and plates, placeholder sprites were used. Veggies names use TextMesh. For end screen, scores and timers, UnityUI is used. All interactable objects have box triggers on them.

### Base Classes, Managers & Garbage Bin
CharacterModelAbstract, IInteractableObject, InteractableObject, IInputControl, GameManager, SeatManager were implemented.

Garbage bin here is simply an instance of InteractableObject class attached to the garbage bin visual in the scene and interacts with PlayerController to just set its model’s inventory to 0.

### Veggie
Veggie class extends from InteractableObject and implements a struct that stores a Sprite for visual, VeggieTypes enum type to define the type of veggie and a bool to check on customer’s side whether the veggie was first readied on the chopping board or not. This struct is used a VeggieDetails property by the Veggie class and is also useful to pass as data for other classes. Instances of this class are also attached to each Veggie in the scene.

### Input Implementation
InputControl class was implemented using IInputControl. This class, is passed keyboard keys to map every action by GameManager, using AssignInputs(). In Update() it reads its assigned keyboard mapping and invokes suitable Actions.


### Player Implementation
The Player is implemented using MVC. Player Model overrides _time from its parent. In other properties, it has _score, _playerID, _inventory and _inventorySizeRestriction.

PlayerView class is attached to the PlayerPrefab. It takes care of the triggers entering or exiting; triggers here being attached to all the interactable objects in the scene. Upon entering or exiting a trigger, the class simply invokes a suitable Action, and passes the triggering GameObject. Apart from this, it displays Inventory besides the player, and score and timer of the player on UI, references to which are passed by PlayerController, which in turn will get its reference of the UI objects from GameManager.

PlayerController class is where a lot of player related work happens. It is defined and attached to the Player prefab at runtime. On calling Init() from GameManager, it’s model, view and input control references are linked as well as action handlers for PlayerView and InputControl are added. It interacts with GameManager to pass and receive data, moves player as per InputControl’s actions, as well as takes suitable decisions depending on the Interactable object the player is interacting with.

Lastly a Player prefab is created which has a chef’s sprite, and 2 TextMesh objects for player’s name and inventory display. PlayerView class is attached to it, along with Box Collider and kinematic RigidBody for interacting with triggers.

### Customer Implementation
Customer is also extended by CharacterModelAbstract, and along with the override of _time, its properties include a flag for being angry, a flag was leaving satisfied, its own _inventory array and PlayerID of the serving player. The inventory in Player is a list whereas in its an array because the player’s inventory is pretty dynamic, while customer inventory, once set, doesn’t change.

CustomerView plays a basic role of displaying angry and happy looks on customer, using simple red and green color changes(which can be changed later to show different expressions) and displaying its own timer and order inventory using TextMesh.

CustomerController takes care of the actual interaction of the customer with the game. On Init() its model and view are linked, as passed from GameManager. Along with that its reference to the SeatManager is also linked. Apart from being instantiated by GameManager and passed model, view and SeatManager references on Init(), no other interaction of the CustomerController happens with GameManager. It interacts with SeatManager, which in turn interacts with GameManager. In the Update() it’s timer decreases depending on Time.deltaTime as well as a _timerDecreaseSpeed multiplier that will become higher than 1 if the customer becomes angry on being served the wrong order. ServeOrder() is called by SeatManager, which passes it a Veggie reference, and the serving player’s PlayerID. Depending on whether the string sequence of it’s model’s inventory and the order’s name is the same or not, and whether the _isReady flag was set on each veggie or not, Leave() or Anger() are called. _isReady basically is a check to see if the veggies went through the chopping board or served raw. Inside Anger(), the model’s _isAngry flag is set to true, which is later read by Leave(). Leave() then calculates whether the customer left satisfied(served before timer ran out), was it angry, and whether the player is supposed to be gifted a pickup, and passes the information to SeatManager by invoking appropriate Actions.

Lastly a prefab, Customer, is created using SpriteRenderer and TextMeshes for timer and inventory display, and CustomerView class is attached to it.

### Chopping Board and Extra Plate Implementation
Both ChoppingBoardModel, and PlateModel extend from InteractableObject.

ChoppingBoardModel has properties for readied(or chopped) salad, ongoing(chopping) veggie as well as a flag for whether it is busy or not. A Set/Get property also takes care of adding up the chopped veggies to the salad visibly, by adding their names to the List.

ChoppingBoardView simply renders or removes the ongoing and readied veggie sprites and names.

ChoppingBoardController interacts with PlayerController and using its model and view sets data and visuals on player interaction.

Plate is a simple implementation with its model, PlateModel having a single property for storing the current Veggie that the player has passed on to it, PlateView showing or removing the Veggie’s sprite and name visuals, and PlateController acting as a bridge for PlayerController to place, remove or tell the Player whether it is already holding a Veggie or not.

Instances of ChoppingBoard’s and Plate’s model, view and controller classes are attached to their visuals in the scene.

### Seat Implementation
Seats in the game are implemented with MVC as well and SeatManager interacts with SeatControllers, which are attached to the seats in the scene along with SeatModel, and SeatView.

SeatModel has 2 properties; one for the customer that comes on to it, and the other for customer’s order.

SeatView is really simple and just holds a reference in the scene for the location in the scene where the customer will appear.

SeatController, interacting with SeatManager, gets assigned a Customer(CustomerController). It forwards the order(Veggie array) to the customer using ServerOrder(), tells whether it is empty and removes customer on leaving. It also looks out for Action invokes of CustomerLeft and GiftPlayer done by its assigned Customer and forwards the suitable data to SeatManager on each of the Action.

### Pickups
PickupModel extends from InteractableObject. It has 2 properties of its own, which are for defining the type of Pickup that it is and the PlayerID of the player that is eligible to use it.

PickupView is a simple class that only places the pickup at a given location.

PickupController interacts with PickupsManager. It takes its reference of the location and eligible PlayerID and passes to model and view, as well as provides the PickupType and the flag whether the interacting player is eligible.

PickupsManager interacts with GameManager and has a reference to prefabs of all the pickups. 

### EndGame
The game over scenario is implemented using EndUIManager, and HighScoreManager which interact with GameManager.

EndUIManager is a simple class that has references of the end screen UI elements, displays high scores, winner, and has Restart function to begin the game again.

HighScoreManager implements score saving functionality along with passing the high scores list when required. All functions in this class are static.



# Notes
The game is thus partially implemented with a lot of basic gameplay functionality in place.Here are some of the extensions that can be made to this in the future to make a more robust, and full game:
An option of single or two player mode can be added on a start screen.
Number of veggies in customers’ order and that the player or the extra plates can handle at a given time can easily be tweaked.
The game can be ported to multiple platforms by tweaking some basic functionality as the IInputControl can be used to make more InputControl classes for each platform. In case of mobiles, the multiplayer will then be implemented using networking.
Apart from veggies there can be a combination of other side orders and drinks kept in the playable area and ordered by the customer.
There can be more pickups.
There can also be an economy system in the game which will let players earn some money along with score, that can be used to unlock better veggies, better pickups and other such things.
