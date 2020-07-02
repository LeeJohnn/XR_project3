using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class grab : MonoBehaviour
{
    private AudioSource grabAudio;
    private AudioSource ungrabAudio;
    // Start is called before the first frame update
    void Start()
    {
        grabAudio = GameObject.FindGameObjectWithTag("grab").GetComponent<AudioSource>();
        ungrabAudio = GameObject.FindGameObjectWithTag("ungrab").GetComponent<AudioSource>();
        //grabAudio.Play();
    }

    public void grabAudioPlay()
    {
        Debug.Log("1");
        grabAudio.Play();
    }
    public void ungrabAudioPlay()
    {
        Debug.Log("2");
        ungrabAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
