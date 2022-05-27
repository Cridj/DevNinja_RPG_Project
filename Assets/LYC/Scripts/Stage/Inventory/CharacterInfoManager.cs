using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoManager : MonoBehaviour
{
    [SerializeField]
    DataManager m_DataManager;
    [SerializeField]
    PlayerData m_PlayerData;
    [SerializeField]
    List<CharacterData> m_CharacterData;
    [SerializeField]
    List<CharacterSkillData> m_CharacterSkillData;

    [SerializeField]
    CharInfoSlot[] CharSlots;
    public CharInfoSlot[] GetCharSlots() { return CharSlots; }
    [SerializeField]
    CharSkillInfoSlot[] SkillSlots;
    public CharSkillInfoSlot[] GetSkillSlots() { return SkillSlots; }

    public PlatformScript m_PlatformScript;
    public InventoryManager m_InventoryManager;

    public Image Char_Img;
    public Text Name_Txt;
    public Text Desc_Txt;

    int nIndex;
    int nSlots;


    // Start is called before the first frame update
    void Start()
    {
        m_DataManager = GameObject.FindGameObjectWithTag("Info").GetComponent<DataManager>();
        m_PlayerData = m_PlatformScript.m_PlayerData;
        m_CharacterData = m_PlatformScript.m_CharacterData;
        m_CharacterSkillData = m_PlatformScript.m_CharacterSkillData;
    }

    public void SetCharTap()
    {
        for(int i = 0; i < CharSlots.Length; i++)
        {
            if(int.TryParse(m_PlayerData.nCharIndex[i], out int Result0))
            {
                CharSlots[i].SetCharSlot(Result0, i);
            }
            else
            {
                CharSlots[i].SetCharSlot(i, i);
            }

            if(i==0)
            {
                CharSlots[i].CharSelect();
            }
            else
            {
                CharSlots[i].CharUnSelect();
            }           
        }   
    }

    public void CharacterSelect(int _index, int _slots)
    {
        nIndex = _index;
        nSlots = _slots;

        for(int i = 0; i < CharSlots.Length; i++)
        {
            if(i==_slots)
            {
                CharSlots[i].Char_Btn.colors = CharSlots[i].SelectColors;
                CharSlots[i].NameBG_Img.color = CharSlots[i].SelectColor;
            }
            else
            {
                CharSlots[i].CharUnSelect();
            }
        }

        for(int i = 0; i<SkillSlots.Length; i++)
        {
            SkillSlots[i].gameObject.SetActive(false);
        }

        for(int i = 0; i<m_CharacterSkillData[_index].nNum.Length; i++)
        {
            SkillSlots[i].SetSkillSlot(_index, i);
            SkillSlots[i].gameObject.SetActive(true);
        }

        Char_Img.sprite = m_DataManager.Char_Spr[_index];
        Name_Txt.text = m_CharacterData[_index].sName;
        Desc_Txt.text = "레벨 : " + m_PlayerData.nLevel[_index] + "\n" +
            "랭크 : " + m_PlayerData.nRank[_index] + "\n" +
            "체력 : " + m_PlayerData.fHealth[_index] + "\n" +
            "공격력 : " + m_PlayerData.fAttack[_index] + "\n" +
            "주문력 : " + m_PlayerData.fMagic[_index] + "\n" +
            "방어력 : " + m_PlayerData.fDefense[_index] + "\n" +
            "행동력 : " + m_PlayerData.fSpeed[_index] + "\n";
    }
}
