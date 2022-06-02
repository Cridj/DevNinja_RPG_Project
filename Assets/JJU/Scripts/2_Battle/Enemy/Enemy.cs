using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;


public enum ENEMY_TYPE
{
    WOLF,BOAR,ELITEWOLF,BAT,BEAR,BLACKBEAR,INFECTED_RAT,RAT,RAT_KING, STAGE2,
    SKELETON, SKELETON_ARCHER, SKELETON_ELITE, SKELETON_EMPERER, SKELETON_MAGICIAN,STAGE3,
    CANNIBAL,DREADNOUGHT,JUGGERNAUT,MANEATER,OGRE,TROLL,MAGICIANTROLL
}

public class Enemy : EnemySkill
{
    [Header("공격 딜레이")]
    public float meeleAttackDelaySec;

    [Header("경험치")]
    [Tooltip("경험치")]
    [Space(10f)]
    /// <summary>
    /// 경험치
    /// </summary>
    public int experience;
    [Space(20f)]

    //?????? ???????? ????
    public bool bSkeleton;
    public bool bRangedMonster;
    public bool bMagician;
    public int SortLayerOrder;
    
    




    public bool bInfected; 
    /// <summary>
    /// 랜덤 타겟 히어로
    /// </summary>
    public int ranHero;

    public bool bInfectedRat;

    public bool bRevive;

    public bool bJab;

    /// <summary>
    /// 선택되었는지 아닌지
    /// </summary>
    public bool bSelected;


    public SortingGroup sortingGroup;


    /// <summary>
    /// ?????????? ????
    /// </summary>
    public Unit unit;


    /// <summary>
    /// ?????????? 1??
    /// </summary>
    public SKILL_TYPE skillSet;

    /// <summary>
    /// ?????????? 1??
    /// </summary>
    public SKILL_TYPE skillSet2;
    
    public GameObject deathEffect;
    
    /// <summary>
    /// ???? ?????? ????
    /// </summary>
    public SKILL_TYPE CurrentSKill;
    
    /// <summary>
    /// ?????????? ????
    /// </summary>
    public PassiveSKill passiveSKill;
    
    /// <summary>
    /// ???? ???????? ?? ????
    /// </summary>
    public Unit damagedUnit;
    
    /// <summary>
    /// ???????? ?????? ????
    /// </summary>
    public Vector3 destinationPosV3;
    
    /// <summary>
    /// ?????? ???????? ????
    /// </summary>
    public bool bAttackEnd;
    
    /// <summary>
    /// ?????????? ????
    /// </summary>
    public bool bAttack;
    
    /// <summary>
    /// ???? ?????????? ????
    /// </summary>
    public bool bSkill;
          
    /// <summary>
    /// ???? ???? ???????? ????
    /// </summary>
    public bool bDie;
            
    public Vector3 targetPosV3;

    public Vector3 beginPosV3;
    
    public bool bHerdHunting;
    
    /// <summary>
    /// ????????
    /// </summary>
    public float moveSpeed;

    /// <summary>
    /// ???????? ????????
    /// </summary>
    public float attackMoveSpeed;

    /// <summary>
    /// ???????? ?????????? ????????
    /// </summary>
    public Animator animator;

    /// <summary>
    /// ???????? ?????? ???? ??????
    /// </summary>
    public bool bMoveTrigger;


