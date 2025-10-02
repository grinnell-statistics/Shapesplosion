using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/* 
 * Class: DataManager
 * Purpose: manages and initialized Data to database
 */
public class DataManager : MonoBehaviour
{

    public static DataManager instance;

    public static Data.datum gameData;

    private void Awake()
    {
        //singleton
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        //persistent
        DontDestroyOnLoad(gameObject);
    }

    public IEnumerator SendData()
    {
        Debug.Log("Sending Data...");
        gameData.date = DateTime.Now;
        if (!gameData.win)
            gameData.time = gameData.reqTime;
        WWWForm form = new WWWForm();
        form.AddField("date", gameData.date.ToString());
        form.AddField("playerID", gameData.playerID);
        form.AddField("groupID", gameData.groupID);
        form.AddField("time", gameData.time.ToString());
        form.AddField("scheme", gameData.matchingScheme);
        form.AddField("win", gameData.win ? 1 : 0);
        form.AddField("numClicks", gameData.numClicks);
        form.AddField("reqTime", gameData.reqTime);
        form.AddField("displayTime", gameData.displayTime ? 1 : 0);
        form.AddField("numShapes", gameData.numShapes);
        form.AddField("shapesMatched", gameData.shapesMatched);
        if (gameData.var1 == null)
            gameData.var1 = "";
        form.AddField("var1", gameData.var1);
        if (gameData.var2 == null)
            gameData.var2 = "";
        form.AddField("var2", gameData.var2);
        if (gameData.var3 == null)
            gameData.var3 = "";
        form.AddField("var3", gameData.var3);
        string url = "https://stat2games.sites.grinnell.edu/php/sendshapeinfo.php";
        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();
		if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
		        {
		            Debug.Log(www.error);
		        }
        yield return Leaderboard.GetLeaderBoard(gameData.matchingScheme, gameData.numShapes);
    }
}
