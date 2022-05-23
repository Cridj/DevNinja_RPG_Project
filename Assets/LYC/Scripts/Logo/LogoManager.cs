using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogoManager : MonoBehaviour
{

    public LogoSound m_LogoSound;

    public Animator LogoFade_Anim;

    public RawImage LogoFade_Img;

    public Button Lobby_Btn;

    public GameObject ExitTap_Obj;

    public bool bFadeIn = true;

    public bool bExitOn = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("키눌림");
            ExitMenuOn();
        }
    }

    public void ReadyToLobby()
    {
        LogoFade_Anim.SetTrigger("Fade");
    }

    private void GoToLobby()
    {
        SceneManager.LoadScene("2_LobbyScene");
    }

    public void CheckFade()
    {      
        if(bExitOn == true)
        {
            ExitGame();

            return;
        }
        else
        {
            if (bFadeIn == true)
            {
                bFadeIn = false;
            }
            else
            {
                GoToLobby();
            }
        }    
    }

    public void MusicControl()
    {
        m_LogoSound.PlayAudio();
    }

    public void ExitMenuOn()
    {
        if (LogoFade_Img.raycastTarget == true)
        {
            return;
        }


        if (ExitTap_Obj.activeSelf == false)
        {
            Lobby_Btn.interactable = false;
            ExitTap_Obj.SetActive(true);
        }
        else
        {
            Lobby_Btn.interactable = true;
            ExitTap_Obj.SetActive(false);
        }
    }

    public void ReadyToExit()
    {
        bFadeIn = true;

        bExitOn = true;

        ReadyToLobby();
    }

    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit(); // 어플리케이션 종료
#endif
    }
}
