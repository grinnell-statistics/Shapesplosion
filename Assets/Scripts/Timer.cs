using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.Assertions.Must;

/* Class: Timer
 * Purpose: Store and display the time of the game
 */
public class Timer : MonoBehaviour
{
    
    public struct ColliderInfo
    {
        public Collider2D collider;
        public int touching;
        public ColliderInfo(Collider2D c, int t)
        {
            collider = c;
            touching = t;
        }
    }

    public static List<ColliderInfo> colliders;
    public static GameObject[] pieces = new GameObject[7];
    

    Text text;
    public float speed = 1;
    public static float timer;
    public static string timerFormatted;
    public string timeTag;
    public bool isOn;
    public GameObject square;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        timer = GameOptionsScript.time;
        timeTag = GameOptionsScript.timeTag;
        DataManager.gameData.time = 0;
        colliders = new List<ColliderInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        /*  if (won)
         {
             GameManager.instance.gameWon();
         } */

        if (DataManager.gameData.win == false)
        {

            System.TimeSpan t = System.TimeSpan.FromSeconds(timer);
            timerFormatted = string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);

            // if the option was "No Limit"
            if (timeTag == "no limit")
            {

                // if the player took 15 minutes 
                if (timer >= 900)
                {
                    GameManager.instance.tooLong();
                }
                else
                {
                    //otherwise, add second to timer
                    timer += Time.deltaTime;
                }
            }

            // if other time option
            else if (timeTag == "other")
            {
                // if time is up
                if (timer <= 0)
                {
                    GameManager.instance.gameLost();
                }

                else
                {
                    // otherwise, subtract second from timer
                    timer -= Time.deltaTime;
                }
            }
            DataManager.gameData.time += Time.deltaTime;

            // If timer was selected in the GameOptions Scene
            if (GameOptionsScript.timerOn)
                text.text = "Time: " + timerFormatted;
            else
                text.text = "";
        }
    }
}
