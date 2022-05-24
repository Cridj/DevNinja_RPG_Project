using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SKILL_TYPE
{
    NONE,
    MEELE_ATTACK_FROM_NOW = 1,
    DOUBLE_ATTACK,
    MONEY_ATTACK,
    CHARGE_ATTACK,
    POISON_ATTACK,
    STRONG_ATTACK,
    DENTINGBLOWS,
    TROLLING100PER,
    AOE_From_Now = 50,
    ATTACK_BUFF,
    Healing,
    BuffAndRangedSkill_From_Now = 70,
    BEAST_STRIKE,
    DARK_ARMOR,
    HERD_HUNTING,
    REDUCES_Damage_Taken,
    BEAST_HOWLING,    
    DARK_ARROW,
    DARKNESS,
    CHAIN_LASHING,
    DRAIN,
    DARKNESS_RITUAL,
    SHOUTING,
    FIREBALL,
    ICESTORM,
    THROWING_TREE,
    TROLLSHOUTING,
    ROLLING_SNOWBALL,
    ICEHELL,
    AVALANCHE,
    QUAKE,
}



public enum PassiveSKill
{
    //패시브없음
    NONE,

    //시작할때 발동되는 패시브
    REVIVE_INFECTED_RAT,
    BUFF_PHYSICAL_ATTACK_POWER,
    Mastered_BlackMagic,
    SnowMountainSpirit,

    //턴마다 발동되는 패시브
    TurnPassive,
    UndeadForce,
    BrainOfReach,
    Regenerative,
    IceHeart,

    //상시 발동되는 패시브
    TickPassive,
    ZealotMode,
    SimpleMagic,
    Stiffness,
}


public class EnemySkill : MonoBehaviour
{
    /// <summary>
    /// 파티클들
    /// </summary>
    public GameObject magicExplosionParticle;

    public GameObject magicSilenceParticle;

    public GameObject bowArrow;

    public GameObject slashParticle;

    public Transform rangedOffset;

    public GameObject throwingBullet;

    public Transform Edge;

    [Header("파티클들")]
    [Space(10f)]
    
    /// <summary>
    /// ?????? ???? ????
    /// </summary>
    public GameObject AttackBuffParticle;

    /// <summary>
    /// ?????? ???? ????
    /// </summary>
    public GameObject DefenseBuffParticle;

    /// <summary>
    /// ???????? ???? ????
    /// </summary>
    public GameObject MultipleBuffParticle;

    public GameObject MagicainBasicAttackMissile;

    public delegate void Action<in T>(T obj);

    public AudioSource sfx_AS;

    /// <summary>
    /// ????????
    /// </summary>
    public GameObject HealingOnce;

    public GameObject fireballParticle;

    public GameObject iceballCharge;
    public GameObject iceballBullet;
    public GameObject iceballExplosion;

    /// <summary>
    /// ?????? ?? hp ??
    /// </summary>
    float drainHP;

    public IEnumerator PlayParticle(GameObject particle, float duration, float delaysec, Vector3 pos)
    {
        yield return new WaitForSeconds(delaysec);
        particle.transform.position = pos;
        particle.SetActive(true);
        yield return new WaitForSeconds(duration);
        particle.SetActive(false);
    }


    private void Awake()
    {
    }

