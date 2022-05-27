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
    List<MonsterData> m_MonsterData;

    public PlatformScript m_PlatformScript;
    public List<SMSlot> m_SMSlot = new List<SMSlot>();
    public int nSlotIndex;
    public bool nSlotEnd;

    public StageMonsterSlot[] Slots;
    public StageMonsterSlot[] GetSlots() { return Slots; }

    private void Start()
    {
        m_DataManager = GameObject.FindGameObjectWithTag("Info").GetComponent<DataManager>();
        m_PlayerData = m_PlatformScript.m_PlayerData;
        m_MonsterData = m_PlatformScript.m_MonsterData;
    }

    void ReadSetSlot(int _level)
    {
        nSlotIndex = 0;
        nSlotEnd = false;

        //TextAsset textAsset = Resources.Load<TextAsset>($"Stage" /{})
    }

    static string GetFileName(int nMonster)
    {
        return string.Format("stage_{0:D4}", nMonster);
    }

    public void SetSlotMonster()
    {
        //int nMonsterIndex = 0;
        //for (int i = 0; i < int.Parse())
        //    switch (m_SMSlot[nSlotIndex].type)
        //    {
        //        case "N0000":
        //            nMonsterIndex = 0;
        //            break;
        //        case "N0001":
        //            nMonsterIndex = 1;
        //            break;
        //        case "N0002":
        //            nMonsterIndex = 1;
        //            break;
        //        case "N0003":
        //            nMonsterIndex = 1;
        //            break;
        //        case "N0004":
        //            nMonsterIndex = 1;
        //            break;
        //        case "N0005":
        //            nMonsterIndex = 1;
        //            break;
        //        case "N0006":
        //            nMonsterIndex = 1;
        //            break;
        //        case "N0007":
        //            nMonsterIndex = 1;
        //            break;
        //        case "N0008":
        //            nMonsterIndex = 1;
        //            break;
        //        case "N0009":
        //            nMonsterIndex = 1;
        //            break;
        //        case "N0010":
        //            nMonsterIndex = 1;
        //            break;
        //        case "N0011":
        //            nMonsterIndex = 1;
        //            break;
        //        case "N0012":
        //            nMonsterIndex = 1;
        //            break;
        //        case "N0013":
        //            nMonsterIndex = 1;
        //            break;
        //        case "N0014":
        //            nMonsterIndex = 1;
        //            break;
        //        case "N0015":
        //            nMonsterIndex = 1;
        //            break;
        //        case "N0016":
        //            nMonsterIndex = 1;
        //            break;
        //        case "N0017":
        //            nMonsterIndex = 1;
        //            break;

        //        default: m_SMSlot[nSlotIndex].type = "N0000"; nMonsterIndex = 0; break;
        //    }

        //Slots[]
    }
}
