using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoSlot : MonoBehaviour
{
    [SerializeField]
    DataManager m_DataManager;
    [SerializeField]
    PlayerData m_PlayerData;
    [SerializeField]
    List<ItemData> m_ItemData;

    public PlatformScript m_PlatformScript;

    public Image Item_Img;
    public Text Name_Txt;
    public GameObject Desc_Obj;
    public Text Desc_Txt;

    public int nIndex;

    public void SetItemSlot(int _index)
    {
        m_DataManager = GameObject.FindGameObjectWithTag("Info").GetComponent<DataManager>();
        m_PlayerData = m_PlatformScript.m_PlayerData;
        m_ItemData = m_PlatformScript.m_ItemData;

        nIndex = _index;

        Item_Img.sprite = m_DataManager.Item_Spr[_index];
        Name_Txt.text = m_ItemData[_index].sName;
    }

    public void ItemDescOn()
    {
        Desc_Txt.text = m_ItemData[nIndex].sExplain;
        Desc_Obj.SetActive(true);
    }

    public void ItemDescOff()
    {
        Desc_Txt.text = null;
        Desc_Obj.SetActive(false);
    }
}
