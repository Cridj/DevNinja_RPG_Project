using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Hero : HeroSkill
{


    #region [������]
    [Header("������")]
    [Space(10f)]

    public int ultCoolDown;



    /// <summary>
    /// ���緹��
    /// </summary>
    public int Level;

    public bool jumpUnit;

    /// <summary>
    /// ���� ����
    /// </summary>
    public float Experience;

    public int heroLevel;

    /// <summary>
    /// ���� ���� ���������� �ʿ��� ����ġ (Lerp����)
    /// </summary>
    public float DestExperience;

    public List<SpriteRenderer> renderers;

    public HeroItem[] items;


    /// <summary>
    /// ���� �θ��� ����
    /// </summary>
    public Unit unit;

    /// <summary>
    /// ���ݽ��� ��ġ
    /// </summary>
    public Vector3 beginPosV3;

    /// <summary>
    /// Ÿ����ġ
    /// </summary>
    public Vector3 targetPosV3;

    /// <summary>
    /// �̵��ӵ�
    /// </summary>
    public float moveSpeed;

    /// <summary>
    /// ���� �����ϰ��ִ���
    /// </summary>
    public bool bAttack;

    /// <summary>
    /// ���� ��ų���������
    /// </summary>
    public bool bSkill;

    /// <summary>
    /// ���� ��������
    /// </summary>
    public bool bTurnEnd;

    /// <summary>
    /// ���� �����������
    /// </summary>
    public bool bDie;

    /// <summary>
    /// ���� Ÿ���� ��
    /// </summary>
    public Enemy targetEnemy;
    

    /// <summary>
    /// ���� Ÿ���� �� (�������� ����Ҷ�)
    /// </summary>
    public Hero targetHero;

    /// <summary>
    /// ����� �ִϸ��̼� ��Ʈ�ѷ�
    /// </summary>
    public Animator animator;


    /// <summary>
    /// ����� ���ÿ���
    /// </summary>
    public bool bSelected;

    [Header("��ų ������ ��������Ʈ")]
    [Space(10f)]
    public Sprite skill1_Sprite;
    public Sprite skill2_Sprite;
    public Sprite skill3_Sprite;


    #endregion



    private void Awake()
    {
        //�÷��̾� ����ġ �䱸�� �ʱ�ȭ
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

    #region [����]

    //���ݸ�� �ޱ�
    public void Attack(Enemy enemy)
    {
        BattleManager.I.heroBattlePanel.SetActive(false);
        //���ݺ��� Ȱ��ȭ
        bAttack = true;

        //�ִϸ��̼� ���� �ȱ���� ���ֱ�
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

    //���� ����
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

        print("����!");
        Unit targetUnit = targetEnemy.transform.GetComponent<Unit>();
        yield return new WaitForSeconds(0.3f);
        targetUnit.DecreaseHP(unit.currentPhysicalAttack, unit);
        Camera.main.GetComponent<CamShake>().ShakeTime = 0.15f;
        StartCoroutine(PlayParticle(swordSlashMini, 0.6f, 0.0f));

        //���� ���ݿ� �¾� ����ϸ�
        if (targetUnit.Hp <= 0)
        {
            yield return new WaitForSeconds(0.1f);
            //targetEnemy.OnDie();

            targetEnemy.OnDie();
            yield return new WaitForSeconds(0.4f);
            //targetEnemy.gameObject.SetActive(false);
            print("�� ����");
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

    #region [�̵�]
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

    ///�ൿ�� ���� Ÿ���������� �̵�
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






    //�ൿ�� ���� �� ���ڸ��� ���ƿ��� �� �ѱ��
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


    //������ �ޱ�
    public void Gaurd(float Damage)
    {

    }


    #region [��ų]
    //��ų��� �ޱ�
    /// <summary>
    /// ��ų
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
            //�ִϸ��̼� ���� �ȱ���� ���ֱ�
            animator.SetInteger("State", 1);
            bSkill = true;
        }
    }

    public void Skill()
    {
        BattleManager.I.heroBattlePanel.SetActive(false);
        //���ݺ��� Ȱ��ȭ


        //StartCoroutine(GetComponent<Hero>().OnTurnJumpToTargetPos());
        bSkill = true;

        //�ִϸ��̼� ���� �ȱ���� ���ֱ�
        animator.SetInteger("State", 1);
        
        if ((int)currentSKill > 20)
        {
            targetPosV3 = transform.position + Vector3.right * 2f;
        }
    }



    //���� ����
    IEnumerator OnSkill()
    {
        //���Ÿ� �����ϋ�
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
        //�ٰŸ� �����϶�
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




    #region [��Ÿ�Լ�]

    /// <summary>
    /// ����� ���� �ʱ�ȭ �� ������ ����
    /// </summary>
    public void InitStat()
    {
        switch (job)
        {
            case PlayerJob.Dealer:
                //�⺻���� ����
                unit.Hp = DataManager.Instance.m_PlayerData.fHealth[0];
                unit.maxHP = DataManager.Instance.m_PlayerData.fHealth[0];
                unit.PhysicalAttack = DataManager.Instance.m_PlayerData.fAttack[0];
                unit.Defense = DataManager.Instance.m_PlayerData.fDefense[0];
                unit.MagicalAttack = DataManager.Instance.m_PlayerData.fMagic[0];
                unit.Speed = DataManager.Instance.m_PlayerData.fSpeed[0];

                //����ġ ����
                Experience = DataManager.Instance.m_PlayerData.fExp[0];

                //���� ����
                heroLevel = DataManager.Instance.m_PlayerData.nLevel[0];

                //��ų ����
                skill_set1 = GameInstance.Instance.gamdData.dealerData.skillSets[0];
                skill_set2 = GameInstance.Instance.gamdData.dealerData.skillSets[1];
                skill_set3 = GameInstance.Instance.gamdData.dealerData.skillSets[2];

                //������ ����
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
                //�⺻���� ����
                unit.Hp = DataManager.Instance.m_PlayerData.fHealth[1];
                unit.maxHP = DataManager.Instance.m_PlayerData.fHealth[1];
                unit.PhysicalAttack = DataManager.Instance.m_PlayerData.fAttack[1];
                unit.Defense = DataManager.Instance.m_PlayerData.fDefense[1];
                unit.MagicalAttack = DataManager.Instance.m_PlayerData.fMagic[1];
                unit.Speed = DataManager.Instance.m_PlayerData.fSpeed[1];

                //����ġ ����
                Experience = DataManager.Instance.m_PlayerData.fExp[1];

                //���� ����
                heroLevel = DataManager.Instance.m_PlayerData.nLevel[1];

                //��ų ����
                skill_set1 = GameInstance.Instance.gamdData.tankerData.skillSets[0];
                skill_set2 = GameInstance.Instance.gamdData.tankerData.skillSets[1];
                skill_set3 = GameInstance.Instance.gamdData.tankerData.skillSets[2];

                ////������ ����
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
                //�⺻���� ����
                unit.Hp = DataManager.Instance.m_PlayerData.fHealth[2];
                unit.maxHP = DataManager.Instance.m_PlayerData.fHealth[2];
                unit.PhysicalAttack = DataManager.Instance.m_PlayerData.fAttack[2];
                unit.Defense = DataManager.Instance.m_PlayerData.fDefense[2];
                unit.MagicalAttack = DataManager.Instance.m_PlayerData.fMagic[2];
                unit.Speed = DataManager.Instance.m_PlayerData.fSpeed[2];

                //����ġ ����
                Experience = DataManager.Instance.m_PlayerData.fExp[2];

                //���� ����
                heroLevel = DataManager.Instance.m_PlayerData.nLevel[2];

                //��ų ����
                skill_set1 = GameInstance.Instance.gamdData.healerData.skillSets[0];
                skill_set2 = GameInstance.Instance.gamdData.healerData.skillSets[1];
                skill_set3 = GameInstance.Instance.gamdData.healerData.skillSets[2];

                ////������ ����
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