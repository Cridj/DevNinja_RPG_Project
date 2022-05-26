using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharSkillInfoSlot : MonoBehaviour
{
    [SerializeField]
    DataManager m_DataManager;
    public PlatformScript m_PlatformScript;
    public PlayerData m_PlayerData;
    public List<CharacterData> m_CharacterData;
    public List<CharacterSkillData> m_CharSkillData;

    public Image Skill_Img;
    public Text Name_Txt;
    public Text Type_Txt;

    public int nIndex;
    public int nNum;

    public void SetSkillSlot(int _index, int _num)
    {
        m_DataManager = GameObject.FindGameObjectWithTag("Info").GetComponent<DataManager>();
        m_PlayerData = m_PlatformScript.m_PlayerData;
        m_CharacterData = m_PlatformScript.m_CharacterData;
        m_CharSkillData = m_PlatformScript.m_CharacterSkillData;

        nIndex = _index;
        nNum = _num;

        Skill_Img.sprite = m_DataManager.CharSkill_Spr[_index].CharSkillSpr[_num];
        Name_Txt.text = m_CharSkillData[_index].sName[_num];
        Type_Txt.text = "е╦ют : " + m_CharacterData[_index].sType;        
    }
}
