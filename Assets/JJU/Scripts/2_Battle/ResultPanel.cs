using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultPanel : MonoBehaviour
{
    public Image itemSprite;
    public TextMeshProUGUI ItemNameText;


    /// <summary>
    /// �Ѱ���ġ��
    /// </summary>
    public float totalEXP_dealer;
    public float totalEXP_healer;
    public float totalEXP_tanker;


    /// <summary>
    /// ����ġ �䱸��
    /// </summary>
    public float DealerMaxExp;
    public float HealerMaxExp;
    public float TankerMaxExp;


    /// <summary>
    /// ����ġ �����ӵ�
    /// </summary>
    public float dealerExpIncreasedPerSecond;
    public float healerExpIncreasedPerSecond;
    public float tankerExpIncreasedPerSecond;

    /// <summary>
    /// ����ġ ���α׷�����
    /// </summary>
    public Slider dealerSlider;
    public Slider healerSlider;
    public Slider tankerSlider;

    /// <summary>
    /// ����� ������Ʈ��
    /// </summary>
    public Hero dealer;
    public Hero healer;
    public Hero tanker;


    /// <summary>
    /// Update ���࿩�� üũ�� bool
    /// </summary>
    bool dealerExp;
    bool healerExp;
    bool TankerExp;

    /// <summary>
    /// ����� ���� �ؽ�Ʈ
    /// </summary>
    public TextMeshProUGUI dealerLevelText;
    public TextMeshProUGUI healerLevelText;
    public TextMeshProUGUI tankerLevelText;


    /// <summary>
    /// ����� ���� ����ġ�� �ؽ�Ʈ
    /// </summary>
    public TextMeshProUGUI dealerExpText;
    public TextMeshProUGUI healerExpText;
    public TextMeshProUGUI tankerExpText;

    public TextMeshProUGUI totalGoldText;
    public TextMeshProUGUI stageGetGoldText;



    private void Start()
    {
        dealerExpIncreasedPerSecond = 100f;

        DealerMaxExp = GameInstance.Instance.gamdData.ExperienceTable[dealer.heroLevel - 1];
        HealerMaxExp = GameInstance.Instance.gamdData.ExperienceTable[healer.heroLevel - 1];
        TankerMaxExp = GameInstance.Instance.gamdData.ExperienceTable[tanker.heroLevel - 1];
    }




    private void Awake()
    {
        //���� �ؽ�Ʈ �ʱ�ȭ
        dealerLevelText.text = DataManager.Instance.m_PlayerData.nLevel[0].ToString();
        healerLevelText.text = DataManager.Instance.m_PlayerData.nLevel[2].ToString();
        tankerLevelText.text = DataManager.Instance.m_PlayerData.nLevel[1].ToString();

        //dealerLevelText.text = GameInstance.Instance.gamdData.dealerData.Level.ToString();
        //healerLevelText.text = GameInstance.Instance.gamdData.healerData.Level.ToString();
        //tankerLevelText.text = GameInstance.Instance.gamdData.tankerData.Level.ToString();
        foreach (var hero in BattleManager.I.heroCollection.collection)
        {
            switch (hero.job)
            {
                case PlayerJob.Dealer:
                    dealer = hero;
                    break;

                case PlayerJob.Healer:
                    healer = hero;
                    break;

                case PlayerJob.Tanker:
                    tanker = hero;
                    break;
            }
        }
    }


    public int ranItemIndex;

    public int dropGold;

    public int dropCurGold = 0;

    private void OnEnable()
    {
        StartCoroutine(StartDelay());

        ranItemIndex = BattleManager.I.ItemDrop();
        

        itemSprite.sprite = DataManager.Instance.Item_Spr[ranItemIndex];
        ItemNameText.text = DataManager.Instance.MyItemList[ranItemIndex].sName;

        DataManager.Instance.MyItemList[ranItemIndex].bUnlock = true;


        //����ġ �ʱ�ȸ
        totalEXP_dealer = BattleManager.I.TotalExp;
        totalEXP_healer = BattleManager.I.TotalExp;
        totalEXP_tanker = BattleManager.I.TotalExp;



        dealerExpText.text = totalEXP_dealer.ToString();
        healerExpText.text = totalEXP_healer.ToString();
        tankerExpText.text = totalEXP_tanker.ToString();

        DataManager.Instance.SavePlayerDataToJson();
        DataManager.Instance.SaveListDataToJson();

        dropGold = 200;


    }




    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(1f);
        StartExp();

        while (true)
        {
            if (dropCurGold == dropGold)
                break;
            dropCurGold = (int)Mathf.Round(Mathf.Lerp(dropCurGold, 200f, 0.1f));
            stageGetGoldText.text = dropCurGold.ToString();
            yield return null;
        }

    }




    void StartExp()
    {
        foreach (var hero in BattleManager.I.heroCollection.collection)
        {
            if (!hero.bDie)
            {
                switch (hero.job)
                {
                    case PlayerJob.Dealer:
                        dealerExpIncreasedPerSecond =
                            GameInstance.Instance.gamdData.ExperienceTable[dealer.heroLevel - 1];
                        dealerExp = true;
                        break;

                    case PlayerJob.Healer:
                        healerExpIncreasedPerSecond =
                        GameInstance.Instance.gamdData.ExperienceTable[healer.heroLevel - 1];
                        healerExp = true;
                        break;

                    case PlayerJob.Tanker:
                        tankerExpIncreasedPerSecond =
                        GameInstance.Instance.gamdData.ExperienceTable[tanker.heroLevel - 1];
                        TankerExp = true;
                        break;
                }
            }

        }
    }


    private void Update()
    {
        if (dealerExp)
        {
            DealerUpdate();
        }

        if (healerExp)
        {
            HealerUpdate();
        }

        if (TankerExp)
        {
            TankerUpdate();
        }
    }




    void DealerUpdate()
    {
        if (totalEXP_dealer < 0f)
        {
            print("��");
            dealerExp = false;
            DataManager.Instance.m_PlayerData.fExp[0] = Mathf.Round(dealer.Experience);
            //GameInstance.Instance.gamdData.dealerData.Exp = Mathf.Round(dealer.Experience);
            dealerExpText.text = "";
            return;
        }
        //�� �÷��ֱ�
        dealer.Experience += dealerExpIncreasedPerSecond * Time.deltaTime;
        totalEXP_dealer -= dealerExpIncreasedPerSecond * Time.deltaTime;
        dealerExpText.text = Mathf.Round(totalEXP_dealer).ToString();
        dealerSlider.value = dealer.Experience / DealerMaxExp;



        if (dealer.Experience > DealerMaxExp)
        {
            DataManager.Instance.m_PlayerData.nLevel[0]++;
            //GameInstance.Instance.gamdData.dealerData.Level++;

            dealer.heroLevel++;

            dealerLevelText.text = DataManager.Instance.m_PlayerData.nLevel[0].ToString();
            //dealerLevelText.text = GameInstance.Instance.gamdData.dealerData.Level.ToString();
            dealer.Experience = 0f;

            DealerMaxExp = GameInstance.Instance.gamdData.ExperienceTable[DataManager.Instance.m_PlayerData.nLevel[0] - 1];

            //����ġ �ӵ� ������
            dealerExpIncreasedPerSecond =
            GameInstance.Instance.gamdData.ExperienceTable[dealer.heroLevel - 1];

            dealerSlider.value = 0f;
        }
    }




    void HealerUpdate()
    {
        if (totalEXP_healer < 0f)
        {
            print("��");
            healerExp = false;

            DataManager.Instance.m_PlayerData.fExp[2] = Mathf.Round(healer.Experience);
            //GameInstance.Instance.gamdData.healerData.Exp = Mathf.Round(healer.Experience);
            healerExpText.text = "";
            return;
        }
        //�� �÷��ֱ�
        healer.Experience += healerExpIncreasedPerSecond * Time.deltaTime;
        totalEXP_healer -= healerExpIncreasedPerSecond * Time.deltaTime;
        healerSlider.value = healer.Experience / HealerMaxExp;
        healerExpText.text = Mathf.Round(totalEXP_healer).ToString();



        if (healer.Experience > HealerMaxExp)
        {
            DataManager.Instance.m_PlayerData.nLevel[2]++;
            //GameInstance.Instance.gamdData.healerData.Level++;
            healer.heroLevel++;

            healerLevelText.text = DataManager.Instance.m_PlayerData.nLevel[2].ToString();
            //healerLevelText.text = GameInstance.Instance.gamdData.healerData.Level.ToString();
            healer.Experience = 0f;
            HealerMaxExp = GameInstance.Instance.gamdData.ExperienceTable[DataManager.Instance.m_PlayerData.nLevel[2] - 1];

            //����ġ �ӵ� ������
            healerExpIncreasedPerSecond =
            GameInstance.Instance.gamdData.ExperienceTable[healer.heroLevel - 1];


            healerSlider.value = 0f;
            print("����������");
        }
    }




    void TankerUpdate()
    {
        if (totalEXP_tanker < 0f)
        {
            print("��");
            TankerExp = false;

            DataManager.Instance.m_PlayerData.fExp[1] = Mathf.Round(tanker.Experience);
            //GameInstance.Instance.gamdData.tankerData.Exp = Mathf.Round(tanker.Experience);
            tankerExpText.text = "";
            return;
        }
        //�� �÷��ֱ�
        tanker.Experience += tankerExpIncreasedPerSecond * Time.deltaTime;
        totalEXP_tanker -= tankerExpIncreasedPerSecond * Time.deltaTime;
        tankerSlider.value = tanker.Experience / TankerMaxExp;
        tankerExpText.text = Mathf.Round(totalEXP_tanker).ToString();



        if (tanker.Experience > TankerMaxExp)
        {
            DataManager.Instance.m_PlayerData.nLevel[1]++;
            //GameInstance.Instance.gamdData.tankerData.Level++;
            tanker.heroLevel++;

            tankerLevelText.text = DataManager.Instance.m_PlayerData.nLevel[1].ToString();
            //tankerLevelText.text = GameInstance.Instance.gamdData.tankerData.Level.ToString();
            tanker.Experience = 0f;
            TankerMaxExp = GameInstance.Instance.gamdData.ExperienceTable[DataManager.Instance.m_PlayerData.nLevel[1] - 1];

            //����ġ �ӵ� ������
            tankerExpIncreasedPerSecond =
            GameInstance.Instance.gamdData.ExperienceTable[tanker.heroLevel - 1];

            tankerSlider.value = 0f;
        }
    }




    public void BackToStageScene()
    {
        SceneLoad.LoadScene(BattleManager.I.nowStage);
    }
}