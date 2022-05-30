using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageFade : MonoBehaviour
{
    public StageManager m_StageManager;

    public void CallAnimEnd()
    {
        m_StageManager.CheckFade();
    }

    public void CallMusicControl()
    {
        m_StageManager.MusicControl();
    }
}
