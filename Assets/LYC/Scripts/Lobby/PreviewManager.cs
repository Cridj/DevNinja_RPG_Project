using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

[System.Serializable]
public class CharSkillAnim
{
    public Animator[] CharSkill_Anim;
}

[System.Serializable]
public class MonsSkillAnim
{
    public Animator[] MonsSkill_Anim;
}

public class PreviewManager : MonoBehaviour
{
    [SerializeField]
    DataManager m_DataManager;
    public PlatformScript m_PlatformScript;
    [SerializeField]
    public List<CharacterSkillData> m_CharacterSkillData = new List<CharacterSkillData>();
    [SerializeField]
    public List<MonsterSkillData> m_MonsterSkillData = new List<MonsterSkillData>();

    public LibraryManager m_LibraryManager;

    [SerializeField]
    PreviewSlot[] Slots;

    public PreviewSlot[] GetSlots() { return Slots; }

    [SerializeField]
    public Button Play_Btn;

    public Animator Preview_Anim;
    public CharSkillAnim[] CharSkill_Anim;
    public MonsSkillAnim[] MonsSkill_Anim;

    public int nType;
    public int nIndex;
    public int nNum;
    public int bUnlock;

    // Start is called before the first frame update
    private void Start()
    {
        m_DataManager = GameObject.FindGameObjectWithTag("Info").GetComponent<DataManager>();
        m_CharacterSkillData = m_PlatformScript.m_CharacterSkillData.ToList();
        m_MonsterSkillData = m_PlatformScript.m_MonsterSkillData.ToList();     
    }

    public void ChangeSlot(int _type, int _index)
    {

        nType = _type;
        nIndex = _index;

        //슬롯 클리어 하기
        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i].gameObject.SetActive(false);
        }

        Preview_Anim = null;

        switch (nType)
        {
            case 0:
                for (int i = 0; i < m_CharacterSkillData[_index].nNum.Length; i++)
                {
                    Slots[i].UnlockSkill(_type, _index, i, m_DataManager.MyCharacterList[_index].bUnlock);
                    Slots[i].gameObject.SetActive(true);                    
                }
                Preview_Anim = CharSkill_Anim[_index].CharSkill_Anim[0];
                break;
            case 1:
                for (int i = 0; i<m_MonsterSkillData[_index].nNum.Length; i++)
                {
                    Slots[i].UnlockSkill(_type, _index, i, m_DataManager.MyMonsterList[_index].bUnlock);
                    Slots[i].gameObject.SetActive(true);
                }
                Preview_Anim = MonsSkill_Anim[_index].MonsSkill_Anim[0];
                break;
            case 2:
                return;
            default:
                break;
        }
    }

    public void ClickSlot(int _type, int _index, int _num, bool _unlock)
    {
        switch(_type)
        {
            case 0:
                if(_unlock == true)
                {
                    Preview_Anim = CharSkill_Anim[_index].CharSkill_Anim[_num];
                    Play_Btn.interactable = true;
                }
                else
                {
                    Preview_Anim = null;
                    Play_Btn.interactable = false;
                }
                break;
            case 1:
                if(_unlock == true)
                {
                    Preview_Anim = MonsSkill_Anim[_index].MonsSkill_Anim[_num];
                    Play_Btn.interactable = true;
                }
                else
                {
                    Preview_Anim = null;
                    Play_Btn.interactable = false;
                }
                break;
            case 2:
                return;
            default:
                break;
        }
    }

    public void AnimPlay()
    {
        Preview_Anim.SetBool("Activate", true);
    }
}
