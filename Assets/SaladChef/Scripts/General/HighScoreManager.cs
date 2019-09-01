using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Class with static functions to get or set current high scores and past high score lists.
/// </summary>
public class HighScoreManager
{

    /// <summary>
    /// Max number of past high scores to be saved.
    /// </summary>
    private static int maxScoresSaved = 10;

    /// <summary>
    /// Scores array, that uses PlayerPrefsX class to store int [] to PlayerPrefs.
    /// </summary>
    private static int [] Scores
    {
        get
        {
            return PlayerPrefsX.GetIntArray ( "Scores", 0, maxScoresSaved );
        }
        set
        {
            PlayerPrefsX.SetIntArray ( "Scores", value );
        }
    }

    /// <summary>
    /// Player names array, that uses PlayerPrefsX class to store string [] to PlayerPrefs.
    /// </summary>
    private static string [] PlayerNames
    {
        get
        {
            return PlayerPrefsX.GetStringArray ( "PlayerNames", "--", maxScoresSaved );
        }
        set
        {
            PlayerPrefsX.SetStringArray ( "PlayerNames", value );
        }
    }

    /// <summary>
    /// Returns past Scores array.
    /// </summary>
    /// <returns></returns>
    public static int [] GetScores ( )
    {
        return Scores;
    }

    /// <summary>
    /// Returns past Scorer names array.
    /// </summary>
    /// <returns></returns>
    public static string [] GetNames ( )
    {
        return PlayerNames;
    }

    /// <summary>
    /// Checks and sets current high score and player into the past high scores list.
    /// </summary>
    /// <param name="currentHighScore">Takes current game's high score.</param>
    /// <param name="currentScorerName">Takes current game's high scorer's list.</param>
    public static void SetScores ( int currentHighScore, string currentScorerName)
    {
        int leastHighScore = Scores.Min ( );
        if ( leastHighScore > currentHighScore )
            return;

        int [] scoresTemp = Scores;
        string [] namestemp = PlayerNames;

        for ( int i = 0; i < scoresTemp.Length; i++ )
        {
            if ( leastHighScore == scoresTemp [ i ] )
            {
                scoresTemp [ i ] = currentHighScore;
                namestemp [ i ] = currentScorerName;
                break;
            }
        }

        Scores = scoresTemp;
        PlayerNames = namestemp;
    }
}
