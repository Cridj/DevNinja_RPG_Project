using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MHomiLibrary;

public class BattleScene : HSingleton<BattleScene>
{
    public SkillInfo[] skillInfos;
    float pointerCurSec;
    float pointerDestSec = 0.3f;
    public bool isPointerDown;

    public GameObject UI_Camera;
    public TextMeshProUGUI ultCollDownText;
    public Image ultButtonImg;



    public GameObject skill1_Trail;
    public GameObject skill2_Trail;
    public GameObject skill3_Trail;
    public GameObject attack_Trail;

    //현재 팝업중인 유닛
    public Unit currentPopupPlayer;

    //현재 팝업중인 UI들
    public Slider currentPlayerSlider;
    public TextMeshProUGUI currentPlayerHp;
    public TextMeshProUGUI currentPlayerMaxHp;
    public Image currentPlayerPortrait;


    //소드이펙트 오프셋
    public Transform[] swordOffsets;

    public Image[] turnImages;
    public List<Sprite> unitPortraits = new List<Sprite>();
    public TextMeshProUGUI[] turnText;
    public TextMeshProUGUI BattleStatePopUpNumText;
    public TextMeshProUGUI BattleStateNumText;
    public GameObject BattleStatePopUp;

    //턴 애니메이션용 플립플롯 변수
    public bool turnFlipFlop;

    //패널 오브젝트
    public GameObject stat1Panel;
    public GameObject stat2Panel;

    //애니메이션 변수
    Animator stat1Anim;
    Animator stat2Anim;

    //캐릭터 UI 관련
    //Stat1
    public TextMeshProUGUI CharacterUI_CurrentHP;
    public TextMeshProUGUI CharacterUI_MaxHP;
    public TextMeshProUGUI CharacterUI_HeroName;
    public Slider CharacterUI_heroHP_ProgressBar;
    public Image CharacterUI_HeroPotrait;


    //stat2
    public TextMeshProUGUI CharacterUI_CurrentHP2;
    public TextMeshProUGUI CharacterUI_MaxHP2;
    public TextMeshProUGUI CharacterUI_HeroName2;
    public Slider CharacterUI_heroHP_ProgressBar2;
    public Image CharacterUI_HeroPotrait2;

    public bool bBattleStart;



    public Image Skill1_Img;
    public Image Skill2_Img;
    public Image Skill3_Img;


    void Awake()
    {
        stat1Anim = stat1Panel.GetComponent<Animator>();
        stat2Anim = stat2Panel.GetComponent<Animator>();
    }


    public void GoBackTestScene()
    {
        SceneLoad.LoadScene("TestBattle");
    }

