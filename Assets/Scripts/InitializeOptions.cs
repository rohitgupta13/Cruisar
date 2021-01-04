using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class InitializeOptions : MonoBehaviour
{
    public Toggle touch;
    public Toggle tilt;
    public Toggle audio;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        Debug.Log("PrintOnEnable: script was enabled");
        if (PlayerPrefs.GetString(Globals.CONTROLS).Equals(Globals.TOUCH))
        {
            touch.SetIsOnWithoutNotify(true);
            tilt.SetIsOnWithoutNotify(false);
        }
        else
        {
            touch.SetIsOnWithoutNotify(false);
            tilt.SetIsOnWithoutNotify(true);
        }

        /*if (PlayerPrefs.GetInt(Globals.AUDIO) == 1)
        {
            audio.SetIsOnWithoutNotify(true);
        }
        else { 
            audio.SetIsOnWithoutNotify(false);
        }*/

        bool b = (PlayerPrefs.GetInt(Globals.AUDIO) != 0);
        audio.SetIsOnWithoutNotify(b);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
