using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MHomiLibrary;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;

public class BattleManager : HSingleton<BattleManager>
{
    #region[������]
    
    [Header (" ������ ")]
    [Space(10f)]
    /// <summary>
    /// ���� �������� �����ϴ� �μ�
    /// </summary>
    public string nowStage;       

    public int stageNum;

    public bool bScrolling;

    public int unitArriveCheck = 0;

    public Transform[] bulletOffsets;

    /// <summary>
    /// ����ε��� ��� ����Ʈ
    /// </summary>
    public HeroCollection heroCollection;

    public int battleNum = 1;

    /// <summary>
    /// ������ ��� ����Ʈ
    /// </summary>
    public EnemyCollection enemyCollection;

    /// <summary>
    /// ����ȭ�� ���� ���ֵ��� ��� ����Ʈ
    /// </summary>
    public List<Unit> unitList = new List<Unit>();

    /// <summary>
    /// ���� ���� �������ִ� ����
    /// </summary>
    public Unit turnUnit;

    [SerializeField]
    /// <summary>
    /// ����óġ �ջ� ����ġ
    /// </summary>
    private int totalEXP;

    public ResultPanel resultPanel;

    
    public ENEMY_TYPE[] EnemyType;
    public ENEMY_TYPE EliteEnemy;

    public Vector3[] enemyDestinationPosV3;
    public Vector3[] enemySpawnPosV3;

    /// <summary>
    /// ��ų �ؽ�Ʈ ��¿� �ؽ�Ʈ
    /// </summary>
    public TextMeshProUGUI skillText;

    private StageMonsterList[] monsterList;

    /// <summary>
    /// ����ִ� ���� ����
    /// </summary>
    public int aliveUnitCount;

    Enemy[] DestroyEnemys = new Enemy[6];

    public GameObject heroBattlePanel;

    List<Unit> turnNoCheckedUnit = new List<Unit>();

    public int passiveCount;
    
    //������ ����Ʈ
    private List<ItemData> CommonDeck = new List<ItemData>();
    private List<ItemData> RareDeck = new List<ItemData>();
    private List<ItemData> EpicDeck = new List<ItemData>();
    private List<ItemData> LegendDeck = new List<ItemData>();

    [Header("�¸� ���â")]
    [Space(10f)]
    /// <summary>
    /// �¸� �� ���â
    /// </summary>
    public GameObject victoryResultPanel;

    #endregion



    private void Awake()
    {
        stageNum = GameInstance.Instance.nowStage;
        monsterList = GameInstance.Instance.MonsterSpawnList;
        InitDestinationVec3();
        //SpawnMonster();
        //enemyCollection.Init();
        //turnUnit = new Unit();
        //EnemyMoveToDestinationPos();

    }
    void Start()
    {
        StartCoroutine(StartBattle());

    }

    public int TotalExp
    {
        get { return totalEXP; }
        set { totalEXP = value; }
    }
    

    private IEnumerator StartBattle()
    {
        BattleScene.I.BattleStatePopUp.SetActive(true);
        BattleScene.I.BattleStateTextUpdate(battleNum);
        SpawnMonster();
        enemyCollection.Init();
        turnUnit = new Unit();
        foreach (Hero hero in heroCollection.collection)
        {
            if (!hero.gameObject.activeSelf)
                continue;
            unitList.Add(hero.transform.GetComponent<Unit>());
        }
        foreach (Enemy enemy in enemyCollection.collection)
        {
            if (!enemy.gameObject.activeSelf)
                continue;
            unitList.Add(enemy.transform.GetComponent<Unit>());
        }

        ScrollingAndUnitAnimationChange();
        yield return new WaitForSeconds(1.5f);
        BattleScene.I.BattleStatePopUp.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        BattleScene.I.WarningBar_Panel.SetActive(true);
        yield return new WaitForSeconds(1f);
        EnemyMoveToDestinationPos();
        yield return new WaitForSeconds(3f);
        BattleScene.I.WarningBar_Panel.SetActive(false);
    }

