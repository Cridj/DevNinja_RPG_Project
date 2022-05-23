using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class LogoFade : MonoBehaviour
{
    public LogoManager m_LogoManager;

    public LogoSound m_LogoSound;

    public void CallAnimEnd()
    {
        m_LogoManager.CheckFade();
    }

    public void CallMusicControl()
    {
        m_LogoSound.PlayAudio();
    }
}
