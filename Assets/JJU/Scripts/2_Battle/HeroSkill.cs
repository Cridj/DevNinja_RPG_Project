using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;


public enum PlayerJob { Healer, Tanker, Ranger, Dealer }

public enum SKILLSET
{
    NONE, SLASH, TRANCE_SWORD, SLASHBURST,
    RANGED_AND_TARGET_IS_HERO = 20, RAISE, MULTIPLE_HEAL, HEAL,
    RANGED_AND_TARGET_IS_ENEMY = 40, ROAR, TAUNT, SCOURGE, BUSTERCALL,

}


public enum PASSIVE_SKILLSET {CALM ,SACRIFICE, MUNCHKIN}

public class HeroSkill : MonoBehaviour
{
    [Header("스킬셋")]
    [Space(10f)]
    public SKILLSET skill_set1 = new SKILLSET();
    public SKILLSET skill_set2 = new SKILLSET();
    public SKILLSET skill_set3 = new SKILLSET();


    public SKILLSET currentSKill = new SKILLSET();

    public Transform HealingOffset;

    public AudioSource sfx_Audio;

    [Header("파티클들")]
    [Space(10f)]
    public GameObject swordSlashMini;

    public GameObject sowrdSladhMidium;

    public GameObject sowrdWave;

    public GameObject[] trance;

    public GameObject HealingBig;

    public GameObject WeaponTrail1;

    public GameObject WeaponTrail2;

    public Transform edge;

    public GameObject ultPanelAnimator;

    /// <summary>
    /// ???? ?? ??????
    /// </summary>
    public GameObject HealingOnce;

    [Header("???? ???? ????")]
    [Space(10f)]
    public int TranceAttackCount;

    public PlayerJob job = new PlayerJob();

    public void InitHealPos()
    {
        HealingOffset = GameObject.Find("HealOffset").transform;
    }


    public IEnumerator PlayParticle(GameObject particle, float duration, float delaysec)
    {
        yield return new WaitForSeconds(delaysec);
        particle.SetActive(true);
        yield return new WaitForSeconds(duration);
        particle.SetActive(false);
    }


    //-----------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------
    //--------------------------------??????????-----------------------------------------------
    //-----------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------

