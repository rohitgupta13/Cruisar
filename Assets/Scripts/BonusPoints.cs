using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusPoints : MonoBehaviour
{
    public GameObject floatingTextPrefab;
    public GameObject canvas;
    

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
       /* if (this.gameObject.tag.Equals("TriggerMain"))
        {
            MusicManager.playVeryClose();
            Debug.Log("PlayDouble");
        }
        // Add bonus hyperTime
        addHyperTime();
        if (this.gameObject.tag.Equals("TriggerSecondary")){ 
            MusicManager.playClose();
            Debug.Log("PlaySingle");
        }*/

        

        if (floatingTextPrefab) {
            //Vector3 pos = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z + 10);

            //Instantiate(floatingTextPrefab, other.transform.position, Quaternion.identity, canvas.transform);

            //CreateText(canvas.transform, Screen.width/2, Screen.height/2, "Close", 300, Color.yellow);
            AddText(canvas.transform);
        }
    }

    private void addHyperTime() {
        HyperDriveManager.hyperdriveTimeRemaining += 0.20f;
        if (HyperDriveManager.hyperdriveTimeRemaining > HyperDriveManager.MAX_LIMIT)
        {
            HyperDriveManager.hyperdriveTimeRemaining = HyperDriveManager.MAX_LIMIT;
        }
    }

    GameObject CreateText(Transform canvas_transform, float x, float y, string text_to_print, int font_size, Color text_color)
    {
        GameObject UItextGO = new GameObject("Text2");
        UItextGO.transform.SetParent(canvas_transform);

        RectTransform trans = UItextGO.AddComponent<RectTransform>();
        trans.anchoredPosition = new Vector2(x, y);

        Text text = UItextGO.AddComponent<Text>();
        text.text = text_to_print;
        text.fontSize = font_size;
        text.color = text_color;

        return UItextGO;
    }

    GameObject AddText(Transform canvas_transform)
    {
        GameObject go = Instantiate(floatingTextPrefab, canvas_transform.position, Quaternion.identity, canvas.transform);
        //TextPrefab.transform.SetParent(canvas_transform);
        return go;
    }
}
