using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    DataManager m_DataManager; //������ ������ ���� ������ �Ŵ��� ���� �ʿ�

    public Animator StageFade_Anim; //������������ ��Ʋ�� �ѱ�� ���� �ִϸ����� �ʿ�
    public RawImage StageFade_Img; //�ش� ���̵� �̹����� �۵� ���ϴ��� raycasttarget���� Ȯ���ϱ� ���� �ʿ�

    public SoundMixerManager m_SMM; //���� ���� ���ؼ� �ʿ�

    public GameObject PauseTap_Obj; //������ ������Ʈ
    public GameObject ButtonGroup_Obj; //��ư�׷� ������Ʈ
    public CanvasGroup ButtonGroup_CG; //��ư�׷쿡 ���� ĵ���� �׷�
    public GameObject SettingTap_Obj; //������ ������Ʈ
    public GameObject LobbyTap_Obj; //�κ��� ������Ʈ (�κ������� �� ���� Ȯ�� ó�� �ʿ�)
    public GameObject ExitTap_Obj; //������ ������Ʈ (�������� Ȯ�� ó�� �ʿ�)

    public bool bFadeIn = true; //���̵����� true�� ��쿡�� UI�� �������� �̵� ���� ��Ȱ��ȭ �ǰ� ����. �׸��� false�� ��쿡
                                //���̵� �ƿ��� �����ϸ� �Ʒ��� ������ ���� �Լ� ó���� ����

    public bool bLobbyOn = false; //bFadeIn�� false�� ���� ������ true�� ��쿡 ���̵� �ƿ��� �Ǹ� �޴� ������ �̵�

    public bool bExitOn = false; //bExitOn�� false�� ���� ������ true�� ��쿡 ���̵� �ƿ� �Ǹ� ���� �����

    public bool bBattleOn = false; //bExitOn�� false�� ���� ������ true�� ��� ���̵� �ƿ� �Ǹ� battle ������ �̵���.

    private void Start()
    {
        m_DataManager = GameObject.FindGameObjectWithTag("Info").GetComponent<DataManager>();
      

        //������ ���� ���� �ϴ� ��
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) //esc ������ �۽� �޴� on/off �ϴ� ���
        {
            PauseTap();
        }
    }

    public void CheckFade()
    {
        if (bFadeIn == true) //���̵��� ����
        {
            bFadeIn = false;

            return;
        }
        else
        {
            bFadeIn = true;
        }

        if (bExitOn==true) //�����ϴ� ������
        {
            ExitGame();
            return;
        }

        if(bLobbyOn==true) //�κ�� ���� ������
        {
            GoToLobby();
            return;
        }

        if(bBattleOn==true) //��Ʋ������ �̵��ϴ� ������
        {
            GoToBattle();
            return;
        }
        
    }

    public void ReadyToExit() //������ �غ�
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
                Application.Quit(); // ���ø����̼� ����
#endif
    }

    public void ReadyToLobby() //�κ������ �� �غ�
    {
        bLobbyOn = true;

        DataManager.Instance.Save();

        FadeStart();
    }

    private void GoToLobby()
    {
        SceneManager.LoadScene("2_LobbyScene");
    }

    public void ReadyToBattle() //��Ʋ������ �� �غ�
    {
        bBattleOn = true;

        FadeStart();
    }

    private void GoToBattle()
    {
        SceneManager.LoadScene("Battle");
    }

    public void FadeStart() //���̵�ƿ� ����
    {
        Debug.Log("��");
        Time.timeScale = 1;
        //StageFade_Img.raycastTarget = true;
        StageFade_Anim.SetBool("Fade", true);
    }

    public void MusicControl() //���� ����
    {
        m_SMM.PlayBGM();
    }


    public void PauseTap()
    {
        if(StageFade_Img.raycastTarget==true) //���̵����� ���� �� ������ ��� ��� ������ ���� ��Ų��.
        {
            return;
        }

        if(SettingTap_Obj.activeSelf==true) //���� �޴��� ������ ��� ���ø޴� ���� ��ư �׷� Ȱ��ȭ
        {
            SettingTap_Obj.SetActive(false);
            m_SMM.SoundValueSave();
            ButtonGroup_Obj.SetActive(true);

            return;
        }

        if(LobbyTap_Obj.activeSelf==true) //�κ�� ������ ���� �޴��� ������ ��� ���� ��ư ĵ���� �׷� interactable Ȱ��ȭ
        {
            LobbyTap_Obj.SetActive(false);
            ButtonGroup_CG.interactable = true;

            return;
        }

        if(ExitTap_Obj.activeSelf==true) //�������� ���� �޴�, ���� ����������
        {
            ExitTap_Obj.SetActive(false);
            ButtonGroup_CG.interactable = true;

            return;
        }

        if(PauseTap_Obj.activeSelf==true) //���� �޴��� Ȱ��ȭ ���� ��쿡�� ���� ���� ����, �ݴ��� ��쿡�� Ȱ��ȭ ��Ű�� ����
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

    public void SettingTapOn() //���� �� �ѱ�
    {
        ButtonGroup_Obj.SetActive(false);
        SettingTap_Obj.SetActive(true);
    }

    public void LobbyMenuTapOn() //�κ� �� �ѱ�
    {
        ButtonGroup_CG.interactable = false;
        LobbyTap_Obj.SetActive(true);
    }

    public void ExitMenuTapOn() //���� �� �ѱ�
    {
        ButtonGroup_CG.interactable = false;
        ExitTap_Obj.SetActive(true);
    }
}
