using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageMonsterSlot : MonoBehaviour
{
    [SerializeField]
    DataManager m_DataManager;

    public PlatformScript m_PlatformScript;
    public List<MonsterData> m_MonsterData;
    public StageInfoManager m_StageInfoManager;
    public Image Mons_Img;
    public Text Mons_Txt;
    public GameObject MonsInfo_Obj;
    public Text MonsName_Txt;
    public Text MonsInfo_Txt;

    public int nIndex;
    public bool bUnlock;

    public void UnlockSlot(int _nIndex, bool _bUnlock)
    {
        m_DataManager = GameObject.FindGameObjectWithTag("Info").GetComponent<DataManager>();
        m_MonsterData = m_PlatformScript.m_MonsterData;

        nIndex = _nIndex;
        bUnlock = _bUnlock;

        if (_bUnlock == true)
        {
            Mons_Img.sprite = m_DataManager.Mons_Spr[nIndex];
            Mons_Img.gameObject.SetActive(true);
            Mons_Txt.text = m_MonsterData[nIndex].sName;
            
        }
        else
        {
            Mons_Img.sprite = m_DataManager.Unknown_Spr;
            Mons_Img.gameObject.SetActive(true);
            Mons_Txt.text = "???";
            
        }
    }
    
    public void DescOn()
    {
        MonsInfo_Obj.SetActive(true);

        if (bUnlock == true)
        {
            MonsName_Txt.text = m_MonsterData[nIndex].sName;
            MonsInfo_Txt.text = "랭크 : " + m_MonsterData[nIndex].sRank + "\n" +
                "타입 : " + m_MonsterData[nIndex].sType + "\n" +
                "체력 : " + m_MonsterData[nIndex].fHealth + "\n" +
                "공격력 : " + m_MonsterData[nIndex].fAttack + "\n" +
                "주문력 : " + m_MonsterData[nIndex].fMagic + "\n" +
                "방어력 : " + m_MonsterData[nIndex].fDefense + "\n" +
                "행동력 : " + m_MonsterData[nIndex].fSpeed;
        }
        else
        {
            MonsName_Txt.text = "???";
            MonsInfo_Txt.text = "랭크 : ???" + "\n" + "타입 : ???" + "\n" + "체력 : ???" + "\n" + "공격력 : ???" + "\n" +
                "주문력 : ???" + "\n" + "방어력 : ???" + "\n" + "행동력 : ???";
        }       
    }

    public void DescOff()
    {
        MonsInfo_Obj.SetActive(false);
    }
}
