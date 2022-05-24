using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PathData
{
    public bool pathOpen;
    public bool pathClear;
}

[System.Serializable]
public class HeroData
{
    public int HP;
    public int Attack;
    public int MagicalAttack;
    public int Defense;
    public int Speed;
    public float Exp;
    public int Level;    
    public SKILLSET[] skillSets;
    public List<HeroItem> heroItems = new List<HeroItem>();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="HP">HP</param> 
    /// <param name="Attack">공격력</param>
    /// <param name="MagicalAttack">주문력</param>
    /// <param name="Defense">방어력</param>
    /// <param name="Speed">스피드</param>
    /// <param name="skillsets">장착된 스킬</param>
    /// <param name="heroitems">소지중인 아이템</param>
    public HeroData(int HP, int Attack, int MagicalAttack, int Defense, int Speed
                    , SKILLSET[] skillsets, List<HeroItem> heroitems, float Exp, int Level)
    {
        this.HP = HP; 
        this.Defense = Defense;
        this.Attack = Attack;
        this.MagicalAttack = MagicalAttack;
        this.Speed = Speed;
        this.skillSets = skillsets;
        this.heroItems = heroitems;
        this.Exp = Exp;
        this.Level = Level;
    }
}


public class GameData : MonoBehaviour
{
    public PathData[] pathData;

    [Header("히어로 데이터들")]
    [Space(15f)]
    public HeroData healerData;
    public HeroData tankerData;
    public HeroData dealerData;

    public float[] ExperienceTable = { 300f, 400f, 500f, 600f, 700f, 800f, 900f, 1000f };

    private void Awake()
    {
        Init();

        //        }
    }

    void Init()
    {

        //히어로 스탯 초기화 나중에 상수 => 변수로 바꾸기

        healerData = new HeroData(160, 
                                  16, 
                                  24, 
                                  16, 
                                  8, 
                                  new SKILLSET[3] { SKILLSET.RAISE, SKILLSET.SCOURGE , SKILLSET.MULTIPLE_HEAL, },
                                  null,
                                  0,
                                  1
                                  );

        tankerData = new HeroData(300,
                                  36,
                                  15,
                                  45,
                                  15,
                                  new SKILLSET[3] { SKILLSET.ROAR, SKILLSET.TAUNT,SKILLSET.BUSTERCALL },
                                  null,
                                  0,
                                  1);
        List<HeroItem> item = new List<HeroItem>();
        item.Add(GameInstance.Instance.ItemDatas["KitchenKnife"]);
        item.Add(GameInstance.Instance.ItemDatas["ClothArmor"]);

        dealerData = new HeroData(200,
                          500,
                          20,
                          20,
                          20,
                          new SKILLSET[3] { SKILLSET.SLASH, SKILLSET.TRANCE_SWORD, SKILLSET.SLASHBURST },
                          item,
                          0,
                          1);
        //tankerData = new HeroData();
        //dealerData = new HeroData();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
