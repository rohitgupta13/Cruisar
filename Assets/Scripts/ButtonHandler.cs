using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class ButtonHandler : MonoBehaviour
{

    public GameObject label;
    public GameObject highScoreSection;
    public GameObject menuButtons;
    public GameObject optionsSection;
    public Toggle touchToggle;
    public Toggle tiltToggle;
    public Toggle audioToggle;

    public void Play() { 
        SceneManager.LoadScene("GameC");
    }

    public void MoreGames()
    {
        Application.OpenURL("https://play.google.com/store/apps/developer?id=blipthirteen");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void HighScore()
    {
        highScoreSection.SetActive(true);
        menuButtons.SetActive(false);
        Debug.Log("High Score " + PlayerPrefs.GetInt("HighScore"));
        TextMeshProUGUI tmp = label.GetComponent<TextMeshProUGUI>();
        tmp.SetText("" + PlayerPrefs.GetInt("HighScore"));
    }

    public void Options()
    {
        optionsSection.SetActive(true);
        menuButtons.SetActive(false);
    }

    public void Back()
    {
        menuButtons.SetActive(true);
        highScoreSection.SetActive(false);
        optionsSection.SetActive(false);
    }

    public void changeToTouch() {
        touchToggle.SetIsOnWithoutNotify(true);
        tiltToggle.SetIsOnWithoutNotify(false);
        PlayerPrefs.SetString(Globals.CONTROLS, Globals.TOUCH);
        PlayerPrefs.Save();
    }

    public void changeToTilt()
    {
        touchToggle.SetIsOnWithoutNotify(false);
        tiltToggle.SetIsOnWithoutNotify(true);
        PlayerPrefs.SetString(Globals.CONTROLS, Globals.TILT);
        PlayerPrefs.Save();
    }

    public void toggleAudio()
    {
        //PlayerPrefs.SetInt("Name", (yourBool ? 1 : 0));
        bool b = (PlayerPrefs.GetInt(Globals.AUDIO) != 0);

        audioToggle.SetIsOnWithoutNotify(!b);
        PlayerPrefs.SetInt(Globals.AUDIO, (b ? 1 : 0));

        /*if (audio == 1)
        {
            audioToggle.SetIsOnWithoutNotify(false);
            PlayerPrefs.SetInt(Globals.AUDIO, 0);
        }
        else {
            audioToggle.SetIsOnWithoutNotify(true);
            PlayerPrefs.SetInt(Globals.AUDIO, 1);
        }*/
        PlayerPrefs.Save();
    }


}
