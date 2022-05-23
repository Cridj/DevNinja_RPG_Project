using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyFade : MonoBehaviour
{
    public LobbyManager m_LobbyManager;

    public void CallAnimEnd()
    {
        m_LobbyManager.CheckFade();
    }

    public void CallMusicControl()
    {
        m_LobbyManager.MusicControl();
    }
}
