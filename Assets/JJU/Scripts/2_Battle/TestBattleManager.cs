using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MHomiLibrary;
using UnityEngine.UI;
using TMPro;

public class TestBattleManager : HSingleton<BattleManager>
{
    #region[������]
    
    [Header (" ������ ")]
    [Space(10f)]
    /// <summary>
    /// ���� �������� �����ϴ� �μ�
    /// </summary>
    public string nowStage;


    public bool bScrolling;

    public int unitArriveCheck = 0;

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

    
    public ENEMY_TYPE[] EnemyType;
    public ENEMY_TYPE EliteEnemy;

    public Vector3[] enemyDestinationPosV3;
    public Vector3[] enemySpawnPosV3;
    /// <summary>
    /// ��ų �ؽ�Ʈ ��¿� �ؽ�Ʈ
    /// </summary>
    public TextMeshProUGUI skillText;

    /// <summary>
    /// ����ִ� ���� ����
    /// </summary>
    public int aliveUnitCount;

    public GameObject heroBattlePanel;
    
    #endregion


    private void Awake()
    {
        InitDestinationVec3();
        //�����ν��Ͻ� ������ ��������1�� ���ư���
        if (GameObject.Find("GameInstance") == null)
        {
            SceneLoad.LoadScene("Stage1");
        }
        SpawnMonster();
        enemyCollection.Init();
        turnUnit = new Unit();
        EnemyMoveToDestinationPos();
    }

    void Start()
    {
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
        //StartCoroutine(StartBattle());
        BattleScene.I.BattleStatePopUp.SetActive(true);
        BattleScene.I.BattleStateTextUpdate(battleNum);
    }

    private IEnumerator StartBattle()
    {

        yield return new WaitForSeconds(2.5f);
        Turn();

    }

