using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public enum ERank
{
    Common = 0,
    Rair = 1,
    Epic = 2,
    Legend = 3
}
public class LibraryManager : MonoBehaviour
{
    DataManager m_DataManager;

    public PlatformScript m_PlatformScript;

    [SerializeField]
    public List<CharacterData> m_CharacterData = new List<CharacterData>();

    [SerializeField]
    public List<MonsterData> m_MonsterData = new List<MonsterData>();

    [SerializeField]
    public List<ItemData> m_ItemData = new List<ItemData>();

    public int nType;

    public RectTransform ListScroll_RT;
    public GameObject Fillter_Obj;

    [SerializeField]
    private GameObject InventoryBase_Obj;
    [SerializeField]
    private GameObject SlotsParent_Obj;

    [SerializeField]
    private LibrarySlot[] Slots;

    public LibrarySlot[] GetSlots() { return Slots; }

    [SerializeField]
    private ColorBlock selectColorBlock;
    [SerializeField]
    private ColorBlock unselectColorBlock;

    public Button Char_Btn;
    public Button Mons_Btn;
    public Button Item_Btn;

    public Button Preview_Btn;

    //infogroup
    public Image Info_Img;
    public Text Name_Txt;
    public Text Ability_Txt;
    public Text Exp_Txt;

    //public ToggleGroup Filter_TG;
    //public Toggle[] FilterToggles;
    public Dropdown Classfication_DD;
    public Dropdown Order_DD;

    public ERank eRank;

    public int nClass;
    public int nOrder;

    //[SerializeField] private 아이템, 게임, 몬스터 예약어 넣기

    // Start is called before the first frame update
    void Start()
    {
        m_DataManager = GameObject.FindGameObjectWithTag("Info").GetComponent<DataManager>();
        m_CharacterData = m_PlatformScript.m_CharacterData.ToList();
        m_MonsterData = m_PlatformScript.m_MonsterData.ToList();
        m_ItemData = m_PlatformScript.m_ItemData.ToList();
        

       
        //Slots = SlotsParent_Obj.GetComponentsInChildren<LibrarySlot>();

        selectColorBlock = Char_Btn.colors;
        unselectColorBlock = Mons_Btn.colors;

        Info_Img.gameObject.SetActive(false);
        

        switch (nType)
        {          
            case 0:              
                ListScroll_RT.offsetMax = new Vector2(ListScroll_RT.offsetMax.x, -170);
                Fillter_Obj.SetActive(false);
                Preview_Btn.interactable = true;
                //캐릭터 for문 돌리면서 슬롯 추가하기
                for (int i = 0; i < m_CharacterData.Count; i++)
                {
                    Slots[i].UnlockSlot(0, i, m_CharacterData[i].bUnlock);
                    Slots[i].gameObject.SetActive(true);
                }

                break;
            case 1:
                ListScroll_RT.offsetMax = new Vector2(ListScroll_RT.offsetMax.x, -170);
                Fillter_Obj.SetActive(false);
                Preview_Btn.interactable = true;
                //몬스터 for문 돌리면서 슬롯 추가하기
                for(int i = 0; i < m_MonsterData.Count; i++)
                {
                    Slots[i].UnlockSlot(0, i, m_MonsterData[i].bUnlock);
                    Slots[i].gameObject.SetActive(true);
                }
                break;
            case 2:
                ListScroll_RT.offsetMax = new Vector2(ListScroll_RT.offsetMax.x, -300);
                Fillter_Obj.SetActive(true);
                Preview_Btn.interactable = false;
                //아이템 for문 돌리면서 슬롯 추가하기
                for (int i = 0; i < m_ItemData.Count; i++)
                {
                    Slots[i].UnlockSlot(0, i, m_ItemData[i].bUnlock);
                    Slots[i].gameObject.SetActive(true);
                }
                break;

            default:
                ListScroll_RT.offsetMax = new Vector2(ListScroll_RT.offsetMax.x, -170);
                Fillter_Obj.SetActive(false);
                Preview_Btn.interactable = true;
                nType = 0;
                break;
        }
    }

