using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public CharacterInfoManager m_CharacterInfoManager;
    public ItemInfoManager m_ItemInfoManager;

    public GameObject CharInfo_Obj;
    public GameObject ItemInfo_Obj;

    public Button Character_Btn;
    public RectTransform Character_RT;
    public Button Item_Btn;
    public RectTransform Item_Rt;

    private ColorBlock SelectColorBlock;
    private ColorBlock UnSelectColorBlock;

    int nSelect = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        SelectColorBlock = Character_Btn.colors;
        UnSelectColorBlock = Item_Btn.colors;
    }

    public void SetInventory()
    {

    }

    public void OpenCharacterInfo()
    {
        if(nSelect == 0)
        {
            return;
        }

        nSelect = 0;

        Character_Btn.colors = SelectColorBlock;
        Item_Btn.colors = UnSelectColorBlock;

        Character_RT.anchoredPosition = new Vector2(5, Character_RT.anchoredPosition.y);
        Item_Rt.anchoredPosition = new Vector2(35, Item_Rt.anchoredPosition.y);
        CharInfo_Obj.SetActive(true);
        ItemInfo_Obj.SetActive(false);

        //선택 초기화
    }

    public void OpenItemInfo()
    {

    }
}