    public void Awake()
    {
        moveSpeed = 8f;
        rangedOffset = GameObject.Find("HealOffset").GetComponent<Transform>();
        sortingGroup = GetComponent<SortingGroup>();
        unit = transform.GetComponent<Unit>();
        sfx_AS = GameObject.Find("Sound").GetComponent<LogoSound>().SFX_as;

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    public void Start()
    {        
        //???????????? ?????? ????????
        if (bSkeleton)
        {
            transform.localEulerAngles = new Vector3(0f, 180f, 0f);
            transform.localScale = Vector3.one;
            RectTransform rect = unit.ccIconManager.transform.GetComponent<RectTransform>();
            rect.Rotate(new Vector3(0f, -180f, 0f));
        }
        beginPosV3 = destinationPosV3;
        targetPosV3 = beginPosV3 + Vector3.left;        
    }


    public void Update()
    {
        if (bMoveTrigger)
        {
            Move();
        }
        if (bAttack)
        {
            OnAttackMove();
        }
        if (bAttackEnd)
        {
            OnAttackEndMove();
        }
    }

    #region[공격로직]
    public IEnumerator AttackEndCoroutine()
    {
        if(passiveSKill != PassiveSKill.NONE)
        {
            switch (passiveSKill)
            {
                case PassiveSKill.SimpleMagic:
                    StartCoroutine(SimpleMagic(unit));
                    yield return new WaitForSeconds(1.2f);
                    break;
            }
        }
        animator.SetInteger("State", 2);
        unit.GetComponent<Enemy>().bAttackEnd = true;
    }

    public void Selected()
    {
        bSelected = true;
        unit.arrow.GetComponent<Renderer>().material.color = Color.red;
        sortingGroup.sortingOrder = 250;
    }

    public void DeSelected()
    {
        bSelected = false;
        unit.arrow.GetComponent<Renderer>().material.color = Color.yellow;
        sortingGroup.sortingOrder = SortLayerOrder;
    }

    public void OnHeal(float value)
    {
        unit.Hp = Mathf.Clamp(unit.Hp += value, 0, unit.maxHP);
        StartCoroutine(PlayParticle(HealingOnce, 0.5f, 0f,transform.position +Vector3.up * 0.8f));
    }

    public void OnDie()
    {
        if(bRevive)
        {
            bRevive = false;
            unit.Hp = unit.maxHP;
            return;
        }
        bDie = true;
        unit.bDie = true;
        if (bSkeleton)
        {
            animator.SetInteger("State",6);
        }
        else
        {
            animator.SetInteger("State", 9);
        }
        BattleManager.I.TotalExp += experience;
        if (gameObject.activeSelf)
        {
            StartCoroutine(DeActivated(0.5f));
        }       
    }

    IEnumerator DeActivated(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject particle = Instantiate(GameInstance.Instance.particlePrefab["DEATH"], transform.position + Vector3.up * 1.5f, Quaternion.identity) as GameObject;

        //particle.AddComponent<SortingGroup>().sortingOrder = 300;

        Destroy(particle, 1f);
        
        if (passiveSKill == PassiveSKill.REVIVE_INFECTED_RAT)
        {
            foreach(var enemy in BattleManager.I.enemyCollection.collection)
            {
                enemy.bInfected = false;
                if(enemy.name == "InfectedRat (0)")
                {
                    enemy.gameObject.SetActive(false); 
                }
            }
        }

        
        if (bInfected)
        {
            GameObject enemy = GameInstance.Instance.CreateEnemyPrefab("INFECTED_RAT",
                                                                   transform.position,
                                                                   Vector3.one * 0.5f,
                                                                   Quaternion.identity);
            enemy.transform.parent = BattleManager.I.enemyCollection.transform;
            BattleManager.I.enemyCollection.collection.Add(enemy.GetComponent<Enemy>());
        }
        gameObject.SetActive(false);
    }

    public void MoveToPos()
    {
        animator.SetInteger("State", 2);
        bMoveTrigger = true;
        this.destinationPosV3 = transform.position + Vector3.left * 10f;
    }

    public void Move()
    {
        if (transform.position == destinationPosV3)
        {
            bMoveTrigger = false;
            animator.SetInteger("State", 0);
            BattleManager.I.ChangeHeroAnimationState(0);
            BattleManager.I.unitArriveCheck++;
            StartCoroutine(BattleManager.I.UnitArriveCheck()); 
        }
        transform.position = Vector3.MoveTowards(transform.position, destinationPosV3, moveSpeed * Time.deltaTime * 0.5f);
    }

    public float originPhysicalAttack;



    public void EnableHerdHunting()
    {
        bHerdHunting = true;        
    }

    public void DisableHerdHunting()
    {
        unit.PhysicalAttack = originPhysicalAttack;
    }

    #region[공격]
    public void Attack()
    {
        animator.SetInteger("State", 2);
        bAttack = true;
        FindTarget();
        //BattleManager.I.heroCollection.collection[ranHero].unit.Hp = 
        //    BattleManager.I.heroCollection.collection[ranHero].unit.Hp - (int)Damage;
    }


    
    
    public void OnAttackMove()
    {
        if (transform.position == targetPosV3)
        {
            if (bSkill)
            {
                bAttack = false;
                animator.SetInteger("State", 0);
                OnSkill();
                return;
            }
            else
            {
                bAttack = false;
                animator.SetInteger("State", 0);
                StartCoroutine(OnAttack());
                return;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPosV3, attackMoveSpeed * Time.deltaTime);
    }
    
    public void OnAttackEndMove()
    {
        if (transform.position == beginPosV3)
        {
            bAttackEnd = false;
            animator.SetInteger("State", 0);
            //BattleManager.I.NextTurn();
            StartCoroutine(AttackEndTurnChange());
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, beginPosV3, attackMoveSpeed * Time.deltaTime);
    }

    public void EnemyDeActivate()
    {
        gameObject.SetActive(false);
    }

    public IEnumerator AttackEndTurnChange()
    {
        yield return new WaitForSeconds(0.3f);
        BattleManager.I.NextTurn();
    }

    public List<Unit> targetList = new List<Unit>();


   
    public IEnumerator OnAttack()
    {
        if (bSkeleton)
        {
            if (bRangedMonster)
            {
                if (bMagician)
                {
                    animator.SetTrigger("Cast");
                    yield return new WaitForSeconds(0.3f);
                    
                    MagicainBasicAttackMissile.SetActive(true);
                    MagicainBasicAttackMissile.transform.localPosition = Vector3.zero;

                    MagicainBasicAttackMissile.transform.DOMove(damagedUnit.transform.position,0.8f);
                    StartCoroutine(DeActiveDelay(MagicainBasicAttackMissile, 0.8f));
                    yield return new WaitForSeconds(0.3f);
                }
                else
                {
                    animator.SetTrigger("SimpleBowShot");
                    yield return new WaitForSeconds(0.3f);

                    ArcherArrow archer = bowArrow.GetComponent<ArcherArrow>();
                    archer.bShot = true;
                    archer.target = damagedUnit.gameObject;
                    bowArrow.SetActive(true);
                    yield return new WaitForSeconds(0.3f);
                }

            }
            else
            {
                if (bJab)
                {
                    animator.SetTrigger("Jab");
                    yield return new WaitForSeconds(meeleAttackDelaySec);
                }
                else
                {
                    animator.SetTrigger("Slash");
                    yield return new WaitForSeconds(meeleAttackDelaySec);
                }


            }
        }
        else
        {
            animator.SetTrigger("Attack");

            //?????????????????? ???????? ?????????? ???????? ?????? ???????? ????
            yield return new WaitForSeconds(meeleAttackDelaySec);
        }

        if (targetList != null)
        {
            sfx_AS.PlayOneShot(GameInstance.Instance.soundPrefab["EnemyHit"]);
            damagedUnit.DecreaseHP(unit.PhysicalAttack, unit);
            Camera.main.GetComponent<CamShake>().ShakeTime = 0.15f;

            //hp?? 0 ????(????)??????
            if (damagedUnit.Hp <= 0)
            {
                yield return new WaitForSeconds(0.5f);
                damagedUnit.GetComponent<Hero>().OnDie();
            }
            yield return new WaitForSeconds(0.45f);
            bAttackEnd = true;
            bSkill = false;
            animator.SetInteger("State", 2);
        }
    }


    //// dir = (target.pos - pos).normalize
    //protected void LookAt2D(Vector3 dir)
    //{
    //    bowArrow.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90);
    //}

    #endregion


    #region [스킬]
    public void OnSkill()
    {
        //????????
        switch (CurrentSKill)
        {
            case SKILL_TYPE.DOUBLE_ATTACK:
                StartCoroutine(DoubleAttack(damagedUnit,animator,unit));
                break;

            case SKILL_TYPE.CHARGE_ATTACK:
                StartCoroutine(ChargeAttack(damagedUnit, animator, unit));
                break;

            case SKILL_TYPE.REDUCES_Damage_Taken:
                StartCoroutine(ReducesDamageTakenForTwoTurn(damagedUnit,animator,unit));
                break;

            case SKILL_TYPE.POISON_ATTACK:
                StartCoroutine(PoisonAttack(damagedUnit, animator, unit));
                break;

            case SKILL_TYPE.HERD_HUNTING:
                StartCoroutine(HerdHunting(animator, unit));
                break;

            case SKILL_TYPE.BEAST_STRIKE:
                StartCoroutine(BeastStrike(animator, unit));
                break;

            case SKILL_TYPE.BEAST_HOWLING:
                StartCoroutine(BeastHowling(animator, unit));
                break;

            case SKILL_TYPE.DARK_ARMOR:
                StartCoroutine(DarkArmor(animator, unit));
                break;

            case SKILL_TYPE.DARK_ARROW:
                StartCoroutine(DarkArrow(damagedUnit, animator, unit));
                break;

            case SKILL_TYPE.DARKNESS:
                StartCoroutine(Darkness(animator, unit));
                break;

            case SKILL_TYPE.CHAIN_LASHING:
                StartCoroutine(ChainLashing(damagedUnit, animator, unit));
                break;

            case SKILL_TYPE.STRONG_ATTACK:
                StartCoroutine(StrongAttack(damagedUnit, animator, unit));
                break;

            case SKILL_TYPE.DRAIN:
                StartCoroutine(Drain(animator, unit));
                break;

            case SKILL_TYPE.DARKNESS_RITUAL:
                StartCoroutine(DarknessRitual(animator, unit));
                break;

            case SKILL_TYPE.SHOUTING:
                StartCoroutine(Shouting(animator, unit));
                break;

            case SKILL_TYPE.DENTINGBLOWS:
                StartCoroutine(DentingBlows(damagedUnit,animator, unit));
                break;

            case SKILL_TYPE.FIREBALL:
                StartCoroutine(Fireball(damagedUnit,animator, unit));
                break;

            case SKILL_TYPE.ICESTORM:
                StartCoroutine(IceStorm(damagedUnit,animator, unit));
                break;

            case SKILL_TYPE.QUAKE:
                StartCoroutine(Quake(animator, unit));
                break;

            case SKILL_TYPE.THROWING_TREE:
                StartCoroutine(ThrowingTree(damagedUnit,animator, unit));
                break;

            case SKILL_TYPE.TROLLSHOUTING:
                StartCoroutine(TrollShouting(animator, unit));
                break;

            case SKILL_TYPE.TROLLING100PER:
                StartCoroutine(Shouting(animator, unit));
                break;

            case SKILL_TYPE.ROLLING_SNOWBALL:
                StartCoroutine(Shouting(animator, unit));
                break;

            case SKILL_TYPE.ICEHELL:
                StartCoroutine(Shouting(animator, unit));
                break;

            case SKILL_TYPE.AVALANCHE:
                StartCoroutine(Shouting(animator, unit));
                break;
        }
        bSkill = false;
        //StartCoroutine(PassiveSkillCheck());
    }

    IEnumerator PassiveSkillCheck()
    {
        if (passiveSKill == PassiveSKill.SimpleMagic)
        {
            StartCoroutine(SimpleMagic(unit));
            yield return new WaitForSeconds(1f);
        }

    }

    public void FindTarget()
    {
        bool bFindTauntHero = false;
        foreach (Hero targetUnit in BattleManager.I.heroCollection.collection)
        {
            if (targetUnit == null)
                continue;
            if(targetUnit.unit.tauntDuration > 0)
            {
                damagedUnit = targetUnit.unit;
                bFindTauntHero = true;
                break;
            }
            if (!targetUnit.unit.bDie)
            {
                targetList.Add(targetUnit.unit);
            }
        }
        if (!bFindTauntHero)
        {
            ranHero = Random.Range(0, targetList.Count);
            damagedUnit = targetList[ranHero];
        }
        targetList.Clear();
        if (bSkill == true)
        {
            if ((int)skillSet < 50)
            {
                targetPosV3 = damagedUnit.transform.position + Vector3.right * 3f;
            }
            else if ((int)skillSet > 70)
            {
                targetPosV3 = beginPosV3 + Vector3.left;
            }
            else
            {
                targetPosV3 = Vector3.down;
            }
        }
        else
        {
            if (bRangedMonster)
            {
                targetPosV3 = transform.position + Vector3.left * 1.5f;
            }
            else
            {
                targetPosV3 = damagedUnit.transform.position + Vector3.right * 3f;
            }            
        }
    }





    /// <summary>
    /// ????
    /// </summary>
    /// <param name="Damage"></param>
    public void Skill()
    {
        if(skillSet2 == SKILL_TYPE.NONE)
        {
            if (skillSet == SKILL_TYPE.REDUCES_Damage_Taken && unit.defenseBuffDuration > 0)
            {
                Attack();
                return;
            }
            CurrentSKill = skillSet;
            bAttack = true;
            bSkill = true;
            FindTarget();
            animator.SetInteger("State", 2);
        }
        else
        {
            int ran = Random.Range(0, 2);
            
            //???????? ????1,2 ?? ???? ????
            if (ran == 0)
            {
                if (skillSet == SKILL_TYPE.REDUCES_Damage_Taken && unit.defenseBuffDuration > 0)
                {
                    Attack();
                    return;
                }
                CurrentSKill = skillSet;
                bAttack = true;
                bSkill = true;
                FindTarget();
                animator.SetInteger("State", 2);
            }
            else
            {
                if (skillSet2 == SKILL_TYPE.REDUCES_Damage_Taken && unit.defenseBuffDuration > 0)
                {
                    Attack();
                    return;
                }
                CurrentSKill = skillSet2;
                bAttack = true;
                bSkill = true;
                FindTarget();
                animator.SetInteger("State", 2);
            }
        }
    }

    public void OnHit()
    {
        if (bSkeleton)
        {
            animator.SetTrigger("Hit");
        }
        else
        {
            //transform.DOShakePosition(0.06f, 0.5f, 6);
        }
    }
}
#endregion
#endregion