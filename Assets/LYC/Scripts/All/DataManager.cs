using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

//플레이어 정보 json
[System.Serializable]
public class PlayerData
{
    [Header("플레이어 데이터")]

    //[Header("돈")]
    public int nCoin;

    //[Header("상점스택")]
    public int nStack;

    //[Header("튜토리얼 클리어 여부")]
    public bool bTutorialClear;

    //[Header("진행 노드 *ex)1-'4'")]
    public int nCurStage;

    //[Header("진행 스테이지 *ex)'1'-4 ")]
    public int nStageProcess;

    //[Header("실제 노드 진행 * 26번째 노드")]
    public int nLevelProcess;

    //[Header("노드 클리어 했는지.")]
    public bool[] bNodeClear = new bool[33];

    [Space(1)]

    //[Header("캐릭터들 인덱스 넘버")]
    public string[] nCharIndex = new string[15];

    //[Header("캐릭터들 랭크")]
    public int[] nRank = new int[15];

    //[Header("캐릭터들 레벨")]
    public int[] nLevel = new int[15];

    //[Header("캐릭터들 경험치")]
    public float[] fExp = new float[15];

    //[Header("캐릭터들 체력치")]
    public float[] fHealth = new float[15];

    //[Header("캐릭터들 공격치")]
    public float[] fAttack = new float[15];

    //[Header("캐릭터들 주문치")]
    public float[] fMagic = new float[15];

    //[Header("캐릭터들 방어치")]
    public float[] fDefense = new float[15];

    //[Header("캐릭터들 행동치")]
    public float[] fSpeed = new float[15];

    [Space(1)]

    //[Header("아이템들 인덱스")]
    public string[] nItemIndex = new string[100];

    //[Header("아이템들 이름 (체크용)")]
    public string[] sItemName = new string[100];
}

//옵션 데이터 정보 json
[System.Serializable]
public class OptionData
{
    [Header("옵션 데이터")]
    public float fMsValue;
    public float fMsPrev;
    public float fBGMsValue;
    public float fBGMsPrev;
    public float fGEsValue;
    public float fGEsPrev;
    public float fUIEsValue;
    public float fUIEsPrev;
    public bool bMsMute;
    public bool bBGMsMute;
    public bool bGEsMute;
    public bool bUIEsMute;
}

[System.Serializable]
public class MapData
{
    public string nNum, sExplain;

    public MapData(string _nNum, string _sExplain)
    {
        nNum = _nNum; sExplain = _sExplain;
    }
}

//캐릭터 데이터 정보 textasset
[System.Serializable]
public class CharacterData
{
    [Header("캐릭터 데이터")]
    public string nIndex, sName, sType, sExplain, nRank, nLevel, fEXP, fHealth, fAttack, fMagic, fDefense, fSpeed, fGHealth, fGAttack, fGMagic, fGDefense, fGSpeed;
    public bool bUnlock;

    public CharacterData(string _nIndex, string _sName, string _sType, string _sExplain, string _nRank, string _nLevel, string _fEXP, string _fHealth, string _fAttack, string _fMagic, string _fDefense, string _fSpeed, string _fGHealth, string _fGAttack, string _fGMagic, string _fGDefense, string _fGSpeed, bool _bUnlock)
    {
        nIndex = _nIndex; sName = _sName; sType = _sType; sExplain = _sExplain; nRank = _nRank; nLevel = _nLevel; fEXP = _fEXP; fHealth = _fHealth; fAttack = _fAttack; fMagic = _fMagic;
        fDefense = _fDefense; fSpeed = _fSpeed; fGHealth = _fGHealth; fGAttack = _fGAttack; fGMagic = _fGMagic; fGDefense = _fGDefense; fGSpeed = _fGSpeed; bUnlock = _bUnlock;
    }
}

[System.Serializable]
public class CharacterSkillData
{
    [Header("캐릭터 스킬 데이터")]
    public string nIndex;
    public string[] nNum, sName, sExplain;

