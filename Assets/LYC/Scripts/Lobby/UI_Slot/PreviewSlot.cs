using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Linq;

public class PreviewSlot : MonoBehaviour
{
    [SerializeField]
    DataManager m_DataManager;
    public PlatformScript m_PlatformScript;
    public PreviewManager m_PreviewManager;
    public Image Skill_Img;
    public Text Name_Txt;
    public Text Exp_Txt;

    public int nType;
    public int nIndex;
    public int nNum;
    public bool bUnlock;

    private void SetColor(float _alpha)
    {
        Color color = Skill_Img.color;
        color.a = _alpha;
        Skill_Img.color = color;
    }

    public void UnlockSkill(int _type, int _index, int _num, bool _unlock)
    {
        m_DataManager = GameObject.FindGameObjectWithTag("Info").GetComponent<DataManager>();

        nType = _type;
        nIndex = _index;
        nNum = _num;
        bUnlock = _unlock;

        switch(_type)
        {
            case 0:
                Skill_Img.sprite = m_DataManager.CharSkill_Spr[_index].CharSkillSpr[_num];
                if(_unlock==false)
                {
                    Skill_Img.gameObject.SetActive(false);
                    Name_Txt.gameObject.SetActive(true);
                    Name_Txt.text = "???";
                    Exp_Txt.gameObject.SetActive(true);
                    Exp_Txt.text = "???";
                }
                else
                {
                    Skill_Img.gameObject.SetActive(true);
                    Name_Txt.gameObject.SetActive(true);
                    Name_Txt.text = m_PreviewManager.m_CharacterSkillData[_index].sName[_num];
                    Exp_Txt.gameObject.SetActive(true);
                    Exp_Txt.text = m_PreviewManager.m_CharacterSkillData[_index].sExplain[_num];
                }
                break;
            case 1:
                Skill_Img.sprite = m_DataManager.MonsSkill_Spr[_index].MonsSkillSpr[_num];
                if(_unlock==false)
                {
                    Skill_Img.gameObject.SetActive(false);
                    Name_Txt.gameObject.SetActive(true);
                    Name_Txt.text = "???";
                    Exp_Txt.gameObject.SetActive(true);
                    Exp_Txt.text = "???";
                }
                else
                {
                    Skill_Img.gameObject.SetActive(false);
                    Name_Txt.gameObject.SetActive(true);
                    Name_Txt.text = m_PreviewManager.m_MonsterSkillData[_index].sName[_num];
                    Exp_Txt.gameObject.SetActive(true);
                    Exp_Txt.text = m_PreviewManager.m_MonsterSkillData[_index].sExplain[_num];
                }
                break;
            case 2:
                return;
            default:
                break;
        }
    }

    public void ClickTap()
    {
        m_PreviewManager.ChangeSlot(nType, nIndex);
    }
}
