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
            MonsInfo_Txt.text = "��ũ : " + m_MonsterData[nIndex].sRank + "\n" +
                "Ÿ�� : " + m_MonsterData[nIndex].sType + "\n" +
                "ü�� : " + m_MonsterData[nIndex].fHealth + "\n" +
                "���ݷ� : " + m_MonsterData[nIndex].fAttack + "\n" +
                "�ֹ��� : " + m_MonsterData[nIndex].fMagic + "\n" +
                "���� : " + m_MonsterData[nIndex].fDefense + "\n" +
                "�ൿ�� : " + m_MonsterData[nIndex].fSpeed;
        }
        else
        {
            MonsName_Txt.text = "???";
            MonsInfo_Txt.text = "��ũ : ???" + "\n" + "Ÿ�� : ???" + "\n" + "ü�� : ???" + "\n" + "���ݷ� : ???" + "\n" +
                "�ֹ��� : ???" + "\n" + "���� : ???" + "\n" + "�ൿ�� : ???";
        }       
    }

    public void DescOff()
    {
        MonsInfo_Obj.SetActive(false);
    }
}
