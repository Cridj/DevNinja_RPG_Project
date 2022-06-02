using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class SMSlot
{
    public string type;
}

public class StageInfoManager : MonoBehaviour
{
    [SerializeField]
    DataManager m_DataManager;

    [SerializeField]
    PlayerData m_PlayerData;

    [SerializeField]
    List<StageData> m_StageData;

    [SerializeField]
    List<MonsterData> m_MonsterData;

    public PlatformScript m_PlatformScript;
    public List<SMSlot> m_SMSlot = new List<SMSlot>();
    public int nSlotIndex;
    public bool nSlotEnd;

    public List<int> nSaveNum;

    public StageMonsterSlot[] Slots;
    public StageMonsterSlot[] GetSlots() { return Slots; }

    private void Start()
    {
        m_DataManager = GameObject.FindGameObjectWithTag("Info").GetComponent<DataManager>();
        m_PlayerData = m_PlatformScript.m_PlayerData;
        m_StageData = m_PlatformScript.m_StageData;
        m_MonsterData = m_PlatformScript.m_MonsterData;
    }

    public void ReadSetSlot(int _level)
    {
        nSaveNum.Clear();

        int nmonlist = GameInstance.Instance.MonsterSpawnList[_level].EnemyList.Length;
        int nMonsterIndex = 0;
        for(int i = 0; i < nmonlist; i++)
        {
            switch(GameInstance.Instance.MonsterSpawnList[_level].EnemyList[i])
            {
                case ENEMY_TYPE.WOLF:
                    nMonsterIndex = 0;
                    break;
                case ENEMY_TYPE.BOAR:
                    nMonsterIndex = 1;
                    break;
                case ENEMY_TYPE.ELITEWOLF:
                    nMonsterIndex = 2;
                    break;
                case ENEMY_TYPE.BAT:
                    nMonsterIndex = 3;
                    break;
                case ENEMY_TYPE.BEAR:
                    nMonsterIndex = 4;
                    break;
                case ENEMY_TYPE.BLACKBEAR:
                    nMonsterIndex = 5;
                    break;
                case ENEMY_TYPE.INFECTED_RAT:
                    nMonsterIndex = 6;
                    break;
                case ENEMY_TYPE.RAT:
                    nMonsterIndex = 7;
                    break;
                case ENEMY_TYPE.RAT_KING:
                    nMonsterIndex = 8;
                    break;
                case ENEMY_TYPE.SKELETON:
                    nMonsterIndex = 9;
                    break;
                case ENEMY_TYPE.SKELETON_ARCHER:
                    nMonsterIndex = 10;
                    break;
                case ENEMY_TYPE.SKELETON_ELITE:
                    nMonsterIndex = 11;
                    break;
                case ENEMY_TYPE.SKELETON_EMPERER:
                    nMonsterIndex = 12;
                    break;
                case ENEMY_TYPE.SKELETON_MAGICIAN:
                    nMonsterIndex = 13;
                    break;
                case ENEMY_TYPE.CANNIBAL:
                    nMonsterIndex = 14;
                    break;
                case ENEMY_TYPE.DREADNOUGHT:
                    nMonsterIndex = 15;
                    break;
                case ENEMY_TYPE.JUGGERNAUT:
                    nMonsterIndex = 16;
                    break;
                case ENEMY_TYPE.MANEATER:
                    nMonsterIndex = 17;
                    break;
                case ENEMY_TYPE.OGRE:
                    nMonsterIndex = 18;
                    break;
                case ENEMY_TYPE.TROLL:
                    nMonsterIndex = 19; 
                    break;
                case ENEMY_TYPE.MAGICIANTROLL:
                    nMonsterIndex = 20;
                    break;      
                default:
                    nMonsterIndex = 20;
                    break;
            }
            Slots[i].UnlockSlot(nMonsterIndex, m_MonsterData[nMonsterIndex].bUnlock);
            Slots[i].gameObject.SetActive(true);

        }
    }

    public void GetTrue()
    {
        for(int i = 0; i < nSaveNum.Count; i++)
        {
            m_DataManager.MyMonsterList[nSaveNum[i]].bUnlock = true;
        }

        m_DataManager.Save();
    }

    //public void ReadSetSlot(int _level)
    //{
    //    m_PlayerData.nLevelProcess = _level;
    //    nSlotIndex = 0;
    //    nSlotEnd = false;

    //    TextAsset textAsset = Resources.Load<TextAsset>($"Stage/{GetFileName(m_PlayerData.nLevelProcess)}") as TextAsset;
    //    if(textAsset == null)
    //    {
    //        return;
    //    }
    //    StringReader stringReader = new StringReader(textAsset.text);

    //    while(stringReader != null)
    //    {
    //        string line = stringReader.ReadLine();

    //        if(line==null)
    //        {
    //            break;
    //        }

    //        SMSlot smslotData = new SMSlot();
    //        smslotData.type = line.Split(',')[0];
    //        m_SMSlot.Add(smslotData);
    //    }

    //    SetSlotMonster();
    //}

    //static string GetFileName(int nMonster)
    //{
    //    return string.Format("stage_{0:D4}", nMonster);
    //}

    //public void SetSlotMonster()
    //{
    //    int nMonsterIndex = 0;
    //    for (int i = 0; i < int.Parse(m_StageData[m_PlayerData.nLevelProcess].nNum); i++)
    //    {
    //        switch (m_SMSlot[nSlotIndex].type)
    //        {
    //            case "N0000":
    //                nMonsterIndex = 0;
    //                break;
    //            case "N0001":
    //                nMonsterIndex = 1;
    //                break;
    //            case "N0002":
    //                nMonsterIndex = 2;
    //                break;
    //            case "N0003":
    //                nMonsterIndex = 3;
    //                break;
    //            case "N0004":
    //                nMonsterIndex = 4;
    //                break;
    //            case "N0005":
    //                nMonsterIndex = 5;
    //                break;
    //            case "N0006":
    //                nMonsterIndex = 6;
    //                break;
    //            case "N0007":
    //                nMonsterIndex = 7;
    //                break;
    //            case "N0008":
    //                nMonsterIndex = 8;
    //                break;
    //            case "N0009":
    //                nMonsterIndex = 9;
    //                break;
    //            case "N0010":
    //                nMonsterIndex = 10;
    //                break;
    //            case "N0011":
    //                nMonsterIndex = 11;
    //                break;
    //            case "N0012":
    //                nMonsterIndex = 12;
    //                break;
    //            case "N0013":
    //                nMonsterIndex = 13;
    //                break;
    //            case "N0014":
    //                nMonsterIndex = 14;
    //                break;
    //            case "N0015":
    //                nMonsterIndex = 15;
    //                break;
    //            case "N0016":
    //                nMonsterIndex = 16;
    //                break;
    //            case "N0017":
    //                nMonsterIndex = 17;
    //                break;

    //            default: m_SMSlot[nSlotIndex].type = "N0000"; nMonsterIndex = 0; break;
    //        }
    //        Slots[i].UnlockSlot(nMonsterIndex, m_MonsterData[i].bUnlock);
    //        Slots[i].gameObject.SetActive(true);
    //        nSlotIndex++;
    //    }

    //}
}
