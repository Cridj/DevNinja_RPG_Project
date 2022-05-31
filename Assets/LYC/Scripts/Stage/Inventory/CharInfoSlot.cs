using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharInfoSlot : MonoBehaviour
{
    [SerializeField]
    DataManager m_DataManager;
    [SerializeField]
    PlayerData m_PlayerData;
    [SerializeField]
    List<CharacterData> m_CharacterData;

    public PlatformScript m_PlatformScript;
    public CharacterInfoManager m_CharInfoManager;

    public Button Char_Btn;
    public Image Char_Img;
    public GameObject Char_Obj;
    public Image NameBG_Img;
    public Text Name_Txt;

    public Color SelectColor;
    public Color UnSelectColor;
    public ColorBlock SelectColors;
    public ColorBlock UnSelectColors;

    public int nIndex;
    public int nSlot;
    public bool bUnlock = false;

    public void SetCharSlot(int _nIndex, int _nSlot)
    {
        m_DataManager = GameObject.FindGameObjectWithTag("Info").GetComponent<DataManager>();
        m_PlayerData = m_PlatformScript.m_PlayerData;
        m_CharacterData = m_PlatformScript.m_CharacterData;

        nIndex = _nIndex;
        nSlot = _nSlot;

        SelectColor = new Color(255, 255, 255, 255);
        UnSelectColor = new Color(200, 200, 200, 128);
        SelectColors = Char_Btn.colors;
        UnSelectColors = Char_Btn.colors;
        UnSelectColors.normalColor = UnSelectColor;

        if(int.TryParse(m_PlayerData.nCharIndex[nIndex], out int nResult0))
        {
            Char_Btn.interactable = true;
            Char_Obj.SetActive(true);
            Char_Img.sprite = m_DataManager.CharHead_Spr[nResult0];
            NameBG_Img.color = SelectColor;
            Name_Txt.text = m_CharacterData[nResult0].sName;
            bUnlock = true;
        }
        else
        {
            //LockSlot();
        }
    }

    public void LockSlot()
    {
        m_DataManager = GameObject.FindGameObjectWithTag("Info").GetComponent<DataManager>();
        m_PlayerData = m_PlatformScript.m_PlayerData;
        m_CharacterData = m_PlatformScript.m_CharacterData;

        nIndex = -1;

        bUnlock = false;

        Char_Btn.interactable = false;
        NameBG_Img.color = UnSelectColor;
        Name_Txt.text = "???";
    }

    public void CharSelect()
    {
        if(bUnlock == false)
        {
            return;
        }

        m_CharInfoManager.CharacterSelect(nIndex, nSlot);

    }

    public void CharUnSelect()
    {
        //Char_Btn.colors = UnSelectColors;
        //NameBG_Img.color = UnSelectColor;
    }

}
