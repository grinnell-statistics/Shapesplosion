using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

//   Class: GamePlay
// Purpose: Manages game play
public class GamePlay : MonoBehaviour
{
    private bool isMatched;
    private bool isActive;
    private Color color;
    public GameObject mold;
    private Vector3 displacementVector;
    public static bool winGame;

    private bool[] isMatchedArr;
    private int arrayPos;
    public WinCondition winCond;
    
    private Vector2 initialPosition;
    public Vector3 startPosition;
    GameObject moldObj;

    void Start()
    {
        /*basic debug function to check if WinCondition Script is connected 
         to this script or not*/
        winCond.Deb();

        isActive = false;
        isMatched = false;
        startPosition = transform.position;
        color = gameObject.GetComponent<SpriteRenderer>().color;
        if (mold != null){
            moldObj = GameObject.Find(mold.name);
        }
        if (GameOptionsScript.chosenSchemeTag == "One Shape Diff Col")
        {
            foreach (GameObject mold in GameObject.FindGameObjectsWithTag("Mold")){
                Debug.Log("Yo");
                if (mold.GetComponent<SpriteRenderer>().color.Equals(color))
                {
                    moldObj = mold;
                    break;
                }
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
            return;
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition)-displacementVector;
    }


    private void OnMouseDown()
    {
        DataManager.gameData.numClicks++;
        if (!isMatched)
        {
            PickUp();
        }
    }

    private void OnMouseUp()
    {
        Drop();
    }

    // Function: PickUp
    //  Purpose: picks up shape
    private void PickUp()
    {
        isActive = true;
        displacementVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    // Function: Drop
    //  Purpose: drops the shape
    private void Drop()
    {
        isActive = false;

        //checks if the shape is near its corresponding mold
        if (Mathf.Abs(transform.position.x - moldObj.transform.position.x) <= 0.5f &&
            Mathf.Abs(transform.position.y - moldObj.transform.position.y) <= 0.5f)
        {
            //if it is, sets both the shape and the corresponding mold inactive
            transform.position = new Vector2(moldObj.transform.position.x, moldObj.transform.position.y);
            isMatched = true;
            ShapeGenerate.shapesMatched++;

            gameObject.transform.position = moldObj.transform.position;

            if (GameOptionsScript.chosenSchemeTag != "Random Colors")
                moldObj.transform.GetChild(0).gameObject.SetActive(true);


            //win method called from WinCondition script
            winCond.Win();
        }
        else
        {
            //if the shape isnt near the corresponding mold, bring it to its original position
            transform.position = new Vector2(startPosition.x, startPosition.y);
        }
    }

}
