using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TMP_Text text;
    int initialPosition;
    int score = 0;
    static int bonusScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = (int) transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        score = (int) (transform.position.z - initialPosition) / 100;
        text.SetText("" + (score + bonusScore));
    }

    /*void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();
        //Rect rect = new Rect(0, 0, w, h * 2 / 100);
        Rect rect = new Rect(0, 0, w, h);
        style.alignment = TextAnchor.LowerLeft;
        style.fontSize = h * 4 / 100;
        style.normal.textColor = Color.yellow;
        string text = "" + score;
        GUI.Label(rect, text, style);
    }*/

    private void OnDestroy()
    {
        Debug.Log("Should save score now");
        if (PlayerPrefs.GetInt("HighScore") == 0) {
            Debug.Log("No high score exists, saving new");
            PlayerPrefs.SetInt("HighScore", score);
        } else if (PlayerPrefs.GetInt("HighScore") < score) {
            Debug.Log("Saving new high score");
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    public static void addToScore(int value) {
       bonusScore += value;
    }
}