using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    PlatformScript m_PlatformScript;

    OptionData m_OD;

    public AudioMixer MSM;

    public static SoundManager Instance;

    public float fMSvalue;
    public float fMSprev;
    public float fBGMSvalue;
    public float fBGMSprev;
    public float fGESvalue;
    public float fGESprev;
    public float fUIESvalue;
    public float fUIESprev;

    public bool bMSmute;
    public bool bBGMSmute;
    public bool bGESmute;
    public bool bUIESmute;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);

        SoundValueLoad();
    }
    
    public void SoundValueSave()
    {
        if (GameObject.FindGameObjectWithTag("PlatformCS").GetComponent<PlatformScript>())
        {
            m_PlatformScript = GameObject.FindGameObjectWithTag("PlatformCS").GetComponent<PlatformScript>();

            m_OD = m_PlatformScript.m_OptionData;
        }
        else
        {
            m_OD = DataManager.Instance.m_OptionData;
        }

        m_OD.fMsValue = fMSvalue;
        m_OD.fBGMsValue = fBGMSvalue;
        m_OD.fGEsValue = fGESvalue;
        m_OD.fUIEsValue = fUIESvalue;
        m_OD.fMsPrev = fMSprev;
        m_OD.fBGMsPrev = fBGMSprev;
        m_OD.fGEsPrev = fGESprev;
        m_OD.fUIEsPrev = fUIESprev;
        m_OD.bMsMute = bMSmute;
        m_OD.bBGMsMute = bBGMSmute;
        m_OD.bGEsMute = bGESmute;
        m_OD.bUIEsMute = bUIESmute;

        DataManager.Instance.SaveOptionDataToJson();
    }

    public void SoundValueLoad()
    {
        m_OD = DataManager.Instance.m_OptionData;

        fMSvalue = m_OD.fMsValue;
        fBGMSvalue = m_OD.fBGMsValue;
        fGESvalue = m_OD.fGEsValue;
        fUIESvalue = m_OD.fUIEsValue;
        fMSprev = m_OD.fMsPrev;
        fBGMSprev = m_OD.fBGMsPrev;
        fGESprev = m_OD.fGEsPrev;
        fUIESprev = m_OD.fUIEsPrev;
        bMSmute = m_OD.bMsMute;
        bBGMSmute = m_OD.bBGMsMute;
        bGESmute = m_OD.bGEsMute;
        bUIESmute = m_OD.bUIEsMute;

        MSM.SetFloat("MS", fMSvalue);
        MSM.SetFloat("BGMS", fBGMSvalue);
        MSM.SetFloat("GES", fGESvalue);
        MSM.SetFloat("UIES", fUIESvalue);
    }

    public void ToggleAudioVolume()
    {
        AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
    }
}
