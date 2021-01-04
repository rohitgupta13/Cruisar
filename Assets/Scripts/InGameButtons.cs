using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameButtons : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject pauseButton;

    public void Pause() {
        Time.timeScale = 0;
        Globals.paused = true;
        menuPanel.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void Resume() {
        Time.timeScale = 1;
        Globals.paused = false;
        menuPanel.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void MainMenu() {
        Globals.Reset();
        SceneManager.LoadScene("MenuDynamic");
    }

    public void Restart()
    {
        Globals.Reset();
        SceneManager.LoadScene("GameC");
    }
}