using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public AudioSource bgm;
    public static bool isFading;

    void Start()
    {
        isFading = false;
        if (Globals.audioEnabled) {
            bgm.Play();
        }
    }
    public static void fading() {
        isFading = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFading && bgm.volume > 0.05f) {
            bgm.volume -= Time.deltaTime;
        }
    }
}
