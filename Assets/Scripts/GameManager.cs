using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/* 
 * Class: GameManager
 * Purpose: Manages the game scene, win conditions
 */
public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;
    public GameObject square;

    private void Awake()
    {
        //singleton
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }


    // Function: gameWon
    //  Purpose: sends data when game won
    public void gameWon()
    {
        DataManager.gameData.win = true;
        DataManager.gameData.shapesMatched = ShapeGenerate.shapesMatched;
        DataManager.gameData.numShapes = PlayerData.numShapesStr;
        DataManager.gameData.matchingScheme = PlayerData.matchingSchemeStr;
        StartCoroutine(DataManager.instance.SendData());
        SceneManager.LoadScene("WinScene");
        DataManager.gameData.win = false;
    }

    // Function: gameLost
    //  Purpose: sends data when game lost
    public void gameLost()
    {
        DataManager.gameData.win = false;
        DataManager.gameData.shapesMatched = ShapeGenerate.shapesMatched;
        DataManager.gameData.numShapes = PlayerData.numShapesStr;
        DataManager.gameData.matchingScheme = PlayerData.matchingSchemeStr;
        StartCoroutine(DataManager.instance.SendData());
        SceneManager.LoadScene("TimeOverScene");
    }

    // Function: tooLong
    //  Purpose: sends data when game took too long
    public void tooLong()
    {
        DataManager.gameData.win = false;
        DataManager.gameData.shapesMatched = ShapeGenerate.shapesMatched;
        DataManager.gameData.numShapes = PlayerData.numShapesStr;
        DataManager.gameData.matchingScheme = PlayerData.matchingSchemeStr;
        StartCoroutine(DataManager.instance.SendData());
        SceneManager.LoadScene("TooLongScene");
    }
}
