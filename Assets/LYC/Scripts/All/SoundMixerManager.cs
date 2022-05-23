using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundMixerManager : MonoBehaviour
{
    SoundManager m_SM;

    public PlatformScript m_PlatformScript;

    public AudioSource BGM_AS;
    public AudioSource GE_AS;
    public AudioSource UIE_AS;

    public Slider MS_Slider;
    public Slider BGMS_Slider;
    public Slider GES_Slider;
    public Slider UIES_Slider;
    public Text MS_Text;
    public Text BGMS_Text;
    public Text GES_Text;
    public Text UIES_Text;
    public Image MS_Img;
    public Image BGMS_Img;
    public Image GES_Img;
    public Image UIES_Img;
    public Sprite[] MS_spr = new Sprite[2];
    public Sprite[] BGMS_spr = new Sprite[2];
    public Sprite[] GES_spr = new Sprite[2];
    public Sprite[] UIES_spr = new Sprite[2];

    public bool bOption = false;
    public bool bPlay = false;

    public bool bMSmute = false;
    public bool bBGMSmute = false;
    public bool bGESmute = false;
    public bool bUIESmute = false;

    public float fMS;
    public float fBGMS;
    public float fGES;
    public float fUIES;

    public void Start()
    {
        m_SM = GameObject.FindGameObjectWithTag("Info").GetComponent<SoundManager>();

        SoundValueLoad();

    }

    public void SetAudio(float volume)
    {
        if(bOption==false)
        {
            return;
        }

        fMS = MS_Slider.value;
        m_SM.fMSvalue = fMS;
        fBGMS = BGMS_Slider.value;
        m_SM.fBGMSvalue = fBGMS;
        fGES = GES_Slider.value;
        m_SM.fGESvalue = fGES;
        fUIES = UIES_Slider.value;
        m_SM.fUIESvalue = fUIES;

        if(fMS == -40f)
        {
            if(bMSmute==true)
            {
                MS_Slider.value = -40f;
                m_SM.MSM.SetFloat("MS", -80f);
                return;
            }
            Debug.Log("작동됨");
            m_SM.MSM.SetFloat("MS", -80f);
            m_SM.fMSvalue = -40f;
            MS_Text.text = 0 + "%";
        }
        else
        {
            if(bMSmute == true)
            {
                MS_Slider.value = -40f;
                m_SM.MSM.SetFloat("MS", -80f);
                return;
            }
            m_SM.MSM.SetFloat("MS", fMS);
            MS_Text.text = Mathf.RoundToInt(100 - fMS * -2.5f) + "%";
        }

        if (fBGMS == -40f)
        {
            if (bBGMSmute == true)
            {
                BGMS_Slider.value = -40f;
                m_SM.MSM.SetFloat("BGMS", -80f);
                return;
            }
            m_SM.MSM.SetFloat("BGMS", -80f);
            m_SM.fBGMSvalue = -40f;
            BGMS_Text.text = 0 + "%";
        }
        else
        {
            if (bBGMSmute == true)
            {
                BGMS_Slider.value = -40f;
                m_SM.MSM.SetFloat("BGMS", -80f);
                return;
            }
            m_SM.MSM.SetFloat("BGMS", fBGMS);
            BGMS_Text.text = Mathf.RoundToInt(100 - fBGMS * -2.5f) + "%";
        }

        if (fGES == -40f)
        {
            if (bGESmute == true)
            {
                GES_Slider.value = -40f;
                m_SM.MSM.SetFloat("GES", -80f);
                return;
            }
            m_SM.MSM.SetFloat("GES", -80f);
            m_SM.fGESvalue = -40f;
            GES_Text.text = 0 + "%";
        }
        else
        {
            if (bGESmute == true)
            {
                GES_Slider.value = -40f;
                m_SM.MSM.SetFloat("GES", -80f);
                return;
            }
            m_SM.MSM.SetFloat("GES", fGES);
            GES_Text.text = Mathf.RoundToInt(100 - fGES * -2.5f) + "%";
        }

        if (fUIES == -40f)
        {
            if (bUIESmute == true)
            {
                UIES_Slider.value = -40f;
                m_SM.MSM.SetFloat("UIES", -80f);
                return;
            }
            m_SM.MSM.SetFloat("UIES", -80f);
            m_SM.fUIESvalue = -40f;
            UIES_Text.text = 0 + "%";
        }

        else
        {
            if (bUIESmute == true)
            {
                UIES_Slider.value = -40f;
                m_SM.MSM.SetFloat("UIES", -80f);
                return;
            }
            m_SM.MSM.SetFloat("UIES", fUIES);
            UIES_Text.text = Mathf.RoundToInt(100 - fUIES * -2.5f) + "%";
        }
    }

    public void MuteMS()
    {
        if(bMSmute==false)
        {
            m_SM.fMSprev = MS_Slider.value;
            fMS = MS_Slider.value;
            MS_Img.sprite = MS_spr[1];
            m_SM.MSM.SetFloat("MS", -80f);
            m_SM.bMSmute = true;
            m_SM.fMSvalue = -40f;
            MS_Slider.value = -40f;
            MS_Text.text = Mathf.RoundToInt(0f) + "%";
            bMSmute = true;
        }
        else
        {
            bMSmute = false;
            fMS = m_SM.fMSprev;
            MS_Slider.value = fMS;
            MS_Img.sprite = MS_spr[0];
            m_SM.MSM.SetFloat("MS", fMS);
            m_SM.bMSmute = false;
            m_SM.fMSvalue = fMS;
            MS_Text.text = Mathf.RoundToInt(100 - fMS * -2.5f) + "%";
        }
    }

    public void MuteBGMS()
    {
        if (bBGMSmute == false)
        {
            m_SM.fBGMSprev = BGMS_Slider.value;
            fBGMS = BGMS_Slider.value;
            BGMS_Img.sprite = BGMS_spr[1];
            m_SM.MSM.SetFloat("BGMS", -80f);
            m_SM.bBGMSmute = true;
            m_SM.fBGMSvalue = -40f;
            BGMS_Slider.value = -40f;
            BGMS_Text.text = Mathf.RoundToInt(0f) + "%";
            bBGMSmute = true;
        }
        else
        {
            bBGMSmute = false;
            fBGMS = m_SM.fBGMSprev;
            BGMS_Slider.value = fBGMS;
            BGMS_Img.sprite = BGMS_spr[0];
            m_SM.MSM.SetFloat("BGMS", fBGMS);
            m_SM.bBGMSmute = false;
            m_SM.fBGMSvalue = fBGMS;
            BGMS_Text.text = Mathf.RoundToInt(100 - fBGMS * -2.5f) + "%";
        }
    }

    public void MuteGES()
    {
        if (bGESmute == false)
        {
            m_SM.fGESprev = GES_Slider.value;
            fGES = GES_Slider.value;
            GES_Img.sprite = GES_spr[1];
            m_SM.MSM.SetFloat("GES", -80f);
            m_SM.bGESmute = true;
            m_SM.fGESvalue = -40f;
            GES_Slider.value = -40f;
            GES_Text.text = Mathf.RoundToInt(0f) + "%";
            bGESmute = true;
        }
        else
        {
            bGESmute = false;
            fGES = m_SM.fGESprev;
            GES_Slider.value = fGES;
            GES_Img.sprite = GES_spr[0];
            m_SM.MSM.SetFloat("GES", fGES);
            m_SM.bGESmute = false;
            m_SM.fGESvalue = fGES;
            GES_Text.text = Mathf.RoundToInt(100 - fGES * -2.5f) + "%";
        }
    }

    public void MuteUIES()
    {
        if (bUIESmute == false)
        {
            m_SM.fUIESprev = UIES_Slider.value;
            fUIES = UIES_Slider.value;
            UIES_Img.sprite = UIES_spr[1];
            m_SM.MSM.SetFloat("UIES", -80f);
            m_SM.bUIESmute = true;
            m_SM.fUIESvalue = -40f;
            UIES_Slider.value = -40f;
            UIES_Text.text = Mathf.RoundToInt(0f) + "%";
            bUIESmute = true;
        }
        else
        {
            bUIESmute = false;
            fUIES = m_SM.fUIESprev;
            UIES_Slider.value = fUIES;
            UIES_Img.sprite = UIES_spr[0];
            m_SM.MSM.SetFloat("UIES", fUIES);
            m_SM.bUIESmute = false;
            m_SM.fUIESvalue = fUIES;
            UIES_Text.text = Mathf.RoundToInt(100 - fUIES * -2.5f) + "%";
        }
    }

    public void SoundValueLoad()
    {
        if (m_SM.bMSmute == false)
        {
            MS_Img.sprite = MS_spr[0];
            bMSmute = false;
        }
        else
        {
            MS_Img.sprite = MS_spr[1];
            bMSmute = true;
        }

        MS_Text.text = Mathf.RoundToInt(100 - m_SM.fMSvalue * -2.5f) + "%";

        if (m_SM.bBGMSmute == false)
        {
            BGMS_Img.sprite = BGMS_spr[0];
            bBGMSmute = false;
        }
        else
        {
            BGMS_Img.sprite = BGMS_spr[1];
            bBGMSmute = true;
        }

        BGMS_Text.text = Mathf.RoundToInt(100 - m_SM.fBGMSvalue * -2.5f) + "%";

        if (m_SM.bGESmute == false)
        {
            GES_Img.sprite = GES_spr[0];
            bGESmute = false;
        }
        else
        {
            GES_Img.sprite = GES_spr[1];
            bGESmute = true;
        }

        GES_Text.text = Mathf.RoundToInt(100 - m_SM.fGESvalue * -2.5f) + "%";

        if (m_SM.bUIESmute == false)
        {
            UIES_Img.sprite = UIES_spr[0];
            bUIESmute = false;
        }
        else
        {
            UIES_Img.sprite = UIES_spr[1];
            bUIESmute = true;
        }

        UIES_Text.text = Mathf.RoundToInt(100 - m_SM.fUIESvalue * -2.5f) + "%";

        fMS = m_SM.fMSvalue;
        fBGMS = m_SM.fBGMSvalue;
        fGES = m_SM.fGESvalue;
        fUIES = m_SM.fUIESvalue;

        MS_Slider.value = m_SM.fMSvalue;
        BGMS_Slider.value = m_SM.fBGMSvalue;
        GES_Slider.value = m_SM.fGESvalue;
        UIES_Slider.value = m_SM.fUIESvalue;


        bOption = true;
    }

    public void SoundValueSave() //저장함수 만들기
    {
        m_SM.SoundValueSave();
    }

    public void PlayBGM()
    {
        if(bPlay==false)
        {
            BGM_AS.Play();
            bPlay = true;
        }
        else
        {
            BGM_AS.Stop();
            bPlay = false;
        }
    }
}
