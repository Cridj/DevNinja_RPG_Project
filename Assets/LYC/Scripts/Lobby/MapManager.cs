using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Linq;

public class MapMonsterSlot
{
    public string type;
}

public class MapManager : MonoBehaviour
{
    [SerializeField]
    DataManager m_DataManager;

    public PlatformScript m_PlatformScript;

    [SerializeField]
    PlayerData m_PlayerData;

    [SerializeField]
    List<MapData> m_MapData;

    [SerializeField]
    List<MonsterData> m_MonsterData;

    public Image Map_Img;
    public Text MapName_Txt;
    public Text MapExp_Txt;

    public List<MapMonsterSlot> MonsterSlotList = new List<MapMonsterSlot>();
    public int nSlotIndex;
    public bool nSlotEnd;

    public MapSlot[] Slots;
    public MapSlot[] Getslots() { return Slots; }

    // Start is called before the first frame update
    void Start()
    {
        m_DataManager = GameObject.FindGameObjectWithTag("Info").GetComponent<DataManager>();
        m_PlayerData = m_PlatformScript.m_PlayerData;
        m_MapData = m_PlatformScript.m_MapData.ToList();
        m_MonsterData = m_PlatformScript.m_MonsterData.ToList();

        if(m_PlayerData.nCurStage == 0||m_PlayerData.nStageProcess == 0)
        {
            MapName_Txt.text = "시작 스테이지";
        }
        else
        {
            MapName_Txt.text = (m_PlayerData.nCurStage) + "-" + (m_PlayerData.nStageProcess) + " 스테이지";
        }

        Map_Img.sprite = m_DataManager.Maps_Spr[m_PlayerData.nCurStage];
        MapExp_Txt.text = m_MapData[m_PlayerData.nCurStage].sExplain;
        ReadSetSlot();
    }

    void ReadSetSlot()
    {
        nSlotIndex = 0;
        nSlotEnd = false;

        TextAsset textAsset = Resources.Load<TextAsset>($"Monster/{GetFileName(m_PlayerData.nCurStage)}") as TextAsset;
        if (textAsset == null)
        {
            return;
        }
        StringReader stringReader = new StringReader(textAsset.text);

        while (stringReader != null)
        {

            string line = stringReader.ReadLine();

            if (line == null)
            {
                break;
            }

            MapMonsterSlot mapmonsterData = new MapMonsterSlot();
            mapmonsterData.type = line.Split(',')[0];
            MonsterSlotList.Add(mapmonsterData);
        }

        SetSlotMonster();

    }

    static string GetFileName(int nMonster)
    {
        return string.Format("monster_{0:D4}", nMonster);
    }

    public void SetSlotMonster()
    {

        int nMonsterIndex = 0;
        for(int i = 0; i < int.Parse(m_MapData[m_PlayerData.nCurStage].nNum); i++)
        {
            switch (MonsterSlotList[nSlotIndex].type)
            {
                case "N0000":
                    nMonsterIndex = 0;
                    break;
                case "N0001":
                    nMonsterIndex = 1;
                    break;
                case "N0002":
                    nMonsterIndex = 2;
                    break;

                default: MonsterSlotList[nSlotIndex].type = "N0000"; nMonsterIndex = 0; break;
            }

            Debug.Log(nMonsterIndex);

            Slots[i].UnlockSlot(nMonsterIndex, m_MonsterData[i].bUnlock);
            Slots[i].gameObject.SetActive(true);
            nSlotIndex++;
        }       

    }

    public void MapReset()
    {
        m_PlayerData.nCoin = 1000;
        m_PlayerData.nStack = 0;
        m_PlayerData.nCurStage = 0;
        m_PlayerData.nStageProcess = 0;
        m_PlayerData.nLevelProcess = 0;
        
        for(int i = 0; i < m_PlayerData.bNodeClear.Length; i++)
        {
            if(m_PlayerData.bNodeClear[i]==false)
            {
                break;
            }

            m_PlayerData.bNodeClear[i] = false;
        }

        for (int i = 0; i < m_PlayerData.nCharIndex.Length; i++)
        {
            if(m_PlayerData.nCharIndex[i]==null)
            {
                break;
            }

            m_PlayerData.nLevel[i] = 1;
            m_PlayerData.fExp[i] = 0;
            m_PlayerData.fHealth[i] = float.Parse(m_PlatformScript.m_CharacterData[int.Parse(m_PlayerData.nCharIndex[i])].fHealth);
            m_PlayerData.fAttack[i] = float.Parse(m_PlatformScript.m_CharacterData[int.Parse(m_PlayerData.nCharIndex[i])].fAttack);
            m_PlayerData.fMagic[i] = float.Parse(m_PlatformScript.m_CharacterData[int.Parse(m_PlayerData.nCharIndex[i])].fMagic);
            m_PlayerData.fDefense[i] = float.Parse(m_PlatformScript.m_CharacterData[int.Parse(m_PlayerData.nCharIndex[i])].fDefense);
            m_PlayerData.fSpeed[i] = float.Parse(m_PlatformScript.m_CharacterData[int.Parse(m_PlayerData.nCharIndex[i])].fSpeed);
        }

        for (int i = 0; i<m_PlayerData.sItemName.Length; i++)
        {
            if(m_PlayerData.nItemIndex[i]==null)
            {
                break;
            }

            m_PlayerData.sItemName[i] = null;
            m_PlayerData.nItemIndex[i] = null;
            
        }

        if(m_PlayerData.bTutorialClear==true)
        {
            m_PlayerData.nCurStage = 1;
            m_PlayerData.nStageProcess = 1;
            m_PlayerData.nLevelProcess = 1;
            m_PlayerData.bNodeClear[0] = true;
        }
        

        Map_Img.sprite = m_DataManager.Maps_Spr[m_PlayerData.nCurStage];
        MapName_Txt.text = "시작 스테이지";
        MapExp_Txt.text = m_MapData[m_PlayerData.nCurStage].sExplain;

        m_DataManager.SavePlayerDataToJson();
    }
}