    public void CharacterUI_Update()
    {
        if (BattleManager.I.turnUnit.GetComponent<Hero>() == null)
            return;

        if (!turnFlipFlop)
        {
            if (!bBattleStart)
            {
                stat1Panel.SetActive(true);
                CharacterUI_CurrentHP.text = (Mathf.Round(BattleManager.I.turnUnit.Hp)).ToString();
                CharacterUI_MaxHP.text = BattleManager.I.turnUnit.maxHP.ToString();
                CharacterUI_HeroName.text = BattleManager.I.turnUnit.name;                
                CharacterUI_heroHP_ProgressBar.value = BattleManager.I.turnUnit.Hp / BattleManager.I.turnUnit.maxHP;
                switch (BattleManager.I.turnUnit.GetComponent<Hero>().job)
                {
                    case PlayerJob.Dealer:
                        CharacterUI_HeroPotrait.sprite = unitPortraits[0];
                        break;

                    case PlayerJob.Tanker:
                        CharacterUI_HeroPotrait.sprite = unitPortraits[1];
                        break;

                    case PlayerJob.Healer:
                        CharacterUI_HeroPotrait.sprite = unitPortraits[2];
                        break;
                }

                turnFlipFlop = true;
                //stat1Anim.SetTrigger("MyTurn");
                currentPopupPlayer = BattleManager.I.turnUnit;
                currentPlayerHp = CharacterUI_CurrentHP;
                currentPlayerMaxHp = CharacterUI_MaxHP;
                currentPlayerSlider = CharacterUI_heroHP_ProgressBar;
                
                bBattleStart = true;
            }
            else
            {
                CharacterUI_CurrentHP.text = (Mathf.Round(BattleManager.I.turnUnit.Hp)).ToString();
                CharacterUI_MaxHP.text = BattleManager.I.turnUnit.maxHP.ToString();
                CharacterUI_HeroName.text = BattleManager.I.turnUnit.name;
                CharacterUI_heroHP_ProgressBar.value = BattleManager.I.turnUnit.Hp / BattleManager.I.turnUnit.maxHP;
                switch (BattleManager.I.turnUnit.GetComponent<Hero>().job)
                {
                    case PlayerJob.Dealer:
                        CharacterUI_HeroPotrait.sprite = unitPortraits[0];
                        break;

                    case PlayerJob.Tanker:
                        CharacterUI_HeroPotrait.sprite = unitPortraits[1];
                        break;

                    case PlayerJob.Healer:
                        CharacterUI_HeroPotrait.sprite = unitPortraits[2];
                        break;
                }

                turnFlipFlop = true;
                stat1Anim.SetTrigger("MyTurn");
                stat2Anim.SetTrigger("TurnOver");
                currentPopupPlayer = BattleManager.I.turnUnit;
                currentPlayerHp = CharacterUI_CurrentHP;
                currentPlayerMaxHp = CharacterUI_MaxHP;
                currentPlayerSlider = CharacterUI_heroHP_ProgressBar;
            }
        }
        else
        {
            stat2Panel.SetActive(true);
            CharacterUI_CurrentHP2.text = (Mathf.Round(BattleManager.I.turnUnit.Hp)).ToString();
            CharacterUI_MaxHP2.text = BattleManager.I.turnUnit.maxHP.ToString();
            CharacterUI_HeroName2.text = BattleManager.I.turnUnit.name;
            CharacterUI_heroHP_ProgressBar2.value = BattleManager.I.turnUnit.Hp / BattleManager.I.turnUnit.maxHP;
            switch (BattleManager.I.turnUnit.GetComponent<Hero>().job)
            {
                case PlayerJob.Dealer:
                    CharacterUI_HeroPotrait.sprite = unitPortraits[0];
                    break;

                case PlayerJob.Tanker:
                    CharacterUI_HeroPotrait.sprite = unitPortraits[1];
                    break;

                case PlayerJob.Healer:
                    CharacterUI_HeroPotrait.sprite = unitPortraits[2];
                    break;
            }

            turnFlipFlop = false;
            stat1Anim.SetTrigger("TurnOver");
            stat2Anim.SetTrigger("MyTurn");
            currentPopupPlayer = BattleManager.I.turnUnit;
            currentPlayerHp = CharacterUI_CurrentHP2;
            currentPlayerMaxHp = CharacterUI_MaxHP2;
            currentPlayerSlider = CharacterUI_heroHP_ProgressBar2;
        }
        Skill1_Img.sprite = BattleManager.I.turnUnit.GetComponent<Hero>().skill1_Sprite;
        Skill2_Img.sprite = BattleManager.I.turnUnit.GetComponent<Hero>().skill2_Sprite;
        Skill3_Img.sprite = BattleManager.I.turnUnit.GetComponent<Hero>().skill3_Sprite;
        if(BattleManager.I.turnUnit.GetComponent<Hero>().ultCoolDown > 0)
        {
            ultCollDownText.text = BattleManager.I.turnUnit.GetComponent<Hero>().ultCoolDown + "턴";
            ultButtonImg.color = Color.gray;
        }
        else
        {
            ultCollDownText.text = "";
            ultButtonImg.color = Color.white;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        PortraitInit();
    }

    void PortraitInit()
    {
        foreach (Unit unit in BattleManager.I.unitList)
        {
            unitPortraits.Add(unit.unitSprite);
        }
    }

    public void currentUI_Update()
    {
        currentPlayerSlider.value = currentPopupPlayer.Hp / currentPopupPlayer.maxHP;
        currentPlayerHp.text = Mathf.Round(currentPopupPlayer.Hp).ToString();
        currentPlayerMaxHp.text = currentPopupPlayer.maxHP.ToString();
    }


       
    public void turnTextUpdate()
    {
        int textNum = 0;
        foreach(Image image in turnImages)
        {
            foreach(Unit unit in BattleManager.I.unitList)
            {
                if(unit.turnOrder == textNum)
                {
                    if (textNum == 5)
                        return;
                    if (!unit.bDie)
                    {
                        image.sprite = unit.unitSprite;
                        textNum++;
                        break;
                    }
                    else
                    {
                        image.sprite = null;
                        continue;
                    }
               
                }
            } 
        }
    }




    /// <summary>
    /// 현재전투 텍스트 업데이트해주는 함수
    /// </summary>
    /// <param name="battleStateNum"></param>
    public void BattleStateTextUpdate(int battleStateNum) 
    {
        BattleStatePopUpNumText.text = battleStateNum.ToString() + " / 3";
        BattleStateNumText.text = battleStateNum.ToString() + " / 3";
    }


    void Update()
    {

    }



    public void PointerDown(int skill)
    {
        isPointerDown = true;
        StartCoroutine(PointerDownCoroutine(skill));
    }

    IEnumerator PointerDownCoroutine(int skill)
    {
        if (isPointerDown)
        {
            while (true)
            {
                if (!isPointerDown)
                    break;
                if(pointerCurSec > pointerDestSec)
                {
                    skillInfos[skill].gameObject.SetActive(true);
                    break;
                }
                pointerCurSec += Time.deltaTime;
                yield return null;
            }
        }
    }

    public void PointerUp(int skill)
    {
        isPointerDown = false;
        pointerCurSec = 0f;
        skillInfos[skill].gameObject.SetActive(false);
    }
}
