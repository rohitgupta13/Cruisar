using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperDriveManager : MonoBehaviour
{
    public static float MAX_LIMIT = 1f;
    public static float hyperdriveTimeRemaining = 1f;
    private float timeController = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeController += Time.deltaTime;
        // Should be controlled centrally 
        if (Input.touchCount > 1 || Input.GetKey(KeyCode.S))
        {
            Globals.hyperDrive = true;
            if (hyperdriveTimeRemaining < 0)
            {
                Globals.hyperDrive = false;
            }
        }
        else
        {
            Globals.hyperDrive = false;
        }

        if (Globals.hyperDrive) {
            hyperdriveTimeRemaining -= Time.deltaTime;
        }
        // Increase every 2 seconds
        /*if (timeController > 2) {
            timeController = 0;
            hyperdriveTimeRemaining = 1;
            if (hyperdriveTimeRemaining > MAX_LIMIT) {
                hyperdriveTimeRemaining = MAX_LIMIT;
            }
        }*/
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();
        //Rect rect = new Rect(0, 0, w, h * 2 / 100);
        Rect rect = new Rect(0, 0, w, h);
        style.alignment = TextAnchor.UpperCenter;
        style.fontSize = h * 4 / 100;
        style.normal.textColor = Color.yellow;
        string text = "" + hyperdriveTimeRemaining;
        GUI.Label(rect, text, style);
    }
}
