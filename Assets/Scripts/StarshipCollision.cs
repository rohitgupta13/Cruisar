using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StarshipCollision : MonoBehaviour {

	// Accessible through Unity Editor
	public float moveSpeed = 50f;
	public float sideSpeed = 2f;
	public float rotateSpeed = 200f;
	public float rotateAngle = 20;
	public GameObject components;
	

	public GameObject light;
	public GameObject panel;
	public GameObject pauseButton;
	// public GameObject pausePanel;
	// public GameObject scoreText;

	// private float timer;
	// private int score;
	
	// public Text text;
	

	// To draw sphere
	// private bool gameOver = false;
	// private bool create = true;
	// private ContactPoint contact;
	// private GameObject sphere;

	// Collision
	public Material black; 
	public float cubeSize = 0.2f;
    public int cubesInRow = 5;

    float cubesPivotDistance;
    Vector3 cubesPivot;

    public float explosionForce = 50f;
    public float explosionRadius = 4f;
    public float explosionUpward = 0.4f;

	// Use this for initialization
	void Start () {
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		// pausePanel.SetActive(false);
		// panel.SetActive(false);
		//calculate pivot distance
        cubesPivotDistance = cubeSize * cubesInRow / 2;
        //use this value to create pivot vector)
        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);
	}
	
	// Update is called once per frame
	void Update () {
		

		if (Input.GetKey(KeyCode.A))
		{
			 //if (transform.localEulerAngles.x < 300) {
				transform.RotateAround (transform.position, -Vector3.forward, -rotateSpeed * Time.deltaTime);
			 //}
		}
		if (Input.GetKey(KeyCode.D))
		{
			// if ( transform.localEulerAngles.x < 300) {
				transform.RotateAround (transform.position, Vector3.forward, -rotateSpeed * Time.deltaTime);
			// }
		}
		float interpolation = 2.5F * Time.deltaTime;
		Vector3 rotation = this.transform.localEulerAngles;
		rotation.x = Mathf.LerpAngle(this.transform.localEulerAngles.x, -90F, interpolation);
		transform.localEulerAngles = rotation;
		if(!Globals.gameOver && !Globals.paused){// && !Globals.hyperDrive){
			androidControls ();
		}
	}

	void androidControls()
	{
		if (Globals.touchEnabled)
		{
			touchControls();
		}
		else
		{
			tiltControls();
		}
	}

	void touchControls()
	{
		if (Input.touchCount > 0)
		{
			Touch t = Input.GetTouch(0);
			if (t.position.x < Screen.width / 2)
			{
				transform.RotateAround(transform.position, -Vector3.forward, -rotateSpeed * Time.deltaTime);
			}
			else if (t.position.x > Screen.width / 2)
			{
				transform.RotateAround(transform.position, Vector3.forward, -rotateSpeed * Time.deltaTime);
			}
		}
	}

	void tiltControls()
	{
		if (Input.acceleration.x > 0.15 && Input.acceleration.x < 0.30)
		{
			//transform.RotateAround(transform.position, Vector3.forward, -4 * Input.acceleration.x);
			transform.RotateAround(transform.position, Vector3.forward, -rotateSpeed/2 * Time.deltaTime);
		}
		else if (Input.acceleration.x >= 0.30)
		{
			//transform.RotateAround(transform.position, Vector3.forward, -4 * Input.acceleration.x);
			transform.RotateAround(transform.position, Vector3.forward, -rotateSpeed * Time.deltaTime);
		}




		if (Input.acceleration.x < -0.15 && Input.acceleration.x > -0.30)
		{
			//transform.RotateAround(transform.position, -Vector3.forward, 4 * Input.acceleration.x);
			transform.RotateAround(transform.position, -Vector3.forward, -rotateSpeed/2 * Time.deltaTime);
		}

		if (Input.acceleration.x <= -0.31)
		{
			//transform.RotateAround(transform.position, -Vector3.forward, 4 * Input.acceleration.x);
			transform.RotateAround(transform.position, -Vector3.forward, -rotateSpeed * Time.deltaTime);
		}
	}



	/*void OnGUI()
	{
		int w = Screen.width, h = Screen.height;

		GUIStyle style = new GUIStyle();
		Rect rect = new Rect(0, 0, w, h * 2 / 100);
		style.alignment = TextAnchor.UpperRight;
		style.fontSize = h * 4 / 100;
		style.normal.textColor = Color.white;
		string text = "" + Input.acceleration.x;
		GUI.Label(rect, text, style);
	}*/

	void ShowPanel()
	{
		
		panel.SetActive(true);
		
		//SceneManager.LoadScene("GameC");
	}

    void OnCollisionEnter(Collision col)
	{
		//StartCoroutine("StartFade");
		//StartCoroutine(FadeAudioSource.StartFade(bgm, 0.1f, 1f));
		//StartCoroutine("Fade");
		//bgm.Stop();
		BGM.fading();
		pauseButton.SetActive(false);
		Globals.gameOver = true;
		// pauseButton.SetActive(false);
		Invoke("ShowPanel", 5f);

		gameObject.SetActive(false);
		components.SetActive(true);
		explode();
	}

	/*private IEnumerator Fade()
	{
		float speed = 0.5f;
		while (bgm.volume > 0.1f)
		{
			bgm.volume -= speed;
			yield return new WaitForSeconds(0.01f);
		}
	}

	public IEnumerator StartFade()
	{

		float currentTime = 0;
		float start = bgm.volume;

		while (currentTime < 0.5)
		{
			currentTime += Time.deltaTime;
			bgm.volume = Mathf.Lerp(start, 0.01f, currentTime / 0.5f);
			yield return null;
		}
		//audioSource.Stop();
		yield break;
	}

	private IEnumerator FadeOut()
	{
		while (bgm.volume > 0.01f)
		{
			bgm.volume -= Time.deltaTime / 1.0f;
			yield return null;
		}
	}*/


	// void OnTriggerEnter(Collider col)
	// {
	// 	Globals.gameOver = true;
	// 	// pauseButton.SetActive(false);
	// 	Invoke("ShowPanel", 1.5f);
	// 	explode();
	// }

	public void explode() {
        //make object disappear
        gameObject.SetActive(false);
		light.SetActive(false);
		// GetComponent<Light>().SetActive(false);

        //loop 3 times to create 5x5x5 pieces in x,y,z coordinates
        for (int x = 0; x < cubesInRow; x++) {
            for (int y = 0; y < cubesInRow; y++) {
                for (int z = 0; z < cubesInRow; z++) {
                    createPiece(x, y, z);
                }
            }
        }

        //get explosion position
        Vector3 explosionPos = transform.position;
        //get colliders in that position and radius
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        //add explosion force to all colliders in that overlap sphere
        foreach (Collider hit in colliders) {
            //get rigidbody from collider object
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null) {
                //add explosion force to this body with given parameters
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpward);
            }
        }

    }

	void createPiece(int x, int y, int z) {

        //create piece
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);
		piece.GetComponent<Renderer>().material = black;
		
		float xS = UnityEngine.Random.Range(0.10f, 0.13f);
		float yS = UnityEngine.Random.Range(0.05f, 0.07f); 
		float zS = UnityEngine.Random.Range(0.15f, 0.2f);

        //set piece position and scale
        piece.transform.position = transform.position + new Vector3(xS * x, yS * y, zS * z) - cubesPivot;
        
		piece.transform.localScale = new Vector3(xS, yS, zS);

		// UnityEngine.Random.Range(0.09f, 0.3f), 
		// 	UnityEngine.Random.Range(0.09f, 0.3f), 
		// 	UnityEngine.Random.Range(0.2f, 2.0f)

        //add rigidbody and set mass
        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = cubeSize;
		// Destroy(piece, UnityEngine.Random.Range(0.5f, 5f));
    }
}