    /// <summary>
    /// ???????? ????
    /// </summary>
    /// <param name="damagedUnit"></param>
    /// <param name="animator"></param>
    /// <param name="unit"></param>
    /// <returns></returns>
    public IEnumerator Trance_Sword(Unit damagedUnit,
                                    Animator animator,
                                    Unit unit)
    {
        //???? ?????? ??????
        BattleManager.I.skillText.gameObject.SetActive(true);
        BattleManager.I.skillText.text = "?????????? ??";

        animator.SetTrigger("TranceSword");

        while (TranceAttackCount < 1)
        {
            yield return null;
        }        
        damagedUnit.DecreaseHP(unit.currentPhysicalAttack * 0.5f, unit);
        sfx_Audio.PlayOneShot(GameInstance.Instance.soundPrefab["Hit"]);
        StartCoroutine(PlayParticle(trance[0], 0.6f, 0f));

        GameObject hitParticle1 = Instantiate(GameInstance.Instance.particlePrefab["WindHit"], damagedUnit.transform.position + Vector3.up * 1.5f, Quaternion.identity);
        hitParticle1.AddComponent<SortingGroup>().sortingOrder = 500;
        Destroy(hitParticle1, 0.5f);
        Camera.main.GetComponent<CamShake>().ShakeTime = 0.15f;
        while (TranceAttackCount < 2)
        {
            yield return null;
        }
        damagedUnit.DecreaseHP(unit.currentPhysicalAttack * 0.5f, unit);
        sfx_Audio.PlayOneShot(GameInstance.Instance.soundPrefab["Hit"]);
        StartCoroutine(PlayParticle(trance[1], 0.6f, 0f));

        GameObject hitParticle2 = Instantiate(GameInstance.Instance.particlePrefab["WindHit"], damagedUnit.transform.position + Vector3.up * 1.5f, Quaternion.identity);
        hitParticle2.AddComponent<SortingGroup>().sortingOrder = 500;
        Destroy(hitParticle2, 0.5f);
        Camera.main.GetComponent<CamShake>().ShakeTime = 0.15f;
        while (TranceAttackCount < 3)
        {
            yield return null;
        }
        damagedUnit.DecreaseHP(unit.currentPhysicalAttack * 0.5f, unit);
        sfx_Audio.PlayOneShot(GameInstance.Instance.soundPrefab["Hit"]);
        StartCoroutine(PlayParticle(trance[2], 0.6f, 0f));

        GameObject hitParticle3 = Instantiate(GameInstance.Instance.particlePrefab["WindHit"], damagedUnit.transform.position + Vector3.up * 1.5f, Quaternion.identity);
        hitParticle3.AddComponent<SortingGroup>().sortingOrder = 500;
        Destroy(hitParticle3, 0.5f);
        Camera.main.GetComponent<CamShake>().ShakeTime = 0.15f;
        while (TranceAttackCount < 4)
        {
            yield return null;
        }
        damagedUnit.DecreaseHP(unit.currentPhysicalAttack * 0.5f, unit);
        sfx_Audio.PlayOneShot(GameInstance.Instance.soundPrefab["Hit"]);
        StartCoroutine(PlayParticle(trance[3], 0.6f, 0f));

        GameObject hitParticle4 = Instantiate(GameInstance.Instance.particlePrefab["WindHit"], damagedUnit.transform.position + Vector3.up * 1.5f, Quaternion.identity);
        hitParticle4.AddComponent<SortingGroup>().sortingOrder = 500;
        Destroy(hitParticle4, 0.5f);
        Camera.main.GetComponent<CamShake>().ShakeTime = 0.15f;
        while (TranceAttackCount < 5)
        {
            yield return null;
        }
        foreach (Enemy enemy in BattleManager.I.enemyCollection.collection)
        {
            enemy.unit.DecreaseHP(unit.currentPhysicalAttack * 1f, unit);
        }
        sfx_Audio.PlayOneShot(GameInstance.Instance.soundPrefab["Hit"]);
        StartCoroutine(PlayParticle(trance[4], 1f, 0f));
        GameObject hitParticle = Instantiate(GameInstance.Instance.particlePrefab["Hit_3"], new Vector3(5, 0, 0), Quaternion.identity);
        Destroy(hitParticle,0.7f);
        Camera.main.GetComponent<CamShake>().ShakeTime = 0.15f;
        TranceAttackCount = 0;
        //hp?? 0 ????(????)??????



        yield return new WaitForSeconds(0.6f);
        GameObject tranceParticle = Instantiate(GameInstance.Instance.particlePrefab["Trance"], new Vector3(5.52f,0f,0f), Quaternion.identity);
        sfx_Audio.PlayOneShot(GameInstance.Instance.soundPrefab["Chimes"]);

        yield return new WaitForSeconds(0.4f);
        for (int i = 0; i < 6; i++)
        {
            foreach (Enemy enemy in BattleManager.I.enemyCollection.collection)
            {
                enemy.unit.DecreaseHP(unit.currentPhysicalAttack * 0.2f, unit);
                var particle = Instantiate(GameInstance.Instance.particlePrefab["SwordHit"], enemy.transform.position + Vector3.up * 1.5f, Quaternion.identity);
                Destroy(particle, 0.5f);
            }
            sfx_Audio.PlayOneShot(GameInstance.Instance.soundPrefab["Punch1"]);
            yield return new WaitForSeconds(0.1f);
            sfx_Audio.PlayOneShot(GameInstance.Instance.soundPrefab["Punch2"]);
            yield return new WaitForSeconds(0.1f);
            Camera.main.GetComponent<CamShake>().ShakeTime = 0.1f;
        }


        Destroy(tranceParticle);


        foreach (Enemy enemy in BattleManager.I.enemyCollection.collection)
        {
            if (enemy.unit.Hp <= 0)
            {
                enemy.OnDie();
            }
        }
        yield return new WaitForSeconds(0.4f);
        animator.SetInteger("State", 2);
        BattleManager.I.skillText.gameObject.SetActive(false);

        unit.GetComponent<Hero>().ActionEnd();
        //bAttackEnd = true;
    }



