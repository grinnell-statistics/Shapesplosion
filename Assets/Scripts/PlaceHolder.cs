using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//   Class: PlaceHolder
// Purpose: Sets the shapes on the holder positions
public class PlaceHolder : MonoBehaviour
{
    // array of holders
    public GameObject[] holder;

    // Function: Set
    //  Purpose: puts shapes or molds in position of the holder and assigns colors
    public void Set(int pos, GameObject shapePrefab, Color col)
    {
        GameObject shape = Instantiate<GameObject>(shapePrefab, holder[pos].transform.parent);
        shape.name = shapePrefab.name;
        shape.transform.position = holder[pos].transform.position;
        shape.transform.position += new Vector3(0.0f, 0.0f, -5.0f);
        shape.transform.rotation = holder[pos].transform.rotation;
        shape.transform.localScale = new Vector3(0.1f, 0.1f, 1);
        shape.GetComponent<SpriteRenderer>().color = col;

        if (shape.tag == "Mold" && GameOptionsScript.chosenSchemeTag != "Random Colors")
        {
            shape.transform.GetChild(0).GetComponentInChildren<SpriteRenderer>().color = col;
        }
        holder[pos].SetActive(false);
    }
}
