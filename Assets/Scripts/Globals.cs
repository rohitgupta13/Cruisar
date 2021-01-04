using UnityEngine;
public static class Globals
{
    public static string HIGH_SCORE = "HighScore";
    public static string CONTROLS = "Controls";
    public static string TOUCH = "Touch";
    public static string TILT = "Tilt";
    public static string AUDIO = "Audio";

    public static bool gameOver = false;
    public static bool paused = false;
    public static bool hyperDrive = false;
    public static int totalObjects = 0;
    public static bool touchEnabled;
    public static bool audioEnabled;

    public static void configureControls() {
        bool b = PlayerPrefs.GetString(Globals.CONTROLS).Equals(TOUCH);
        Debug.Log("Touch Enabled:" + touchEnabled);
        touchEnabled = b;
    }

    public static void configureAudio()
    {
        bool b = PlayerPrefs.GetInt(Globals.AUDIO) == 1 ? true : false;
        Debug.Log("Audio Enabled:" + audioEnabled);
        audioEnabled = b;
    }

    public static void Reset()
    {
        paused = false;
        gameOver = false;
        hyperDrive = false;
        totalObjects = 0;
        Time.timeScale = 1;
    }


}