    /// <summary>
    /// ??????
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="unit"></param>
    /// <returns></returns>
    public IEnumerator Multiple_Heal(Animator animator,
                                     Unit unit)
    {
        //???? ?????? ??????
        BattleManager.I.skillText.gameObject.SetActive(true);
        BattleManager.I.skillText.text = "힐링";

        animator.SetTrigger("Cast");

        yield return new WaitForSeconds(1f);
        StartCoroutine(PlayParticle(HealingBig, 0.5f, 0f));
        HealingBig.transform.position = HealingOffset.position;
        sfx_Audio.PlayOneShot(GameInstance.Instance.soundPrefab["HealExplosion"]);
        yield return new WaitForSeconds(0.5f);

        foreach (Hero hero in BattleManager.I.heroCollection.collection)
        {
            if (hero.bDie)
                continue;
            hero.OnHeal(hero.unit.maxHP * 0.3f);
            if (Random.Range(0f, 100f) > 90f)
            {
                StartCoroutine(Munchkin((hero.unit.maxHP * 0.7f) * 0.2f, hero));
            }
            yield return new WaitForSeconds(0.4f);
        }

        yield return new WaitForSeconds(1f);
        animator.SetInteger("State", 2);
        BattleManager.I.skillText.gameObject.SetActive(false);
        unit.GetComponent<Hero>().ActionEnd();
        //bAttackEnd = true;
    }


    /// <summary>
    /// ???????? : ????
    /// </summary>
    /// <param name="damagedUnit"></param>
    /// <param name="animator"></param>
    /// <param name="unit"></param>
    /// <returns></returns>
    public IEnumerator Slash(Unit damagedUnit,
                             Animator animator,
                             Unit unit)
    {
        //???? ?????? ??????
        BattleManager.I.skillText.gameObject.SetActive(true);
        BattleManager.I.skillText.text = "베어가르기";

        animator.SetTrigger("ChargeAttack2H");
        yield return new WaitForSeconds(0.75f);
        StartCoroutine(PlayParticle(sowrdSladhMidium, 0.5f, 0f));
        damagedUnit.DecreaseHP(unit.currentPhysicalAttack, unit);

        var particle = Instantiate(GameInstance.Instance.particlePrefab["Energy"], damagedUnit.transform.position + Vector3.up, Quaternion.identity);


        //hp?? 0 ????(????)??????
        if (damagedUnit.Hp <= 0)
        {
            yield return new WaitForSeconds(0.2f);
            damagedUnit.GetComponent<Enemy>().OnDie();
        }



        yield return new WaitForSeconds(1f);
        animator.SetInteger("State", 2);

        unit.GetComponent<Hero>().ActionEnd(); 
        //???? ?????? ????????
        BattleManager.I.skillText.gameObject.SetActive(false);
    }