    public IEnumerator DeActiveDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
    }

    //-----------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------
    //--------------------------------??????????-----------------------------------------------
    //-----------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------


    /// <summary>
    /// ????????
    /// </summary>
    /// <param name="damagedUnit"></param>
    /// <param name="animator"></param>
    /// <param name="unit"></param>
    /// <returns></returns>
    public IEnumerator DoubleAttack(Unit damagedUnit, 
                                    Animator animator, 
                                    Unit unit)
    {
        //???? ?????? ??????
        BattleManager.I.skillText.gameObject.SetActive(true);
        BattleManager.I.skillText.text = "????????";

        animator.SetTrigger("Attack");               
        damagedUnit.DecreaseHP(unit.PhysicalAttack * 0.6f, unit);
        Camera.main.GetComponent<CamShake>().ShakeTime = 0.1f;
        yield return new WaitForSeconds(0.8f);

        animator.SetTrigger("Attack");
        damagedUnit.DecreaseHP(unit.PhysicalAttack * 0.6f, unit);
        Camera.main.GetComponent<CamShake>().ShakeTime = 0.1f;

        yield return new WaitForSeconds(1.8f);

        BattleManager.I.skillText.gameObject.SetActive(false);

        //???? ?????? ????????
        BattleManager.I.skillText.gameObject.SetActive(false);

        StartCoroutine(unit.GetComponent<Enemy>().AttackEndCoroutine());
    }

    /// <summary>
    /// ????????
    /// </summary>
    /// <param name="damagedUnit"></param>
    /// <param name="animator"></param>
    /// <param name="unit"></param>
    /// <returns></returns>
    public IEnumerator ChargeAttack(Unit damagedUnit, 
                                    Animator animator, 
                                    Unit unit)
    {
        //???? ?????? ??????
        BattleManager.I.skillText.gameObject.SetActive(true);
        BattleManager.I.skillText.text = "????????";
        animator.SetTrigger("ChargeAttack");
        damagedUnit.DecreaseHP(unit.PhysicalAttack, unit);



        ////hp?? 0 ????(????)??????
        //if (damagedUnit.Hp <= 0)
        //{
        //    yield return new WaitForSeconds(1f);
        //    damagedUnit.GetComponent<Hero>().OnDie();
        //}
        yield return new WaitForSeconds(1f);

        //bAttackEnd = true;
        //???? ?????? ????????
        BattleManager.I.skillText.gameObject.SetActive(false);
        StartCoroutine(unit.GetComponent<Enemy>().AttackEndCoroutine());
    }
    
    /// <summary>
    /// ?????????? ????
    /// </summary>
    /// <param name="damagedUnit"></param >
    /// <param name="animator"></param>
    /// <param name="unit"></param>
    /// <returns></returns>
    public IEnumerator ReducesDamageTakenForTwoTurn(Unit damagedUnit, 
                                          Animator animator, 
                                          Unit unit)
    {
        //???? ?????? ??????
        BattleManager.I.skillText.gameObject.SetActive(true);
        BattleManager.I.skillText.text = "????????";

        DefenseBuffParticle.SetActive(true);
        unit.defenseBuffDuration = 2;
        yield return new WaitForSeconds(1f);


        //???? ?????? ????????
        unit.MonsterStatIconUpdate();

        //???? ?????? ????????
        BattleManager.I.skillText.gameObject.SetActive(false);
        print("??");

        StartCoroutine(unit.GetComponent<Enemy>().AttackEndCoroutine());

        while (unit.defenseBuffDuration > 0)
        {
            yield return null;
        }
        DefenseBuffParticle.SetActive(false);

    }

    public IEnumerator PoisonAttack(Unit damagedUnit,
                                        Animator animator,
                                        Unit unit)
    {
        //???? ?????? ??????
        BattleManager.I.skillText.gameObject.SetActive(true);
        BattleManager.I.skillText.text = "????";

        damagedUnit.PoisonDuration = 2;
        animator.SetTrigger("Attack");
        damagedUnit.DecreaseHP(unit.PhysicalAttack, unit);
        Camera.main.GetComponent<CamShake>().ShakeTime = 0.1f;
        damagedUnit.HeroStatIconUpdate();


        ////hp?? 0 ????(????)??????
        //if (damagedUnit.Hp <= 0)
        //{
        //    yield return new WaitForSeconds(0.5f);
        //    damagedUnit.GetComponent<Hero>().OnDie();
        //    yield return new WaitForSeconds(0.5f);
        //    animator.SetInteger("State", 2);
        //    unit.GetComponent<Enemy>().bAttackEnd = true;
        //}
        //else
        //{
            yield return new WaitForSeconds(1f);
        StartCoroutine(unit.GetComponent<Enemy>().AttackEndCoroutine());
        //}
        //???? ?????? ????????
        BattleManager.I.skillText.gameObject.SetActive(false);
    }

    /// <summary>
    /// ???????? : ???? ???? ???? ?? ?????? ?????? 30???? ????
    /// </summary>
    public IEnumerator HerdHunting(Animator animator,
                                   Unit unit) 
    {
        //???? ?????? ??????
        BattleManager.I.skillText.gameObject.SetActive(true);
        BattleManager.I.skillText.text = "????????";

        foreach (Enemy enemy in BattleManager.I.enemyCollection.collection)
        {
            enemy.EnableHerdHunting();
            enemy.AttackBuffParticle.SetActive(true);
            //???? ?????? ????????
            enemy.unit.MonsterStatIconUpdate();
        }

        yield return new WaitForSeconds(1f);

        StartCoroutine(unit.GetComponent<Enemy>().AttackEndCoroutine());
        //???? ?????? ????????
        BattleManager.I.skillText.gameObject.SetActive(false);
    }

    /// <summary>
    /// ???????????? ???? ??????????
    /// </summary>
    /// <param name="damagedUnit"></param>
    /// <param name="animator"></param>
    /// <param name="unit"></param>
    /// <returns></returns>
    public IEnumerator BeastStrike(Animator animator,
                                    Unit unit)
    {
        //???? ?????? ??????
        BattleManager.I.skillText.gameObject.SetActive(true);
        BattleManager.I.skillText.text = "?????? ????";

        int i = 0;
        animator.SetTrigger("Attack");
        foreach(Hero hero in BattleManager.I.heroCollection.collection)
        {
            if(hero.bDie == true)
            {
                continue;
            }
            hero.unit.DecreaseHP(unit.PhysicalAttack, unit);
            StartCoroutine(PlayParticle((slashParticle),0.5f,0f,hero.transform.position));
            i++;
            ////hp?? 0 ????(????)??????
            //if (hero.unit.Hp <= 0)
            //{
            //    hero.unit.GetComponent<Hero>().OnDie();
            //}
        }
        Camera.main.GetComponent<CamShake>().ShakeTime = 0.3f;
        yield return new WaitForSeconds(1f);

        StartCoroutine(unit.GetComponent<Enemy>().AttackEndCoroutine());

        //???? ?????? ????????
        BattleManager.I.skillText.gameObject.SetActive(false);
    }

    /// <summary>
    /// 2???? ???? ???? ?????? ??????, ?????? 10???? ????
    /// ???? ???????? ??????, ?????? 10???? ????
    /// </summary>
    /// <param name="damagedUnit"></param>
    /// <param name="animator"></param>
    /// <param name="unit"></param>
    /// <returns></returns>
    public IEnumerator BeastHowling(Animator animator,
                                    Unit unit)
    {
        //???? ?????? ??????
        BattleManager.I.skillText.gameObject.SetActive(true);
        BattleManager.I.skillText.text = "?????? ????????";

        foreach (Enemy enemy in BattleManager.I.enemyCollection.collection)
        {
            enemy.unit.howlingBuffDebuffDuration = 2;
            enemy.MultipleBuffParticle.SetActive(true);
            StartCoroutine(enemy.unit.HowlingBuffDeBuff());
            //???? ?????? ????????
            enemy.unit.MonsterStatIconUpdate();
        }
        foreach(Hero hero in BattleManager.I.heroCollection.collection)
        {
            hero.unit.howlingBuffDebuffDuration = 2;
            StartCoroutine(hero.unit.HowlingBuffDeBuff());
            //???? ?????? ????????
            hero.unit.HeroStatIconUpdate();
        }
        yield return new WaitForSeconds(1f);
        foreach (Enemy enemy in BattleManager.I.enemyCollection.collection)
        {
            enemy.MultipleBuffParticle.SetActive(false);

        }

        //foreach (Hero hero in BattleManager.I.heroCollection.collection)
        //{
        //}
        yield return new WaitForSeconds(.2f);
        StartCoroutine(unit.GetComponent<Enemy>().AttackEndCoroutine());
        //???? ?????? ????????
        BattleManager.I.skillText.gameObject.SetActive(false);
    }

    public IEnumerator DarkArmor(Animator animator,
                                 Unit unit)
    {
        //???? ?????? ??????
        BattleManager.I.skillText.gameObject.SetActive(true);
        BattleManager.I.skillText.text = "??????";

        unit.darkArmorDuration = 3;
        StartCoroutine(unit.DarkArmor());
        StartCoroutine(PlayParticle(MultipleBuffParticle, 1.5f, 0.0f, transform.position));
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(unit.GetComponent<Enemy>().AttackEndCoroutine());
        //???? ?????? ????????
        BattleManager.I.skillText.gameObject.SetActive(false);
    }

    public IEnumerator DarkArrow(Unit damagedUnit,
                                 Animator animator,
                                 Unit unit)
    {      
        //???? ?????? ??????
        BattleManager.I.skillText.gameObject.SetActive(true);
        BattleManager.I.skillText.text = "???? ????";

        animator.SetTrigger("ChargeShot");
        yield return new WaitForSeconds(0.95f);

        ArcherArrow archer = bowArrow.GetComponent<ArcherArrow>();
        bowArrow.SetActive(true);
        archer.bShot = true;
        archer.target = damagedUnit.gameObject;
        yield return new WaitForSeconds(0.35f);
        damagedUnit.DecreaseHP(unit.currentPhysicalAttack, unit);

        //?? ???? ????
        damagedUnit.darkDuration = 2;

        StartCoroutine(unit.GetComponent<Enemy>().AttackEndCoroutine());
        //???? ?????? ????????
        BattleManager.I.skillText.gameObject.SetActive(false);
    }

    public IEnumerator Darkness(Animator animator,
                                 Unit unit)
    {
        //???? ?????? ??????
        BattleManager.I.skillText.gameObject.SetActive(true);
        BattleManager.I.skillText.text = "????????";

        animator.SetTrigger("Cast");
        yield return new WaitForSeconds(0.3f);

        MagicainBasicAttackMissile.SetActive(true);
        MagicainBasicAttackMissile.transform.localPosition = Vector3.zero;
        MagicainBasicAttackMissile.transform.DOMove(rangedOffset.position, 0.8f);
        StartCoroutine(PlayParticle(magicExplosionParticle, 1f, 0.8f, rangedOffset.position + Vector3.left));
        StartCoroutine(DeActiveDelay(MagicainBasicAttackMissile, 0.8f));
        //StartCoroutine(PlayParticle())

        //?????? ????


        yield return new WaitForSeconds(0.8f);

        foreach (var hero in BattleManager.I.heroCollection.collection)
        {
            //?? ????????
            hero.unit.darkDuration = 2;
            hero.unit.DecreaseHP(unit.currentPhysicalAttack * 1.2f, unit);
        }
        
        StartCoroutine(unit.GetComponent<Enemy>().AttackEndCoroutine());
        //???? ?????? ????????
        BattleManager.I.skillText.gameObject.SetActive(false);
    }

    public IEnumerator ChainLashing(Unit damagedUnit,
                                    Animator animator,
                                    Unit unit)
    {
        //???? ?????? ??????
        BattleManager.I.skillText.gameObject.SetActive(true);
        BattleManager.I.skillText.text = "????????";

        animator.SetTrigger("Cast");
        yield return new WaitForSeconds(0.3f);

        MagicainBasicAttackMissile.SetActive(true);
        MagicainBasicAttackMissile.transform.localPosition = Vector3.zero;
        MagicainBasicAttackMissile.transform.DOMove(damagedUnit.transform.position, 0.8f);


        StartCoroutine(PlayParticle(magicSilenceParticle, 0.5f, 0.8f, damagedUnit.transform.position));
        StartCoroutine(DeActiveDelay(MagicainBasicAttackMissile, 0.8f));


        yield return new WaitForSeconds(0.8f);

        damagedUnit.DecreaseHP(unit.currentPhysicalAttack * 1.5f, unit);
        damagedUnit.chainLashingDuration = 2;
        damagedUnit.HeroStatIconUpdate();

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(unit.GetComponent<Enemy>().AttackEndCoroutine());

        //???? ?????? ????????
        BattleManager.I.skillText.gameObject.SetActive(false);
    }

    public IEnumerator StrongAttack(Unit damagedUnit,
                                    Animator animator,
                                    Unit unit)
    {
        //???? ?????? ??????
        BattleManager.I.skillText.gameObject.SetActive(true);
        BattleManager.I.skillText.text = "?????? ????";

        animator.SetTrigger("Slash");
        yield return new WaitForSeconds(0.5f);
        damagedUnit.DecreaseHP(unit.PhysicalAttack * 1.5f, unit);
        Camera.main.GetComponent<CamShake>().ShakeTime = 0.15f;

        //??
        Enemy enemy = GetComponent<Enemy>();
        yield return new WaitForSeconds(0.5f);
        enemy.OnHeal(enemy.unit.currentPhysicalAttack * (1f - (enemy.unit.currentDefense / (enemy.unit.currentDefense + 100))));
        yield return new WaitForSeconds(0.6f);

        StartCoroutine(unit.GetComponent<Enemy>().AttackEndCoroutine());


        //???? ?????? ????????
        BattleManager.I.skillText.gameObject.SetActive(false);
    }



    public IEnumerator Drain(Animator animator,
                             Unit unit)
    {
        //???? ?????? ??????
        BattleManager.I.skillText.gameObject.SetActive(true);
        BattleManager.I.skillText.text = "????";

        drainHP = 0f;
        animator.SetTrigger("ChargeAttack2H");
        yield return new WaitForSeconds(1f);


        foreach(var hero in BattleManager.I.heroCollection.collection)
        {
            if (hero.bDie)
                continue;
            //?????? ?????????? 10% ????
            hero.unit.DecreaseHP(hero.unit.maxHP * 0.1f, unit);
            drainHP += hero.unit.maxHP * 0.1f;
            var particle = Instantiate(GameInstance.Instance.particlePrefab["BLOOD"]) as GameObject;
            particle.SetActive(false);
            StartCoroutine(PlayParticle(particle, 0.5f, 0.0f, hero.transform.position + Vector3.up));
            Destroy(particle, 2f);
        }
        Camera.main.GetComponent<CamShake>().ShakeTime = 0.15f;

        //??
        Enemy enemy = GetComponent<Enemy>();
        yield return new WaitForSeconds(0.5f);
        enemy.OnHeal(drainHP);
        yield return new WaitForSeconds(0.6f);

        StartCoroutine(unit.GetComponent<Enemy>().AttackEndCoroutine());


        //???? ?????? ????????
        BattleManager.I.skillText.gameObject.SetActive(false);
    }

    GameObject[] swords = new GameObject[5];
    public IEnumerator DarknessRitual(Animator animator,
                                      Unit unit)
    {
        //???? ?????? ??????
        BattleManager.I.skillText.gameObject.SetActive(true);
        BattleManager.I.skillText.text = "?????? ????";

        animator.SetTrigger("ChargeAttack2H");
        yield return new WaitForSeconds(1f);

        for(int i = 0; i < 5; i++)
        {
            swords[i] = GameObject.Instantiate(GameInstance.Instance.particlePrefab["SwordHallow"]);
            swords[i].transform.position = transform.position;
            swords[i].transform.DOMove(BattleScene.I.swordOffsets[i].position,0.3f);
            yield return new WaitForSeconds(0.3f);
        }

        yield return new WaitForSeconds(0.2f);

        foreach (GameObject sword in swords)
        {
            int ran = UnityEngine.Random.Range(0, 3);

            //???????????? ?????????? ????????
            if (BattleManager.I.heroCollection.collection[ran].bDie)
            {
                if (!BattleManager.I.heroCollection.collection[0].bDie)
                {
                    ran = 0;
                }
                else if (!BattleManager.I.heroCollection.collection[1].bDie)
                {
                    ran = 1;
                }
                else
                {
                    ran = 2;
                }
            }

            //?? ????
            sword.transform.DOMove(BattleManager.I.heroCollection.collection[ran].transform.position + Vector3.up * 1f, 0.5f);
            StartCoroutine(DarknessRitualAssi
                (unit.currentPhysicalAttack * 0.7f, 
                BattleManager.I.heroCollection.collection[ran].unit, 
                unit, 
                sword));

            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(1f);
        StartCoroutine(unit.GetComponent<Enemy>().AttackEndCoroutine());


        //???? ?????? ????????
        BattleManager.I.skillText.gameObject.SetActive(false);
    }

    IEnumerator DarknessRitualAssi(float Damage,Unit DamagedUnit,Unit Instigator, GameObject sword)
    {
        yield return new WaitForSeconds(0.5f);
        Camera.main.GetComponent<CamShake>().ShakeTime = 0.1f;
        DamagedUnit.DecreaseHP(Damage, Instigator);
        Destroy(sword);
    }

    //SHOUTING,
    //FIREBALL,
    //ICESTORM,
    //THROWING_TREE,
    //ROLLING_STONE,
    //ROLLING_SNOWBALL,
    //ICEHELL,
    //AVALANCHE,
    //DENTINGBLOWS,
    //IGNORANT_RUSH,
    //TROLLING100PER,

    
    //??????????
    public IEnumerator Shouting(Animator animator,
                                Unit unit)
    {        
        //???? ????????
        BattleManager.I.skillText.gameObject.SetActive(true);
        BattleManager.I.skillText.text = "??????????";



        animator.SetTrigger("Shouting");
        yield return new WaitForSeconds(1.5f);
        foreach (var enemy in BattleManager.I.enemyCollection.collection)
        {
            StartCoroutine(enemy.PlayParticle(enemy.MultipleBuffParticle, 1f, 0.0f, enemy.transform.position));
            enemy.unit.shoutingDuration = 2;
            StartCoroutine(enemy.unit.ShoutingBuff());
        }
        yield return new WaitForSeconds(1f);


        //???? ????????
        StartCoroutine(unit.GetComponent<Enemy>().AttackEndCoroutine());
        //???? ?????? ????????
        BattleManager.I.skillText.gameObject.SetActive(false);
    }

    //???????????? (????????)
    public IEnumerator DentingBlows(Unit damagedUnit,
                                    Animator animator,
                                    Unit unit)
    {
        //???? ????????
        BattleManager.I.skillText.gameObject.SetActive(true);
        BattleManager.I.skillText.text = "????????????";



        animator.SetTrigger("JumpAttack");
        yield return new WaitForSeconds(0.8f);
        damagedUnit.DecreaseHP(unit.currentPhysicalAttack * 2f, unit);

        //????????
        var particle = Instantiate(GameInstance.Instance.particlePrefab["BLOOD"]) as GameObject;
        particle.SetActive(false);
        StartCoroutine(PlayParticle(particle, 0.5f, 0.0f, damagedUnit.transform.position + Vector3.up));
        Destroy(particle, 1f);



        yield return new WaitForSeconds(0.5f);


        //???? ????????
        StartCoroutine(unit.GetComponent<Enemy>().AttackEndCoroutine());
        //???? ?????? ????????
        BattleManager.I.skillText.gameObject.SetActive(false);
    }

    //????????
    public IEnumerator Fireball(Unit damagedUnit,
                                Animator animator,
                                Unit unit)
    {
        //???? ????????
        BattleManager.I.skillText.gameObject.SetActive(true);
        BattleManager.I.skillText.text = "파이어볼!";

        animator.SetTrigger("OgreCast");
        yield return new WaitForSeconds(0.8f);

        GameObject chargeParticle = Instantiate(GameInstance.Instance.particlePrefab["FireballCharge"]);
        chargeParticle.transform.position = Edge.position;
        Destroy(chargeParticle, 2f);
        //StartCoroutine(DeActiveDelay(chargeParticle, 1f));


        yield return new WaitForSeconds(2f);



        GameObject projectileParticle = Instantiate(GameInstance.Instance.particlePrefab["FireballProjectile"]);
        projectileParticle.transform.position = Edge.position;
        projectileParticle.transform.DOMove(damagedUnit.transform.position, 0.8f);

        Destroy(projectileParticle, 0.8f);
        //LookAt2D((damagedUnit.transform.position-bowArrow.transform.position).normalized);

        //StartCoroutine(DeActiveDelay(projectileParticle, 0.8f));
        yield return new WaitForSeconds(0.8f);

        //?????????? ????
        GameObject explosion = 
            GameObject.Instantiate(GameInstance.Instance.particlePrefab["FireballExplosion"],
            damagedUnit.transform.position + Vector3.up,Quaternion.identity);
        explosion.transform.localScale = Vector3.one * 2.5f;
        Destroy(explosion, 0.5f);

        damagedUnit.DecreaseHP(unit.currentPhysicalAttack * 2f, unit);


        //???? ????????
        StartCoroutine(unit.GetComponent<Enemy>().AttackEndCoroutine());
        //???? ?????? ????????
        BattleManager.I.skillText.gameObject.SetActive(false);
    }

    public IEnumerator IceStorm(Unit damagedUnit,
                                Animator animator,
                                Unit unit)
    {
        //???? ????????
        BattleManager.I.skillText.gameObject.SetActive(true);
        BattleManager.I.skillText.text = "아이스 스s";

        animator.SetTrigger("OgreCast");
        yield return new WaitForSeconds(0.8f);

        iceballCharge.SetActive(true);
        StartCoroutine(DeActiveDelay(iceballCharge, 1f));
        yield return new WaitForSeconds(1f);
        iceballBullet.SetActive(true);
        iceballBullet.transform.localPosition = Vector3.zero;
        iceballBullet.transform.DOMove(damagedUnit.transform.position, 0.8f);
        //LookAt2D((damagedUnit.transform.position-bowArrow.transform.position).normalized);
        StartCoroutine(DeActiveDelay(iceballBullet, 0.8f));

        yield return new WaitForSeconds(0.8f);

        //?????????? ????
        GameObject explosion =
            GameObject.Instantiate(GameInstance.Instance.particlePrefab["IceballExplosion"],
            damagedUnit.transform.position + Vector3.up, Quaternion.identity);
        explosion.transform.localScale = Vector3.one * 2.5f;
        Destroy(explosion, 0.5f);

        damagedUnit.DecreaseHP(unit.currentPhysicalAttack * 2f, unit);


        //???? ????????
        StartCoroutine(unit.GetComponent<Enemy>().AttackEndCoroutine());
        //???? ?????? ????????
        BattleManager.I.skillText.gameObject.SetActive(false);
    }



    public IEnumerator Quake(Animator animator,
                             Unit unit)
    {
        BattleManager.I.skillText.gameObject.SetActive(true);
        BattleManager.I.skillText.text = "지진일으키기";

        animator.SetTrigger("Quake");
        yield return new WaitForSeconds(2.4f);

        StartCoroutine(unit.GetComponent<Enemy>().AttackEndCoroutine());

        BattleManager.I.skillText.gameObject.SetActive(false);
    }

    public void QuakeAttack()
    {
        foreach(var hero in BattleManager.I.heroCollection.collection)
        {
            hero.unit.DecreaseHP(GetComponent<Enemy>().unit.currentPhysicalAttack * 0.5f, GetComponent<Enemy>().unit);
            Camera.main.GetComponent<CamShake>().ShakeTime = 0.2f;
        }
    }


    public IEnumerator ThrowingTree(Unit damagedUnit,
                                    Animator animator,
                                    Unit unit)
    {
        BattleManager.I.skillText.gameObject.SetActive(true);
        BattleManager.I.skillText.text = "나무 던지기";

        animator.SetTrigger("OgreThrowing");
        yield return new WaitForSeconds(1.3f);

        throwingBullet.SetActive(true);
        throwingBullet.transform.localPosition = rangedOffset.localPosition + Vector3.up * 3f;
        throwingBullet.transform.DOMove(damagedUnit.transform.position + Vector3.up, 0.8f);
        StartCoroutine(DeActiveDelay(throwingBullet, 0.8f));

        yield return new WaitForSeconds(0.8f);

        GameObject explosion =
            GameObject.Instantiate(GameInstance.Instance.particlePrefab["IceballExplosion"],
            damagedUnit.transform.position + Vector3.up, Quaternion.identity);
        explosion.transform.localScale = Vector3.one * 2.5f;
        Destroy(explosion, 0.5f);

        damagedUnit.DecreaseHP(unit.currentPhysicalAttack * 2f, unit);


        StartCoroutine(unit.GetComponent<Enemy>().AttackEndCoroutine());

        BattleManager.I.skillText.gameObject.SetActive(false);
    }

    public IEnumerator TrollShouting(Animator animator,
                                     Unit unit)
    {
        //트롤의 울부짖음
        BattleManager.I.skillText.gameObject.SetActive(true);
        BattleManager.I.skillText.text = "트롤의 울부짖음";

        yield return new WaitForSeconds(0.6f);
        animator.SetTrigger("Shout");
        yield return new WaitForSeconds(0.3f);

        for(int i = 0; i< 4; i++)
        {
            var shoke = Instantiate(GameInstance.Instance.particlePrefab["ShokeWave"], 
                      transform.position + Vector3.up, Quaternion.identity);
            Destroy(shoke, 0.5f);
            sfx_AS.PlayOneShot(GameInstance.Instance.soundPrefab["Punch1"]);
            foreach (var hero in BattleManager.I.heroCollection.collection)
            {
                hero.unit.DecreaseHP(0.3f, unit);
                var hit = Instantiate(GameInstance.Instance.particlePrefab["Hit_1"],hero.transform.position + Vector3.up,Quaternion.identity);
                Destroy(hit, 0.5f);
            }
            yield return new WaitForSeconds(0.15f);
        }

        foreach (Enemy enemy in BattleManager.I.enemyCollection.collection)
        {
            enemy.unit.trollShoutingDuration = 2;
            enemy.MultipleBuffParticle.SetActive(true);
            StartCoroutine(enemy.unit.TrollShoutingBuff());
            enemy.unit.MonsterStatIconUpdate();
        }
        yield return new WaitForSeconds(1f);
        foreach (Enemy enemy in BattleManager.I.enemyCollection.collection)
        {
            enemy.MultipleBuffParticle.SetActive(false);

        }


        yield return new WaitForSeconds(1f);

        StartCoroutine(unit.GetComponent<Enemy>().AttackEndCoroutine());

        BattleManager.I.skillText.gameObject.SetActive(false);
    }

    public IEnumerator RollingSnowball()
    {
        yield return new WaitForSeconds(0.9f);
    }

    public IEnumerator IceHell()
    {
        yield return new WaitForSeconds(0.9f);
    }

    public IEnumerator Avalanche()
    {
        yield return new WaitForSeconds(0.9f);
    }



    public IEnumerator Trolling100Per()
    {
        yield return new WaitForSeconds(0.9f);
    }

    //-----------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------
    //--------------------------------??????????-----------------------------------------------
    //-----------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------


    //SnowMountainSpirit,
    //    Regenerative,
    //IceHeart,
    //    ZealotMode,
    //SimpleMagic,
    //Stiffness,

    public IEnumerator SnowMountainSpirit()
    {
        yield return new WaitForSeconds(0.9f);
    }

    public IEnumerator Regenerative()
    {
        yield return new WaitForSeconds(0.9f);
    }

    public IEnumerator IceHeart()
    {
        yield return new WaitForSeconds(0.9f);
    }

    //HP?? 1% ???????? ???????? 1% ???? HP?? ???????????? ????
    /// <summary>
    /// 
    /// </summary>
    /// <param name="unit"></param>
    /// <param name="hpChangeValue"></param>
    /// <param name="hpChangeCheck"> true?? hp????, false?? hp ????</param>
    public void ZealotMode(Unit unit, float hpChangeValue, bool hpChangeCheck)
    {
        if(hpChangeCheck)
        {
            unit.currentPhysicalAttack += unit.PhysicalAttack * (hpChangeValue / unit.maxHP);
        }
        else
        {
            unit.currentPhysicalAttack -= unit.PhysicalAttack * (hpChangeValue / unit.maxHP);
        }

    }

    //?????? ????
    public IEnumerator SimpleMagic(Unit unit)
    {
        StartCoroutine(PlayParticle(DefenseBuffParticle, 1f, 0f, transform.position));
        yield return new WaitForSeconds(1f);
        unit.currentMagicalAttack += unit.MagicalAttack * 0.1f;
    }

    public IEnumerator Stiffness()
    {
        yield return new WaitForSeconds(0.9f);
    }


    public IEnumerator Passive_AOE_AttackIncrease(Animator animator,
                                                  Unit unit)
    {
        print("?????? ??????");
        foreach (Enemy enemy in BattleManager.I.enemyCollection.collection)
        {
            enemy.unit.currentPhysicalAttack *= 1.2f;
            enemy.AttackBuffParticle.SetActive(true);
            enemy.unit.bAttackIncrease_Passive_Bear = true;
            //???? ?????? ????????
            enemy.unit.MonsterStatIconUpdate();
        }

        yield return new WaitForSeconds(1.5f);

        foreach (Enemy enemy in BattleManager.I.enemyCollection.collection)
        {
            enemy.AttackBuffParticle.SetActive(false);
        }
    }

    public IEnumerator Passive_Mastered_BlackMagic(Animator animator,
                                              Unit unit)
    {
        print("?????? ???????????? ??????");
        foreach (Enemy enemy in BattleManager.I.enemyCollection.collection)
        {
            enemy.unit.currentMagicalAttack *= 1.3f;
            StartCoroutine(PlayParticle(enemy.AttackBuffParticle, 1.5f, 0.0f, enemy.transform.position));
            enemy.unit.bMasteredBlackMagic_Passive = true;

            //???? ?????? ????????
            enemy.unit.MonsterStatIconUpdate();
        }
        yield return new WaitForSeconds(.5f);
    }

    public IEnumerator Passive_InfectRat() 
    {
        yield return new WaitForSeconds(0.5f);
        foreach(var enemy in BattleManager.I.enemyCollection.collection)
        {
            ///???????? ????
            if (enemy.passiveSKill != PassiveSKill.NONE)
                continue;
            enemy.bInfected = true;
        }
        print("?????????? ????");
    }

    public IEnumerator UndeadForce()
    {
        var enemy = GetComponent<Enemy>();
        StartCoroutine(PlayParticle(AttackBuffParticle, 2f, 0.0f, transform.position));
        yield return new WaitForSeconds(2f);
        //?????? 10%????
        enemy.unit.currentPhysicalAttack += enemy.unit.PhysicalAttack * 0.1f;
        yield return new WaitForSeconds(0.5f);
        BattleManager.I.passiveCount--;
        print("???????? ????");
    }


    public IEnumerator BrainOfReach(Enemy enemy)
    {
        enemy.bRevive = true;
        print("??????");
        StartCoroutine(PlayParticle(MultipleBuffParticle, 2f, 0.0f, transform.position));
        //???? 30?? ????
        foreach (var enemy1 in BattleManager.I.enemyCollection.collection)
        {
            enemy1.unit.currentPhysicalAttack += enemy1.unit.PhysicalAttack * 0.3f;
            enemy1.unit.currentMagicalAttack += enemy1.unit.MagicalAttack * 0.3f;
            enemy1.unit.currentDefense += enemy1.unit.Defense * 0.3f;
        }
        yield return new WaitForSeconds(2f);
    }

}