    public CharacterSkillData(string _nIndex, string[] _nNum, string[] _sName, string[] _sExplain)
    {
        nIndex = _nIndex; nNum = _nNum; sName = _sName; sExplain = _sExplain;
    }
}

[System.Serializable]
public class CharacterSkillSprite
{
    [Header("캐릭터 스킬 이미지")]
    public Sprite[] CharSkillSpr;
}

//몬스터 데이터 정보 textasset
[System.Serializable]
public class MonsterData
{
    [Header("몬스터 데이터")]
    public string nIndex, sName, sType, sExplain, sRank, fHealth, fAttack, fMagic, fDefense, fSpeed, fExp;
    public bool bUnlock;

    public MonsterData(string _nIndex, string _sName, string _sType, string _sExplain, string _sRank, string _fHealth, string _fAttack, string _fMagic, string _fDefense, string _fSpeed, string _fExp, bool _bUnlock)
    {
        nIndex = _nIndex; sName = _sName; sType = _sType; sExplain = _sExplain; sRank = _sRank; fHealth = _fHealth; fAttack = _fAttack; fMagic = _fMagic; fDefense = _fDefense; fSpeed = _fSpeed; fExp = _fExp; bUnlock = _bUnlock;
    }
}

[System.Serializable]
public class MonsterSkillData
{
    [Header("몬스터 스킬 데이터")]
    public string nIndex;
    public string[] nNum, sName, sExplain;

    public MonsterSkillData(string _nIndex, string[] _nNum, string[] _sName, string[] _sExplain)
    {
        nIndex = _nIndex; nNum = _nNum; sName = _sName; sExplain = _sExplain;
    }
}

[System.Serializable]
public class MonsterSkillSprite
{
    [Header("몬스터 스킬 이미지")]
    public Sprite[] MonsSkillSpr;
}

//아이템 데이터 정보 textasset
[System.Serializable]
public class ItemData
{
    [Header("아이템 데이터")]
    public string nIndex, sName, sType, nCost, sExplain, sRank, fHealth, fAttack, fMagic, fDefense, fSpeed;
    public bool bUnlock;

    public ItemData(string _nIndex, string _sName, string _sType, string _nCost, string _sExplain, string _sRank, string _fHealth, string _fAttack, string _fMagic, string _fDefense, string _fSpeed, bool _bUnlock)
    {
        nIndex = _nIndex; sName = _sName; sType = _sType; nCost = _nCost; sExplain = _sExplain; sRank = _sRank; fHealth = _fHealth; fAttack = _fAttack; fMagic = _fMagic; fSpeed = _fSpeed; bUnlock = _bUnlock;
    }
}

public class DataManager : MonoBehaviour
{
    [SerializeField]
    PlatformScript m_PlatformScript;

    //[Header("플레이어 정보")]
    public PlayerData m_PlayerData;

    //[Header("옵션 정보")]
    public OptionData m_OptionData;

    //[Header("맵 정보")]
    public List<MapData> MyMapList;

    //[Header("캐릭터 기본 정보")]
    public List<CharacterData> MyCharacterList;

    //[Header("캐릭터 스킬 기본 정보")]
    public List<CharacterSkillData> MyCharacterSkillList;

    //[Header("몬스터 기본 정보")]
    public List<MonsterData> MyMonsterList;

    //[Header("몬스터 스킬 기본 정보")]
    public List<MonsterSkillData> MyMonsterSkillList;

    //[Header("아이템 기본 정보")]
    public List<ItemData> MyItemList;

    //[Header("스프라이트 정보")]
    public Sprite[] Maps_Spr;
    public Sprite[] Char_Spr;
    public Sprite[] Mons_Spr;
    public Sprite[] Item_Spr;
    public Sprite Unknown_Spr;

    public CharacterSkillSprite[] CharSkill_Spr;
    public MonsterSkillSprite[] MonsSkill_Spr;