    public void ChangeSlot(int ntype)
    {
        if(nType == ntype)
        {
            return;
        }

        nType = ntype;

        //슬롯 클리어 하기
        for(int i = 0; i < Slots.Length; i++)
        {
            Slots[i].gameObject.SetActive(false);
        }

        switch(ntype)
        {
            case 0:
                Char_Btn.colors = selectColorBlock;
                Mons_Btn.colors = unselectColorBlock;
                Item_Btn.colors = unselectColorBlock;
                Preview_Btn.interactable = true;
                ListScroll_RT.offsetMax = new Vector2(ListScroll_RT.offsetMax.x, -170);
                Fillter_Obj.SetActive(false);
                Preview_Btn.interactable = true;

                for (int i = 0; i < m_CharacterData.Count; i++)
                {
                    Slots[i].UnlockSlot(0, i, m_CharacterData[i].bUnlock);
                    Slots[i].gameObject.SetActive(true);
                }

                break;
            case 1:

                Char_Btn.colors = unselectColorBlock;
                Mons_Btn.colors = selectColorBlock;
                Item_Btn.colors = unselectColorBlock;
                Preview_Btn.interactable = true;
                ListScroll_RT.offsetMax = new Vector2(ListScroll_RT.offsetMax.x, -170);
                Fillter_Obj.SetActive(false);
                Preview_Btn.interactable = true;

                for (int i = 0; i < m_MonsterData.Count; i++)
                {
                    Slots[i].UnlockSlot(1, i, m_MonsterData[i].bUnlock);
                    Slots[i].gameObject.SetActive(true);
                }

                break;
            case 2:

                Char_Btn.colors = unselectColorBlock;
                Mons_Btn.colors = unselectColorBlock;
                Item_Btn.colors = selectColorBlock;
                Preview_Btn.interactable = false;
                ListScroll_RT.offsetMax = new Vector2(ListScroll_RT.offsetMax.x, -300);
                Fillter_Obj.SetActive(true);
                Preview_Btn.interactable = false;

                Classfication_DD.value = 0;
                Order_DD.value = 0;

                for (int i = 0; i < m_ItemData.Count; i++)
                {
                    Slots[i].UnlockSlot(2, i, m_ItemData[i].bUnlock);
                    Slots[i].gameObject.SetActive(true);
                }

                break;

            default:

                Char_Btn.colors = selectColorBlock;
                Mons_Btn.colors = unselectColorBlock;
                Item_Btn.colors = unselectColorBlock;
                Preview_Btn.interactable = true;
                nType = 0;
                break;
        }
    }

