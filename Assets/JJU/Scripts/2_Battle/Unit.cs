using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;

public class Unit : Stats
{
    #region[변수들]

    [Header("필수설정 컴포넌트")]
    [Space(10f)]
    public CC_IconManager ccIconManager;
    public GameObject arrow;
    public TextMeshProUGUI DamageText;
    public Slider HpBarSlider;
    public Camera UI_Camera;
    public AudioSource sfx_Audio;
    public StateTextManager stateTextManager;



    [Header(" 변수들 ")]
    [Space(10f)]
    public bool bDie;
    public bool bTurn;
    public bool bTurnover;
    /// <summary>
    /// 유닛이 행동중인지 체크
    /// </summary>
    public bool OnAction;



    public IEnumerator damageCoroutine;

    /// <summary>
    /// 스킬사용확률
    /// </summary>
    public float fSkillChance;



    public bool attackAble;
    Camera mainCamera;
    public Vector3 mouseStartPos;
    public Sprite unitSprite;
    public bool skillAble;





    [Header ("지속시간 관련")]
    [Space(10f)]
    public int PoisonDuration;
    /// <summary>
    /// 하울링 버프디버프 지속시간 공/방
    /// </summary>
    public int howlingBuffDebuffDuration;
    /// <summary>
    /// 트롤의 울부짖음 지속시간 방어력 증가
    /// </summary>
    public int trollShoutingDuration;
    /// <summary>
    /// 곰 패시브 공격력증가 지속시간 /공
    /// </summary>
    public bool bAttackIncrease_Passive_Bear;

    public bool bMasteredBlackMagic_Passive;

    /// <summary>
    /// 박쥐새끼 방어력증가 지속시간 /방
    /// </summary>
    public int defenseBuffDuration;

    /// <summary>
    /// 도발 온오프유무
    /// </summary>
    public int tauntDuration;

    public int chainLashingDuration;

    /// <summary>
    /// 갈비뼈 지속시간
    /// </summary>
    public int darkArmorDuration;

    /// <summary>
    /// 포효 지속시간 히어로 : 방어력증가, 적 : 행동력감소
    /// </summary>
    public int roarDuration;

    /// <summary>
    /// 암흑 지속시간
    /// </summary>
    public int darkDuration;

    public int shoutingDuration;
    /// <summary>
    /// 현재 선택중인 행동
    /// </summary>
    string nowAction;
    /// <summary>
    /// 남은턴 체크용 변수 아직 턴 안돌아온 유닛용
    /// </summary>
    public bool noTurnSelected;

    int TotalDamage;


    /// <summary>
    /// 남은턴 체크용 변수 턴을 사용한 유닛용
    /// </summary>
    public bool turnChecked;
    public int turnOrder;
    
    public int howlingBuffDebuff;
    #endregion

    private void Awake()
    {
        sfx_Audio = GameObject.Find("Sound").GetComponent<LogoSound>().SFX_as;
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        UI_Camera = GameObject.Find("UI_Camera").GetComponent<Camera>();
    }

    private void Update()
    {
        HpBarUpdate();
        if (bTurn)
        {
            if (attackAble)
            {
                AttackCheck();
            }
            if (skillAble)
                SkillCheck();
        }
        else
        {
        }
    }

