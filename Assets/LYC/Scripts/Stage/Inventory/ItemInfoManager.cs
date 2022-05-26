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

    ItemInfoSlot[] Slots;

    public ItemInfoSlot[] GetSlots() { return Slots; }

    public PlatformScript m_PlatformScript;

    public Text PlayerInfo_Txt;
    public Text Effect_Txt;

    public float fDHealth, fDAttack, fDMagic, fDDefense, fDSpeed;
    public float fTHealth, fTAttack, fTMagic, fTDefense, fTSpeed;
    public float fHHealth, fHAttack, fHMagic, fHDefense, fHSpeed;

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
        for(int i = 0; i < Slots.Length; i++)
        {
            if(m_PlayerData.nItemIndex[i] == null || m_PlayerData.nItemIndex[i] == "")
            {
                break;
            }

            if(int.TryParse(m_PlayerData.nItemIndex[i], out int Result0))
            {
                Slots[i].SetItemSlot(Result0);
            }
        }
    }

    
}
