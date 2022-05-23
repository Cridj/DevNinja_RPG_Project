using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultPanel : MonoBehaviour
{
    /// <summary>
    /// 총경험치량
    /// </summary>
    public float totalEXP_dealer;    
    public float totalEXP_healer;    
    public float totalEXP_tanker;


    /// <summary>
    /// 경험치 요구량
    /// </summary>
    public float DealerMaxExp;
    public float HealerMaxExp;
    public float TankerMaxExp;


    /// <summary>
    /// 경험치 증가속도
    /// </summary>
    public float dealerExpIncreasedPerSecond;
    public float healerExpIncreasedPerSecond;
    public float tankerExpIncreasedPerSecond;

    /// <summary>
    /// 경험치 프로그레스바
    /// </summary>
    public Slider dealerSlider;
    public Slider healerSlider;
    public Slider tankerSlider;

    /// <summary>
    /// 히어로 컴포넌트들
    /// </summary>
    public Hero dealer;
    public Hero healer;
    public Hero tanker;


    /// <summary>
    /// Update 실행여부 체크할 bool
    /// </summary>
    bool dealerExp;
    bool healerExp;
    bool TankerExp;

    /// <summary>
    /// 히어로 레벨 텍스트
    /// </summary>
    public TextMeshProUGUI dealerLevelText;
    public TextMeshProUGUI healerLevelText;
    public TextMeshProUGUI tankerLevelText;


    /// <summary>
    /// 히어로 남은 경험치량 텍스트
    /// </summary>
    public TextMeshProUGUI dealerExpText;
    public TextMeshProUGUI healerExpText;
    public TextMeshProUGUI tankerExpText;




    private void Start()
    {
        dealerExpIncreasedPerSecond = 100f;

        DealerMaxExp = GameInstance.Instance.gamdData.ExperienceTable[dealer.heroLevel - 1];
        HealerMaxExp = GameInstance.Instance.gamdData.ExperienceTable[healer.heroLevel - 1];
        TankerMaxExp = GameInstance.Instance.gamdData.ExperienceTable[tanker.heroLevel - 1];
    }




    private void Awake()
    {
        //레벨 텍스트 초기화
        dealerLevelText.text = GameInstance.Instance.gamdData.dealerData.Level.ToString();
        healerLevelText.text = GameInstance.Instance.gamdData.healerData.Level.ToString();
        tankerLevelText.text = GameInstance.Instance.gamdData.tankerData.Level.ToString();
        foreach (var hero in BattleManager.I.heroCollection.collection)
        {
            switch (hero.job)
            {
                case PlayerJob.Dealer:
                    dealer = hero;
                    break;
    
                case PlayerJob.Healer:
                    healer = hero;
                    break;

                case PlayerJob.Tanker:
                    tanker = hero;
                    break;
            }
        }
    }




    private void OnEnable()
    {
        StartCoroutine(StartDelay());

        //경험치 초기회
        totalEXP_dealer = BattleManager.I.TotalExp;
        totalEXP_healer = BattleManager.I.TotalExp;
        totalEXP_tanker = BattleManager.I.TotalExp;



        dealerExpText.text = totalEXP_dealer.ToString();
        healerExpText.text = totalEXP_healer.ToString(); 
        tankerExpText.text = totalEXP_tanker.ToString();

    }




    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(1f);
        StartExp();
    }




    void StartExp()
    { 
        foreach(var hero in BattleManager.I.heroCollection.collection)
        {
            if (!hero.bDie)
            {
                switch (hero.job)
                { 
                case PlayerJob.Dealer:
                        dealerExpIncreasedPerSecond = 
                            GameInstance.Instance.gamdData.ExperienceTable[dealer.heroLevel - 1];
                        dealerExp = true;
                        break;

                case PlayerJob.Healer:
                        healerExpIncreasedPerSecond =
                        GameInstance.Instance.gamdData.ExperienceTable[healer.heroLevel - 1];
                        healerExp = true;
                        break;

                case PlayerJob.Tanker:
                        tankerExpIncreasedPerSecond =
                        GameInstance.Instance.gamdData.ExperienceTable[tanker.heroLevel - 1];
                        TankerExp = true;
                        break;
                }
            }

            }
    }


    private void Update()
    {
        if (dealerExp)
        {
            DealerUpdate();
        }

        if (healerExp)
        {
            HealerUpdate();
        }

        if (TankerExp)
        {
            TankerUpdate();
        }
    }




    void DealerUpdate()
    {
        if (totalEXP_dealer < 0f)
        {
            print("끝");
            dealerExp = false;
            GameInstance.Instance.gamdData.dealerData.Exp = Mathf.Round(dealer.Experience);
            dealerExpText.text = "";
            return;
        }
        //값 올려주기
        dealer.Experience += dealerExpIncreasedPerSecond * Time.deltaTime;
        totalEXP_dealer -= dealerExpIncreasedPerSecond * Time.deltaTime;
        dealerExpText.text = Mathf.Round(totalEXP_dealer).ToString(); 
        dealerSlider.value = dealer.Experience / DealerMaxExp;



        if (dealer.Experience > DealerMaxExp)
        {
            GameInstance.Instance.gamdData.dealerData.Level++;
            dealer.heroLevel++;
            dealerLevelText.text = GameInstance.Instance.gamdData.dealerData.Level.ToString();
            dealer.Experience = 0f;
            DealerMaxExp = GameInstance.Instance.gamdData.ExperienceTable[GameInstance.Instance.gamdData.dealerData.Level - 1];

            //경험치 속도 재조정
            dealerExpIncreasedPerSecond = 
            GameInstance.Instance.gamdData.ExperienceTable[dealer.heroLevel - 1];

            dealerSlider.value = 0f;
        }
    }




    void HealerUpdate()
    {
        if (totalEXP_healer < 0f)
        {
            print("끝");
            healerExp = false;
            GameInstance.Instance.gamdData.healerData.Exp = Mathf.Round(healer.Experience);
            healerExpText.text = "";
            return;
        }
        //값 올려주기
        healer.Experience += healerExpIncreasedPerSecond * Time.deltaTime;
        totalEXP_healer -= healerExpIncreasedPerSecond * Time.deltaTime;
        healerSlider.value = healer.Experience / HealerMaxExp;
        healerExpText.text = Mathf.Round(totalEXP_healer).ToString();



        if (healer.Experience > HealerMaxExp)
        {
            GameInstance.Instance.gamdData.healerData.Level++;
            healer.heroLevel++;
            healerLevelText.text = GameInstance.Instance.gamdData.healerData.Level.ToString();
            healer.Experience = 0f;
            HealerMaxExp = GameInstance.Instance.gamdData.ExperienceTable[GameInstance.Instance.gamdData.healerData.Level - 1];

            //경험치 속도 재조정
            healerExpIncreasedPerSecond = 
            GameInstance.Instance.gamdData.ExperienceTable[healer.heroLevel - 1];


            healerSlider.value = 0f;
            print("힐러레벨업");
        }
    }




    void TankerUpdate()
    {
        if (totalEXP_tanker < 0f)
        {
            print("끝");
            TankerExp = false;
            GameInstance.Instance.gamdData.tankerData.Exp = Mathf.Round(tanker.Experience);
            tankerExpText.text = "";
            return;
        }
        //값 올려주기
        tanker.Experience += tankerExpIncreasedPerSecond * Time.deltaTime;
        totalEXP_tanker -= tankerExpIncreasedPerSecond * Time.deltaTime;
        tankerSlider.value = tanker.Experience / TankerMaxExp;
        tankerExpText.text = Mathf.Round(totalEXP_tanker).ToString();



        if (tanker.Experience > TankerMaxExp)
        {
            GameInstance.Instance.gamdData.tankerData.Level++;
            tanker.heroLevel++;
            tankerLevelText.text = GameInstance.Instance.gamdData.tankerData.Level.ToString();
            tanker.Experience = 0f;
            TankerMaxExp = GameInstance.Instance.gamdData.ExperienceTable[GameInstance.Instance.gamdData.tankerData.Level - 1];

            //경험치 속도 재조정
            tankerExpIncreasedPerSecond =                 
            GameInstance.Instance.gamdData.ExperienceTable[tanker.heroLevel - 1];

            tankerSlider.value = 0f;
        }
    }




    public void BackToStageScene()
    {
        SceneLoad.LoadScene(BattleManager.I.nowStage);
    }

    //러프없는버전임

    //IEnumerator StartGoGo()
    //{
    //    yield return new WaitForSeconds(5f);
    //    StartExp();
    //}

    ///// <summary>
    ///// 경험치바 올려서 레벨업 실시하는함수
    ///// </summary>
    //void StartExp()
    //{
    //    totalEXP_dealer = BattleManager.I.TotalExp;
    //    totalEXP_healer = BattleManager.I.TotalExp;
    //    totalEXP_tanker = BattleManager.I.TotalExp;

    //    foreach(var hero in BattleManager.I.heroCollection.collection)
    //    {
    //        StartLevelUp(hero);
    //    }
    //}

    //void StartLevelUp(Hero hero)
    //{
    //    switch (hero.job)
    //    {
    //        case PlayerJob.Dealer:
    //            hero.DestExperienceRequired -= totalEXP_dealer;
    //            dealerExp = true;

    //            expMinus(hero);
    //            break;

    //        case PlayerJob.Healer:
    //            hero.ExperienceRequired -= totalEXP_healer;

    //            expMinus(hero);
    //            break;

    //        case PlayerJob.Tanker:
    //            hero.ExperienceRequired -= totalEXP_tanker;

    //            expMinus(hero);
    //            break;
    //    }
    //}

    //void Update()
    //{
    //    if (dealerExp)
    //    {
    //        DealerUpdate();
    //    }
    //    if (TankerExp)
    //    {

    //    }
    //    if (healerExp)
    //    {

    //    }
    //}

    //void DealerUpdate()
    //{
    //    foreach (var hero in BattleManager.I.heroCollection.collection)
    //    {
    //        if (hero.job == PlayerJob.Dealer)
    //        {
    //            Mathf.Lerp(hero.ExperienceRequired, hero.DestExperienceRequired, 0.1f * Time.deltaTime);
    //            dealerSlider.value = GameInstance.Instance.gamdData.ExperienceTable[GameInstance.Instance.gamdData.dealerData.Level - 1]
    //               / hero.ExperienceRequired;
    //            print("레벨업중");
    //        }
    //    }
    //}

    //void expMinus(Hero hero)
    //{
    //    if (hero.ExperienceRequired < 0)
    //    {
    //        //남은 경험치량 절대값으로 환산해 정수로 저장
    //        Mathf.Abs(hero.ExperienceRequired);

    //        //히어로 레벨 올려주기
    //        switch (hero.job)
    //        {
    //            case PlayerJob.Dealer:
    //                GameInstance.Instance.gamdData.dealerData.Level++;

    //                //히어로 경험치량 재설정하기.
    //                hero.ExperienceRequired = 
    //                    GameInstance.Instance.gamdData.ExperienceTable[GameInstance.Instance.gamdData.dealerData.Level-1];
    //                break;

    //            case PlayerJob.Healer:
    //                GameInstance.Instance.gamdData.healerData.Level++;
    //                //히어로 경험치량 재설정하기.
    //                hero.ExperienceRequired =
    //                    GameInstance.Instance.gamdData.ExperienceTable[GameInstance.Instance.gamdData.healerData.Level-1];
    //                break;

    //            case PlayerJob.Tanker:
    //                GameInstance.Instance.gamdData.tankerData.Level++;
    //                 //히어로 경험치량 재설정하기.
    //                hero.ExperienceRequired = 
    //                    GameInstance.Instance.gamdData.ExperienceTable[GameInstance.Instance.gamdData.tankerData.Level-1];
    //                break;
    //        }



    //        //다시 호출해서 경험치 올려주기
    //        expMinus(hero);
    //        print("우애앵");
    //    }
    //    //레벨업 끝났을때
    //    else
    //    {
    //        switch (hero.job)
    //        {
    //            case PlayerJob.Dealer:
    //                GameInstance.Instance.gamdData.dealerData.ExpRequire = hero.ExperienceRequired;
    //                break;

    //            case PlayerJob.Healer:
    //                GameInstance.Instance.gamdData.healerData.ExpRequire = hero.ExperienceRequired;
    //                break;

    //            case PlayerJob.Tanker:
    //                GameInstance.Instance.gamdData.tankerData.ExpRequire = hero.ExperienceRequired;
    //                break;
    //        }
    //        print("레벨업끝");
    //    }
    //}
}
