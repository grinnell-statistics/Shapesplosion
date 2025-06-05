using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//   Class: WinCondition
// Purpose: Manages the win condition of the game
public class WinCondition : MonoBehaviour
{
    public static bool[] isMatchedArr;
    public static int arrayPos;
    public static int numShape;

    public GameObject explosion;
    // public ShapeExplosion shapeExplodeScript;

    // Start is called before the first frame update
    void Start()
    {
        /*creates an array where the size of the
        array = the number of shapes chosen*/
        isMatchedArr = new bool[numShape];
        arrayPos = 0;

        for (int i = 0; i < isMatchedArr.Length; i++)
        {
            isMatchedArr[i] = false;
        }
    }

    // Function: Win
    //  Purpose: determines if game won
    public void Win()
    {
        //sets the arrayPos position of the array to true if matched
        isMatchedArr[arrayPos] = true;
        //moving forward in the array
        arrayPos++;

        //if all the things in the array are true then win scene
        if (isMatchedArr[(numShape-1)] == true){
            GameObject.Find("ExitButton").SetActive(false);
            GamePlay.winGame = true;
            DataManager.gameData.win = true;
            if (GameOptionsScript.timeTag == "no limit")
            {
                PlayerData.timeTakenStr = Timer.timerFormatted;
                DataManager.gameData.time = Timer.timer;
            }

            else if (GameOptionsScript.timeTag == "other")
            {
                float timeTaken = GameOptionsScript.time - Timer.timer;

                System.TimeSpan t = System.TimeSpan.FromSeconds(timeTaken);
                string timerFormatted = string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);

                PlayerData.timeTakenStr = timerFormatted;
                DataManager.gameData.time = timeTaken;
            }
            Instantiate (explosion, transform.position, Quaternion.identity);
            Invoke("ScreenLoad", 3);
        }
    }

    // Function: ScreenLoad
    //  Purpose: Loads win scene
    public void ScreenLoad()
    {
        GameManager.instance.gameWon();
    }

    //method to just check if its working
    public void Deb()
    {
        Debug.Log("working?");
    }

    
}