    public IEnumerator UnitArriveCheck()
    {
        if (unitArriveCheck == 6)
        {
            BattleScene.I.BattleStatePopUp.SetActive(false);
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
        switch (EnemyType.Length)
        {
            //���� ������ �Ѹ����϶�
            case 1:
                for (int i = 0; i < 6; i++)
                {
                    enemy = GameInstance.Instance.CreateEnemyPrefab(EnemyType[0].ToString(),
                                                                    enemySpawnPosV3[0],
                                                                    Vector3.one * 0.5f,
                                                                    Quaternion.identity);
                    enemy.transform.parent = enemyCollection.transform;
                }
                break;
            //���� ������ �θ����϶�
            case 2:
                for (int i = 0; i < 3; i++)
                {
                    enemy = GameInstance.Instance.CreateEnemyPrefab(EnemyType[0].ToString(),
                                                                    enemySpawnPosV3[i],
                                                                    Vector3.one * 0.5f,
                                                                    Quaternion.identity);
                    enemy.transform.parent = enemyCollection.transform;
                }
                for (int i = 0; i < 3; i++)
                {
                    enemy = GameInstance.Instance.CreateEnemyPrefab(EnemyType[1].ToString(),
                                                                    enemySpawnPosV3[i + 3],
                                                                    Vector3.one * 0.5f,
                                                                    Quaternion.identity);
                    enemy.transform.parent = enemyCollection.transform;
                }
                break;
            //���� ������ �������϶�
            case 3:
                for (int i = 0; i < 2; i++)
                {
                    enemy = GameInstance.Instance.CreateEnemyPrefab(EnemyType[0].ToString(),
                                                                    enemySpawnPosV3[i],
                                                                    Vector3.one * 0.5f,
                                                                    Quaternion.identity);
                    enemy.transform.parent = enemyCollection.transform;
                }
                for (int i = 0; i < 2; i++)
                {
                    enemy = GameInstance.Instance.CreateEnemyPrefab(EnemyType[1].ToString(),
                                                                    enemySpawnPosV3[i + 2],
                                                                    Vector3.one * 0.5f,
                                                                    Quaternion.identity);
                    enemy.transform.parent = enemyCollection.transform;
                }
                for (int i = 0; i < 2; i++)
                {
                    enemy = GameInstance.Instance.CreateEnemyPrefab(EnemyType[2].ToString(),
                                                                    enemySpawnPosV3[i + 4],
                                                                    Vector3.one * 0.5f,
                                                                    Quaternion.identity);
                    enemy.transform.parent = enemyCollection.transform;
                }
                break;
        }
        
    }

    void SpawnEliteMonster()
    {
        GameObject enemy;
        switch (EnemyType.Length)
        {
            //���� ������ �Ѹ����϶�
            case 1:
                for (int i = 0; i < 6; i++)
                {
                    if(i == 4)
                    {
                        enemy = GameInstance.Instance.CreateEnemyPrefab(EliteEnemy.ToString(),
                                                                   enemySpawnPosV3[0],
                                                                   Vector3.one * 0.8f,
                                                                   Quaternion.identity);
                        enemy.transform.parent = enemyCollection.transform;
                    }
                    else
                    {
                        enemy = GameInstance.Instance.CreateEnemyPrefab(EnemyType[0].ToString(),
                                                enemySpawnPosV3[0],
                                                Vector3.one * 0.5f,
                                                Quaternion.identity);
                        enemy.transform.parent = enemyCollection.transform;
                    }
                }
                break;

            //���� ������ �θ����϶�
            case 2:
                for (int i = 0; i < 3; i++)
                {
                    enemy = GameInstance.Instance.CreateEnemyPrefab(EnemyType[0].ToString(),
                                                                    enemySpawnPosV3[i],
                                                                    Vector3.one * 0.5f,
                                                                    Quaternion.identity);
                    enemy.transform.parent = enemyCollection.transform;
                }
                for (int i = 0; i < 3; i++)
                {
                    if(i == 1)
                    {
                        enemy = GameInstance.Instance.CreateEnemyPrefab(EliteEnemy.ToString(),
                                                enemySpawnPosV3[i + 3],
                                                Vector3.one * 0.8f,
                                                Quaternion.identity);
                        enemy.transform.parent = enemyCollection.transform;
                    }
                    else
                    {
                        enemy = GameInstance.Instance.CreateEnemyPrefab(EnemyType[1].ToString(),
                                                enemySpawnPosV3[i + 3],
                                                Vector3.one * 0.5f,
                                                Quaternion.identity);
                        enemy.transform.parent = enemyCollection.transform;
                    }
                }
                break;         
        }

    }


    List<Unit> turnNoCheckedUnit = new List<Unit>();

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

        if (turnUnit.GetComponent<Enemy>())
        {
            StartCoroutine(EnemyTurn());
        }
        else
        {
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

        //���� ��� óġ�ߴ��� Ȯ��
        if (EnemyAllDie())
            return;

        if (HeroAllDie())
            return;

        if (TurnOverCheck())
        {
            Turn();
            foreach(Unit unit in unitList)
            {
                unit.BuffAndDeBuffDurationDecrease();
                unit.TickDamageDecrese();
            }
            return;
        }
        Turn();
    }

    public void EnemyMoveToDestinationPos()
    {
        int i = 0;
        foreach(var enemy in enemyCollection.collection)
        {
            enemy.MoveToPos(enemyDestinationPosV3[i]);
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


    /// <summary>
    /// �� ����óġ, �Ʊ� ��� üũ
    /// </summary>
    /// <returns></returns>
    bool EnemyAllDie()
    {
        int bEnemyDieCount = 0;
        //�� ����óġ üũ
        foreach (Enemy enemy in enemyCollection.collection)
        {
            if (enemy.bDie)
            {
                bEnemyDieCount++;
                if (bEnemyDieCount.Equals(6))
                {
                    if(battleNum == 3)
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


    Enemy[] DestroyEnemys = new Enemy[6];
    /// <summary>
    /// ���� ���̺�� �Ѿ�� �Լ�
    /// </summary>
    void NextWave()
    {
        battleNum++;
        DestroyEnemys = null;
        DestroyEnemys = new Enemy[6];
        //����° �����϶�
        //������ ������ֱ�     
        //Turn();
        if (battleNum == 3)
        {
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
            SpawnEliteMonster();
            EnemyInit();
            EnemyMoveToDestinationPos();
            ScrollingAndUnitAnimationChange();
            BattleScene.I.BattleStatePopUp.SetActive(true);
            BattleScene.I.BattleStateTextUpdate(battleNum);
        }
        else
        {
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
            SpawnMonster();
            EnemyInit(); 
            EnemyMoveToDestinationPos();
            ScrollingAndUnitAnimationChange();
            BattleScene.I.BattleStatePopUp.SetActive(true);
            BattleScene.I.BattleStateTextUpdate(battleNum);
            //Turn();
        }
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
        print("�� ���óġ!, ��� �������� Ŭ����! �ʾ����� ���ư��ϴ�.");     
        //���߿� ������ �ٲٱ�
        SceneLoad.LoadScene(nowStage);
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
