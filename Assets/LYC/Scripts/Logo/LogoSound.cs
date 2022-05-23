using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class LogoSound : MonoBehaviour
{
    SoundManager m_SM;

    public AudioSource BGM_as;
    public AudioSource SFX_as;


    public bool bPlay = false;

    public void Start()
    {
        m_SM = GameObject.FindGameObjectWithTag("Info").GetComponent<SoundManager>();

        m_SM.SoundValueLoad();
    }

    public void PlayAudio()
    {
        if(bPlay==false)
        {
            BGM_as.loop = true;
            BGM_as.Play();

            bPlay = true;
        }
        else
        {
            BGM_as.loop = false;
            BGM_as.Stop();

            bPlay = false;
        }
    }
}
