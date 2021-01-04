using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPoints1 : MonoBehaviour
{
    public GameObject canvas;
    public GameObject floatingTextPrefab;
    public int add = 0;
    public GameObject dangerLights;

    static bool sentinel = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Canvas transform " + canvas.transform.position.ToString());
    }

    private void OnTriggerEnter(Collider other)
    {
        dangerLights.SetActive(true);
        Invoke("LightsOff", 2f);
        if (!sentinel) { 
            sentinel = true;
        }
    }

    void LightsOff() {
        dangerLights.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        //dangerLights.SetActive(false);
        if (floatingTextPrefab && sentinel)
        {
            Debug.Log("That was close");
            Score.addToScore(add);
            //Vector3 pos = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z + 10);

            //Instantiate(floatingTextPrefab, other.transform.position, Quaternion.identity, canvas.transform);
            
            //CreateText(canvas.transform, Screen.width/2, Screen.height/2, "Close", 300, Color.yellow);
            AddText(canvas.transform);
        }
        sentinel = false;
    }

    GameObject AddText(Transform canvas_transform)
    {
        int w = Screen.width, h = Screen.height;

        Rect rect;
        if (add == 10) {
            rect = new Rect(300, h - 80, w, h);
        }
        else{
            rect = new Rect(200, h - 80, w, h);
        }
        rect = new Rect(200, h - 80, w, h);
        
        GameObject go = Instantiate(floatingTextPrefab, rect.position, Quaternion.identity, canvas.transform);
        
        //TextPrefab.transform.SetParent(canvas_transform);
        return go;
    }
}