    public TextAsset MapDataBase_TA;
    private List<MapData> m_MapData;
    public TextAsset CharacterDataBase_TA;
    private List<CharacterData> m_CharacterData;
    //public TextAsset CharacterSkillDataBase_TA;
    private List<CharacterSkillData> m_CharacterSkillData;
    public TextAsset MonsterDataBase_TA;
    private List<MonsterData> m_MonsterData;
    //public TextAsset MonsterSkillDataBase_TA;
    private List<MonsterSkillData> m_MonsterSkillData;
    public TextAsset ItemDataBase_TA;
    private List<ItemData> m_ItemData;

    private static DataManager instance = null;

    public static DataManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        //로드하기
        LoadListDataFromJson();
        LoadPlayerDataFromJson();
        LoadOptionDataFromJson();


        if(GameObject.FindGameObjectWithTag("PlatformCS").GetComponent<PlatformScript>())
        {
            m_PlatformScript = GameObject.FindGameObjectWithTag("PlatformCS").GetComponent<PlatformScript>();

            m_PlatformScript.m_MapData = MyMapList;
            m_PlatformScript.m_PlayerData = m_PlayerData;
            m_PlatformScript.m_OptionData = m_OptionData;
            m_PlatformScript.m_CharacterData = MyCharacterList;
            m_PlatformScript.m_CharacterSkillData = MyCharacterSkillList;
            m_PlatformScript.m_MonsterData = MyMonsterList;
            m_PlatformScript.m_MonsterSkillData = MyMonsterSkillList;
            m_PlatformScript.m_ItemData = MyItemList;
        }
    }

    public void Save()
    {
        SaveListDataToJson();
        SavePlayerDataToJson();
        SaveOptionDataToJson();
    }

    [ContextMenu("Load CSV")]
    public void LoadCSV()
    {
        string[] mapLine = MapDataBase_TA.text.Substring(0, MapDataBase_TA.text.Length - 1).Split("\n");
        for (int i = 0; i < mapLine.Length; i++)
        {
            string[] row = mapLine[i].Split('\t');

            m_MapData.Add(new MapData(row[0], row[1]));
        }

        string[] characterLine = CharacterDataBase_TA.text.Substring(0, CharacterDataBase_TA.text.Length - 1).Split("\n");
        for (int i = 0; i < characterLine.Length; i++)
        {
            string[] row = characterLine[i].Split('\t');

            m_CharacterData.Add(new CharacterData(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7], row[8], row[9], row[10], row[11], row[12], row[13], row[14], row[15], row[16], row[17] == "FALSE"));
        }

        string[] monsterLine = MonsterDataBase_TA.text.Substring(0, MonsterDataBase_TA.text.Length - 1).Split("\n");
        for (int i = 0; i < monsterLine.Length; i++)
        {
            string[] row = monsterLine[i].Split('\t');

            m_MonsterData.Add(new MonsterData(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7], row[8], row[9], row[10], row[11] == "FALSE"));
        }

        string[] itemLine = ItemDataBase_TA.text.Substring(0, ItemDataBase_TA.text.Length - 1).Split("\n");
        for (int i = 0; i < itemLine.Length; i++)
        {
            string[] row = itemLine[i].Split('\t');

            m_ItemData.Add(new ItemData(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7], row[8], row[9], row[10], row[11] == "FALSE"));
        }
    }
    
    [ContextMenu("Save Json PlayerData")]
    public void SavePlayerDataToJson()
    {
        string jsonData = JsonUtility.ToJson(m_PlayerData, true);
#if UNITY_EDITOR
        string path = Path.Combine(Application.dataPath, "Resources/PlayerData.json");
#elif UNITY_ANDROID
        string path = Path.Combine(Application.persistentDataPath, "Resources/PlayerData.json");
#endif
        File.WriteAllText(path, jsonData);
    }

    [ContextMenu("Load Json PlayerData")]
    public void LoadPlayerDataFromJson()
    {
#if UNITY_EDITOR
        string path = Path.Combine(Application.dataPath, "Resources/PlayerData.json");
#elif UNITY_ANDROID
        string path = Path.Combine(Application.persistentDataPath, "Resources/PlayerData.json");
#endif
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            m_PlayerData = JsonUtility.FromJson<PlayerData>(jsonData);
        }
        else
        {
            m_PlayerData = new PlayerData();
        }

        if(m_PlayerData.nCharIndex[0] == "" || m_PlayerData.nCharIndex[0] == null)
        {
            m_PlayerData.nCoin = 1000;

            for (int i = 0; i < 3; i++)
            {               
                m_PlayerData.nCharIndex[i] = i.ToString();
                Debug.Log(MyCharacterList[i].nIndex);
                MyCharacterList[i].bUnlock = true;
                m_PlayerData.nRank[i] = 1;
                m_PlayerData.nLevel[i] = 1;
                m_PlayerData.fExp[i] = 0;
                m_PlayerData.fHealth[i] = float.Parse(MyCharacterList[i].fHealth);
                m_PlayerData.fAttack[i] = float.Parse(MyCharacterList[i].fAttack);
                m_PlayerData.fMagic[i] = float.Parse(MyCharacterList[i].fMagic);
                m_PlayerData.fDefense[i] = float.Parse(MyCharacterList[i].fDefense);
                m_PlayerData.fSpeed[i] = float.Parse(MyCharacterList[i].fSpeed);
            }
        }
    }

    [ContextMenu("Save Json OptionData")]
    public void SaveOptionDataToJson()
    {
        string jsonData = JsonUtility.ToJson(m_OptionData, true);
#if UNITY_EDITOR
        string path = Path.Combine(Application.dataPath, "Resources/OptionData.json");
#elif UNITY_ANDROID
         string path = Path.Combine(Application.persistentDataPath, "Resources/OptionData.json");
#endif
        File.WriteAllText(path, jsonData);
    }

    [ContextMenu("Load Json OptionData")]
    public void LoadOptionDataFromJson()
    {
#if UNITY_EDITOR
        string path = Path.Combine(Application.dataPath, "Resources/OptionData.json");
#elif UNITY_ANDROID
        string path = Path.Combine(Application.persistentDataPath, "Resources/OptionData.json");
#endif
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            m_OptionData = JsonUtility.FromJson<OptionData>(jsonData);
        }
        else
        {
            m_OptionData = new OptionData();
        }
    }

    [ContextMenu("Save CSV Data")]
    public void SaveCSVData()
    {
        string mapdata = JsonConvert.SerializeObject(m_MapData);
#if UNITY_EDITOR
        string mappath = Path.Combine(Application.dataPath, "Resources/MyMapData.txt");
#elif UNITY_ANDROID
            string mappath = Path.Combine(Application.persistentDataPath, "Resources/MyMapData.txt");
#endif
        File.WriteAllText(mappath, mapdata);

        string chardata = JsonConvert.SerializeObject(m_CharacterData); //캐릭터
#if UNITY_EDITOR
        string charpath = Path.Combine(Application.dataPath, "Resources/MyCharacterData.txt");
#elif UNITY_ANDROID
            string charpath = Path.Combine(Application.persistentDataPath, "Resources/MyCharacterData.txt");
#endif
        File.WriteAllText(charpath, chardata);

        string charskilldata = JsonConvert.SerializeObject(MyCharacterSkillList); //캐릭터스킬
#if UNITY_EDITOR
        string charskillpath = Path.Combine(Application.dataPath, "Resources/MyCharacterSkillData.txt");
#elif UNITY_ANDROID
            string charskillpath = Path.Combine(Application.persistentDataPath, "Resources/MyCharacterSkillData.txt");
#endif
        File.WriteAllText(charskillpath, charskilldata);

        string monsdata = JsonConvert.SerializeObject(m_MonsterData); //몬스터
#if UNITY_EDITOR
        string monspath = Path.Combine(Application.dataPath, "Resources/MyMonsterData.txt");
#elif UNITY_ANDROID
            string monspath = Path.Combine(Application.persistentDataPath, "Resources/MyMonsterData.txt");
#endif
        File.WriteAllText(monspath, monsdata);

        string monsskilldata = JsonConvert.SerializeObject(MyMonsterSkillList); //몬스터스킬
#if UNITY_EDITOR
        string monsskillpath = Path.Combine(Application.dataPath, "Resources/MyMonsterSkillData.txt");
#elif UNITY_ANDROID
            string monsskillpath = Path.Combine(Application.persistentDataPath, "Resources/MyMonsterSkillData.txt");
#endif
        File.WriteAllText(monsskillpath, monsskilldata);

        string itemdata = JsonConvert.SerializeObject(m_ItemData); //아이템
#if UNITY_EDITOR
        string itempath = Path.Combine(Application.dataPath, "Resources/MyItemData.txt");
#elif UNITY_ANDROID
            string itempath = Path.Combine(Application.persistentDataPath, "Resources/MyItemData.txt");
#endif
        File.WriteAllText(itempath, itemdata);
    }

    [ContextMenu("Save Json ListData")]
    public void SaveListDataToJson()
    {
        string mapdata = JsonConvert.SerializeObject(MyMapList);
#if UNITY_EDITOR
        string mappath = Path.Combine(Application.dataPath, "Resources/MyMapData.txt");
#elif UNITY_ANDROID
            string mappath = Path.Combine(Application.persistentDataPath, "Resources/MyMapData.txt");
#endif
        File.WriteAllText(mappath, mapdata);

        string chardata = JsonConvert.SerializeObject(MyCharacterList); //캐릭터
#if UNITY_EDITOR
        string charpath = Path.Combine(Application.dataPath, "Resources/MyCharacterData.txt");
#elif UNITY_ANDROID
            string charpath = Path.Combine(Application.persistentDataPath, "Resources/MyCharacterData.txt");
#endif
        File.WriteAllText(charpath, chardata);

        string charskilldata = JsonConvert.SerializeObject(MyCharacterSkillList); //캐릭터스킬
#if UNITY_EDITOR
        string charskillpath = Path.Combine(Application.dataPath, "Resources/MyCharacterSkillData.txt");
#elif UNITY_ANDROID
            string charskillpath = Path.Combine(Application.persistentDataPath, "Resources/MyCharacterSkillData.txt");
#endif
        File.WriteAllText(charskillpath, charskilldata);

        string monsdata = JsonConvert.SerializeObject(MyMonsterList); //몬스터
#if UNITY_EDITOR
        string monspath = Path.Combine(Application.dataPath, "Resources/MyMonsterData.txt");
#elif UNITY_ANDROID
            string monspath = Path.Combine(Application.persistentDataPath, "Resources/MyMonsterData.txt");
#endif
        File.WriteAllText(monspath, monsdata);

        string monsskilldata = JsonConvert.SerializeObject(MyMonsterSkillList); //몬스터스킬
#if UNITY_EDITOR
        string monsskillpath = Path.Combine(Application.dataPath, "Resources/MyMonsterSkillData.txt");
#elif UNITY_ANDROID
            string monsskillpath = Path.Combine(Application.persistentDataPath, "Resources/MyMonsterSkillData.txt");
#endif
        File.WriteAllText(monsskillpath, monsskilldata);

        string itemdata = JsonConvert.SerializeObject(MyItemList); //아이템
#if UNITY_EDITOR
        string itempath = Path.Combine(Application.dataPath, "Resources/MyItemData.txt");
#elif UNITY_ANDROID
            string itempath = Path.Combine(Application.persistentDataPath, "Resources/MyItemData.txt");
#endif
        File.WriteAllText(itempath, itemdata);
    }

