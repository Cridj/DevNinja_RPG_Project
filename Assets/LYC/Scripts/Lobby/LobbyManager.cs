 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class LobbyManager : MonoBehaviour
{
    [SerializeField]
    DataManager m_DataManager;

    public PlatformScript m_PlatformScript;
    
    public Animator LobbyFade_Anim;

    public RawImage LobbyFade_Img;


    public SoundMixerManager m_SMM;


    public GameObject ExitTap_Obj;
    public GameObject SettingTap_Obj;
    public GameObject MainTap_Obj;
    public CanvasGroup MainTap_CG;
    public GameObject LibraryTap_Obj;
    public GameObject LibraryNormalTap_Obj;
    public GameObject PreviewTap_Obj;
    public GameObject StageTap_Obj;
    public CanvasGroup StageTap_CG;
    public GameObject WarnGroup_Obj;

    public bool bFadeIn = true;
    public bool bExitOn = false;

    private void Start()
    {     
        m_DataManager = GameObject.FindGameObjectWithTag("Info").GetComponent<DataManager>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnTap();
        }
    }

    public void CheckFade()
    {
        if(bExitOn==true)
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
                GoToGame();
                bFadeIn = true;
            }
        }      
    }

    public void MusicControl()
    {
        m_SMM.PlayBGM();
    }

    #region [Tap 관리 함수]
    public void ReturnTap()
    {
        if(LobbyFade_Img.raycastTarget==true)
        {
            return;
        }

        if(WarnGroup_Obj.activeSelf==true)
        {
            WarnGroup_Obj.SetActive(false);
            StageTap_CG.interactable = true;

            return;
        }

        if(StageTap_Obj.activeSelf==true)
        {
            StageTap_Obj.SetActive(false);
            MainTap_Obj.SetActive(true);

            return;
        }

        if(PreviewTap_Obj.activeSelf==true)
        {
            PreviewTap_Obj.SetActive(false);
            LibraryNormalTap_Obj.SetActive(true);

            return;
        }

        if(LibraryTap_Obj.activeSelf==true)
        {
            LibraryTap_Obj.SetActive(false);
            MainTap_Obj.SetActive(true);

            return;
        }

        if(SettingTap_Obj.activeSelf==true)
        {
            SettingTap_Obj.SetActive(false);
            m_SMM.SoundValueSave();
            MainTap_CG.interactable = true;

            return;
        }

        if(ExitTap_Obj.activeSelf==true)
        {
            ExitTap_Obj.SetActive(false);
            MainTap_CG.interactable = true;

            return;
        }

        if(MainTap_Obj.activeSelf==true)
        {
            MainTap_CG.interactable = false;

            ExitTap_Obj.SetActive(true);

            return;
        }
    }

    public void PreviewTapOn()
    {
        LibraryNormalTap_Obj.SetActive(false);

        PreviewTap_Obj.SetActive(true);
    }

    public void LibraryTapOn()
    {
        MainTap_Obj.SetActive(false);

        LibraryTap_Obj.SetActive(true);
    }

    public void WarnTapOn()
    {
        StageTap_CG.interactable = false;

        WarnGroup_Obj.SetActive(true);
    }

    public void ResetData()
    {
        m_DataManager = DataManager.Instance;

        m_DataManager.m_PlayerData.nCurStage = 1;

        StageTap_CG.interactable = false;
    }

    public void StageTapOn()
    {
        MainTap_Obj.SetActive(false);

        StageTap_Obj.SetActive(true);
    }

    public void SettingTapOn()
    {
        MainTap_CG.interactable = false;

        SettingTap_Obj.SetActive(true);
    }

    public void ExitTapOn()
    {
        MainTap_CG.interactable = false;

        ExitTap_Obj.SetActive(true);
    }
    #endregion

    #region [종료, 이동 함수]

    public void ReadyToExit()
    {
        bFadeIn = true;
        bExitOn = true;

        ReadyToGame();
    }

    private void ExitGame()
    {
        m_DataManager.SavePlayerDataToJson();
        m_DataManager.SaveOptionDataToJson();
        m_DataManager.SaveListDataToJson();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit(); // 어플리케이션 종료
#endif
    }

    public void ReadyToGame()
    {
        LobbyFade_Img.raycastTarget = true;
        LobbyFade_Anim.SetTrigger("Fade");
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("Stage1");
    }

    #endregion

}