    public void MonsterStatIconUpdate()
    {
        //공격력 아이콘 관리
        if(howlingBuffDebuffDuration > 0 || 
            bAttackIncrease_Passive_Bear || 
            GetComponent<Enemy>().bHerdHunting || 
            darkArmorDuration > 0 ||
            shoutingDuration > 0)
        {
            ccIconManager.attackBuffIcon.SetActive(true);
        }
        else
        {
            ccIconManager.attackBuffIcon.SetActive(false);
        }

        //방어력 아이콘 관리
        if(howlingBuffDebuffDuration > 0 || 
            defenseBuffDuration > 0 || 
            darkArmorDuration > 0 ||
            shoutingDuration > 0 ||
            trollShoutingDuration > 0)
        {
            ccIconManager.defenseBuffIcon.SetActive(true);
        }
        else 
        {
            ccIconManager.defenseBuffIcon.SetActive(false);
        }

        //둔화 아이콘 관리
        if (roarDuration > 0)
        {
            ccIconManager.slowIcon.SetActive(true);
        }
        else
        {
            ccIconManager.slowIcon.SetActive(false);
        }

        //암흑 아이콘 관리
        if(darkDuration > 0)
        {
            ccIconManager.darknessIcon.SetActive(true);
        }
        else
        {
            ccIconManager.darknessIcon.SetActive(false);
        }

        if (bMasteredBlackMagic_Passive)
        {
            ccIconManager.magicBuffIcon.SetActive(true);
        }
        else
        {
            ccIconManager.magicBuffIcon.SetActive(false);
        }

    }
    public void HeroStatIconUpdate()
    {
        ////공격력 버프 아이콘 관리
        //if (roatDuration > 0)
        //{
        //    ccIconManager.attackBuffIcon.SetActive(true);
        //}
        //else
        //{
        //    ccIconManager.attackBuffIcon.SetActive(false);
        //}

        //공격력 디버프 아이콘 관리
        if (howlingBuffDebuffDuration > 0)
        {
            ccIconManager.attackDeBuffIcon.SetActive(true);
        }
        else
        {
            ccIconManager.attackDeBuffIcon.SetActive(false);
        }

        //방어력 버프 아이콘 관리
        if (roarDuration > 0)
        {
            ccIconManager.defenseBuffIcon.SetActive(true);
        }
        else
        {
            ccIconManager.defenseBuffIcon.SetActive(false);
        }

        //방어력 디버프 아이콘 관리
        if (howlingBuffDebuffDuration > 0)
        {
            ccIconManager.defenseDeBuffIcon.SetActive(true);
        }
        else
        {
            ccIconManager.defenseDeBuffIcon.SetActive(false);
        }

        //독 아이콘 관리
        if(PoisonDuration > 0)
        {
            ccIconManager.poisonIcon.SetActive(true);
        }
        else
        {
            ccIconManager.poisonIcon.SetActive(false);
        }

        //암흑 아이콘 관리
        if (darkDuration > 0)
        {
            ccIconManager.darknessIcon.SetActive(true);
        }
        else
        {
            ccIconManager.darknessIcon.SetActive(false);
        }

        //침묵 아이콘 관리
        if(chainLashingDuration > 0)
        {
            ccIconManager.silenceIcon.SetActive(true);
        }
        else
        {
            ccIconManager.silenceIcon.SetActive(false);
        }

    }
    public void DecreaseHP(float Damage, Unit initiator)
    {
        float currentDamage;
        //대미지 공식 적용
        currentDamage = Damage * (1f - (currentDefense / (currentDefense + 100)));        



        if (CheckHero())
        {         

            if (initiator.GetComponent<Enemy>().bHerdHunting)
            {
                Hp = Hp - currentDamage * 1.3f;
                initiator.GetComponent<Enemy>().bHerdHunting = false;
                initiator.GetComponent<Enemy>().AttackBuffParticle.SetActive(false);
                StartCoroutine(transform.GetComponent<Hero>().OnHit());
                StartCoroutine(DamageTextActivate(((int)(currentDamage * 1.3f)),0f,0.4f));  
                return;
            }
            else
            {
                Hp = Hp - currentDamage;
                StartCoroutine(transform.GetComponent<Hero>().OnHit());
                StartCoroutine(DamageTextActivate(((int)currentDamage), 0f, 0.4f));
            }
            //탱커면 희생정신 
            if(GetComponent<Hero>().job == PlayerJob.Tanker)
            {
                GetComponent<Hero>().Sacrifice(this);
            }
            BattleScene.I.currentUI_Update();
        }
        else
        {
            //방어력버프 활성화 되있을 시 대미지 반감시키기.
            if(defenseBuffDuration > 0)
            {
                if(initiator.GetComponent<Hero>().currentSKill == SKILLSET.TRANCE_SWORD 
                    && DamageText.gameObject.activeSelf)
                {
                    TotalDamage += (int)(currentDamage * 0.5f);
                    DamageText.text = TotalDamage.ToString();
                    Hp = Hp - currentDamage * 0.5f;
                    return;
                }
                if(initiator.GetComponent<Hero>().currentSKill == SKILLSET.TRANCE_SWORD)
                {
                    damageCoroutine = DamageTextActivate(((int)(currentDamage * 0.5f)), 0f, 3f);
                }
                else
                {
                    damageCoroutine = DamageTextActivate(((int)(currentDamage * 0.5f)), 0f, 0.4f);
                }                
                Hp = Hp - currentDamage * 0.5f;
                TotalDamage += (int)(currentDamage * 0.5f);
                StartCoroutine(damageCoroutine);
            }
            else
            {
                if (initiator.GetComponent<Hero>().currentSKill == SKILLSET.TRANCE_SWORD 
                    && DamageText.gameObject.activeSelf)
                {
                    TotalDamage += (int)(currentDamage);
                    DamageText.text = TotalDamage.ToString();
                    Hp = Hp - currentDamage;
                    return;
                }
                if(initiator.GetComponent<Hero>().currentSKill == SKILLSET.TRANCE_SWORD)
                {
                    damageCoroutine = DamageTextActivate(((int)currentDamage), 0f, 3f);
                }
                else
                {
                    damageCoroutine = DamageTextActivate(((int)currentDamage), 0f, 0.4f);

                }                
                TotalDamage += (int)currentDamage;
                Hp = Hp - currentDamage;
                if (gameObject.activeSelf)
                {
                    StartCoroutine(damageCoroutine);
                }
            }
            if (GetComponent<Enemy>())
            {
                var enemy = GetComponent<Enemy>();
                if(enemy.passiveSKill == PassiveSKill.ZealotMode)
                {
                    GetComponent<Enemy>().ZealotMode(this, Damage,true);
                }
            }
        }
        //GameObject hitParticle = Instantiate(GameInstance.Instance.particlePrefab["Hit_1"],transform.position + Vector3.up * 1.5f,Quaternion.identity);
        //hitParticle.AddComponent<SortingGroup>().sortingOrder = 500;
        //Destroy(hitParticle,0.5f);

        if(CheckHero() && Hp <= 0)
        {
            GetComponent<Hero>().OnDie();
        }
    }
    public IEnumerator DamageTextActivate(int Damage, float Delaytime, float Duration)
    {
        if (GetComponent<Enemy>())
        {
            GetComponent<Enemy>().OnHit();
        }

        //yield return new WaitForSeconds(Delaytime);
        DamageText.gameObject.SetActive(true);
        DamageText.text = "-" + Damage.ToString();
        yield return new WaitForSeconds(Duration);
        DamageText.gameObject.SetActive(false);
    }
    bool CheckHero()
    {
        if (transform.GetComponent<Hero>())
        {
            return true;
        }
        else
        { 
            return false;
        }
    }
    public void BuffAndDeBuffDurationDecrease()
    {
        Mathf.Clamp(howlingBuffDebuffDuration--, 0, 5);
        Mathf.Clamp(defenseBuffDuration--, 0, 5);
        Mathf.Clamp(PoisonDuration--, 0, 5);
    }
    public void HeroPassiveCheck()
    {
        if (GetComponent<Hero>())
        {
            if(GetComponent<Hero>().job == PlayerJob.Dealer)
            {
                GetComponent<Hero>().Calm(this);
            }
        }     
    }
    public IEnumerator ShoutingBuff()
    {
        if (shoutingDuration > 0)
        {
            shoutingDuration = 3;
            yield break;
        }

        float defenseChange = Defense * 0.2f;
        float attackChange = PhysicalAttack * 0.2f;


        currentDefense += defenseChange;
        currentPhysicalAttack += attackChange;


        while (shoutingDuration > 0)
        {
            yield return null ;
        }


        currentDefense -= defenseChange;
        currentPhysicalAttack -= attackChange;

    }
    public IEnumerator RoarBuffDeBuff()
    {
        if (roarDuration > 0)
        {
            roarDuration = 3;
            yield break;
        }

        float defenseChange = Defense / 10;
        float speedChange = 1;

        if (CheckHero())
        {
            currentDefense += defenseChange;
        }
        else
        {
            currentSpeed -= speedChange;
        }

        while (roarDuration > 0)
        {
            yield return null;
        }

        if (CheckHero())
        {
            currentDefense -= defenseChange;
        }
        else
        {
            currentSpeed += speedChange;
        }
    }
    public IEnumerator HowlingBuffDeBuff()
    {
        if (howlingBuffDebuffDuration > 0)
        {
            howlingBuffDebuffDuration = 2;
            yield break;
        }

        float damageChange = PhysicalAttack * 0.1f;
        float defenseChange = Defense * 0.1f;

        if (CheckHero())
        {
            currentPhysicalAttack -= damageChange;
            currentDefense -= defenseChange;
        }
        else
        {
            currentPhysicalAttack += damageChange;
            currentDefense += defenseChange;
        }

        while (howlingBuffDebuffDuration > 0)
        {
            yield return null;
        }

        if (CheckHero())
        {
            currentPhysicalAttack += damageChange;
            currentDefense += defenseChange;
        }
        else
        {
            currentPhysicalAttack -= damageChange;
            currentDefense -= defenseChange;
        }
    }
    public IEnumerator TrollShoutingBuff()
    {
        if (trollShoutingDuration > 0)
        {
            trollShoutingDuration = 2;
            yield break;
        }

        float defenseChange = Defense * 0.2f;

        if (CheckHero())
        {
        }
        else
        {
            currentDefense += defenseChange;
        }

        while (howlingBuffDebuffDuration > 0)
        {
            yield return null;
        }

        if (CheckHero())
        {
        }
        else
        {
            currentDefense -= defenseChange;
        }
    }
    public IEnumerator DarkArmor()
    {
        if(darkArmorDuration > 0)
        {
            howlingBuffDebuffDuration = 3;
            yield break;
        }

        float damageChange = PhysicalAttack * 0.3f;
        float defenseChange = Defense * 0.1f;

        currentPhysicalAttack += damageChange;
        currentDefense += defenseChange;

        while(darkArmorDuration > 0)
        {
            yield return null;
        }


        currentPhysicalAttack -= damageChange;
        currentDefense -= defenseChange;

    }
    public void TickDamageDecrese()
    {
        if(PoisonDuration > 0)
        {
            Hp = Hp * 0.95f;
        }
    }
    public void HpBarUpdate()
    {
        HpBarSlider.value = Hp / maxHP;
    }
    private void AttackCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseStartPos = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));

            //Ray2D ray = new Ray2D(mouseStartPos, Vector2.zero);
            Ray2D ray = new Ray2D(mouseStartPos, mainCamera.transform.position);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit)
            {
                //레이발사 사거리 무한
                //레이에 맞은 오브젝트의 태그가 Enemy 라면.
                if (hit.collider.CompareTag("Enemy"))
                {
                    Enemy enemy = hit.transform.GetComponent<Enemy>();
                    if (enemy.bSelected)
                    {
                        transform.GetComponent<Hero>().Attack(enemy);
                        enemy.bSelected = false;
                        foreach (Enemy enemy1 in BattleManager.I.enemyCollection.collection)
                        {
                            enemy1.unit.arrow.GetComponent<Renderer>().material.color = Color.yellow;
                            enemy1.unit.arrow.SetActive(false);
                        }
                    }
                    else
                    {
                        //현재 선택한 적들 이외의 적을 선택한 경우 다른 적으로 선택 변경하는 코드 작성하기
                        foreach(Enemy enemy1 in BattleManager.I.enemyCollection.collection)
                        {
                            if(enemy1.bSelected == true)
                            {
                                enemy1.DeSelected();
                                enemy.Selected();
                                return;
                            }
                        }
                        if (enemy.bDie)
                            return;
                        enemy.Selected();

                    }
                }
            }
        }
    }    
    /// <summary>
    ///  스킬 체크
    /// </summary>
    private void SkillCheck()
    {
        if (OnAction)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            mouseStartPos = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));

            //Ray2D ray = new Ray2D(mouseStartPos, Vector2.zero);
            Ray2D ray = new Ray2D(mouseStartPos, mainCamera.transform.position);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            ///타겟이 적일때
            if (hit)
            {
                //레이발사 사거리 무한
                //레이에 맞은 오브젝트의 태그가 Enemy 라면.
                if (hit.collider.CompareTag("Enemy"))
                {
                    Enemy enemy = hit.transform.GetComponent<Enemy>();
                    if (enemy.bSelected)
                    {
                        StartCoroutine(SkillAction(enemy));
                        OnAction = true;
                    }
                    else
                    {
                        //현재 선택한 적들 이외의 적을 선택한 경우 다른 적으로 선택 변경하는 코드 작성하기
                        foreach (Enemy enemy1 in BattleManager.I.enemyCollection.collection)
                        {
                            if (enemy1.bSelected == true)
                            {
                                enemy1.DeSelected();
                                enemy.Selected();
                                return;
                            }
                        }
                        if (enemy.bDie)
                            return;
                        enemy.Selected();

                    }
                }

                ///타겟이 히어로일때
                else if (hit.collider.CompareTag("Player") && (int)GetComponent<Hero>().currentSKill > 20 && 
                                                              (int)GetComponent<Hero>().currentSKill < 40)
                {
                    Hero hero = hit.transform.GetComponent<Hero>();
                    if (hero.bSelected)
                    {
                        StartCoroutine(SkillAction(hero));
                        OnAction = true;
                    }
                    else
                    {
                        //현재 선택한 적들 이외의 적을 선택한 경우 다른 적으로 선택 변경하는 코드 작성하기
                        foreach (Hero hero1 in BattleManager.I.heroCollection.collection)
                        {
                            if (hero1.bSelected == true)
                            {
                                hero1.DeSelected();
                                hero.Selected();
                                return;
                            }
                        }
                        if (hero.bDie)
                            return;
                        hero.Selected();

                    }
                }
            }
        }
    }
    public void UnitTurnAction(string type)
    {
        //방금 눌렀던 버튼과 다른버튼을 누르면
        if(type != nowAction && GetComponent<Hero>())
        {
            foreach(var unit in BattleManager.I.unitList)
            {
                unit.arrow.SetActive(false);                
            }
            BattleScene.I.attack_Trail.SetActive(false);
            BattleScene.I.skill1_Trail.SetActive(false);
            BattleScene.I.skill2_Trail.SetActive(false);
            BattleScene.I.skill3_Trail.SetActive(false);
            skillAble = false;
            attackAble = false;
        }

        //히어로일때
        if (GetComponent<Hero>())
        {            
            switch (type)
            {
                case "Attack":
                    foreach (Enemy enemy in BattleManager.I.enemyCollection.collection)
                    {
                        if (enemy.bDie)
                            continue;
                        enemy.unit.arrow.SetActive(true);
                    }
                    attackAble = true;
                    nowAction = "Attack";
                    BattleScene.I.attack_Trail.SetActive(true);
                    break;

                case "Guard":

                    break;

                case "Skill1":
                    GetComponent<Hero>().currentSKill = transform.GetComponent<Hero>().skill_set1;
                    if ((int)GetComponent<Hero>().currentSKill > 20 && (int)GetComponent<Hero>().currentSKill < 40)
                    {
                        foreach (Hero hero in BattleManager.I.heroCollection.collection)
                        {
                            if (hero.bDie)
                                continue;
                            hero.unit.arrow.SetActive(true);
                        }
                        skillAble = true;
                        nowAction = "Skill1";
                    }
                    else if ((int)GetComponent<Hero>().currentSKill > 40)
                    {
                        foreach (Enemy enemy in BattleManager.I.enemyCollection.collection)
                        {
                            if (enemy.bDie)
                                continue;
                            enemy.unit.arrow.SetActive(true);
                        }
                        skillAble = true;
                        nowAction = "Skill1";
                    }
                    else
                    {
                        foreach (Enemy enemy in BattleManager.I.enemyCollection.collection)
                        {
                            if (enemy.bDie)
                                continue;
                            enemy.unit.arrow.SetActive(true);
                        }
                        skillAble = true;
                        nowAction = "Skill1";
                    }
                    BattleScene.I.skill1_Trail.SetActive(true);

                    break;

                case "Skill2":
                    GetComponent<Hero>().currentSKill = transform.GetComponent<Hero>().skill_set2;
                    if ((int)GetComponent<Hero>().currentSKill > 20 && (int)GetComponent<Hero>().currentSKill < 40)
                    {
                        foreach (Hero hero in BattleManager.I.heroCollection.collection)
                        {
                            if (hero.bDie)
                                continue;
                            hero.unit.arrow.SetActive(true);
                        }
                        skillAble = true;
                        nowAction = "Skill2";
                    }
                    else if ((int)GetComponent<Hero>().currentSKill > 40)
                    {
                        foreach (Enemy enemy in BattleManager.I.enemyCollection.collection)
                        {
                            if (enemy.bDie)
                                continue;
                            enemy.unit.arrow.SetActive(true);
                        }
                        skillAble = true;
                        nowAction = "Skill2";
                    }
                    else
                    {
                        foreach (Enemy enemy in BattleManager.I.enemyCollection.collection)
                        {
                            if (enemy.bDie)
                                continue;
                            enemy.unit.arrow.SetActive(true);
                        }
                        skillAble = true;
                        nowAction = "Skill2";
                    }
                    BattleScene.I.skill2_Trail.SetActive(true);
                    break;
                case "Skill3":

                    if (GetComponent<Hero>().ultCoolDown > 0)
                        return;

                    GetComponent<Hero>().currentSKill = transform.GetComponent<Hero>().skill_set3;
                    if ((int)GetComponent<Hero>().currentSKill > 20 && (int)GetComponent<Hero>().currentSKill < 40)
                    {
                        foreach (Hero hero in BattleManager.I.heroCollection.collection)
                        {
                            if (hero.bDie)
                                continue;
                            hero.unit.arrow.SetActive(true);
                        }
                        skillAble = true;
                        nowAction = "Skill3";
                    }
                    else if ((int)GetComponent<Hero>().currentSKill > 40)
                    {
                        foreach (Enemy enemy in BattleManager.I.enemyCollection.collection)
                        {
                            if (enemy.bDie)
                                continue;
                            enemy.unit.arrow.SetActive(true);
                        }
                        skillAble = true;
                        nowAction = "Skill3";
                    }
                    else
                    {
                        foreach (Enemy enemy in BattleManager.I.enemyCollection.collection)
                        {
                            if (enemy.bDie)
                                continue;
                            enemy.unit.arrow.SetActive(true);
                        }
                        skillAble = true;
                        nowAction = "Skill3";
                    }
                    BattleScene.I.skill3_Trail.SetActive(true);
                    break;
            }
        }
        //적의 턴일때
        else
        {
            switch (type)
            {
                case "Attack":
                    GetComponent<Enemy>().Attack();
                    break;

                case "Skill":
                    if(GetComponent<Enemy>().skillSet == SKILL_TYPE.NONE)
                    {
                        GetComponent<Enemy>().Attack();
                        break;
                    }
                    GetComponent<Enemy>().Skill();
                    break;
            }
        }
    }
    IEnumerator SkillAction(Enemy enemy)
    {
        UI_Camera.gameObject.SetActive(false);
        mainCamera.GetComponent<CamShake>().enabled = false;

        Vector3 originPos = mainCamera.transform.position;
        Vector3 targetPos = new Vector3();
        switch (GetComponent<Hero>().job)
        {
            case PlayerJob.Dealer:
                targetPos = new Vector3(-4.2f, -2f, -20);
                break;
            case PlayerJob.Healer:
                targetPos = new Vector3(-4.2f, 2f, -20);
                break;
            case PlayerJob.Tanker:
                targetPos = new Vector3(-3f, 0f, -20);
                break;
        }

        while (true)
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, 3, 0.1f);
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPos, 0.1f);
           
            if (mainCamera.orthographicSize <= 3.001f && mainCamera.transform.position.x < targetPos.x * 0.99f)
                break;
            yield return null;
        }

        Animator animator = GetComponent<Hero>().animator;

        //애니메이션 플레이
        animator.SetTrigger("Skill");
        sfx_Audio.PlayOneShot(GameInstance.Instance.soundPrefab["Skill"]);
        yield return new WaitForSeconds(1f);

        while (true)
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, 5, 0.1f);
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, originPos, 0.1f);

         
            if (mainCamera.orthographicSize >= 4.999f && mainCamera.transform.position.x > -0.0001f)
                break;
            yield return null;
        }
        UI_Camera.gameObject.SetActive(true);
        mainCamera.GetComponent<CamShake>().enabled = true;

        GetComponent<Hero>().Skill(enemy);
        enemy.bSelected = false;
        foreach (Enemy enemy1 in BattleManager.I.enemyCollection.collection)
        {
            enemy1.unit.arrow.GetComponent<Renderer>().material.color = Color.yellow;
            enemy1.unit.arrow.SetActive(false);
        }
    }
    IEnumerator SkillAction(Hero hero)
    {
        print("스킬사용");
        UI_Camera.gameObject.SetActive(false);
        mainCamera.GetComponent<CamShake>().enabled = false;

        Vector3 originPos = mainCamera.transform.position;
        Vector3 targetPos = new Vector3();
        switch (GetComponent<Hero>().job)
        {
            case PlayerJob.Dealer:
                targetPos = new Vector3(-4.2f, -2f, -20);
                break;
            case PlayerJob.Healer:
                targetPos = new Vector3(-4.2f, 2f, -20);
                break;
            case PlayerJob.Tanker:
                targetPos = new Vector3(-3f, 0f, -20);
                break;
        }

        while (true)
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, 3, 0.1f);
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPos, 0.1f);


            print("되고있는거냐");
            if (mainCamera.orthographicSize <= 3.001f && mainCamera.transform.position.x < targetPos.x * 0.99f)
                break;
            yield return null;
        }

        Animator animator = GetComponent<Hero>().animator;
        sfx_Audio.PlayOneShot(GameInstance.Instance.soundPrefab["SkillHero"],3);

        //애니메이션 플레이
        animator.SetTrigger("Skill");
        yield return new WaitForSeconds(1f);

        

        while (true)
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, 5, 0.1f);
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, originPos, 0.1f);


            print("되고있는거냐2");
            if (mainCamera.orthographicSize >= 4.999f && mainCamera.transform.position.x > -0.0001f)
                break;
            yield return new WaitForEndOfFrame();
        }
        UI_Camera.gameObject.SetActive(true);
        mainCamera.GetComponent<CamShake>().enabled = true;

        transform.GetComponent<Hero>().Skill();
        transform.GetComponent<Hero>().targetHero = hero;
        hero.bSelected = false;
        foreach (Enemy enemy1 in BattleManager.I.enemyCollection.collection)
        {
            enemy1.unit.arrow.GetComponent<Renderer>().material.color = Color.yellow;
            enemy1.unit.arrow.SetActive(false);
        }
    }
}
