using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MHomiLibrary;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;

public class BattleManager : HSingleton<BattleManager>
{
    #region[변수들]
    
    [Header (" 변수들 ")]
    [Space(10f)]
    /// <summary>
    /// 현재 스테이지 저장하는 인수
    /// </summary>
    public string nowStage;

    public bool bScrolling;

    public int unitArriveCheck = 0;

    public Transform[] bulletOffsets;

    /// <summary>
    /// 히어로들을 담는 리스트
    /// </summary>
    public HeroCollection heroCollection;

    public int battleNum = 1;

    /// <summary>
    /// 적들을 담는 리스트
    /// </summary>
    public EnemyCollection enemyCollection;

    /// <summary>
    /// 전투화면 내의 유닛들을 담는 리스트
    /// </summary>
    public List<Unit> unitList = new List<Unit>();

    /// <summary>
    /// 현재 턴을 가지고있는 유닛
    /// </summary>
    public Unit turnUnit;

    [SerializeField]
    /// <summary>
    /// 유닛처치 합산 경험치
    /// </summary>
    private int totalEXP;

    public ResultPanel resultPanel;

    
    public ENEMY_TYPE[] EnemyType;
    public ENEMY_TYPE EliteEnemy;

    public Vector3[] enemyDestinationPosV3;
    public Vector3[] enemySpawnPosV3;

    /// <summary>
    /// 스킬 텍스트 출력용 텍스트
    /// </summary>
    public TextMeshProUGUI skillText;

    /// <summary>
    /// 살아있는 유닛 갯수
    /// </summary>
    public int aliveUnitCount;

    public GameObject heroBattlePanel;


    [Header("승리 결과창")]
    [Space(10f)]
    /// <summary>
    /// 승리 시 결과창
    /// </summary>
    public GameObject victoryResultPanel;

    #endregion



