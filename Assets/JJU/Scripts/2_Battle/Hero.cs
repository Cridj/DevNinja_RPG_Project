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
        switch (job)
        {
            case PlayerJob.Dealer :
                Experience = GameInstance.Instance.gamdData.dealerData.Exp;
                heroLevel = GameInstance.Instance.gamdData.dealerData.Level;
                break;

            case PlayerJob.Healer:
                Experience = GameInstance.Instance.gamdData.healerData.Exp;
                heroLevel = GameInstance.Instance.gamdData.healerData.Level;
                break;

            case PlayerJob.Tanker:
                Experience = GameInstance.Instance.gamdData.tankerData.Exp;
                heroLevel = GameInstance.Instance.gamdData.tankerData.Level;
                break;

        }
            
        
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
            yield return new WaitForEndOfFrame();
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
            yield return new WaitForEndOfFrame();
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
        //if (WeaponTrail1)
        //    WeaponTrail1.SetActive(false);
        //if (WeaponTrail2)
        //    WeaponTrail2.SetActive(false);
        if (transform.position == beginPosV3)
        {
            bTurnEnd = false;
            unit.attackAble = false;
            unit.skillAble = false;
            animator.SetInteger("State", 0);
            BattleManager.I.NextTurn();
            //if (WeaponTrail1)
            //    WeaponTrail1.SetActive(false);
            //if (WeaponTrail2)
            //    WeaponTrail2.SetActive(false);
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
                //���� ����
                unit.Hp = GameInstance.Instance.gamdData.dealerData.HP;
                unit.maxHP = GameInstance.Instance.gamdData.dealerData.HP;
                unit.PhysicalAttack = GameInstance.Instance.gamdData.dealerData.Attack;
                unit.Defense = GameInstance.Instance.gamdData.dealerData.Defense;
                unit.MagicalAttack = GameInstance.Instance.gamdData.dealerData.MagicalAttack;
                unit.Speed = GameInstance.Instance.gamdData.dealerData.Speed;

                //����ġ ����
                Experience = GameInstance.Instance.gamdData.dealerData.Exp;                

                //��ų ����
                skill_set1 = GameInstance.Instance.gamdData.dealerData.skillSets[0];
                skill_set2 = GameInstance.Instance.gamdData.dealerData.skillSets[1];
                skill_set3 = GameInstance.Instance.gamdData.dealerData.skillSets[2];


                //������ ����
                foreach (var item in GameInstance.Instance.gamdData.dealerData.heroItems)
                {
                    if(item != null)
                    {
                        ApplyItem(item);
                    }
                }
                unit.currentPhysicalAttack = unit.PhysicalAttack;
                unit.currentMagicalAttack = unit.MagicalAttack;
                unit.currentDefense = unit.Defense;
                unit.currentSpeed = unit.Speed;
                break;

            case PlayerJob.Tanker:
                //���� ����
                unit.Hp = GameInstance.Instance.gamdData.tankerData.HP;
                unit.maxHP = GameInstance.Instance.gamdData.tankerData.HP;
                unit.PhysicalAttack = GameInstance.Instance.gamdData.tankerData.Attack;
                unit.Defense = GameInstance.Instance.gamdData.tankerData.Defense;
                unit.MagicalAttack = GameInstance.Instance.gamdData.tankerData.MagicalAttack;
                unit.Speed = GameInstance.Instance.gamdData.tankerData.Speed;

                //����ġ ����
                Experience = GameInstance.Instance.gamdData.tankerData.Exp;

                //��ų ����
                skill_set1 = GameInstance.Instance.gamdData.tankerData.skillSets[0];
                skill_set2 = GameInstance.Instance.gamdData.tankerData.skillSets[1];
                skill_set3 = GameInstance.Instance.gamdData.tankerData.skillSets[2];

                unit.currentPhysicalAttack = unit.PhysicalAttack;
                unit.currentMagicalAttack = unit.MagicalAttack;
                unit.currentDefense = unit.Defense;
                unit.currentSpeed = unit.Speed;
                break;

            case PlayerJob.Healer:
                //���� ����
                unit.Hp = GameInstance.Instance.gamdData.healerData.HP;
                unit.maxHP = GameInstance.Instance.gamdData.healerData.HP;
                unit.PhysicalAttack = GameInstance.Instance.gamdData.healerData.Attack;
                unit.Defense = GameInstance.Instance.gamdData.healerData.Defense;
                unit.MagicalAttack = GameInstance.Instance.gamdData.healerData.MagicalAttack;
                unit.Speed = GameInstance.Instance.gamdData.healerData.Speed;

                //����ġ ����
                Experience = GameInstance.Instance.gamdData.healerData.Exp;

                //��ų ����
                skill_set1 = GameInstance.Instance.gamdData.healerData.skillSets[0];
                skill_set2 = GameInstance.Instance.gamdData.healerData.skillSets[1];
                skill_set3 = GameInstance.Instance.gamdData.healerData.skillSets[2];

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