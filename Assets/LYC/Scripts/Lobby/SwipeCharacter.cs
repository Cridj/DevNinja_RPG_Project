using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeCharacter : MonoBehaviour
{
    LobbyManager m_LobbyManager;

    public void LeftChange()
    {
        m_LobbyManager.ChangingLeft();
    }

    public void RightChange()
    {
        m_LobbyManager.ChangingRight();
    }
}
