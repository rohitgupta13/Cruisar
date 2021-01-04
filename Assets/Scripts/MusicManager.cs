using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static AudioClip close;
    //public static AudioSource closeDup;

    static bool closePlaying = false;

    /*void Start()
    {
        close = GetComponent<AudioSource>();
        //closeDup = GetComponent<AudioSource>();
    }

    public static void playClose() {
        if (!close.isPlaying)
        {
            close.Play();
        }
    }

    IEnumerator playEngineSound()
    {
        audio.clip = engineStartClip;
        close.Play();
        yield return new WaitForSeconds(close.clip.length);
        audio.clip = engineLoopClip;
        audio.Play();
    }

    public static void playVeryClose()
    {
        if (!close.isPlaying) {
            close.Play();
            closeDup.PlayDelayed(close.clip.length);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}
