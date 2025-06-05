using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* 
 * Class: PlayerData
 * Purpose: Manages the player data that is displayed in the win scene
 */
public class PlayerData : MonoBehaviour
{
    
    public static string groupIDstr;
    public static string playerAliasStr;
    public static string matchingSchemeStr;
    public static string numShapesStr;
    public static string timeTakenStr;
    bool timerStatus;
    public Text playerID;
    public Text groupID;
    public Text timeTaken;
    public Text matchingScheme;
    public Text numShapes;

    private static int puzzleNum;

    void Start()
    {

        playerID.text = playerAliasStr;
        if (groupID != null)
        {
            groupID.text = groupIDstr;
            matchingScheme.text = matchingSchemeStr;
            numShapes.text = numShapesStr;
            timeTaken.text = DataManager.gameData.time.ToString("F2").Split(new char[] { '.' }, System.StringSplitOptions.None)[0] + "." + DataManager.gameData.time.ToString().Split(new char[] { '.' }, System.StringSplitOptions.None)[1].Substring(0, 2);
        }
        //timeTaken.text = timeTakenStr;

    }
}