//    [ContextMenu("Load Json ListData")]
//    public void LoadListDataFromJson()
//    {
//#if UNITY_EDITOR
//        string mappath = Path.Combine(Application.dataPath, "Resources", "MYMapData.txt");
//#elif UNITY_ANDROID
//            string mappath = Path.Combine(Application.persistentDataPath, "MYMapData.txt");
//#endif
//        string mapdata = File.ReadAllText(mappath);

//        MyMapList = JsonConvert.DeserializeObject<List<MapData>>(mapdata);

//#if UNITY_EDITOR
//        string charpath = Path.Combine(Application.dataPath, "Resources", "MYCharacterData.txt");
//#elif UNITY_ANDROID
//            string charpath = Path.Combine(Application.persistentDataPath, "MYCharacterData.txt");
//#endif
//        string chardata = File.ReadAllText(charpath);

//        MyCharacterList = JsonConvert.DeserializeObject<List<CharacterData>>(chardata);

//#if UNITY_EDITOR
//        string charskillpath = Path.Combine(Application.dataPath, "Resources", "MYCharacterSkillData.txt");
//#elif UNITY_ANDROID
//            string charskillpath = Path.Combine(Application.persistentDataPath, "MYCharacterSkillData.txt");
//#endif
//        string charskilldata = File.ReadAllText(charskillpath);

