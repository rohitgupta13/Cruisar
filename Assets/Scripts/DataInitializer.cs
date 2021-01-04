using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataInitializer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string s = PlayerPrefs.GetString("FirstRun");

        if (string.IsNullOrEmpty(s))
        {
            Debug.Log("No first run data");
            PlayerPrefs.SetString(Globals.CONTROLS, Globals.TOUCH);
            PlayerPrefs.SetInt(Globals.HIGH_SCORE, 0);
            PlayerPrefs.SetInt(Globals.AUDIO, 1);

            // Set first run flag
            PlayerPrefs.SetString("FirstRun", "Completed");
            PlayerPrefs.Save();
        }
        else {
            Debug.Log("Data exists");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