    public IEnumerator UnitArriveCheck()
    {
        if (unitArriveCheck == enemyCollection.collection.Count)
        {
            //BattleScene.I.BattleStatePopUp.SetActive(false);
            unitArriveCheck = 0;
            bScrolling = false;
            foreach (Enemy enemy in enemyCollection.collection)
            {
                //�нú꽺ų�� ������ �ߵ�
                if(enemy.passiveSKill != PassiveSKill.NONE)
                {
                    switch (enemy.passiveSKill)
                    {
                        case PassiveSKill.BUFF_PHYSICAL_ATTACK_POWER:
                            StartCoroutine(enemy.Passive_AOE_AttackIncrease(enemy.animator, 
                                                                              enemy.unit));
                            yield return new WaitForSeconds(2f);
                            break;
                        case PassiveSKill.REVIVE_INFECTED_RAT:
                            StartCoroutine(enemy.Passive_InfectRat());
                            break;
                        case PassiveSKill.Mastered_BlackMagic:
                            StartCoroutine(enemy.Passive_Mastered_BlackMagic(enemy.animator,
                                                                              enemy.unit));
                            yield return new WaitForSeconds(1.7f);
                            break;
                        case PassiveSKill.BrainOfReach:
                            StartCoroutine(enemy.BrainOfReach(enemy));
                            yield return new WaitForSeconds(2f);
                            break;
                    }
                }
            }
            yield return new WaitForSeconds(0.5f);
            Turn();
        }   
    }


