using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListSound : MonoBehaviour
{
    public AudioSource BGM_AS;
    public AudioSource GE_AS;
    public AudioSource UIE_AS;

    public AudioClip[] BGM_AC;
    public AudioClip[] GE_AC;
    public AudioClip[] UIE_AC;

    public void BGMPlay(int _bgmac)
    {
        BGM_AS.loop = true;
        BGM_AS.clip = BGM_AC[_bgmac];
        BGM_AS.Play();
    }

    public void GEPlay(int _geac)
    {
        GE_AS.PlayOneShot(GE_AC[_geac]);
    }

    public void UIEPlay(int _uieac)
    {
        UIE_AS.PlayOneShot(UIE_AC[_uieac]);
    }
}
