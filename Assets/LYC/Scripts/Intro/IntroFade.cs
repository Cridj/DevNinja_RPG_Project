using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroFade : MonoBehaviour
{
    public IntroManager m_IntroManager;

    public void CallAnimeEnd()
    {
        m_IntroManager.GoToLogoScene();
    }
}
