using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Transform myPlay;
	public float followDistance = 5;
    public float transitionDuration = 2.5f;
    public bool startFade = false;


    void Start () {
		
	}

    IEnumerator Transition()
    {
        float t = 0.0f;
        Vector3 startingPos = transform.position;
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale/transitionDuration);

            transform.position = Vector3.Lerp(startingPos, myPlay.position, t);
            transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler(45, transform.rotation.y, transform.rotation.z), t);
            yield return 0;
        }
    }
	
    public void Update()
    {
        if(Globals.gameOver && !startFade){
            startFade = true; 
            Vector3 temp = myPlay.position;
            temp.y += 6;
            temp.z -= 6;
            myPlay.position = temp;
            StartCoroutine(Transition());
        }
    }

    private void LateUpdate()
    {
        Vector3 position = this.transform.position;
        position.x = myPlay.transform.position.x;
		position.z = myPlay.transform.position.z - followDistance;
		this.transform.position = position;
    }
}
