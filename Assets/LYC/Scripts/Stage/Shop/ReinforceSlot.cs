using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReinforceSlot : MonoBehaviour
{
    [SerializeField]
    DataManager m_DataManager;
    public PlatformScript m_PlatformScript;
    public ShopManager m_ShopManager;
    public PlayerData m_PlayerData;
    public List<CharacterData> m_CharacterData;

    public Button Reinforce_Btn;
    public Image Char_Img;
    public Image CharInfo_Img;
    public Text CharName_Txt;
    public Text CharInfo_Txt;
    public GameObject Check_Obj;
    public Color SelectColor;
    public Color UnSelectColor;

    public int nIndex;
    public int nSlot;

    public void SetCharSlot(int _nIndex, int _nSlot)
    {
        m_DataManager = GameObject.FindGameObjectWithTag("Info").GetComponent<DataManager>();
        m_PlayerData = m_PlatformScript.m_PlayerData;
        m_CharacterData = m_PlatformScript.m_CharacterData;

        nIndex = _nIndex; 
        nSlot = _nSlot;

        Check_Obj.SetActive(false);

        Char_Img.gameObject.SetActive(true);
        Char_Img.sprite = m_DataManager.Char_Spr[int.Parse(m_PlayerData.nCharIndex[nIndex])];
        CharName_Txt.text = m_CharacterData[int.Parse(m_PlayerData.nCharIndex[nIndex])].sName + "(" + m_PlayerData.nLevel[int.Parse(m_PlayerData.nCharIndex[nIndex])] + ": LV)";
        if(m_PlayerData.nRank[int.Parse(m_PlayerData.nCharIndex[nIndex])]==m_PlayerData.nLevel[int.Parse(m_PlayerData.nCharIndex[nIndex])])
        {
            ReinforceMax();
        }
        else
        {
            CharInfo_Txt.text = "강화 등급 : " + m_PlayerData.nRank[int.Parse(m_PlayerData.nCharIndex[nIndex])] + "->" + (m_PlayerData.nRank[int.Parse(m_PlayerData.nCharIndex[nIndex])] + 1) + "\n" +
          "체력 : " + m_PlayerData.fHealth[int.Parse(m_PlayerData.nCharIndex[nIndex])] + "->" + (m_PlayerData.fHealth[int.Parse(m_PlayerData.nCharIndex[nIndex])] + int.Parse(m_CharacterData[int.Parse(m_PlayerData.nCharIndex[nIndex])].fGHealth)) + "\n" +
          "공격력 : " + m_PlayerData.fAttack[int.Parse(m_PlayerData.nCharIndex[nIndex])] + "->" + (m_PlayerData.fAttack[int.Parse(m_PlayerData.nCharIndex[nIndex])] + int.Parse(m_CharacterData[int.Parse(m_PlayerData.nCharIndex[nIndex])].fGAttack)) + "\n" +
          "주문력 : " + m_PlayerData.fMagic[int.Parse(m_PlayerData.nCharIndex[nIndex])] + "->" + (m_PlayerData.fMagic[int.Parse(m_PlayerData.nCharIndex[nIndex])] + int.Parse(m_CharacterData[int.Parse(m_PlayerData.nCharIndex[nIndex])].fMagic)) + "\n" +
          "방어력 : " + m_PlayerData.fDefense[int.Parse(m_PlayerData.nCharIndex[nIndex])] + "->" + (m_PlayerData.fDefense[int.Parse(m_PlayerData.nCharIndex[nIndex])] + int.Parse(m_CharacterData[int.Parse(m_PlayerData.nCharIndex[nIndex])].fDefense)) + "\n" +
          "행동력 : " + m_PlayerData.fSpeed[int.Parse(m_PlayerData.nCharIndex[nIndex])] + "->" + (m_PlayerData.fSpeed[int.Parse(m_PlayerData.nCharIndex[nIndex])] + int.Parse(m_CharacterData[int.Parse(m_PlayerData.nCharIndex[nIndex])].fSpeed));

            SelectColor = new Color(255, 255, 255, 255);
            UnSelectColor = new Color(200, 200, 200, 128);

            CharInfo_Img.color = SelectColor;

            Reinforce_Btn.interactable = true;
        }

       
    }

    public void RenewalInfo()
    {
        CharInfo_Txt.text = "강화 등급 : " + m_PlayerData.nRank[int.Parse(m_PlayerData.nCharIndex[nIndex])] + "->" + (m_PlayerData.nRank[int.Parse(m_PlayerData.nCharIndex[nIndex])] + 1) + "\n" +
          "체력 : " + m_PlayerData.fHealth[int.Parse(m_PlayerData.nCharIndex[nIndex])] + "->" + (m_PlayerData.fHealth[int.Parse(m_PlayerData.nCharIndex[nIndex])] + int.Parse(m_CharacterData[int.Parse(m_PlayerData.nCharIndex[nIndex])].fGHealth)) + "\n" +
          "공격력 : " + m_PlayerData.fAttack[int.Parse(m_PlayerData.nCharIndex[nIndex])] + "->" + (m_PlayerData.fAttack[int.Parse(m_PlayerData.nCharIndex[nIndex])] + int.Parse(m_CharacterData[int.Parse(m_PlayerData.nCharIndex[nIndex])].fGAttack)) + "\n" +
          "주문력 : " + m_PlayerData.fMagic[int.Parse(m_PlayerData.nCharIndex[nIndex])] + "->" + (m_PlayerData.fMagic[int.Parse(m_PlayerData.nCharIndex[nIndex])] + int.Parse(m_CharacterData[int.Parse(m_PlayerData.nCharIndex[nIndex])].fMagic)) + "\n" +
          "방어력 : " + m_PlayerData.fDefense[int.Parse(m_PlayerData.nCharIndex[nIndex])] + "->" + (m_PlayerData.fDefense[int.Parse(m_PlayerData.nCharIndex[nIndex])] + int.Parse(m_CharacterData[int.Parse(m_PlayerData.nCharIndex[nIndex])].fDefense)) + "\n" +
          "행동력 : " + m_PlayerData.fSpeed[int.Parse(m_PlayerData.nCharIndex[nIndex])] + "->" + (m_PlayerData.fSpeed[int.Parse(m_PlayerData.nCharIndex[nIndex])] + int.Parse(m_CharacterData[int.Parse(m_PlayerData.nCharIndex[nIndex])].fSpeed));
        Check_Obj.SetActive(true);
    }

    public void CharSelect()
    {
        Check_Obj.SetActive(true);
        m_ShopManager.SelectSlot(int.Parse(m_PlayerData.nCharIndex[nIndex]), nSlot);
    }

    public void CharUnSelect()
    {
        Check_Obj.SetActive(false);
        m_ShopManager.SelectSlot(-1, -1);
    }

    public void ReinforceMax()
    {
        Check_Obj.SetActive(false);
        CharInfo_Img.color = UnSelectColor;
        Reinforce_Btn.interactable = false;
        CharInfo_Txt.text = "강화 등급 : 현재 레벨 최고치에 도달함" + "\n" +
           "체력 : " + m_PlayerData.fHealth[int.Parse(m_PlayerData.nCharIndex[nIndex])] + "\n" +
           "공격력 : " + m_PlayerData.fAttack[int.Parse(m_PlayerData.nCharIndex[nIndex])] + "\n" +
           "주문력 : " + m_PlayerData.fMagic[int.Parse(m_PlayerData.nCharIndex[nIndex])] +"\n" +
           "방어력 : " + m_PlayerData.fDefense[int.Parse(m_PlayerData.nCharIndex[nIndex])] +"\n" +
           "행동력 : " + m_PlayerData.fSpeed[int.Parse(m_PlayerData.nCharIndex[nIndex])];

        CharInfo_Img.color = UnSelectColor;

        Reinforce_Btn.interactable = false;
    }
}
