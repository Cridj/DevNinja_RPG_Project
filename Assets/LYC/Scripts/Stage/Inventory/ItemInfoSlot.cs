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

    public void SetItemSlot(int _index)
    {

    }
}