    /// <summary>
    /// ???????? ????
    /// </summary>
    /// <param name="damagedUnit"></param>
    /// <param name="animator"></param>
    /// <param name="unit"></param>
    /// <returns></returns>
    public IEnumerator Taunt(Animator animator,
                             Unit unit)
    {
        BattleManager.I.skillText.gameObject.SetActive(true);
        BattleManager.I.skillText.text = "????";

        unit.tauntDuration = 3;
        animator.SetInteger("State", 4);
        yield return new WaitForSeconds(0.15f);

        sfx_Audio.PlayOneShot(GameInstance.Instance.soundPrefab["Shield"],3);
        var shoke1 = Instantiate(GameInstance.Instance.particlePrefab["ShokeWave"],transform.position + Vector3.up,Quaternion.identity);
        Destroy(shoke1, 0.3f);
        yield return new WaitForSeconds(0.15f);
        var shoke2 = Instantiate(GameInstance.Instance.particlePrefab["ShokeWave"],transform.position + Vector3.up, Quaternion.identity);
        Destroy(shoke2, 0.3f);
        sfx_Audio.PlayOneShot(GameInstance.Instance.soundPrefab["Shield"],3);
        yield return new WaitForSeconds(0.7f);
        animator.SetInteger("State", 2);

        unit.GetComponent<Hero>().ActionEnd();
        //???? ?????? ????????
        BattleManager.I.skillText.gameObject.SetActive(false);
    }

    /// <summary>
    /// ???????? : ????????
    /// </summary>
    /// <param name="damagedUnit"></param>
    /// <param name="animator"></param>
    /// <param name="unit"></param>
    /// <returns></returns>
    public IEnumerator Roar(Animator animator,
                            Unit unit)
    {
        //???? ?????? ??????
        BattleManager.I.skillText.gameObject.SetActive(true);
        BattleManager.I.skillText.text = "????";
        animator.SetTrigger("Cast");
        yield return new WaitForSeconds(0.3f);

        foreach (var Unit in BattleManager.I.unitList)
        {
            Unit.roarDuration = 3;
            StartCoroutine(Unit.RoarBuffDeBuff());
        }
        yield return new WaitForSeconds(1f);

        animator.SetInteger("State", 2);
        unit.GetComponent<Hero>().ActionEnd();
        //???? ?????? ????????
        BattleManager.I.skillText.gameObject.SetActive(false);
    }

    /// <summary>
    /// ???????? : ??????, ?????????? ????
    /// </summary>
    /// <param name="damagedUnit"></param>
    /// <param name="animator"></param>
    /// <param name="unit"></param>
    /// <returns></returns>
    public IEnumerator Raise(Hero targetUnit,
                             Animator animator,
                             Unit unit)
    {
        BattleManager.I.skillText.gameObject.SetActive(true);
        BattleManager.I.skillText.text = "레이즈";
        animator.SetTrigger("Cast");
        yield return new WaitForSeconds(1f);
        targetUnit.OnHeal(targetUnit.unit.maxHP * 0.7f);

        if(Random.Range(0f,100f) > 90f)
        {
            StartCoroutine(Munchkin((targetUnit.unit.maxHP * 0.7f) * 0.2f, targetUnit));
            yield return new WaitForSeconds(0.3f);
        }

        yield return new WaitForSeconds(1f);

        animator.SetInteger("State", 2);
        unit.GetComponent<Hero>().ActionEnd();

        BattleManager.I.skillText.gameObject.SetActive(false);
    }


