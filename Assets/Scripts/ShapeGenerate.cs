using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//   Class: ShapeGenerate
// Purpose: Generates shapes depending on no of shapes and matching scheme chosen
public class ShapeGenerate : MonoBehaviour
{
    public GameObject[] shapes;
    private bool[] shapeposfilled;
    public GameObject[] molds;
    private bool[] moldposfilled;
    public Color[] colors;
    private bool[] colorUsed;
    private bool[] secondColorUsed;
    private int shapepos;
    private int moldpos;
    private int colpos;
    private int chosenShapePos;
    public static int buildIndex;
    public static int shapesMatched;

    // Start is called before the first frame update
    void Start()
    {
        shapesMatched = 0;
        DataManager.gameData.numClicks = 0;
        buildIndex = SceneManager.GetActiveScene().buildIndex;
        shapeposfilled = new bool[shapes.Length];
        moldposfilled = new bool[molds.Length];
        colorUsed = new bool[colors.Length];
        secondColorUsed = new bool[colors.Length];

        colpos = Random.Range(0, shapes.Length - 1);

        if (GameOptionsScript.chosenSchemeTag == "One Shape Diff Col")
        {
            chosenShapePos = Random.Range(0, shapes.Length - 1);
        }

        for (int i = 0; i < shapes.Length; i++)
        {
            shapeposfilled[i] = false;
            moldposfilled[i] = false;
            colorUsed[i] = false;

            if (GameOptionsScript.chosenSchemeTag == "Random Colors")
            {
                //initialize second array for mold colors
                secondColorUsed[i] = false; 
            }
        }
        Generate();
    }

    // Function: Generate
    //  Purpose: Generates shapes in positions of placeholders
    void Generate()
    {
        PlaceHolder shapePH = GameObject.Find("Shapes").GetComponent<PlaceHolder>();
        PlaceHolder moldPH = GameObject.Find("Molds").GetComponent<PlaceHolder>();  

        // iterate through 15 positions
        for (int i = 0; i < shapes.Length; i++)
        {
            // get random positions for shapes and mold
            shapepos = Random.Range(0, shapes.Length - 1);
            moldpos = Random.Range(0, shapes.Length - 1);

            CheckPosFilled();

            // if player chose "One Color" matching scheme
            if (GameOptionsScript.chosenSchemeTag == "One Color")
            {
                shapePH.Set(shapepos, shapes[i], colors[colpos]);
                moldPH.Set(moldpos, molds[i], colors[colpos]);
                shapeposfilled[shapepos] = true;
                moldposfilled[moldpos] = true;
            }

            // if player chose "Matching" matching scheme
            else if (GameOptionsScript.chosenSchemeTag == "Matching Colors")
            {
                shapePH.Set(shapepos, shapes[i], colors[colpos]);
                moldPH.Set(moldpos, molds[i], colors[colpos]);
                shapeposfilled[shapepos] = true;
                moldposfilled[moldpos] = true;
                colorUsed[colpos] = true;
                colpos = Random.Range(0, shapes.Length - 1);
            }

            // if player chose "Random Colors" matching scheme
            else if (GameOptionsScript.chosenSchemeTag == "Random Colors")
            {
                int secondcolpos = Random.Range(0, shapes.Length - 1);

                while (secondcolpos == colpos)
                {
                    secondcolpos = Random.Range(0, shapes.Length - 1);
                }

                while (secondColorUsed[secondcolpos] == true)
                {
                    secondcolpos++;
                    if (secondcolpos == shapes.Length)
                    {
                        secondcolpos = 0;
                    }
                }

                shapePH.Set(shapepos, shapes[i], colors[colpos]);
                moldPH.Set(moldpos, molds[i], colors[secondcolpos]);
                shapeposfilled[shapepos] = true;
                moldposfilled[moldpos] = true;
                colorUsed[colpos] = true;
                secondColorUsed[secondcolpos] = true;
                colpos = Random.Range(0, shapes.Length - 1);
            }

            // if player chose "One Shape, Different Colors" matching scheme
            else if (GameOptionsScript.chosenSchemeTag == "One Shape Diff Col")
            {
                shapePH.Set(shapepos, shapes[chosenShapePos], colors[colpos]);
                moldPH.Set(moldpos, molds[chosenShapePos], colors[colpos]);
                shapeposfilled[shapepos] = true;
                moldposfilled[moldpos] = true;
                colorUsed[colpos] = true;
                colpos = Random.Range(0, shapes.Length - 1);
            }
        }
    }

    // Function: CheckPosFilled
    //  Purpose: checks if shape and mold positions have been filled and if color has been used
    void CheckPosFilled() {
        while (shapeposfilled[shapepos] == true)
        {
            shapepos++;
            if (shapepos == shapes.Length)
            {
                shapepos = 0;
            }
        }

        while (moldposfilled[moldpos] == true)
        {
            moldpos++;
            if (moldpos == shapes.Length)
            {
                moldpos = 0;
            }
        }

        while (colorUsed[colpos] == true)
        {
            colpos++;
            if (colpos == shapes.Length)
            {
                colpos = 0;
            }
        }
    }
}
