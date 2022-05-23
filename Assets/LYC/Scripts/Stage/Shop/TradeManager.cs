using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeManager : MonoBehaviour
{
    [SerializeField]
    DataManager m_DataManager;

    public ShopManager m_ShopManager;
    public PlatformScript m_PlatformScript;
    public PlayerData m_PlayerData;
    public List<ItemData> m_ItemData;

    public List<ItemData> CommonDeck = new List<ItemData>();
    public List<ItemData> RareDeck = new List<ItemData>();
    public List<ItemData> EpicDeck = new List<ItemData>();
    public List<ItemData> LegendDeck = new List<ItemData>();

    public int total = 0;

    [SerializeField]
    private GameObject SlotsBase_Obj;

    [SerializeField]
    private TradeSlot[] Slots;

    public TradeSlot[] GetSlots() { return Slots; }

    [ContextMenu("s")]
    void Start()
    {
        m_DataManager = GameObject.FindGameObjectWithTag("Info").GetComponent<DataManager>();

        m_PlayerData = m_PlatformScript.m_PlayerData;
        m_ItemData = m_PlatformScript.m_ItemData;
            
    }

    [ContextMenu("b")]
    public void SetDeck()
    {
        CommonDeck.Clear();
        RareDeck.Clear();
        EpicDeck.Clear();
        LegendDeck.Clear();

        for (int i = 0; i < m_ItemData.Count; i++)
        {
            switch (m_ItemData[i].sRank)
            {
                case "Common":
                    CommonDeck.Add(m_ItemData[i]);
                    break;
                case "Rare":
                    RareDeck.Add(m_ItemData[i]);
                    break;
                case "Epic":
                    EpicDeck.Add(m_ItemData[i]);
                    break;
                case "Legend":
                    LegendDeck.Add(m_ItemData[i]);
                    break;
                default:
                    break;
            }
        }

        for (int i = 0; i < m_PlayerData.nItemIndex.Length; i++)
        {
            if (m_PlayerData.nItemIndex[i] == "")
            {
                break;
            }

            for (int j = 0; j < CommonDeck.Count; j++)
            {
                if (m_PlayerData.nItemIndex[i] == CommonDeck[j].nIndex)
                {
                    CommonDeck.RemoveAt(j);
                }
            }
        }

        for (int i = 0; i < m_PlayerData.nItemIndex.Length; i++)
        {
            if (m_PlayerData.nItemIndex[i] == "")
            {
                break;
            }

            for (int j = 0; j < RareDeck.Count; j++)
            {
                if (m_PlayerData.nItemIndex[i] == RareDeck[j].nIndex)
                {
                    RareDeck.RemoveAt(j);
                }
            }
        }

        for (int i = 0; i < m_PlayerData.nItemIndex.Length; i++)
        {
            if (m_PlayerData.nItemIndex[i] == "")
            {
                break;
            }

            for (int j = 0; j < EpicDeck.Count; j++)
            {
                if (m_PlayerData.nItemIndex[i] == EpicDeck[j].nIndex)
                {
                    EpicDeck.RemoveAt(j);
                }
            }
        }

        for (int i = 0; i < m_PlayerData.nItemIndex.Length; i++)
        {
            if (m_PlayerData.nItemIndex[i] == "")
            {
                break;
            }

            for (int j = 0; j < LegendDeck.Count; j++)
            {
                if (m_PlayerData.nItemIndex[i] == LegendDeck[j].nIndex)
                {
                    LegendDeck.RemoveAt(j);
                }
            }
        }
    }

    public void SetTradeItem()
    {
        Start();

        SetDeck();

        int setItemNum;

        for(int i=0; i < Slots.Length; i++)
        {
            if(i<=3)
            {
                setItemNum = ItemResult(i, 0);
                
                if (setItemNum <= -1)
                {
                    Slots[i].LockSlot();
                    Slots[i].gameObject.SetActive(true);
                }
                else
                {
                    Slots[i].SetItemSlot(setItemNum, i);
                    Slots[i].gameObject.SetActive(true);
                }
            }
            else if(i<=6)
            {
                setItemNum = ItemResult(i, 1);

                if (setItemNum <= -1)
                {
                    Slots[i].LockSlot();
                    Slots[i].gameObject.SetActive(true);
                }
                else
                {
                    Slots[i].SetItemSlot(setItemNum, i);
                    Slots[i].gameObject.SetActive(true);
                }
            }
            else if(i<=8)
            {
                setItemNum = ItemResult(i, 2);


                if (setItemNum <= -1)
                {
                    Slots[i].LockSlot();
                    Slots[i].gameObject.SetActive(true);
                }
                else
                {
                    Slots[i].SetItemSlot(setItemNum, i);
                    Slots[i].gameObject.SetActive(true);
                }
            }
            else if(i<=9)
            {
                setItemNum = ItemResult(i, 3);


                if (setItemNum <= -1)
                {
                    Slots[i].LockSlot();
                    Slots[i].gameObject.SetActive(true);
                }
                else
                {
                    Slots[i].SetItemSlot(setItemNum, i);
                    Slots[i].gameObject.SetActive(true);
                }
            }
        }
    }

    int ItemResult(int _slot, int _rank)
    {

        int selectNum = 0;

        switch(_rank)
        {
            case 0:

                for (int i = 0; i < m_PlayerData.nItemIndex.Length; i++)
                {
                    if (m_PlayerData.nItemIndex[i] == "")
                    {
                        break;
                    }

                    for (int j = 0; j < CommonDeck.Count; j++)
                    {
                        if (m_PlayerData.nItemIndex[i] == CommonDeck[j].nIndex)
                        {
                            CommonDeck.RemoveAt(j);
                        }
                    }
                }

                int commonNum = CommonDeck.Count;

                for (int i = 0; i <m_PlayerData.nItemIndex.Length; i++) //만약 common 아이템을 다 갖고 있을 때
                {
                    if(m_PlayerData.nItemIndex[i] == "")
                    {
                        break;
                    }

                    if(m_ItemData[int.Parse(m_PlayerData.nItemIndex[i])].sRank == "Common")
                    {
                        commonNum -= 1;

                        if(commonNum <= 0)
                        {
                            return -1;
                        }
                    }
                }

                Debug.Log(CommonDeck.Count);
                selectNum = Random.Range(0, CommonDeck.Count);

                if (_slot>0)
                {
                    if (selectNum == Slots[_slot - 1].nIndex)
                    {
                        CommonDeck.RemoveAt(selectNum);
                        Debug.Log("일반1");
                        selectNum = Random.Range(0, CommonDeck.Count);
                        //selectNum = ItemResult(_slot, 0);
                    }
                }               

                //for (int i = 0; i < m_PlayerData.nItemIndex.Length; i++)
                //{
                //    if(m_PlayerData.nItemIndex[i] == "")
                //    {
                //        break;
                //    }

                //    if(selectNum == int.Parse(m_PlayerData.nItemIndex[i]))
                //    {
                //        Debug.Log("일반2");
                //        selectNum = ItemResult(_slot, 0);
                //        break;
                //    }
                //}

                Debug.Log(selectNum);
                return int.Parse(CommonDeck[selectNum].nIndex);
            case 1:

                for (int i = 0; i < m_PlayerData.nItemIndex.Length; i++)
                {
                    if (m_PlayerData.nItemIndex[i] == "")
                    {
                        break;
                    }

                    for (int j = 0; j < RareDeck.Count; j++)
                    {
                        if (m_PlayerData.nItemIndex[i] == RareDeck[j].nIndex)
                        {
                            RareDeck.RemoveAt(j);
                        }
                    }
                }

                int rareNum = RareDeck.Count;

                for (int i = 0; i < m_PlayerData.nItemIndex.Length; i++)
                {
                    if (m_PlayerData.nItemIndex[i] == "")
                    {
                        break;
                    }

                    if (m_ItemData[int.Parse(m_PlayerData.nItemIndex[i])].sRank == "Rare")
                    {
                        rareNum -= 1;

                        if (rareNum <= 0)
                        {
                            return -1;
                        }
                    }
                }

                Debug.Log(RareDeck.Count);
                selectNum = Random.Range(0, RareDeck.Count);

                if (_slot > 0)
                {
                    if (selectNum == Slots[_slot - 1].nIndex)
                    {
                        RareDeck.RemoveAt(selectNum);
                        Debug.Log("레어1");
                        selectNum = Random.Range(0, RareDeck.Count);
                        //selectNum = ItemResult(_slot, 1);
                    }
                }

                //for (int i = 0; i < m_PlayerData.nItemIndex.Length; i++)
                //{
                //    if (m_PlayerData.nItemIndex[i] == "")
                //    {
                //        break;
                //    }

                //    if (selectNum == int.Parse(m_PlayerData.nItemIndex[i]))
                //    {
                //        Debug.Log("레어2");
                //        selectNum = ItemResult(_slot, 1);
                //        break;
                //    }
                //}

                Debug.Log(selectNum);
                return int.Parse(RareDeck[selectNum].nIndex);

            case 2:

                for (int i = 0; i < m_PlayerData.nItemIndex.Length; i++)
                {
                    if (m_PlayerData.nItemIndex[i] == "")
                    {
                        break;
                    }

                    for (int j = 0; j < EpicDeck.Count; j++)
                    {
                        if (m_PlayerData.nItemIndex[i] == EpicDeck[j].nIndex)
                        {
                            EpicDeck.RemoveAt(j);
                        }
                    }
                }

                int epicNum = EpicDeck.Count;

                for (int i = 0; i < m_PlayerData.nItemIndex.Length; i++)
                {
                    if (m_PlayerData.nItemIndex[i] == "")
                    {
                        break;
                    }

                    if (m_ItemData[int.Parse(m_PlayerData.nItemIndex[i])].sRank == "Epic")
                    {
                        epicNum -= 1;

                        if (epicNum <= 0)
                        {
                            return -1;
                        }
                    }
                }

                Debug.Log(EpicDeck.Count);
                selectNum = Random.Range(0, EpicDeck.Count);

                if (_slot > 0)
                {
                    if (selectNum == Slots[_slot - 1].nIndex)
                    {
                        EpicDeck.RemoveAt(selectNum);
                        Debug.Log("에픽1");
                        selectNum = Random.Range(0, EpicDeck.Count);
                        //selectNum = ItemResult(_slot, 2);
                    }
                }

                //for (int i = 0; i < m_PlayerData.nItemIndex.Length; i++)
                //{
                //    if (m_PlayerData.nItemIndex[i] == "")
                //    {
                //        break;
                //    }

                //    if (selectNum == int.Parse(m_PlayerData.nItemIndex[i]))
                //    {
                //        Debug.Log("에픽2");
                //        selectNum = ItemResult(_slot, 2);
                //        break;
                //    }
                //}

                Debug.Log(selectNum);
                return int.Parse(EpicDeck[selectNum].nIndex);

            case 3:

                for (int i = 0; i < m_PlayerData.nItemIndex.Length; i++)
                {
                    if (m_PlayerData.nItemIndex[i] == "")
                    {
                        break;
                    }

                    for (int j = 0; j < LegendDeck.Count; j++)
                    {
                        if (m_PlayerData.nItemIndex[i] == LegendDeck[j].nIndex)
                        {
                            LegendDeck.RemoveAt(j);
                        }
                    }
                }

                int legendNum = LegendDeck.Count;

                for (int i = 0; i < m_PlayerData.nItemIndex.Length; i++)
                {
                    if (m_PlayerData.nItemIndex[i] == "")
                    {
                        break;
                    }

                    if (m_ItemData[int.Parse(m_PlayerData.nItemIndex[i])].sRank == "Legend")
                    {
                        legendNum -= 1;

                        if (legendNum <= 0)
                        {
                            return -1;
                        }
                    }
                }

                Debug.Log(LegendDeck.Count);
                selectNum = Random.Range(0, LegendDeck.Count);

                if (_slot > 0)
                {
                    if (selectNum == Slots[_slot - 1].nIndex)
                    {
                        LegendDeck.RemoveAt(selectNum);
                        Debug.Log("레전드1");
                        selectNum = Random.Range(0, LegendDeck.Count);
                        //selectNum = ItemResult(_slot, 3);
                    }
                }

                //for (int i = 0; i < m_PlayerData.nItemIndex.Length; i++)
                //{
                //    if (m_PlayerData.nItemIndex[i] == "")
                //    {
                //        break;
                //    }

                //    if (selectNum == int.Parse(m_PlayerData.nItemIndex[i]))
                //    {
                //        Debug.Log("레전드2");
                //        selectNum = ItemResult(_slot, 3);
                //        break;
                //    }
                //}

                Debug.Log(selectNum);
                return int.Parse(LegendDeck[selectNum].nIndex);

            default:
                return -1;
        }
    }

    public void ItemSold(int _slots)
    {
        Slots[_slots].ItemSold();
    }

    public void ItemSelect(int _slots)
    {
        Slots[_slots].ItemSelect();
    }

    public void ItemUnselect(int _slots)
    {
        Slots[_slots].ItemUnSelect();
    }
}