    //힐러 공격스킬
    public IEnumerator Scourge(Unit damagedUnit,
                               Animator animator,
                               Unit unit)
    {
        //스킬 이름 출력
        BattleManager.I.skillText.gameObject.SetActive(true);
        BattleManager.I.skillText.text = "맹약";

        //animator.SetTrigger("Cast");
        //yield return new WaitForSeconds(0.2f);
        animator.SetInteger("State", 8);
        yield return new WaitForSeconds(0.3f);
        var charge = Instantiate(GameInstance.Instance.particlePrefab["DarknessCharge"], edge.position, Quaternion.identity);
        yield return new WaitForSeconds(1.5f);
        animator.SetInteger("State", 0);
        Destroy(charge);
        var projectile = Instantiate(GameInstance.Instance.particlePrefab["DarknessProjectile"], damagedUnit.transform.position + Vector3.up, Quaternion.identity);

        yield return new WaitForSeconds(0.7f);

        var explosion = Instantiate(GameInstance.Instance.particlePrefab["DarknessExplosion"], damagedUnit.transform.position + Vector3.up, Quaternion.identity);
        ParticleSystem par_exp = explosion.GetComponent<ParticleSystem>();
        yield return new WaitForSeconds(0.15f);

        for(int i = 0; i < 7; i++)
        {
            par_exp.Play();
            damagedUnit.DecreaseHP(unit.currentPhysicalAttack, unit);
            sfx_Audio.PlayOneShot(GameInstance.Instance.soundPrefab["Hit"]);
            Camera.main.GetComponent<CamShake>().ShakeTime = 0.15f;
            yield return new WaitForSeconds(0.18f);
        }
        projectile.transform.DOScale(0f, 0.15f);
        Destroy(explosion);
        yield return new WaitForSeconds(0.15f);
        Destroy(projectile);
        var particle = Instantiate(GameInstance.Instance.particlePrefab["Energy"], damagedUnit.transform.position + Vector3.up, Quaternion.identity);
        damagedUnit.DecreaseHP(unit.currentPhysicalAttack, unit);
        sfx_Audio.PlayOneShot(GameInstance.Instance.soundPrefab["Hit"]);
        Camera.main.GetComponent<CamShake>().ShakeTime = 0.15f;


        //hp?? 0 ????(????)??????
        if (damagedUnit.Hp <= 0)
        {
            yield return new WaitForSeconds(0.2f);
            damagedUnit.GetComponent<Enemy>().OnDie();
        }
        


        yield return new WaitForSeconds(0.6f);
        animator.SetInteger("State", 2);

        unit.GetComponent<Hero>().ActionEnd();
        //???? ?????? ????????
        BattleManager.I.skillText.gameObject.SetActive(false);
    }

    //탱커 궁극기
    public IEnumerator BusterCall(Animator animator,
                                  Unit unit)
    {
        //스킬 이름 출력
        BattleManager.I.skillText.gameObject.SetActive(true);
        BattleManager.I.skillText.text = "버스터콜";

        animator.SetTrigger("Buster");
        yield return new WaitForSeconds(1f);
        Camera.main.GetComponent<CamShake>().ShakeTime = 7f;

        yield return new WaitForSeconds(2f);
        
        for (int i = 0; i < 30; i++)
        {
            int ran = Random.Range(0, 5);
            var bullet = Instantiate(GameInstance.Instance.particlePrefab["Bullet"], BattleManager.I.bulletOffsets[ran].position, Quaternion.identity);


            float ranX = Random.Range(1f, 8f);
            float ranY = Random.Range(-2.5f, 3f);
            Vector3 targetPos = new Vector3(ranX, ranY, 0f);

            float ranSpeed = Random.Range(0.5f, 1f);
            bullet.transform.DOMove(targetPos, ranSpeed);
            
            Vector3 dir = targetPos - BattleManager.I.bulletOffsets[ran].position;

            bullet.GetComponent<BusterCallBullet>().targetPos = targetPos;
            bullet.GetComponent<BusterCallBullet>().audioSource = sfx_Audio;
            bullet.GetComponent<BusterCallBullet>().instigator = unit;

            // 타겟 방향으로 회전함
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            yield return new WaitForSeconds(0.15f);
        }






        yield return new WaitForSeconds(1f);

        foreach (Enemy enemy in BattleManager.I.enemyCollection.collection)
        {
            if (enemy.unit.Hp <= 0)
            {
                enemy.OnDie();
            }
        }

        animator.SetInteger("State", 2);

        unit.GetComponent<Hero>().ActionEnd();
        //???? ?????? ????????
        BattleManager.I.skillText.gameObject.SetActive(false);
    }

