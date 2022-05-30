using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    DataManager m_DataManager; //데이터 연동을 위해 데이터 매니저 선언 필요

    public Animator StageFade_Anim; //스테이지에서 배틀로 넘기기 위한 애니메이터 필요
    public RawImage StageFade_Img; //해당 페이드 이미지가 작동 안하는지 raycasttarget으로 확인하기 위해 필요

    public SoundMixerManager m_SMM; //사운드 조절 위해서 필요

    public GameObject PauseTap_Obj; //정지탭 오브젝트
    public GameObject ButtonGroup_Obj; //버튼그룹 오브젝트
    public CanvasGroup ButtonGroup_CG; //버튼그룹에 관한 캔버스 그룹
    public GameObject SettingTap_Obj; //설정탭 오브젝트
    public GameObject LobbyTap_Obj; //로비탭 오브젝트 (로비탭으로 갈 건지 확인 처리 필요)
    public GameObject ExitTap_Obj; //종료탭 오브젝트 (종료할지 확인 처리 필요)

    public bool bFadeIn = true; //페이드인이 true일 경우에는 UI나 스테이지 이동 등이 비활성화 되게 막음. 그리고 false일 경우에
                                //페이드 아웃을 실행하면 아래의 변수에 따른 함수 처리를 실행

    public bool bLobbyOn = false; //bFadeIn이 false고 현재 변수가 true일 경우에 페이드 아웃이 되면 메뉴 씬으로 이동

    public bool bExitOn = false; //bExitOn이 false고 현재 변수가 true일 경우에 페이드 아웃 되면 게임 종료됨

    public bool bBattleOn = false; //bExitOn이 false고 현재 변수가 true일 경우 페이드 아웃 되면 battle 씬으로 이동함.

    private void Start()
    {
        m_DataManager = GameObject.FindGameObjectWithTag("Info").GetComponent<DataManager>();
      

        //데이터 변수 참조 하는 거
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) //esc 누르면 퍼스 메뉴 on/off 하는 기능
        {
            PauseTap();
        }
    }

    public void CheckFade()
    {
        if (bFadeIn == true) //페이드인 종료
        {
            bFadeIn = false;

            return;
        }
        else
        {
            bFadeIn = true;
        }

        if (bExitOn==true) //종료하는 것인지
        {
            ExitGame();
            return;
        }

        if(bLobbyOn==true) //로비로 가는 것인지
        {
            GoToLobby();
            return;
        }

        if(bBattleOn==true) //배틀씬으로 이동하는 것인지
        {
            GoToBattle();
            return;
        }
        
    }

    public void ReadyToExit() //종료할 준비
    {
        bExitOn = true;

        DataManager.Instance.Save();

        FadeStart();
    }

    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit(); // 어플리케이션 종료
#endif
    }

    public void ReadyToLobby() //로비씬으로 갈 준비
    {
        bLobbyOn = true;

        DataManager.Instance.Save();

        FadeStart();
    }

    private void GoToLobby()
    {
        SceneManager.LoadScene("2_LobbyScene");
    }

    public void ReadyToBattle() //배틀씬으로 갈 준비
    {
        bBattleOn = true;

        FadeStart();
    }

    private void GoToBattle()
    {
        SceneManager.LoadScene("Battle");
    }

    public void FadeStart() //페이드아웃 시작
    {
        Debug.Log("ㅊ");
        Time.timeScale = 1;
        //StageFade_Img.raycastTarget = true;
        StageFade_Anim.SetBool("Fade", true);
    }

    public void MusicControl() //사운드 시작
    {
        m_SMM.PlayBGM();
    }


    public void PauseTap()
    {
        if(StageFade_Img.raycastTarget==true) //페이드인이 아직 안 끝났을 경우 모든 동작을 리턴 시킨다.
        {
            return;
        }

        if(SettingTap_Obj.activeSelf==true) //세팅 메뉴가 켜졌을 경우 세팅메뉴 끄고 버튼 그룹 활성화
        {
            SettingTap_Obj.SetActive(false);
            m_SMM.SoundValueSave();
            ButtonGroup_Obj.SetActive(true);

            return;
        }

        if(LobbyTap_Obj.activeSelf==true) //로비로 나갈지 묻는 메뉴가 켜졌을 경우 끄고 버튼 캔버스 그룹 interactable 활성화
        {
            LobbyTap_Obj.SetActive(false);
            ButtonGroup_CG.interactable = true;

            return;
        }

        if(ExitTap_Obj.activeSelf==true) //종료할지 묻는 메뉴, 위와 마찬가지임
        {
            ExitTap_Obj.SetActive(false);
            ButtonGroup_CG.interactable = true;

            return;
        }

        if(PauseTap_Obj.activeSelf==true) //정지 메뉴가 활성화 됐을 경우에는 끄고 정지 해제, 반대일 경우에는 활성화 시키고 정지
        {
            PauseTap_Obj.SetActive(false);
            Time.timeScale = 1f;

            return;
        }
        else
        {
            PauseTap_Obj.SetActive(true);
            Time.timeScale = 0f;

            return;
        }
    }

    public void SettingTapOn() //세팅 탭 켜기
    {
        ButtonGroup_Obj.SetActive(false);
        SettingTap_Obj.SetActive(true);
    }

    public void LobbyMenuTapOn() //로비 탭 켜기
    {
        ButtonGroup_CG.interactable = false;
        LobbyTap_Obj.SetActive(true);
    }

    public void ExitMenuTapOn() //종료 탭 켜기
    {
        ButtonGroup_CG.interactable = false;
        ExitTap_Obj.SetActive(true);
    }
}
