using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class IceCollision : MonoBehaviour
{
    public GameObject components;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col)
	{
        components.transform.position = transform.position;
        components.transform.rotation = transform.rotation;
        gameObject.SetActive(false);
        components.SetActive(true);
         

		// Debug.Log("XXXX");
        // Debug.Log("comp: " + components.transform.position);
        // Debug.Log("mesh: " + transform.position);
        // Debug.Log("XXXX");
        
        // Debug.Log("Should happen now");
        
	}
}