    //딜러 궁극기
    public IEnumerator SlashBurst(Unit damagedUnit,
                                  Animator animator,
                                  Unit unit)
    {
        //스킬 이름 출력
        BattleManager.I.skillText.gameObject.SetActive(true);
        BattleManager.I.skillText.text = "슬래쉬 버스트";

        animator.SetTrigger("ChargeAttack2H");
        //Camera.main.GetComponent<CamShake>().ShakeTime = 7f;

        yield return new WaitForSeconds(1f);
        ultPanelAnimator.SetActive(true);
        unit.GetComponent<SortingGroup>().sortingOrder = 200;
        damagedUnit.GetComponent<SortingGroup>().sortingOrder = 200;
        BattleScene.I.UI_Camera.SetActive(false);
        yield return new WaitForSeconds(1f);
        var lens = Instantiate(GameInstance.Instance.particlePrefab["LensFlare"],
                               Vector3.zero + Vector3.up * 2f + Vector3.left * 2f, Quaternion.identity);
        sfx_Audio.PlayOneShot(GameInstance.Instance.soundPrefab["Chimes"]);
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 50; i++)
        {
            float ranX = Random.Range(-8f, 8f);
            float ranY = Random.Range(-3f, 3f);
            var slash = Instantiate(GameInstance.Instance.particlePrefab["SwordSlash"], 
                                    new Vector3(ranX,ranY,0f), Quaternion.identity);

            float ranX1 = Random.Range(-8f, 8f);
            float ranY1 = Random.Range(-3f, 3f);
            var hit = Instantiate(GameInstance.Instance.particlePrefab["SwordHit"],
                                  new Vector3(ranX1, ranY1, 0f), Quaternion.identity);

            string ranClip = Random.Range(1, 6).ToString();

            sfx_Audio.PlayOneShot(GameInstance.Instance.soundPrefab["SwordSwing" + ranClip] );

            yield return new WaitForSeconds(0.03f);

        }
        yield return new WaitForSeconds(0.5f);


        ultPanelAnimator.GetComponent<Animator>().SetTrigger("FadeOut");
        BattleScene.I.UI_Camera.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        ultPanelAnimator.SetActive(false);

        sfx_Audio.PlayOneShot(GameInstance.Instance.soundPrefab["Hit"]);
        foreach (Enemy enemy in BattleManager.I.enemyCollection.collection)
        {
            enemy.unit.DecreaseHP(unit.currentPhysicalAttack * 5f, unit);
            var particle = Instantiate(GameInstance.Instance.particlePrefab["SwordHit"], enemy.transform.position + Vector3.up * 1.5f, Quaternion.identity);
            Destroy(particle, 0.5f);
        }

        yield return new WaitForSeconds(.5f);
        unit.GetComponent<SortingGroup>().sortingOrder = 250;
        damagedUnit.GetComponent<SortingGroup>().sortingOrder = 250;
        foreach (Enemy enemy in BattleManager.I.enemyCollection.collection)
        {
            if (enemy.unit.Hp <= 0)
            {
                enemy.OnDie();
            }
        }

        animator.SetInteger("State", 2);

        unit.GetComponent<Hero>().ActionEnd();
        //???? ?????? ????????
        BattleManager.I.skillText.gameObject.SetActive(false);
    }


    //-----------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------
    //--------------------------------??????????-----------------------------------------------
    //-----------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------

    /// <summary>
    /// ???? ??????
    /// </summary>
    /// <returns></returns>
    public IEnumerator Munchkin(float value,
                                Hero targetHero)
    {
        yield return new WaitForSeconds(1f);
        targetHero.OnHeal(value);
        print("???????? ????");
    }

    /// <summary>
    /// ??????
    /// </summary>
    /// <param name="unit"></param>
    public void Calm(Unit unit)
    {
        unit.currentPhysicalAttack += unit.PhysicalAttack * 0.02f;
    }

    public void Sacrifice(Unit unit)
    {
        unit.currentDefense += unit.Defense * 0.03f;
    }
}
