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

    public int nIndex;

    // Start is called before the first frame update
    void Start()
    {
        m_DataManager = GameObject.FindGameObjectWithTag("Info").GetComponent<DataManager>();

        m_PlayerData = m_PlatformScript.m_PlayerData;
        m_ItemData = m_PlatformScript.m_ItemData;
    }

    
}
