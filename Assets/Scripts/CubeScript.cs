using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeScript : MonoBehaviour
{

    public float moveSpeed = 50f;
	public float sideSpeed = 0.50f;
	public float boundLeft = -143.7F;
	public float boundRight = 143.7F;
	public GameObject hyperDriveLights;
	public GameObject blinkingLights;
	private float blinkTime = 0.30f;
	private float blinkDelay = 0.99f;
	private float timeController = 0f;
	
	void Start()
    {
		Globals.configureControls();
		Globals.configureAudio();
	}

    void Update()
    {
		timeController += Time.deltaTime;
		//Debug.Log("TimeController: " + timeController);
		if (timeController > blinkDelay && timeController < blinkDelay + blinkTime) {
			blinkingLights.SetActive(true);
		}
		if (timeController > blinkDelay + blinkTime) {
			blinkingLights.SetActive(false);
			timeController = 0;
		}

		/*if (Globals.hyperDrive) {
			hyperDriveLights.SetActive(true);
		}else {
			hyperDriveLights.SetActive(false);
		}*/

		if (!Globals.gameOver && !Globals.paused)// && !Globals.hyperDrive)
		{
			transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
			
			// Hyper drive controls to move backwards
			/*if (Input.GetKey(KeyCode.Z))
			{
				transform.Translate(-Vector3.forward * moveSpeed * 2 * Time.deltaTime, Space.World);
			}*/

			if (Input.GetKey(KeyCode.A))
			{
				transform.Translate(-sideSpeed * Time.deltaTime, 0, 0, Space.World);
				// transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World);
				// transform.position = new Vector3(147, transform.position.y, transform.position.z);
			}
			if (Input.GetKey(KeyCode.D))
			{
			    transform.Translate(sideSpeed * Time.deltaTime, 0, 0, Space.World);
			}
			//if (!Globals.hyperDrive) {
				androidControls ();
			//}
		}
	}

	void androidControls(){
		if (Globals.touchEnabled) {
			touchControls();
        }
        else{
			tiltControls();
		}
	}

	void touchControls() {
		if (Input.touchCount > 0)
		{
			Touch t = Input.GetTouch(0);
			if (t.position.x < Screen.width / 2)
			{
				transform.Translate(-sideSpeed * Time.deltaTime, 0, 0, Space.World);
				//transform.Translate(-sideSpeed, 0, 0, Space.World);
			}
			else if (t.position.x > Screen.width / 2)
			{
				transform.Translate(sideSpeed * Time.deltaTime, 0, 0, Space.World);
				//transform.Translate(sideSpeed, 0, 0, Space.World);
			}
		}
	}

	void tiltControls()
	{
		if (Input.acceleration.x < -0.075)
		{
			transform.Translate(Input.acceleration.x, 0, 0, Space.World);
		}

		if (Input.acceleration.x > 0.075)
		{
			transform.Translate(Input.acceleration.x, 0, 0, Space.World);
		}
	}

	// Debug screen timer for lights
	/*void OnGUI()
    {
        int w = Screen.width, h = Screen.height;
        GUIStyle style = new GUIStyle();
        Rect rect = new Rect(0, 0, w, h);
        style.alignment = TextAnchor.LowerCenter;
        style.fontSize = h * 4 / 100;
        style.normal.textColor = Color.white;
        string text = "" + timeController;
        GUI.Label(rect, text, style);
    }*/
}