    public void ClickSlot(int _type, int _index, bool _unlock)
    {
        switch(_type)
        {
            case 0:
                if(_unlock==true)
                {
                    Info_Img.transform.gameObject.SetActive(true);
                    Info_Img.sprite = m_DataManager.Char_Spr[_index];
                    Name_Txt.text = m_CharacterData[_index].sName;
                    Ability_Txt.text = "타입 : " + m_CharacterData[_index].sType + "\n" + "체력 : " + m_CharacterData[_index].fHealth + "\n" + "공격력 : " + m_CharacterData[_index].fAttack + "\n"+ "주문력 : " + m_CharacterData[_index].fMagic + "\n" + "방어력 : " + m_CharacterData[_index].fDefense + "\n" + "행동력 : " + m_CharacterData[_index].fSpeed;
                    Exp_Txt.text = m_CharacterData[_index].sExplain;
                }
                else
                {
                    Info_Img.transform.gameObject.SetActive(false);
                    Info_Img.sprite = m_DataManager.Char_Spr[_index];
                    Name_Txt.text = "???";
                    Ability_Txt.text = "타입 : " + m_CharacterData[_index].sType + "\n" + "체력 : " + "???" + "\n" + "공격력 : " + "???" + "\n" + "주문력 : " + "???" + "\n" + "방어력 : " + "???" + "\n" + "행동력 : " + "???";
                    Exp_Txt.text = "???";
                }
                break;
            case 1:
                if(_unlock==true)
                {
                    Info_Img.transform.gameObject.SetActive(true);
                    Info_Img.sprite = m_DataManager.Mons_Spr[_index];
                    Name_Txt.text = m_MonsterData[_index].sName+"("+m_MonsterData[_index].sRank+")";
                    Ability_Txt.text = "타입 : " + m_MonsterData[_index].sType + "\n" + "체력 : " + m_MonsterData[_index].fHealth + "\n" + "공격력 : " + m_MonsterData[_index].fAttack + "\n" + "주문력 : " + m_MonsterData[_index].fMagic + "\n" + "방어력 : " + m_MonsterData[_index].fDefense + "\n" + "행동력 : " + m_MonsterData[_index].fSpeed;
                    Exp_Txt.text = m_MonsterData[_index].sExplain; 
                }
                else
                {
                    Info_Img.transform.gameObject.SetActive(false);
                    Info_Img.sprite = m_DataManager.Mons_Spr[_index];
                    Name_Txt.text = "???";
                    Ability_Txt.text = "타입 : " + "???" + "\n" + "체력 : " + "???" + "\n" + "공격력 : " + "???" + "\n" + "주문력 : " + "???" + "\n" + "방어력 : " + "???" + "\n" + "행동력 : " + "???";
                    Exp_Txt.text = "???";
                }
                break;
            case 2:
                if(_unlock==true)
                {
                    Info_Img.transform.gameObject.SetActive(true);
                    Info_Img.sprite = m_DataManager.Item_Spr[_index];
                    
                    switch(m_ItemData[_index].sRank)
                    {
                        case "Common":
                            Name_Txt.text = m_ItemData[_index].sName + "(평범)";
                            break;
                        case "Rare":
                            Name_Txt.text = m_ItemData[_index].sName + "(희귀)";
                            break;
                        case "Epic":
                            Name_Txt.text = m_ItemData[_index].sName + "(영웅)";
                            break;
                        case "Legend":
                            Name_Txt.text = m_ItemData[_index].sName + "(전설)";
                            break;
                    }

                    string _name = "타입 : " + m_ItemData[_index].sType + "\n" + "가격 : " + m_ItemData[_index].nCost + "\n";
                    if(m_ItemData[_index].fHealth != "0")
                    {
                        _name = _name + "체력 : " + m_ItemData[_index].fHealth + "%" + "\n";
                    }
                    if(m_ItemData[_index].fAttack != "0")
                    {
                        _name = _name + "공격력 : " + m_ItemData[_index].fAttack + "%" + "\n";
                    }
                    if(m_ItemData[_index].fMagic != "0")
                    {
                        _name = _name + "주문력 : " + m_ItemData[_index].fMagic + "%" + "\n";
                    }
                    if(m_ItemData[_index].fDefense != "0")
                    {
                        _name = _name + "방어력 : " + m_ItemData[_index].fDefense + "%" + "\n";
                    }
                    if(m_ItemData[_index].fSpeed != "0")
                    {
                        _name = _name + "행동력 : " + m_ItemData[_index].fSpeed + "%";
                    }

                    Ability_Txt.text = _name;
                    Exp_Txt.text = m_ItemData[_index].sExplain;
                }
                else
                {
                    Info_Img.transform.gameObject.SetActive(false);
                    Info_Img.sprite = m_DataManager.Item_Spr[_index];
                    Name_Txt.text = "???";
                    Ability_Txt.text = "???";
                    Exp_Txt.text = "???";
                }
                break;
            default:
                break;
        }
    }

    public void FilterTap()
    {

    }

    public void SelectClassfication(int _nclassfication)
    {
        _nclassfication = Classfication_DD.value;

        nClass = _nclassfication;

        int i = 0;

        for (i = 0; i < Slots.Length; i++)
        {
            Slots[i].gameObject.SetActive(false);
        }

        i = 0;

        switch(_nclassfication)
        {
            case 0:
                switch(nOrder)
                {
                    case 0:
                        foreach (var type in m_ItemData.OrderBy(n => n.nIndex))
                        {
                            Slots[i].UnlockSlot(nType, int.Parse(type.nIndex), type.bUnlock);
                            Slots[i].gameObject.SetActive(true);

                            i++;
                        }
                        i = 0;
                        break;
                    case 1:
                        foreach (var type in m_ItemData.OrderByDescending(n => n.nIndex))
                        {
                            Slots[i].UnlockSlot(nType, int.Parse(type.nIndex), type.bUnlock);
                            Slots[i].gameObject.SetActive(true);

                            i++;
                        }
                        i = 0;
                        break;
                }
                break;
            case 1:
                switch(nOrder)
                {
                    case 0:
                        foreach (var type in m_ItemData.OrderBy(n => n.sName))
                        {
                            Slots[i].UnlockSlot(nType, int.Parse(type.nIndex), type.bUnlock);
                            Slots[i].gameObject.SetActive(true);

                            i++;
                        }
                        i = 0;
                        break;
                    case 1:
                        foreach (var type in m_ItemData.OrderByDescending(n=>n.sName))
                        {
                            Slots[i].UnlockSlot(nType, int.Parse(type.nIndex), type.bUnlock);
                            Slots[i].gameObject.SetActive(true);

                            i++;
                        }
                        i = 0;
                        break;
                }
                break;
            case 2:
                switch (nOrder)
                {
                    case 0:
                        foreach (var type in m_ItemData.OrderBy(n => n.sType))
                        {
                            Slots[i].UnlockSlot(nType, int.Parse(type.nIndex), type.bUnlock);
                            Slots[i].gameObject.SetActive(true);

                            i++;
                        }
                        i = 0;
                        break;
                    case 1:
                        foreach (var type in m_ItemData.OrderByDescending(n => n.sType))
                        {
                            Slots[i].UnlockSlot(nType, int.Parse(type.nIndex), type.bUnlock);
                            Slots[i].gameObject.SetActive(true);

                            i++;
                        }
                        i = 0;
                        break;
                }
                break;
            case 3:
                switch (nOrder)
                {
                    case 0:
                        foreach (var type in m_ItemData.OrderBy(n=> (eRank = (ERank)Enum.Parse(typeof(ERank), n.sRank)).ToString()))
                        {
                            Slots[i].UnlockSlot(nType, int.Parse(type.nIndex), type.bUnlock);
                            Slots[i].gameObject.SetActive(true);

                            i++;
                        }
                        i = 0;
                        break;
                    case 1:
                        foreach (var type in m_ItemData.OrderByDescending(n => (eRank = (ERank)Enum.Parse(typeof(ERank), n.sRank)).ToString()))
                        {
                            Slots[i].UnlockSlot(nType, int.Parse(type.nIndex), type.bUnlock);
                            Slots[i].gameObject.SetActive(true);

                            i++;
                        }
                        i = 0;
                        break;
                }
                break;

            default:
                break;
        }
    }