//        MyCharacterSkillList = JsonConvert.DeserializeObject<List<CharacterSkillData>>(charskilldata);

//        m_CharacterSkillData = MyCharacterSkillList;

//#if UNITY_EDITOR
//        string monspath = Path.Combine(Application.dataPath, "Resources", "MYMonsterData.txt");
//#elif UNITY_ANDROID
//            string monspath = Path.Combine(Application.persistentDataPath, "MYMonsterData.txt");
//#endif
//        string monsdata = File.ReadAllText(monspath);

//        MyMonsterList = JsonConvert.DeserializeObject<List<MonsterData>>(monsdata);


//#if UNITY_EDITOR
//        string monsskillpath = Path.Combine(Application.dataPath, "Resources", "MYMonsterSkillData.txt");
//#elif UNITY_ANDROID
//            string monsskillpath = Path.Combine(Application.persistentDataPath, "MYMonsterSkillData.txt");
//#endif
//        string monsskilldata = File.ReadAllText(monsskillpath);

//        MyMonsterSkillList = JsonConvert.DeserializeObject<List<MonsterSkillData>>(monsskilldata);

//        m_MonsterSkillData = MyMonsterSkillList;

//#if UNITY_EDITOR
//        string itempath = Path.Combine(Application.dataPath, "Resources", "MYItemData.txt");
//#elif UNITY_ANDROID
//            string itempath = Path.Combine(Application.persistentDataPath, "MYItemData.txt");
//#endif
//        string itemdata = File.ReadAllText(itempath);