    void SpawnMonster()
    {
        GameObject enemy;
     
        switch (battleNum)
        {
            case 1:
                //    GameObject enemy;
                for (int i = 0; i < monsterList[stageNum].wave1Monster.Length; i++)
                {
                    enemy = GameInstance.Instance.CreateEnemyPrefab(monsterList[stageNum].wave1Monster[i].ToString(),
                                                                    enemySpawnPosV3[i],
                                                                    Vector3.one * 0.5f,
                                                                    Quaternion.identity);
                    enemy.transform.parent = enemyCollection.transform;
                }
                break;
            case 2:
                //    GameObject enemy;
                for (int i = 0; i < monsterList[stageNum].wave2Monster.Length; i++)
                {
                    enemy = GameInstance.Instance.CreateEnemyPrefab(monsterList[stageNum].wave2Monster[i].ToString(),
                                                                    enemySpawnPosV3[i],
                                                                    Vector3.one * 0.5f,
                                                                    Quaternion.identity);
                    enemy.transform.parent = enemyCollection.transform;
                }
                break;
        }

    }
    void SpawnEliteMonster()
    {
        //1
        GameObject enemy1 = GameInstance.Instance.CreateEnemyPrefab(monsterList[stageNum].wave3Monster[0].ToString(),
                                                enemySpawnPosV3[0],
                                                Vector3.one * 0.5f,
                                                Quaternion.identity);
        enemy1.transform.parent = enemyCollection.transform;


        //2
        GameObject enemy2 = GameInstance.Instance.CreateEnemyPrefab(monsterList[stageNum].wave3Monster[1].ToString(),
                                        enemySpawnPosV3[2],
                                        Vector3.one * 0.5f,
                                        Quaternion.identity);
        enemy2.transform.parent = enemyCollection.transform;


        //3
        GameObject enemy3 = GameInstance.Instance.CreateEnemyPrefab(monsterList[stageNum].wave3Monster[2].ToString(),
                                        enemySpawnPosV3[4],
                                        Vector3.one * 0.5f,
                                        Quaternion.identity);
        enemy3.transform.parent = enemyCollection.transform;


        //Boss
        GameObject enemy4 = GameInstance.Instance.CreateEnemyPrefab(monsterList[stageNum].BossMonster.ToString(),
                                        enemySpawnPosV3[3],
                                        Vector3.one * 1f,
                                        Quaternion.identity);
        enemy4.transform.parent = enemyCollection.transform;
    }
    /// <summary>
    /// �� üũ�ؼ� ���ֿ��� �� �ο����ִ� �Լ�
    /// </summary>
    void Turn()
    {
        ///�� üũ�� �ӽ� ���ú���
        Unit TurnOrderUnit;
        //���ǵ� ���ؼ� ������� ������ ���������� �����ϱ�
        foreach(Unit unit in unitList)
        {
            if (unit.GetComponent<Enemy>() != null)
            {
                unit.MonsterStatIconUpdate();
            }
            else
            {
                unit.HeroStatIconUpdate();
            }

            //���ֵ� ���ÿ��� �ʱ�ȭ
            if (unit.GetComponent<Enemy>())
            {
                unit.GetComponent<SortingGroup>().sortingOrder = unit.GetComponent<Enemy>().SortLayerOrder;
            }
            else
            {
                unit.GetComponent<SortingGroup>().sortingOrder = 200;
            }



            unit.turnChecked = false;
            unit.noTurnSelected = false;
            if (unit.bTurnover || unit.bDie)
            {
                continue;           
            }
            if (!turnUnit || unit.Speed  > turnUnit.Speed )
            {
                turnUnit = unit;
                
            }
        }
        turnUnit.turnOrder = 0;
        turnUnit.turnChecked = true;
    
        
    
    
    
        int turnCount = 0;
        //�� ������ ���� �˻�
        for(int i = 1; i < unitList.Count; i++)
        {
            TurnOrderUnit = null;
            foreach (Unit unit in unitList)
            {
                if (unit.bTurnover)
                {
                    turnNoCheckedUnit.Add(unit);
                    continue;
                }
                if (unit.bDie || unit.turnChecked)
                    continue;
                if (!TurnOrderUnit || unit.Speed > TurnOrderUnit.Speed)
                {
                    TurnOrderUnit = unit;
                }
            }
            if (TurnOrderUnit)
            {
                TurnOrderUnit.turnOrder = i;
                TurnOrderUnit.turnChecked = true;
                turnCount = i;
            }
        }

        turnCount++;

        //�� ���� ���� �˻�
        for (int i = 0; i < turnNoCheckedUnit.Count ; i++)
        {
            TurnOrderUnit = null;
            foreach (Unit unit in turnNoCheckedUnit)
            {
                if(unit.bDie || unit.noTurnSelected)
                {
                    continue;
                }
                if(!TurnOrderUnit || unit.Speed > TurnOrderUnit.Speed)
                {
                    TurnOrderUnit = unit;
                }
            }
            if (TurnOrderUnit)
            {
                TurnOrderUnit.turnOrder = turnCount;
                TurnOrderUnit.noTurnSelected = true;
                turnCount++;
            }
        }
        BattleScene.I.turnTextUpdate();
        turnNoCheckedUnit.Clear();
        turnUnit.bTurn = true;

        turnUnit.arrow.SetActive(true);

        ////�� ������ ����ζ��
        //if (turnUnit.GetComponent<Hero>())
        //{

        //}
        if (turnUnit.GetComponent<Enemy>())
        {
            StartCoroutine(EnemyTurn());
        }
        else
        {
            BattleScene.I.CharacterUI_Update();
            turnUnit.GetComponent<SortingGroup>().sortingOrder = 250;
            heroBattlePanel.SetActive(true);
        }
        BattleScene.I.turnTextUpdate();
    }
    public int AliveUnitCountCheck()
    {
        aliveUnitCount = 0;
        foreach (Unit unit in unitList)
        {
            if (!unit.bDie)
                aliveUnitCount++;
        }
        return aliveUnitCount;
    }
    /// <summary>
    /// ���� �������� �Ѿ�� �Լ�
    /// </summary>
    public void NextTurn()
    {
        turnUnit.arrow.SetActive(false);
        turnUnit.bTurn = false;
        turnUnit.bTurnover = true;
        turnUnit = null;
        BattleScene.I.attack_Trail.SetActive(false);
        BattleScene.I.skill1_Trail.SetActive(false);
        BattleScene.I.skill2_Trail.SetActive(false);
        BattleScene.I.skill3_Trail.SetActive(false);

        foreach (Unit unit in unitList)
        {
            unit.arrow.SetActive(false);
        }

        //���� ��� óġ�ߴ��� Ȯ��
        if (EnemyAllDie())
            return;

        //�Ʊ� ��� üũ
        if (HeroAllDie())
            return;

        //����� ���� �������� üũ
        if (TurnOverCheck())
        {
            StartCoroutine(unitPassiveCheck());
            //Turn();
            foreach (Unit unit in unitList)
            {
                unit.BuffAndDeBuffDurationDecrease();
                unit.TickDamageDecrese();
                unit.HeroPassiveCheck();
            }
            return;
        }



        //������ ����
        Turn();
    }
    /// <summary>
    /// �ϸ��� ������ ���ֵ� �нú갡 �ִ��� üũ
    /// </summary>
    /// <returns></returns>
    public IEnumerator unitPassiveCheck()
    {
        foreach(Unit unit in unitList)
        {
            //���϶�
            if (unit.GetComponent<Enemy>())
            {

                var enemy = unit.GetComponent<Enemy>();
                if (!enemy)
                    continue;
                //�ϸ��� �����ϴ� �нú꽺ų�� �ִٸ�
                if(enemy.passiveSKill > PassiveSKill.TurnPassive && enemy.passiveSKill < PassiveSKill.TickPassive)
                {
                    switch (enemy.passiveSKill) 
                    {
                        case PassiveSKill.UndeadForce:
                            passiveCount++;
                            StartCoroutine(enemy.UndeadForce());                            
                            yield return new WaitForSeconds(2.5f);
                            continue;

                        //case PassiveSKill.SimpleMagic:
                        //    StartCoroutine(enemy.SimpleMagic(enemy.unit));
                        //    yield return new WaitForSeconds(1f);
                        //    continue;
                    }
                }
            }
            //�Ʊ��϶�
            else
            {
                Mathf.Clamp(unit.GetComponent<Hero>().ultCoolDown--,0,3) ;
            }
        }

        //�нú곡�������� ���
        while(true)
        {
            yield return null;
            if(passiveCount == 0)
            {
                break;
            }
            print("�����");
        }

        //�ٳ�����
        //������ ����
        Turn();
    }
    /// <summary>
    /// ����
    /// </summary>
    public void EnemyMoveToDestinationPos()
    {
        int i = 0;
        foreach(var enemy in enemyCollection.collection)
        {
            if (enemy == null)
                continue;

            enemy.MoveToPos();
            enemy.beginPosV3 = enemy.destinationPosV3;
            i++;
        }
    }
    /// <summary>
    /// ���� �� �����Ű�� �Լ�
    /// </summary>
    private IEnumerator EnemyTurn()
    {
        print("�� ����");
        yield return new WaitForSeconds(1f);
        float ran = Random.Range(0f, 100f);
        if(ran >= turnUnit.fSkillChance)
        {
            turnUnit.UnitTurnAction("Attack");
        }
        else
        {
            turnUnit.UnitTurnAction("Skill");
        }

    }
    /// <summary>
    /// ���� �������� üũ���ִ� �Լ�
    /// </summary>
    /// <returns></returns>
    bool TurnOverCheck()
    {
        int count = 0;
        int maxCount = unitList.Count;
        foreach (Unit unit in unitList)
        {
            if (unit.bTurnover || unit.bDie)
            {
                count++;
            }
        }
        if (count == maxCount)
        {
            //���� ��������� �ൿ
            foreach (Unit unit1 in unitList)
            {
                print("�Ͽ��� ����");
                unit1.bTurnover = false;                
            }
            return true;
        }
        return false;
    }    
    /// <summary>
    /// �� �ൿ �����ϴ� �Լ� (��ư����)
    /// </summary>
    /// <param name="type"></param>
    public void TurnAction(string type)
    {
        turnUnit.UnitTurnAction(type);
    }
    public void ItemInit()
    {

    }        
    public int ItemDrop()
    {
        print("�����۵��");
        CommonDeck.Clear();
        RareDeck.Clear();
        EpicDeck.Clear();
        LegendDeck.Clear();

        PlayerData m_PlayerData = DataManager.Instance.m_PlayerData;
        List<ItemData> m_ItemData = DataManager.Instance.MyItemList;

        //������ �ʱ�ȭ
        for (int i = 0; i < m_ItemData.Count; i++)
        {
            switch (m_ItemData[i].sRank)
            {
                case "Common":
                    CommonDeck.Add(m_ItemData[i]);
                    break;
                case "Rare":
                    RareDeck.Add(m_ItemData[i]);
                    break;
                case "Epic":
                    EpicDeck.Add(m_ItemData[i]);
                    break;
                case "Legend":
                    LegendDeck.Add(m_ItemData[i]);
                    break;
                default:
                    break;
            }
        }
    
        //�ߺ��Ǵ� ������ �ɷ�����
        for (int i = 0; i < m_PlayerData.nItemIndex.Length; i++)
        {
            if (m_PlayerData.nItemIndex[i] == "" || m_PlayerData.nItemIndex[i] == null)
            {
                break;
            }

            for (int j = 0; j < CommonDeck.Count; j++)
            {
                if (m_PlayerData.nItemIndex[i] == CommonDeck[j].nIndex)
                {
                    CommonDeck.RemoveAt(j);
                }
            }
        }
        for (int i = 0; i < m_PlayerData.nItemIndex.Length; i++)
        {
            if (m_PlayerData.nItemIndex[i] == "" || m_PlayerData.nItemIndex[i] == null)
            {
                break;
            }

            for (int j = 0; j < RareDeck.Count; j++)
            {
                if (m_PlayerData.nItemIndex[i] == RareDeck[j].nIndex)
                {
                    RareDeck.RemoveAt(j);
                }
            }
        }
        for (int i = 0; i < m_PlayerData.nItemIndex.Length; i++)
        {
            if (m_PlayerData.nItemIndex[i] == "" || m_PlayerData.nItemIndex[i] == null)
            {
                break;
            }

            for (int j = 0; j < EpicDeck.Count; j++)
            {
                if (m_PlayerData.nItemIndex[i] == EpicDeck[j].nIndex)
                {
                    EpicDeck.RemoveAt(j);
                }
            }
        }
        for (int i = 0; i < m_PlayerData.nItemIndex.Length; i++)
        {
            if (m_PlayerData.nItemIndex[i] == "" || m_PlayerData.nItemIndex[i] == null)
            {
                break;
            }

            for (int j = 0; j < LegendDeck.Count; j++)
            {
                if (m_PlayerData.nItemIndex[i] == LegendDeck[j].nIndex)
                {
                    LegendDeck.RemoveAt(j);
                }
            }
        }
        
        //�������� �����۵�� ����
        int ranRank = Random.Range(0,101);

        
        
        int? ranItem = RanDropItem(ranRank).Value;

        if (ranItem.HasValue)
        {
            for (int i = 0; i < m_PlayerData.nItemIndex.Length; i++)
            {
                if (m_PlayerData.nItemIndex[i] == "" || m_PlayerData.nItemIndex[i] == null)
                {
                    m_PlayerData.nItemIndex[i] = ranItem.Value.ToString();
                    m_PlayerData.sItemName[i] = m_ItemData[ranItem.Value].sName;
                    m_ItemData[ranItem.Value].bUnlock = true;
                    break;
                }
            }
        }

        return ranItem.Value;
    }
    private int? RanDropItem(int ranRank)
    {
        int ranCommon;
        switch (ranRank)
        {
            //Ŀ��
            case int n when (0 <= n && n <= 50):

                ranCommon = Random.Range(0, CommonDeck.Count);

                if (int.TryParse(CommonDeck[ranCommon].nIndex, out int nResult1))
                {
                    return nResult1;
                }
                return null;

            //����
            case int n when (51 <= n && n <= 80):

                ranCommon = Random.Range(0, CommonDeck.Count);

                if (int.TryParse(CommonDeck[ranCommon].nIndex, out int nResult2))
                {
                    return nResult2;
                }
                return null;

            //����
            case int n when (81 <= n && n <= 95):

                ranCommon = Random.Range(0, CommonDeck.Count);

                if (int.TryParse(CommonDeck[ranCommon].nIndex, out int nResult3))
                {
                    return nResult3;
                }
                return null;

            //����
            case int n when (96 <= n && n <= 100):

                ranCommon = Random.Range(0, CommonDeck.Count);

                if (int.TryParse(CommonDeck[ranCommon].nIndex, out int nResult4))
                {
                    return nResult4;
                }
                return null;
        }
        return null;
    }
    /// <summary>
    /// �� ����óġ, �Ʊ� ��� üũ
    /// </summary>
    /// <returns></returns>
    /// 
    bool EnemyAllDie()
    {
        int bEnemyDieCount = 0;
        //�� ����óġ üũ
        foreach (Enemy enemy in enemyCollection.collection)
        {
            if (enemy.bDie)
            {
                bEnemyDieCount++;
                if (bEnemyDieCount.Equals(enemyCollection.collection.Count))
                {
                    if (battleNum == 3)
                    {
                        BackToStageScene();
                        return true;
                    }
                    NextWave();
                    //BackToStageScene();
                    return true;
                }
            }
        }
        return false;
    }
    /// <summary>
    /// ���� ���̺�� �Ѿ�� �Լ�
    /// </summary>
    void NextWave()
    {
        BattleScene.I.bBattleStart = false;
        BattleScene.I.turnFlipFlop = false;
        BattleScene.I.stat1Panel.SetActive(false);
        BattleScene.I.stat2Panel.SetActive(false);
        battleNum++;
        DestroyEnemys = null;
        DestroyEnemys = new Enemy[6];

        StartCoroutine(NextWaveCoroutine());
    }