    public void SelectOrder(int _norder)
    {
        _norder = Order_DD.value;

        nOrder = _norder;

        int i = 0;

        for (i = 0; i < Slots.Length; i++)
        {
            Slots[i].gameObject.SetActive(false);
        }

        i = 0;

        switch(_norder)
        {
            case 0:
                switch(nClass)
                {
                    case 0:
                        foreach (var type in m_ItemData.OrderBy(n => n.nIndex))
                        {
                            Slots[i].UnlockSlot(nType, int.Parse(type.nIndex), type.bUnlock);
                            Slots[i].gameObject.SetActive(true);

                            i++;
                        }
                        i = 0;
                        break;
                    case 1:
                        foreach (var type in m_ItemData.OrderBy(n => n.sName))
                        {
                            Slots[i].UnlockSlot(nType, int.Parse(type.nIndex), type.bUnlock);
                            Slots[i].gameObject.SetActive(true);

                            i++;
                        }
                        i = 0;
                        break;
                    case 2:
                        foreach (var type in m_ItemData.OrderBy(n => n.sType))
                        {
                            Slots[i].UnlockSlot(nType, int.Parse(type.nIndex), type.bUnlock);
                            Slots[i].gameObject.SetActive(true);

                            i++;
                        }
                        i = 0;
                        break;
                    case 3:
                        foreach (var type in m_ItemData.OrderBy(n => (eRank = (ERank)Enum.Parse(typeof(ERank), n.sRank)).ToString()))
                        {
                            Slots[i].UnlockSlot(nType, int.Parse(type.nIndex), type.bUnlock);
                            Slots[i].gameObject.SetActive(true);

                            i++;
                        }
                        i = 0;
                        break;
                }
                break;
            case 1:
                switch(nClass)
                {
                    case 0:
                        foreach (var type in m_ItemData.OrderByDescending(n => n.nIndex))
                        {
                            Slots[i].UnlockSlot(nType, int.Parse(type.nIndex), type.bUnlock);
                            Slots[i].gameObject.SetActive(true);

                            i++;
                        }
                        i = 0;
                        break;
                    case 1:
                        foreach (var type in m_ItemData.OrderByDescending(n => n.sName))
                        {
                            Slots[i].UnlockSlot(nType, int.Parse(type.nIndex), type.bUnlock);
                            Slots[i].gameObject.SetActive(true);

                            i++;
                        }
                        i = 0;
                        break;
                    case 2:
                        foreach (var type in m_ItemData.OrderByDescending(n => n.sType))
                        {
                            Slots[i].UnlockSlot(nType, int.Parse(type.nIndex), type.bUnlock);
                            Slots[i].gameObject.SetActive(true);

                            i++;
                        }
                        i = 0;
                        break;
                    case 3:
                        foreach (var type in m_ItemData.OrderByDescending(n =>(eRank = (ERank)Enum.Parse(typeof(ERank), n.sRank)).ToString()))
                        {
                            Slots[i].UnlockSlot(nType, int.Parse(type.nIndex), type.bUnlock);
                            Slots[i].gameObject.SetActive(true);
                            
                            i++;
                        }
                        i = 0;
                        break;
                }
                break;
        }        
    }
}
