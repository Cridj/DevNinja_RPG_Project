using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoManager : MonoBehaviour
{
    [SerializeField]
    DataManager m_DataManager;
    [SerializeField]
    PlayerData m_PlayerData;
    [SerializeField]
    List<ItemData> m_ItemData;

    [SerializeField]
    ItemInfoSlot[] Slots;

    public ItemInfoSlot[] GetSlots() { return Slots; }

    public PlatformScript m_PlatformScript;

    public Text PlayerInfo_Txt;
    public Text EffectInfo_Txt;

    public float fDHealth, fDAttack, fDMagic, fDDefense, fDSpeed;
    public float fTHealth, fTAttack, fTMagic, fTDefense, fTSpeed;
    public float fHHealth, fHAttack, fHMagic, fHDefense, fHSpeed;

    int nItemStack, nCommonStack, nRareStack, nEpicStack, nLegendStack;

    public int nIndex;

    // Start is called before the first frame update
    void Start()
    {
        m_DataManager = GameObject.FindGameObjectWithTag("Info").GetComponent<DataManager>();

        m_PlayerData = m_PlatformScript.m_PlayerData;
        m_ItemData = m_PlatformScript.m_ItemData;
    }

    public void SetItemTap()
    {
        nItemStack = 0; nCommonStack = 0; nRareStack = 0; nEpicStack = 0; nLegendStack = 0;
        fDHealth = 0; fDAttack = 0; fDMagic = 0; fDDefense = 0; fDSpeed = 0;
        fTHealth = 0; fTAttack = 0; fTMagic = 0; fTDefense = 0; fTSpeed = 0;
        fHHealth = 0; fHAttack = 0; fHMagic = 0; fHDefense = 0; fHSpeed = 0;

        for(int i = 0; i < Slots.Length; i++)
        {
            if(m_PlayerData.nItemIndex[i] == null || m_PlayerData.nItemIndex[i] == "")
            {
                break;
            }

            if(int.TryParse(m_PlayerData.nItemIndex[i], out int Result0))
            {
                Slots[i].SetItemSlot(Result0);
                Slots[i].gameObject.SetActive(true);
            }

            nItemStack++;

            switch(m_ItemData[Result0].sRank)
            {
                case "Common":
                    nCommonStack++;
                    break;
                case "Rare":
                    nRareStack++;
                    break;
                case "Epic":
                    nEpicStack++;
                    break;
                case "Legend":
                    nLegendStack++;
                    break;
            }

            switch(m_ItemData[Result0].sType)
            {
                case "딜러":
                    if(m_ItemData[Result0].fHealth != "" && m_ItemData[Result0].fHealth != null)
                    {
                        fDHealth += float.Parse(m_ItemData[Result0].fHealth);
                    }
                    if(m_ItemData[Result0].fAttack != "" && m_ItemData[Result0].fAttack != null)
                    {
                        fDAttack += float.Parse(m_ItemData[Result0].fAttack);
                    }
                    if(m_ItemData[Result0].fMagic != "" && m_ItemData[Result0].fMagic != null)
                    {
                        fDMagic += float.Parse(m_ItemData[Result0].fMagic);
                    }
                    if (m_ItemData[Result0].fDefense != "" && m_ItemData[Result0].fDefense != null)
                    {
                        fDDefense += float.Parse(m_ItemData[Result0].fDefense);
                    }
                    if (m_ItemData[Result0].fSpeed != "" && m_ItemData[Result0].fSpeed != null)
                    {
                        fDSpeed += float.Parse(m_ItemData[Result0].fSpeed);
                    }                    
                    break;
                case "탱커":
                    if (m_ItemData[Result0].fHealth != "" && m_ItemData[Result0].fHealth != null)
                    {
                        fTHealth += float.Parse(m_ItemData[Result0].fHealth);
                    }
                    if (m_ItemData[Result0].fAttack != "" && m_ItemData[Result0].fAttack != null)
                    {
                        fTAttack += float.Parse(m_ItemData[Result0].fAttack);
                    }
                    if (m_ItemData[Result0].fMagic != "" && m_ItemData[Result0].fMagic != null)
                    {
                        fTMagic += float.Parse(m_ItemData[Result0].fMagic);
                    }
                    if (m_ItemData[Result0].fDefense != "" && m_ItemData[Result0].fDefense != null)
                    {
                        fTDefense += float.Parse(m_ItemData[Result0].fDefense);
                    }
                    if (m_ItemData[Result0].fSpeed != "" && m_ItemData[Result0].fSpeed != null)
                    {
                        fTSpeed += float.Parse(m_ItemData[Result0].fSpeed);
                    }                
                    break;
                case "힐러":
                    if (m_ItemData[Result0].fHealth != "" && m_ItemData[Result0].fHealth != null)
                    {
                        fHHealth += float.Parse(m_ItemData[Result0].fHealth);
                    }
                    if (m_ItemData[Result0].fAttack != "" && m_ItemData[Result0].fAttack != null)
                    {
                        fHAttack += float.Parse(m_ItemData[Result0].fAttack);
                    }
                    if (m_ItemData[Result0].fMagic != "" && m_ItemData[Result0].fMagic != null)
                    {
                        fHMagic += float.Parse(m_ItemData[Result0].fMagic);
                    }
                    if (m_ItemData[Result0].fDefense != "" && m_ItemData[Result0].fDefense != null)
                    {
                        fHDefense += float.Parse(m_ItemData[Result0].fDefense);
                    }
                    if (m_ItemData[Result0].fSpeed != "" && m_ItemData[Result0].fSpeed != null)
                    {
                        fHSpeed += float.Parse(m_ItemData[Result0].fSpeed);
                    }            
                    break;
                default:
                    break;
            }
        }

        PlayerInfo_Txt.text = "소지금 : " + m_PlayerData.nCoin + "\n" +
            "아이템 수 : " + nItemStack + "\n" +
            "일반 아이템 수 : " + nCommonStack + "\n" +
            "희귀 아이템 수 : " + nRareStack + "\n" +
            "영웅 아이템 수 : " + nEpicStack + "\n" +
            "전설 아이템 수 : " + nLegendStack + "\n";

        EffectInfo_Txt.text = "딜러" + "\n" +
            "체 : " + fDHealth + "%, " + "공 : " + fDAttack + "%, " + "주 : " + fDMagic + "%, " + "방 : " + fDDefense + "%, " + "행 : " + fDSpeed + "%" + "\n" +
            "\n" + "탱커" + "\n" +
            "\n" + "체 : " + fTHealth + "%, " + "공 : " + fTAttack + "%, " + "주 : " + fTMagic + "%, " + "방 : " + fTDefense + "%, " + "행 : " + fTSpeed + "%" + "\n" +
            "\n" + "힐러" + "\n" +
            "체 : " + fHHealth + "%, " + "공 : " + fHAttack + "%, " + "주 : " + fHMagic + "%, " + "방 : " + fHDefense + "%, " + "행 : " + fHSpeed + "%";
    }

    
}
