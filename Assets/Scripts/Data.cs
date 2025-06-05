using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* 
 * Class: Data
 * Purpose: contains variables to be stored in the database
 */
public class Data : MonoBehaviour
{
    public struct datum
    {
        public System.DateTime date;
        public string playerID;
        public string groupID;
        public float time;
        public string matchingScheme;
        public bool win;
        public int numClicks;
        public int reqTime;
        public bool displayTime;
        public string numShapes;
        public int shapesMatched;
        public string var1;
        public string var2;
        public string var3;
    }
}
