using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

enum POINT_TYPE { BATTLE, SP_BATTLE, ITEM, SHOP, HEALLING, BOSS }

public class Point : MonoBehaviour
{
    [SerializeField]
    POINT_TYPE type = new POINT_TYPE();

    public ENEMY_TYPE[] enemies;
    public ENEMY_TYPE eliteEnemy;

    public Animator StageInfoAnim;

    public Sprite notOpenSprite;
    public Sprite openSprite;
    public Sprite clearSprite;

    public Image spriteImage;

    [SerializeField]
    public GameObject[] egdes;

    public GraphNode<Point> node;

    public Vector3 worldPos;
    public bool bClicked;

    public StageInfoManager m_StageInfoManager;

    [Header("������")]
    
    public int stageNum;

    /// <summary>
    /// ������ġ
    /// </summary>
    public float fX;

    /// <summary>
    /// ������ġ
    /// </summary>
    public float fY;

    /// <summary>
    /// �ش� ���������� ���µǾ� �ִ��� üũ
    /// </summary>
    public bool bOpen;

    /// <summary>
    /// �ش� ����Ʈ Ŭ�����
    /// </summary>
    public bool bClear;

    /// <summary>
    /// �ش罺�������� ��ġ�� �ִ��� üũ
    /// </summary>
    public bool bStay;

    /// <summary>
    /// �̵��� ��
    /// </summary>
    public string moveSceneName;



    private void Awake()
    {
        InitVertex();
        worldPos = Camera.main.ScreenToWorldPoint(transform.position);
        worldPos.x = (float)(Math.Truncate(worldPos.x * 100) / 100);
        worldPos.y = (float)(Math.Truncate(worldPos.y * 100) / 100);

    }

    private void Start()
    {
        bOpen = GameInstance.Instance.gamdData.pathData[stageNum].pathOpen;
        bClear = GameInstance.Instance.gamdData.pathData[stageNum].pathClear;
        fX = (float)(Math.Truncate(transform.position.x * 100) / 100);
        fY = (float)(Math.Truncate(transform.position.y * 100) / 100);

        if (bOpen && !bClear)
        {
            spriteImage.sprite = openSprite;

        }
        else if (bClear)
        {
            spriteImage.sprite = clearSprite;
        }
        else
        {
            spriteImage.sprite = notOpenSprite;
        }
    }

    public void InitVertex()
    {
        node = new GraphNode<Point>();
        node.Data = this;

         
        //foreach (GameObject point in egdes)
        //{
        //    if (node != null)
        //    {
        //        node._neighbors.Add(
        //        point.GetComponent<Point>().node);
        //    }

        //}
    }



    //public void ClearAndOpenNextPoint()
    //{
    //    this.bClear = true;
    //    foreach(Point p in NextPoint)
    //    {
    //        p.bOpen = true;
    //    }
    //}


    public void ClickButton()
    {
        if (!bOpen || bClear)
            return;
        //StageScene.I.MapInfoPanel.SetActive(true);

        if (!bClicked)
        {
            StageScene.I.selectedPoint = this;
            //StageScene.I.MapInfoPanel.SetActive(true);

            m_StageInfoManager.ReadSetSlot(stageNum);

            StageInfoAnim.SetBool("Click", true);

            foreach (var point in StageScene.I.points)
            {
                Point pointComp =point.GetComponent<Point>();
                if (pointComp.bClicked == true)
                {                   
                    pointComp.bClicked = false;
                }
            }
            bClicked = true;
            return;
        }    
        
        //StartPointEvent();
    }


    /// <summary>
    /// �ش� ����Ʈ�� �̵������� ����Ǵ� �Լ�
    /// </summary>
    public void StartPointEvent()
    {
        GameInstance.Instance.nowStage = stageNum;
        switch (enemies.Length)
        {
            case 1:
                GameInstance.Instance.enemies = new ENEMY_TYPE[enemies.Length];
                GameInstance.Instance.enemies[0] = enemies[0];
                GameInstance.Instance.eliteEnemy = eliteEnemy;
                break;

            case 2:
                GameInstance.Instance.enemies = new ENEMY_TYPE[enemies.Length];
                GameInstance.Instance.enemies[0] = enemies[0];
                GameInstance.Instance.enemies[1] = enemies[1];
                GameInstance.Instance.eliteEnemy = eliteEnemy;
                break;

            case 3:
                GameInstance.Instance.enemies = new ENEMY_TYPE[enemies.Length];
                GameInstance.Instance.enemies[0] = enemies[0];
                GameInstance.Instance.enemies[1] = enemies[1];
                GameInstance.Instance.enemies[2] = enemies[2];
                GameInstance.Instance.eliteEnemy = eliteEnemy;
                break;
        }

        //GameInstance.Instance.gamdData.InPoint = this;
        switch (type)
        {
            case POINT_TYPE.BATTLE:
                //��Ʋ ���������� �̵��ϱ�
                StartCoroutine(BattleRoom()); 
                print("��Ʋ���̵�");
                break;

            case POINT_TYPE.SP_BATTLE:
                //Ư�� ���������� �̵�
                SpecialRoom();
                break;

            case POINT_TYPE.ITEM:
                //������ 3���߿� �Ѱ��� ���ؼ� ��������
                ItemRoom();
                break;

            case POINT_TYPE.SHOP:
                //���� �̿�
                ShopRoom();
                break;

            case POINT_TYPE.HEALLING:
                //ü���� �Ϻ� ȸ���ϴ� ��
                HealingRoom();
                break;

            case POINT_TYPE.BOSS:
                //������ ������ �������� ��
                BossRoom();
                break;

        }
    }

    ////////////////////////////////////////////////////////////////
    //---------------------����� �����Լ�------------------------//
    ////////////////////////////////////////////////////////////////
    

    private IEnumerator BattleRoom()
    {
        StageScene.I.FadeAnimator.SetTrigger("Fade");
        yield return new WaitForSeconds(2f);
        SceneLoad.LoadScene("Battle");
    }

    private void SpecialRoom()
    {
        print("����ȷ�");
    }

    private void ItemRoom()
    {
        print("�����۷�");
    }

    private void ShopRoom()
    {
        print("������");
    }

    private void HealingRoom()
    {
        print("���� ��");
    }

    private void BossRoom()
    {
        print("������");
    }
}