    private void Awake()
    {
        GameObject test = GameObject.Find("TestInstance");
        if(test != null)
        {
            EnemyType = new ENEMY_TYPE[3];
            EliteEnemy = new ENEMY_TYPE();
            EnemyType[0] = GameInstance.Instance.enemy1;
            EnemyType[1] = GameInstance.Instance.enemy2;
            EnemyType[2] = GameInstance.Instance.enemy3;
            EliteEnemy = GameInstance.Instance.eliteENum;
        }
        GameObject gameInstance = GameObject.Find("GameInstance");
        if (gameInstance != null)
        {
            EnemyType = new ENEMY_TYPE[GameInstance.Instance.enemies.Length];
            EliteEnemy = new ENEMY_TYPE();
            switch (GameInstance.Instance.enemies.Length)
            {

                case 1:
                    EnemyType[0] = GameInstance.Instance.enemies[0];
                    EliteEnemy = GameInstance.Instance.eliteEnemy;
                    break;

                case 2:
                    EnemyType[0] = GameInstance.Instance.enemies[0];
                    EnemyType[1] = GameInstance.Instance.enemies[1];
                    EliteEnemy = GameInstance.Instance.eliteEnemy;
                    break;

                case 3:
                    EnemyType[0] = GameInstance.Instance.enemies[0];
                    EnemyType[1] = GameInstance.Instance.enemies[1];
                    EnemyType[2] = GameInstance.Instance.enemies[2];
                    EliteEnemy = GameInstance.Instance.eliteEnemy;
                    break;
    
            }
        }     
        InitDestinationVec3();

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

    public int TotalExp
    {
        get { return totalEXP; }
        set { totalEXP = value; }
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
                //패시브스킬이 있으면 발동
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
        switch (EnemyType.Length)
        {
            //몬스터 종류가 한마리일때
            case 1:
                for (int i = 0; i < 6; i++)
                {
                    enemy = GameInstance.Instance.CreateEnemyPrefab(EnemyType[0].ToString(),
                                                                    transform.position,
                                                                    Vector3.one * 0.5f,
                                                                    Quaternion.identity);
                    enemy.transform.parent = enemyCollection.transform;
                }
                break;
            //몬스터 종류가 두마리일때
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
            //몬스터 종류가 세마리일때
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
            //몬스터 종류가 한마리일때
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

            //몬스터 종류가 두마리일때
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

            //몬스터 종류가 두마리일때
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
                    if (i == 1)
                    {
                        enemy = GameInstance.Instance.CreateEnemyPrefab(EliteEnemy.ToString(),
                                                enemySpawnPosV3[i + 2],
                                                Vector3.one * 0.8f,
                                                Quaternion.identity);
                        enemy.transform.parent = enemyCollection.transform;
                    }
                    else
                    {
                        enemy = GameInstance.Instance.CreateEnemyPrefab(EnemyType[1].ToString(),
                                                enemySpawnPosV3[i + 2],
                                                Vector3.one * 0.5f,
                                                Quaternion.identity);
                        enemy.transform.parent = enemyCollection.transform;
                    }
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


    List<Unit> turnNoCheckedUnit = new List<Unit>();

    /// <summary>
    /// 턴 체크해서 유닛에게 턴 부여해주는 함수
    /// </summary>
    void Turn()
    {
        ///턴 체크용 임시 로컬변수
        Unit TurnOrderUnit;
        //스피드 비교해서 가장빠른 유닛을 턴유닛으로 설정하기
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

            //유닛들 소팅오더 초기화
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
        //턴 안지난 유닛 검사
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

        //턴 지난 유닛 검사
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

        ////턴 유닛이 히어로라면
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


    public int passiveCount;

    /// <summary>
    /// 다음 순서으로 넘어가는 함수
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

        //적을 모두 처치했는지 확인
        if (EnemyAllDie())
            return;

        //아군 사망 체크
        if (HeroAllDie())
            return;

        //모두의 턴이 끝났는지 체크
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



        //다음턴 진행
        Turn();
    }

    public IEnumerator unitPassiveCheck()
    {
        foreach(Unit unit in unitList)
        {
            //적일때
            if (unit.GetComponent<Enemy>())
            {

                var enemy = unit.GetComponent<Enemy>();
                if (!enemy)
                    continue;
                //턴마다 실행하는 패시브스킬이 있다면
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
            //아군일때
            else
            {

            }
        }

        //패시브끝날때까지 대기
        while(true)
        {
            yield return null;
            if(passiveCount == 0)
            {
                break;
            }
            print("대기중");
        }

        //다끝나면
        //다음턴 진행
        Turn();
    }

    public void EnemyMoveToDestinationPos()
    {
        int i = 0;
        foreach(var enemy in enemyCollection.collection)
        {
            if (enemy == null)
                continue;
            enemy.MoveToPos(enemyDestinationPosV3[i]);
            i++;
        }
    }

    /// <summary>
    /// 적의 턴 실행시키는 함수
    /// </summary>
    private IEnumerator EnemyTurn()
    {
        print("턴 실행");
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
    /// 턴이 끝났는지 체크해주는 함수
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
            //턴이 종료됐을때 행동
            foreach (Unit unit1 in unitList)
            {
                print("턴오버 해제");
                unit1.bTurnover = false;                
            }
            return true;
        }
        return false;
    }
    
    /// <summary>
    /// 턴 행동 실행하는 함수 (버튼전용)
    /// </summary>
    /// <param name="type"></param>
    public void TurnAction(string type)
    {
        turnUnit.UnitTurnAction(type);
    }


    /// <summary>
    /// 적 전원처치, 아군 사망 체크
    /// </summary>
    /// <returns></returns>
    bool EnemyAllDie()
    {
        int bEnemyDieCount = 0;
        //적 전원처치 체크
        foreach (Enemy enemy in enemyCollection.collection)
        {
            if (enemy.bDie)
            {
                bEnemyDieCount++;
                if (bEnemyDieCount.Equals(6))
                {
                    //if(battleNum == 3)
                    //{
                    //    BackToStageScene();
                    //    return true;
                    //}
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


    Enemy[] DestroyEnemys = new Enemy[6];
    /// <summary>
    /// 다음 웨이브로 넘어가는 함수
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
        //세번째 전투일때
        //정예몹 만들어주기     
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
        print("적 초기화");
        //에너미 콜렉션 초기화
        enemyCollection.Init2();

        //유닛리스트에 넣어주기
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
    /// 아군 전원처치 체크
    /// </summary>
    /// <returns></returns>
    bool HeroAllDie()
    {        //아군 전원사망 체크
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
        print("아군 전원사망! 게임오버");
    }


    /// <summary>
    /// 모든 적을 처치해서 스테이지로 넘어가는 함수
    /// </summary>
    void BackToStageScene()
    {
        foreach (var unit in unitList)
        {
            //유닛들 체력바 없애주기
            unit.HpBarSlider.gameObject.SetActive(false);
            //유닛들 소팅오더 초기화
            unit.GetComponent<SortingGroup>().sortingOrder = 200;
        }

        print("적 모두처치!, 모든 스테이지 클리어! 맵씬으로 돌아갑니다.");

        //캐릭터 꺼주기
        BattleScene.I.stat1Panel.SetActive(false);
        BattleScene.I.stat2Panel.SetActive(false);

        //나중에 변수로 바꾸기
        GameInstance.Instance.gamdData.pathData[GameInstance.Instance.nowStage].pathClear = true;
        GameInstance.Instance.gamdData.pathData[GameInstance.Instance.nowStage + 1].pathOpen = true;
                       

        victoryResultPanel.SetActive(true);       

        foreach(var enemy in enemyCollection.collection)
        {
            enemy.OnDie();
        }

        //SceneLoad.LoadScene(nowStage);
        //GameInstance.Instance.gamdData.InPoint.ClearAndOpenNextPoint();
    }

    /// <summary>
    /// 스폰위치 배열 초기화 함수
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