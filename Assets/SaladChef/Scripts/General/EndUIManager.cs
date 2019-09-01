using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// MonoBehaviour Class for implementing End Screen of the game.
/// Interacts with GameManager to display winner.
/// Also with help of HighScoreManager, displays past high scores.
/// </summary>
public class EndUIManager : MonoBehaviour
{
    [SerializeField]
    private Text _winnerText;
    [SerializeField]
    private Text [] _highScores;
    [SerializeField]
    private Text [] _highScoreNames;

    public GameObject _endGameScreen;
    
    /// <summary>
    /// Called by GameManager. Activates _endGameScreen GameObject that has end screen UI.
    /// </summary>
    /// <param name="highscore">Takes current player high score</param>
    /// <param name="PlayerName">Takes current high scoring player</param>
    public void DisplayEndGame ( int highscore, string PlayerName )
    {
        _winnerText.text = "Winner: " + PlayerName + "(" + highscore.ToString ( ) + ")";
        _endGameScreen.SetActive ( true );
        HighScoreManager.SetScores ( highscore, PlayerName );
        DisplayHighScores ( );
    }

    /// <summary>
    /// Simple level load call that just restarts the scene.
    /// Called from a button in end screen UI.
    /// </summary>
    public void Restart ( )
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene ( 0 );
    }

    /// <summary>
    /// Displays the list of past high scores.
    /// </summary>
    private void DisplayHighScores ( )
    {
        for ( int i = 0; i < _highScores.Length; i++ )
        {
            _highScores [ i ].text = HighScoreManager.GetScores ( ) [ i ].ToString();
            _highScoreNames [ i ].text = HighScoreManager.GetNames ( ) [ i ] + " : ";
        }
    }
}
