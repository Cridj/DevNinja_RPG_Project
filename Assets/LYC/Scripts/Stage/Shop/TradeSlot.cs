using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeSlot : MonoBehaviour
{
    [SerializeField]
    DataManager m_DataManager;
    public PlatformScript m_PlatformScript;
    public ShopManager m_ShopManager;
    public PlayerData m_PlayerData;
    public List<ItemData> m_ItemData;

    public Button Trade_btn;
    public Image Item_Img;
    public Image ItemName_Img;
    public Text ItemName_Txt;
    public GameObject ItemDesc_Obj;
    public Text ItemDesc_Txt;
    public RectTransform ItemDesc_RT;
    public GameObject Check_Obj;
    public GameObject Sold_Obj;
    public Color SelectColor;
    public Color UnSelectColor;

    public int nIndex;
    public int nSlot;
    public bool bUnlock = false;

    public void SetItemSlot(int _nIndex, int _nSlot)
    {
        m_DataManager = GameObject.FindGameObjectWithTag("Info").GetComponent<DataManager>();
        m_PlayerData = m_PlatformScript.m_PlayerData;
        m_ItemData = m_PlatformScript.m_ItemData;

        nIndex = _nIndex;
        nSlot = _nSlot;

        Check_Obj.SetActive(false);
        Sold_Obj.SetActive(false);

        Item_Img.gameObject.SetActive(true);
        Item_Img.sprite = m_DataManager.Item_Spr[_nIndex];
        ItemName_Txt.text = m_ItemData[_nIndex].sName;
        ItemDesc_Txt.text = m_ItemData[_nIndex].sExplain;

        SelectColor = new Color(255, 255, 255, 255);
        UnSelectColor = new Color(200, 200, 200, 128);

        ItemName_Img.color = SelectColor;

        Trade_btn.interactable = true;
        bUnlock = true;
    }

    public void LockSlot()
    {
        m_DataManager = GameObject.FindGameObjectWithTag("Info").GetComponent<DataManager>();
        m_PlayerData = m_PlatformScript.m_PlayerData;
        m_ItemData = m_PlatformScript.m_ItemData;

        nIndex = -1;

        Check_Obj.SetActive(false);
        Sold_Obj.SetActive(false);

        Item_Img.gameObject.SetActive(false);
        Item_Img.sprite = null;
        ItemName_Txt.text = "¾øÀ½";
        ItemDesc_Txt.text = "null";

        SelectColor = new Color(200, 200, 200, 128);
        UnSelectColor = new Color(200, 200, 200, 128);

        ItemName_Img.color = UnSelectColor;

        Trade_btn.interactable = false;
        bUnlock = false;
    }

    public void ItemDescOn()
    {
        if(bUnlock==false)
        {
            return;
        }
        if (Sold_Obj.activeSelf == true)
        {
            return;
        }

        ItemDesc_Obj.SetActive(true);
    }

    public void ItemDescOff()
    {
        ItemDesc_Obj.SetActive(false);
    }

    public void ItemUnSelect()
    {
        Check_Obj.SetActive(false);
       
        m_ShopManager.SelectSlot(-1, -1);               
    }

    public void ItemSelect()
    {
        if(Sold_Obj.activeSelf == true)
        {
            return;
        }

        Check_Obj.SetActive(true);
        Debug.Log(nIndex + ", " + nSlot);
        m_ShopManager.SelectSlot(nIndex, nSlot);
    }

    public void ItemSold()
    {
        Check_Obj.SetActive(false);
        Sold_Obj.SetActive(true);
        ItemName_Img.color = UnSelectColor;
        Trade_btn.interactable = false;
    }
}