    IEnumerator NextWaveCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        int inum = 0;
        foreach (Enemy enemy in enemyCollection.collection)
        {
            unitList.Remove(enemy.unit);
            DestroyEnemys[inum] = enemy;
            inum++;
        }

        EnemyDummyRemove();
        enemyCollection.collection.Clear();
        InitDestinationVec3();
        if(battleNum == 3)
        {
            SpawnEliteMonster();
        }
        else
        {
            SpawnMonster();
        }
        EnemyInit();

        ScrollingAndUnitAnimationChange();
        BattleScene.I.BattleStatePopUp.SetActive(true);
        BattleScene.I.BattleStateTextUpdate(battleNum);
        yield return new WaitForSeconds(1.5f);
        BattleScene.I.BattleStatePopUp.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        BattleScene.I.WarningBar_Panel.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        EnemyMoveToDestinationPos();

        yield return new WaitForSeconds(3f);
        BattleScene.I.WarningBar_Panel.SetActive(false);
        //Turn();
    }


    void EnemyDummyRemove()
    {
        for (int i = 0; i < DestroyEnemys.Length; i++)
        {
            Destroy(DestroyEnemys[i].gameObject);
        }
    }
    void ScrollingAndUnitAnimationChange()
    {
        bScrolling = true;
        foreach (Unit unit in unitList)
        {
            if (unit.GetComponent<Hero>())
            {
                Hero hero = unit.GetComponent<Hero>();
                hero.animator.SetInteger("State", 2);
            }
        }
    }
    void EnemyInit()
    {
        print("�� �ʱ�ȭ");
        //���ʹ� �ݷ��� �ʱ�ȭ
        enemyCollection.Init2();

        //���ָ���Ʈ�� �־��ֱ�
        foreach (Enemy enemy in enemyCollection.collection)
        {
            unitList.Add(enemy.transform.GetComponent<Unit>());
        }
        foreach(Unit unit in unitList)
        {
            unit.bTurnover = false;
        }
    }
    public void ChangeHeroAnimationState(int state)
    {
        foreach(Hero hero in heroCollection.collection)
        {
            hero.animator.SetInteger("State", state);
        }
    }
    /// <summary>
    /// �Ʊ� ����óġ üũ
    /// </summary>
    /// <returns></returns>
    bool HeroAllDie()
    {        //�Ʊ� ������� üũ
        int bHeroDieCount = 0;
        foreach (Hero hero in heroCollection.collection)
        {
            if (hero.bDie)
            {
                bHeroDieCount++;
                if (bHeroDieCount.Equals(3))
                {
                    GameOver();
                    return true;
                }
            }
        }
        return false;
    }
    void GameOver()
    {
        print("�Ʊ� �������! ���ӿ���");
    }
    /// <summary>
    /// ��� ���� óġ�ؼ� ���������� �Ѿ�� �Լ�
    /// </summary>
    void BackToStageScene()
    {
        foreach (var unit in unitList)
        {
            //���ֵ� ü�¹� �����ֱ�
            unit.HpBarSlider.gameObject.SetActive(false);
            //���ֵ� ���ÿ��� �ʱ�ȭ
            unit.GetComponent<SortingGroup>().sortingOrder = 200;
        }

        print("�� ���óġ!, ��� �������� Ŭ����! �ʾ����� ���ư��ϴ�.");

        //ĳ���� ���ֱ�
        BattleScene.I.stat1Panel.SetActive(false);
        BattleScene.I.stat2Panel.SetActive(false);

        //���߿� ������ �ٲٱ�
        GameInstance.Instance.gamdData.pathData[GameInstance.Instance.nowStage].pathClear = true;
        GameInstance.Instance.gamdData.pathData[GameInstance.Instance.nowStage + 1].pathOpen = true;
                       

        victoryResultPanel.SetActive(true);
        //Ŭ���� ���� ����
        DataManager.Instance.m_PlayerData.nStack++;
        foreach(var enemy in enemyCollection.collection)
        {
            enemy.OnDie();
        }

        //SceneLoad.LoadScene(nowStage);
        //GameInstance.Instance.gamdData.InPoint.ClearAndOpenNextPoint();
    }
    /// <summary>
    /// ������ġ �迭 �ʱ�ȭ �Լ�
    /// </summary>
    void InitDestinationVec3()
    {
        for(int i = 0; i < enemySpawnPosV3.Length; i++)
        {
            enemyDestinationPosV3[i].x = enemySpawnPosV3[i].x - 5f;
            enemyDestinationPosV3[i].y = enemySpawnPosV3[i].y;
        }
    }
}