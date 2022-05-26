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

    public GameObject skill1_Trail;
    public GameObject skill2_Trail;
    public GameObject skill3_Trail;
    public GameObject attack_Trail;

    //���� �˾����� ����
    public Unit currentPopupPlayer;

    //���� �˾����� UI��
    public Slider currentPlayerSlider;
    public TextMeshProUGUI currentPlayerHp;
    public TextMeshProUGUI currentPlayerMaxHp;

    //�ҵ�����Ʈ ������
    public Transform[] swordOffsets;

    public Image[] turnImages;
    public List<Sprite> unitPortraits = new List<Sprite>();
    public TextMeshProUGUI[] turnText;
    public TextMeshProUGUI BattleStatePopUpNumText;
    public TextMeshProUGUI BattleStateNumText;
    public GameObject BattleStatePopUp;

    //�� �ִϸ��̼ǿ� �ø��÷� ����
    public bool turnFlipFlop;

    //�г� ������Ʈ
    public GameObject stat1Panel;
    public GameObject stat2Panel;

    //�ִϸ��̼� ����
    Animator stat1Anim;
    Animator stat2Anim;

    //ĳ���� UI ����
    //Stat1
    public TextMeshProUGUI CharacterUI_CurrentHP;
    public TextMeshProUGUI CharacterUI_MaxHP;
    public TextMeshProUGUI CharacterUI_HeroName;
    public Slider CharacterUI_heroHP_ProgressBar;

    //stat2
    public TextMeshProUGUI CharacterUI_CurrentHP2;
    public TextMeshProUGUI CharacterUI_MaxHP2;
    public TextMeshProUGUI CharacterUI_HeroName2;
    public Slider CharacterUI_heroHP_ProgressBar2;

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
                turnFlipFlop = true;
                //stat1Anim.SetTrigger("MyTurn");
                print("ó������");
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
                turnFlipFlop = true;
                stat1Anim.SetTrigger("MyTurn");
                stat2Anim.SetTrigger("TurnOver");
                currentPopupPlayer = BattleManager.I.turnUnit;
                currentPlayerHp = CharacterUI_CurrentHP;
                currentPlayerMaxHp = CharacterUI_MaxHP;
                currentPlayerSlider = CharacterUI_heroHP_ProgressBar;
                print("������");
            }
        }
        else
        {
            stat2Panel.SetActive(true);
            CharacterUI_CurrentHP2.text = (Mathf.Round(BattleManager.I.turnUnit.Hp)).ToString();
            CharacterUI_MaxHP2.text = BattleManager.I.turnUnit.maxHP.ToString();
            CharacterUI_HeroName2.text = BattleManager.I.turnUnit.name;
            CharacterUI_heroHP_ProgressBar2.value = BattleManager.I.turnUnit.Hp / BattleManager.I.turnUnit.maxHP;
            turnFlipFlop = false;
            stat1Anim.SetTrigger("TurnOver");
            stat2Anim.SetTrigger("MyTurn");
            currentPopupPlayer = BattleManager.I.turnUnit;
            currentPlayerHp = CharacterUI_CurrentHP2;
            currentPlayerMaxHp = CharacterUI_MaxHP2;
            currentPlayerSlider = CharacterUI_heroHP_ProgressBar2;
            print("�ι�°");
        }
        Skill1_Img.sprite = BattleManager.I.turnUnit.GetComponent<Hero>().skill1_Sprite;
        Skill2_Img.sprite = BattleManager.I.turnUnit.GetComponent<Hero>().skill2_Sprite;
        Skill3_Img.sprite = BattleManager.I.turnUnit.GetComponent<Hero>().skill3_Sprite;
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
    /// �������� �ؽ�Ʈ ������Ʈ���ִ� �Լ�
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