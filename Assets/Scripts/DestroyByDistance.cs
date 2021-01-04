using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByDistance : MonoBehaviour
{
    private GameObject referenceObject;
    public float bufferDistance = 5;

    void Start()
    {
        referenceObject = GameObject.FindGameObjectWithTag("MainGameObject");
    }


    void Update()
    {
        if (transform.position.z + bufferDistance < referenceObject.transform.position.z) {
            if (!gameObject.tag.Equals("Untagged"))
            {
                Debug.Log(gameObject.tag + " destroyed!");
            }
            Destroy(gameObject);
            Globals.totalObjects--;
        }   
    }
}