//        MyItemList = JsonConvert.DeserializeObject<List<ItemData>>(itemdata);
//    }

    [ContextMenu("Load Json ListData")]
    public void LoadListDataFromJson()
    {
        TextAsset mappath = Resources.Load<TextAsset>("MyMapData");
        MyMapList = JsonConvert.DeserializeObject<List<MapData>>(mappath.text);

        TextAsset charpath = Resources.Load<TextAsset>("MyCharacterData");
        MyCharacterList = JsonConvert.DeserializeObject<List<CharacterData>>(charpath.text);

        TextAsset charskillpath = Resources.Load<TextAsset>("MyCharacterSkillData");
        MyCharacterSkillList = JsonConvert.DeserializeObject<List<CharacterSkillData>>(charskillpath.text);

        TextAsset monspath = Resources.Load<TextAsset>("MyMonsterData");
        MyMonsterList = JsonConvert.DeserializeObject<List<MonsterData>>(monspath.text);

        TextAsset monsskillpath = Resources.Load<TextAsset>("MyMonsterSkillData");
        MyMonsterSkillList = JsonConvert.DeserializeObject<List<MonsterSkillData>>(monsskillpath.text);

        TextAsset itempath = Resources.Load<TextAsset>("MyItemData");
        MyItemList = JsonConvert.DeserializeObject<List<ItemData>>(itempath.text);
    }
}