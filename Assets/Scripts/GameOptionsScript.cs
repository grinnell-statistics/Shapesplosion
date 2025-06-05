using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/* Class: GameOptions
 * Purpose: Managage the options selected by the user
 */
public class GameOptionsScript : MonoBehaviour
{
    public InputField[] vars;
    public GameObject startButton;
    public ToggleGroup noOfShapes;
    public ToggleGroup matchingScheme;
    public static string chosenSchemeTag;
    Toggle shapeChosen;
    public Toggle noLimit;
    public Toggle shortTime;
    public Toggle mediumTime;
    public Toggle longTime;
    public Toggle timer;
    public static bool timerOn;
    public static float time;
    public static string timeTag = "";
    public static int buildIndexScene;
    
    public GameObject badWordMenu;
    [SerializeField] public TextAsset badWordsFile;
    private string[] badWords;


    public void Start()
    {
    
    }


    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if (vars[0].isFocused)
                vars[2].ActivateInputField();
            if (vars[2].isFocused)
                vars[1].ActivateInputField();
            if (vars[1].isFocused)
                startButton.GetComponent<Button>().Select();
        }

        
    }


    // initializes the hint selection, time selection, and loads game scene
    public void StartGame()
    {
        //checks if the additional variables have any bad words inputted
         if (IsBadWord(vars[0].text) || IsBadWord(vars[1].text) || IsBadWord(vars[2].text))
        {
            badWordMenu.SetActive(true);
        }
        else {
            if (!(vars[0].text=="")) {
                Debug.Log("not empty");
                GetVariable1(vars[0].text);
            }
            if (!(vars[1].text=="")) {
                GetVariable2(vars[1].text);
            }
            if (!(vars[2].text=="")) {
                GetVariable3(vars[2].text);
            }

            timerOn = timer.isOn;

            foreach (var toggle in matchingScheme.ActiveToggles())
            {
                chosenSchemeTag = toggle.tag;
                PlayerData.matchingSchemeStr = chosenSchemeTag;
                break;
            }

            DataManager.gameData.displayTime = timer.isOn;
            TimeSelection();
            ChooseScene();
        }
    }


     /// <summary>
    /// Checks to see if the corresponding word matches with any words in the bad word file.
    /// </summary>
    /// <param name="word"></param>
    /// <returns></returns>
    private bool IsBadWord(string word)
    {
        word = word.ToLower();
        //Removes whitespace. Another method might be better (splitting the word and checking each)
        word = word.Replace(" ", string.Empty);
        
        int left, right;
        left = 0;
        right = badWords.Length - 1;

        while (right >= left)
        {
            if (word.Length <= 3 && word == badWords[left])
            {
                return true;
            }
            else if (word.Length > 3 && badWords[left].Length > 2 && word.Contains(badWords[left]))
            {
                return true;
            }
            else
                left++;
        }
        
        return false;
    }

     
    public void Awake()
    {
        
        badWords = badWordsFile.text.Split(',');
        for (int i = 0; i < badWords.Length; i++)
        {
            badWords[i] = badWords[i].Replace(" ", string.Empty);
            badWords[i] = badWords[i].ToLower();
        }
       
    }

    // assigns time depending on time selection
    public void TimeSelection()
    {
        if (noLimit.isOn)
        {
            time = 0;
            timeTag = "no limit"; 
        }

        else if (shortTime.isOn)
        {
            time = 30;
            timeTag = "other";
        }

        else if (mediumTime.isOn)
        {
            time = 60;
            timeTag = "other";
        }

        else if (longTime.isOn)
        {
            time = 90;
            timeTag = "other";
        }
        DataManager.gameData.reqTime = (int) time;
    }


    public void GetVariable1(string s)
    {
        Debug.Log ("hello1");
        DataManager.gameData.var1 = s;
    }

    public void GetVariable2(string s)
    {
        
        DataManager.gameData.var2 = s;
    }

    public void GetVariable3(string s)
    {
        DataManager.gameData.var3 = s;
    }


    public void ChooseScene()
    {
        foreach (var toggle in noOfShapes.ActiveToggles())
        {
            shapeChosen = toggle;
            break;
        }

        DataManager.gameData.time = 0;

        if (shapeChosen.tag == "15")
        {
            PlayerData.numShapesStr = "15";
            WinCondition.numShape = 15;
            SceneManager.LoadScene("15Shapes");
        }

        else if (shapeChosen.tag == "18")
        {
            PlayerData.numShapesStr = "18";
            WinCondition.numShape = 18;
            SceneManager.LoadScene("18ShapesScene(2)");
        }

        else if (shapeChosen.tag == "21")
        {
            PlayerData.numShapesStr = "21";
            WinCondition.numShape = 21;
            SceneManager.LoadScene("21ShapesScene");
        }

        else if (shapeChosen.tag == "24")
        {
            PlayerData.numShapesStr = "24";
            WinCondition.numShape = 24;
            SceneManager.LoadScene("24ShapesScene");
        }
    }
}
