using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Hero : HeroSkill
{


    #region [변수들]
    [Header("변수들")]
    [Space(10f)]

    public int ultCoolDown;



    /// <summary>
    /// 현재레벨
    /// </summary>
    public int Level;

    public bool jumpUnit;

    /// <summary>
    /// 현재 보유
    /// </summary>
    public float Experience;

    public int heroLevel;

    /// <summary>
    /// 현재 다음 레벨업까지 필요함 경험치 (Lerp전용)
    /// </summary>
    public float DestExperience;

    public List<SpriteRenderer> renderers;

    public HeroItem[] items;


    /// <summary>
    /// 현재 부모인 유닛
    /// </summary>
    public Unit unit;

    /// <summary>
    /// 공격시작 위치
    /// </summary>
    public Vector3 beginPosV3;

    /// <summary>
    /// 타겟위치
    /// </summary>
    public Vector3 targetPosV3;

    /// <summary>
    /// 이동속도
    /// </summary>
    public float moveSpeed;

    /// <summary>
    /// 현재 공격하고있는지
    /// </summary>
    public bool bAttack;

    /// <summary>
    /// 현재 스킬사용중인지
    /// </summary>
    public bool bSkill;

    /// <summary>
    /// 턴이 끝났는지
    /// </summary>
    public bool bTurnEnd;

    /// <summary>
    /// 현재 사망상태인지
    /// </summary>
    public bool bDie;

    /// <summary>
    /// 현재 타겟인 적
    /// </summary>
    public Enemy targetEnemy;
    

    /// <summary>
    /// 현재 타겟인 적 (힐같은거 사용할때)
    /// </summary>
    public Hero targetHero;

    /// <summary>
    /// 히어로 애니메이션 컨트롤러
    /// </summary>
    public Animator animator;


    /// <summary>
    /// 히어로 선택여부
    /// </summary>
    public bool bSelected;

    [Header("스킬 아이콘 스프라이트")]
    [Space(10f)]
    public Sprite skill1_Sprite;
    public Sprite skill2_Sprite;
    public Sprite skill3_Sprite;


    #endregion



    private void Awake()
    {
        //플레이어 경험치 요구량 초기화
        //switch (job)
        //{
        //    case PlayerJob.Dealer :

        //        //Experience = GameInstance.Instance.gamdData.dealerData.Exp;
        //        //heroLevel = GameInstance.Instance.gamdData.dealerData.Level;


        //        Experience = DataManager.Instance.m_PlayerData.fExp[0];
        //        heroLevel = DataManager.Instance.m_PlayerData.nLevel[0];

        //        break;

        //    case PlayerJob.Healer:
        //        //Experience = GameInstance.Instance.gamdData.healerData.Exp;
        //        //heroLevel = GameInstance.Instance.gamdData.healerData.Level;

        //        Experience = DataManager.Instance.m_PlayerData.fExp[2];
        //        heroLevel = DataManager.Instance.m_PlayerData.nLevel[2];
        //        break;

        //    case PlayerJob.Tanker:
        //        //Experience = GameInstance.Instance.gamdData.tankerData.Exp;
        //        //heroLevel = GameInstance.Instance.gamdData.tankerData.Level;

        //        Experience = DataManager.Instance.m_PlayerData.fExp[1];
        //        heroLevel = DataManager.Instance.m_PlayerData.nLevel[1];
        //        break;

        //}
            
        
        sfx_Audio = GameObject.Find("Sound").GetComponent<LogoSound>().SFX_as;
        InitHealPos();
        unit = transform.GetComponent<Unit>();
        beginPosV3 = transform.position;
        //targetPosV3 = transform.position + Vector3.right;
        animator = transform.GetChild(0).GetComponent<Animator>();
    }


    private void Start()
    {
        InitStat();

        skill1_Sprite = GameInstance.Instance.SkillInfoData[skill_set1.ToString()].icon;
        skill2_Sprite = GameInstance.Instance.SkillInfoData[skill_set2.ToString()].icon;
        skill3_Sprite = GameInstance.Instance.SkillInfoData[skill_set3.ToString()].icon;

    }
    void Update()
    {
        if (bAttack || bSkill)
        {
            OnTurnMove();
        }
        if (bTurnEnd)
        {
            OnTurnEndMove();
        }
    }

    #region [공격]

    //공격명령 받기
    public void Attack(Enemy enemy)
    {
        BattleManager.I.heroBattlePanel.SetActive(false);
        //공격변수 활성화
        bAttack = true;

        //애니메이션 상태 걷기모드로 해주기
        animator.SetInteger("State", 1);

        targetEnemy = enemy;
        targetPosV3 = enemy.destinationPosV3 + Vector3.left  * 2f;
    }

    public void OnHeal(float value)
    {
        unit.Hp = Mathf.Clamp(unit.Hp += value, 0, unit.maxHP);
        sfx_Audio.PlayOneShot(GameInstance.Instance.soundPrefab["Heal"]);
        StartCoroutine(PlayParticle(HealingOnce, 0.5f, 0f));
    }

    //공격 실행
    IEnumerator OnAttack()
    {
        switch (job)
        {
            case PlayerJob.Healer:
                animator.SetTrigger("Cast");
                break;
            case PlayerJob.Tanker:
                animator.SetTrigger("Slash");
                break;
            case PlayerJob.Ranger:
                animator.SetInteger("HoldType", 1);
                animator.SetInteger("WeaponType", 5);
                animator.SetTrigger("Ready");
                yield return new WaitForSeconds(0.5f);
                animator.SetTrigger("Reloading");
                break;
            case PlayerJob.Dealer :
                animator.SetTrigger("Slash");
                break;
        }

        print("공격!");
        Unit targetUnit = targetEnemy.transform.GetComponent<Unit>();
        yield return new WaitForSeconds(0.3f);
        targetUnit.DecreaseHP(unit.currentPhysicalAttack, unit);
        Camera.main.GetComponent<CamShake>().ShakeTime = 0.15f;
        StartCoroutine(PlayParticle(swordSlashMini, 0.6f, 0.0f));

        //적이 공격에 맞아 사망하면
        if (targetUnit.Hp <= 0)
        {
            yield return new WaitForSeconds(0.1f);
            //targetEnemy.OnDie();

            targetEnemy.OnDie();
            yield return new WaitForSeconds(0.4f);
            //targetEnemy.gameObject.SetActive(false);
            print("적 죽음");
            bTurnEnd = true;
            animator.SetInteger("State", 1);
        }
        else
        {
            yield return new WaitForSeconds(0.2f);
            //targetEnemy.animator.SetTrigger("Hit");

            yield return new WaitForSeconds(0.4f);
            bTurnEnd = true;
            animator.SetInteger("State", 1);
        }

    }
    #endregion

    #region [이동]
    bool check;

    public void ActionEnd()
    {
        if (jumpUnit)
        {
            StartCoroutine(OnTurnEndJumpToOriginPos());
        }
        else
        {
            bTurnEnd = true;
        }
    }

    ///행동을 위해 타겟지점으로 이동
    void OnTurnMove()
    {
        
        if (transform.position == targetPosV3)
        {

            animator.SetInteger("State", 0);
            if (bAttack)
            {
                StartCoroutine(OnAttack());
            }
            else
            {
                StartCoroutine(OnSkill());
            }
            bAttack = false;
            bSkill = false;
            //if (WeaponTrail1)
            //    WeaponTrail1.SetActive(true);
            //if (WeaponTrail2)
            //    WeaponTrail2.SetActive(true);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosV3, moveSpeed * Time.deltaTime);
        //transform.position = Vector3.Lerp(transform.position, targetPosV3, 1f * Time.deltaTime);
    }




    public IEnumerator OnTurnJumpToTargetPos()
    {
        animator.SetInteger("State", 4);
        sfx_Audio.PlayOneShot(GameInstance.Instance.soundPrefab["Dash"]);
        yield return new WaitForSeconds(0.15f);   

        var obj = Instantiate(GameInstance.Instance.particlePrefab["Dash"],transform.position,Quaternion.identity);

        Destroy(obj, 0.5f);
        yield return new WaitForSeconds(0.45f);



        animator.SetInteger("State", 3);


        Color color = new Color();
        color.a = 0f;

        List<Color> colors = new List<Color>();
        //ArrayList colors = new ArrayList();

        foreach (SpriteRenderer sprite in renderers)
        {
            colors.Add(sprite.color);
            sprite.color = color;
        }



        transform.DOJump(targetPosV3, 0.6f, 1, 0.3f);
        sfx_Audio.PlayOneShot(GameInstance.Instance.soundPrefab["JumpDash"],5f);
        int i = 0;
        yield return new WaitForSeconds(0.3f);
        foreach (SpriteRenderer sprite in renderers)
        {
            sprite.DOColor(colors[i], 0.3f);
            i++;
        }
        yield return new WaitForSeconds(0.3f);


        colors.Clear();
        while (true)
        {
            yield return null;
            if (transform.position == targetPosV3)
            { break; }
        }



        animator.SetInteger("State", 0);
        if (bAttack)
        {
            StartCoroutine(OnAttack());
        }
        else
        {
            StartCoroutine(OnSkill());
        }
        bAttack = false;
        bSkill = false;
    }

    public IEnumerator OnTurnEndJumpToOriginPos()
    {
        animator.SetInteger("State", 4);
        sfx_Audio.PlayOneShot(GameInstance.Instance.soundPrefab["Dash"]);
        yield return new WaitForSeconds(0.15f);

        var obj = Instantiate(GameInstance.Instance.particlePrefab["Dash"], transform.position, Quaternion.identity);

        Destroy(obj, 0.5f);
        yield return new WaitForSeconds(0.2f);



        animator.SetInteger("State", 3);




        transform.DOJump(beginPosV3, .8f, 1, 0.3f);
        sfx_Audio.PlayOneShot(GameInstance.Instance.soundPrefab["JumpDash"], 5f);

        yield return new WaitForSeconds(0.3f);
        yield return new WaitForSeconds(0.3f);


        while (true)
        {
            yield return null;
            if (transform.position == beginPosV3)
            { break; }
        }

        bTurnEnd = false;
        unit.attackAble = false;
        unit.skillAble = false;
        animator.SetInteger("State", 0);
        BattleManager.I.NextTurn();
    }






    //행동이 끝난 후 제자리로 돌아오고 턴 넘기기
    public void OnTurnEndMove()
    { 
        if (transform.position == beginPosV3)
        {
            bTurnEnd = false;
            unit.attackAble = false;
            unit.skillAble = false;
            animator.SetInteger("State", 0);
            BattleManager.I.NextTurn();
            unit.OnAction = false;
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, beginPosV3, moveSpeed * Time.deltaTime);
    }

    #endregion


    //가드명령 받기
    public void Gaurd(float Damage)
    {

    }


    #region [스킬]
    //스킬명령 받기
    /// <summary>
    /// 스킬
    /// </summary>
    /// <param name="Damage"></param>
    public void Skill(Enemy enemy)
    {
        BattleManager.I.heroBattlePanel.SetActive(false);
        


        targetEnemy = enemy;
        if ((int)currentSKill > 20)
        {
            targetPosV3 = transform.position + Vector3.right * 2f;
        }
        else
        {
            targetPosV3 = enemy.destinationPosV3 + Vector3.left * 3f;
        }

        if (jumpUnit)
        {
            StartCoroutine(GetComponent<Hero>().OnTurnJumpToTargetPos());
        }
        else
        {      
            //애니메이션 상태 걷기모드로 해주기
            animator.SetInteger("State", 1);
            bSkill = true;
        }
    }

    public void Skill()
    {
        BattleManager.I.heroBattlePanel.SetActive(false);
        //공격변수 활성화


        //StartCoroutine(GetComponent<Hero>().OnTurnJumpToTargetPos());
        bSkill = true;

        //애니메이션 상태 걷기모드로 해주기
        animator.SetInteger("State", 1);
        
        if ((int)currentSKill > 20)
        {
            targetPosV3 = transform.position + Vector3.right * 2f;
        }
    }



    //공격 실행
    IEnumerator OnSkill()
    {
        //원거리 공격일떄
        if((int)currentSKill > 20 && (int)currentSKill < 40)
        {
            targetPosV3 = transform.position + Vector3.right * 2f;
            switch (currentSKill)
            {
                case SKILLSET.MULTIPLE_HEAL:
                    StartCoroutine(Multiple_Heal(animator,unit));
                    break;
                case SKILLSET.RAISE:
                    StartCoroutine(Raise(targetHero, animator, unit));
                    break;
            }
            yield return null;
        }
        else if((int)currentSKill > 40)
        {
            targetPosV3 = transform.position + Vector3.right * 2f;
            switch (currentSKill)
            {
                case SKILLSET.TAUNT:
                    StartCoroutine(Taunt(animator, unit));
                    break;

                case SKILLSET.ROAR:
                    StartCoroutine(Roar(animator, unit));
                    break;

                case SKILLSET.SCOURGE:
                    StartCoroutine(Scourge(targetEnemy.unit, animator, unit));
                    break;

                case SKILLSET.BUSTERCALL:
                    StartCoroutine(BusterCall(animator, unit));
                    break;

            }
            yield return null;
        }
        //근거리 공격일때
        else
        {
            switch (currentSKill)
            {
                case SKILLSET.TRANCE_SWORD:
                    StartCoroutine(Trance_Sword(targetEnemy.unit, animator, unit));
                    break;

                case SKILLSET.SLASH:
                    StartCoroutine(Slash(targetEnemy.unit, animator, unit));
                    break;
                case SKILLSET.SLASHBURST:
                    StartCoroutine(SlashBurst(targetEnemy.unit ,animator, unit));
                    break;
            }
            yield return null;
        }
    }

    #endregion




    #region [기타함수]

    /// <summary>
    /// 히어로 스탯 초기화 및 아이템 적용
    /// </summary>
    public void InitStat()
    {
        switch (job)
        {
            case PlayerJob.Dealer:
                //기본스탯 적용
                unit.Hp = DataManager.Instance.m_PlayerData.fHealth[0];
                unit.maxHP = DataManager.Instance.m_PlayerData.fHealth[0];
                unit.PhysicalAttack = DataManager.Instance.m_PlayerData.fAttack[0];
                unit.Defense = DataManager.Instance.m_PlayerData.fDefense[0];
                unit.MagicalAttack = DataManager.Instance.m_PlayerData.fMagic[0];
                unit.Speed = DataManager.Instance.m_PlayerData.fSpeed[0];

                //경험치 적용
                Experience = DataManager.Instance.m_PlayerData.fExp[0];

                //레벨 적용
                heroLevel = DataManager.Instance.m_PlayerData.nLevel[0];

                //스킬 적용
                skill_set1 = GameInstance.Instance.gamdData.dealerData.skillSets[0];
                skill_set2 = GameInstance.Instance.gamdData.dealerData.skillSets[1];
                skill_set3 = GameInstance.Instance.gamdData.dealerData.skillSets[2];

                //아이템 적용
                unit.Hp *= GameInstance.Instance.itemStatData[0].hp / 100f + 1f;
                unit.maxHP *= GameInstance.Instance.itemStatData[0].hp / 100 + 1f;
                unit.PhysicalAttack *= GameInstance.Instance.itemStatData[0].attack / 100 + 1f;
                unit.MagicalAttack *= GameInstance.Instance.itemStatData[0].magic / 100 + 1f;
                unit.Defense *= GameInstance.Instance.itemStatData[0].defense / 100 + 1f;
                unit.Speed += GameInstance.Instance.itemStatData[0].speed;


                unit.currentPhysicalAttack = unit.PhysicalAttack;
                unit.currentMagicalAttack = unit.MagicalAttack;
                unit.currentDefense = unit.Defense;
                unit.currentSpeed = unit.Speed;
                break;

            case PlayerJob.Tanker:
                //기본스탯 적용
                unit.Hp = DataManager.Instance.m_PlayerData.fHealth[1];
                unit.maxHP = DataManager.Instance.m_PlayerData.fHealth[1];
                unit.PhysicalAttack = DataManager.Instance.m_PlayerData.fAttack[1];
                unit.Defense = DataManager.Instance.m_PlayerData.fDefense[1];
                unit.MagicalAttack = DataManager.Instance.m_PlayerData.fMagic[1];
                unit.Speed = DataManager.Instance.m_PlayerData.fSpeed[1];

                //경험치 적용
                Experience = DataManager.Instance.m_PlayerData.fExp[1];

                //레벨 적용
                heroLevel = DataManager.Instance.m_PlayerData.nLevel[1];

                //스킬 적용
                skill_set1 = GameInstance.Instance.gamdData.tankerData.skillSets[0];
                skill_set2 = GameInstance.Instance.gamdData.tankerData.skillSets[1];
                skill_set3 = GameInstance.Instance.gamdData.tankerData.skillSets[2];

                ////아이템 적용
                unit.Hp *= GameInstance.Instance.itemStatData[1].hp / 100f + 1f;
                unit.maxHP *= GameInstance.Instance.itemStatData[1].hp / 100 + 1f;
                unit.PhysicalAttack *= GameInstance.Instance.itemStatData[1].attack / 100 + 1f;
                unit.MagicalAttack *= GameInstance.Instance.itemStatData[1].magic / 100 + 1f;
                unit.Defense *= GameInstance.Instance.itemStatData[1].defense / 100 + 1f;
                unit.Speed += GameInstance.Instance.itemStatData[1].speed;

                unit.currentPhysicalAttack = unit.PhysicalAttack;
                unit.currentMagicalAttack = unit.MagicalAttack;
                unit.currentDefense = unit.Defense;
                unit.currentSpeed = unit.Speed;
                break;

            case PlayerJob.Healer:
                //기본스탯 적용
                unit.Hp = DataManager.Instance.m_PlayerData.fHealth[2];
                unit.maxHP = DataManager.Instance.m_PlayerData.fHealth[2];
                unit.PhysicalAttack = DataManager.Instance.m_PlayerData.fAttack[2];
                unit.Defense = DataManager.Instance.m_PlayerData.fDefense[2];
                unit.MagicalAttack = DataManager.Instance.m_PlayerData.fMagic[2];
                unit.Speed = DataManager.Instance.m_PlayerData.fSpeed[2];

                //경험치 적용
                Experience = DataManager.Instance.m_PlayerData.fExp[2];

                //레벨 적용
                heroLevel = DataManager.Instance.m_PlayerData.nLevel[2];

                //스킬 적용
                skill_set1 = GameInstance.Instance.gamdData.healerData.skillSets[0];
                skill_set2 = GameInstance.Instance.gamdData.healerData.skillSets[1];
                skill_set3 = GameInstance.Instance.gamdData.healerData.skillSets[2];

                ////아이템 적용
                unit.Hp *= GameInstance.Instance.itemStatData[2].hp / 100f + 1f;
                unit.maxHP *= GameInstance.Instance.itemStatData[2].hp / 100 + 1f;
                unit.PhysicalAttack *= GameInstance.Instance.itemStatData[2].attack / 100 + 1f;
                unit.MagicalAttack *= GameInstance.Instance.itemStatData[2].magic / 100 + 1f;
                unit.Defense *= GameInstance.Instance.itemStatData[2].defense / 100 + 1f;
                unit.Speed += GameInstance.Instance.itemStatData[2].speed;

                unit.currentPhysicalAttack = unit.PhysicalAttack;
                unit.currentMagicalAttack = unit.MagicalAttack;
                unit.currentDefense = unit.Defense;
                unit.currentSpeed = unit.Speed;
                break;
        }

    }

    void ApplyItem(HeroItem item)
    {
        unit.Hp += item.HP;
        unit.maxHP += item.HP;
        unit.PhysicalAttack += item.Attack;
        unit.MagicalAttack += item.MagicalAttack;
        unit.Defense += item.Defense;
        unit.Speed += item.Speed;
    }

    public void OnDie()
    {
        bDie = true;
        unit.bDie = true;
        int ran = UnityEngine.Random.Range(6, 8);
        animator.SetInteger("State", ran);
    }

    public IEnumerator OnHit()
    {        
        yield return new WaitForSeconds(0.05f);

        List<Color> colors = new List<Color>();
        //ArrayList colors = new ArrayList();

        foreach (SpriteRenderer sprite in renderers)
        {
            colors.Add(sprite.color); 
            sprite.color = Color.red;
        }
        yield return new WaitForSeconds(0.1f);

        int i = 0;
        foreach (SpriteRenderer sprite in renderers)
        {
            sprite.color = colors[i];
            i++;
        }
    }

    public void Selected()
    {
        bSelected = true;
        unit.arrow.GetComponent<Renderer>().material.color = Color.red;
    }

    public void DeSelected()
    {
        bSelected = false;
        unit.arrow.GetComponent<Renderer>().material.color = Color.yellow;
    }
     
    #endregion




}