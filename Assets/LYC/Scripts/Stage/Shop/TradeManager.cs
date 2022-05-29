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

    int nCommon, nRare, nEpic, nLegend;

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

        nCommon = CommonDeck.Count;
        nRare = RareDeck.Count;
        nEpic = EpicDeck.Count;
        nLegend = LegendDeck.Count;

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

        nCommon = CommonDeck.Count;
        nRare = RareDeck.Count;
        nEpic = EpicDeck.Count;
        nLegend = LegendDeck.Count;

        for (int i = 0; i < m_PlayerData.nItemIndex.Length; i++)
        {
            if (m_PlayerData.nItemIndex[i] == "" || m_PlayerData.nItemIndex[i] == null)
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
            if (m_PlayerData.nItemIndex[i] == "" || m_PlayerData.nItemIndex[i] == null)
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
            if (m_PlayerData.nItemIndex[i] == "" || m_PlayerData.nItemIndex[i] == null)
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
            if (m_PlayerData.nItemIndex[i] == "" || m_PlayerData.nItemIndex[i] == null)
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
                    if (m_PlayerData.nItemIndex[i] == "" || m_PlayerData.nItemIndex[i] == null)
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

                Debug.Log(CommonDeck.Count+"카운트");
                for (int i = 0; i <m_PlayerData.nItemIndex.Length; i++) //만약 common 아이템을 다 갖고 있을 때
                {
                    if(m_PlayerData.nItemIndex[i] == "" || m_PlayerData.nItemIndex == null)
                    {
                        break;
                    }

                    Debug.Log(m_PlayerData.nItemIndex[i]);

                    if (int.TryParse(m_PlayerData.nItemIndex[i], out int nIndexNum))
                    {
                        if (m_ItemData[nIndexNum].sRank == "Common")
                        {
                            commonNum -= 1;
                            
                            Debug.Log(commonNum+"커먼넘");

                            if (commonNum <= 0)
                            {
                                Debug.Log("버림");
                                return -1;
                            }
                        }
                    }
                }

                Debug.Log(CommonDeck.Count);
                selectNum = Random.Range(0, CommonDeck.Count);

                //if (_slot>0) //이걸로 대체할 것
                //{
                //    for(int nslots = _slot; nslots>0; nslots--)
                //    {
                //        if(m_ItemData[Slots[nslots - 1].nIndex].sType != "Common")
                //        {
                //            break;
                //        }
                //    }
                //}

                if (_slot>0)  //이거 변경할 것 slots 현 위치부터 0 까지 돌리기
                {
                    if (CommonDeck[selectNum].nIndex == Slots[_slot - 1].nIndex.ToString())
                    {
                        CommonDeck.RemoveAt(selectNum);
                        Debug.Log("일반1");
                        selectNum = Random.Range(0, CommonDeck.Count);
                    }
                }               

                Debug.Log(selectNum);

                if(int.TryParse(CommonDeck[selectNum].nIndex, out int nResult0))
                {
                    return nResult0;
                }
                else
                {
                    return -1;
                }               
            case 1:

                for (int i = 0; i < m_PlayerData.nItemIndex.Length; i++)
                {
                    if (m_PlayerData.nItemIndex[i] == "" || m_PlayerData.nItemIndex == null)
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
                    if (m_PlayerData.nItemIndex[i] == "" || m_PlayerData.nItemIndex == null)
                    {
                        break;
                    }

                    if (int.TryParse(m_PlayerData.nItemIndex[i], out int nIndexNum))
                    {
                        if (m_ItemData[nIndexNum].sRank == "Rare")
                        {
                            rareNum -= 1;

                            if (rareNum <= 0)
                            {
                                return -1;
                            }
                        }
                    }
                        
                }

                Debug.Log(RareDeck.Count);
                selectNum = Random.Range(0, RareDeck.Count);

                if (_slot > 0)
                {
                    if (RareDeck[selectNum].nIndex == Slots[_slot - 1].nIndex.ToString())
                    {
                        RareDeck.RemoveAt(selectNum);
                        Debug.Log("레어1");
                        selectNum = Random.Range(0, RareDeck.Count);
                    }
                }

                Debug.Log(selectNum);

                if (int.TryParse(RareDeck[selectNum].nIndex, out int nResult1))
                {
                    return nResult1;
                }
                else
                {
                    return -1;
                }

            case 2:

                for (int i = 0; i < m_PlayerData.nItemIndex.Length; i++)
                {
                    if (m_PlayerData.nItemIndex[i] == "" || m_PlayerData.nItemIndex == null)
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
                    if (m_PlayerData.nItemIndex[i] == "" || m_PlayerData.nItemIndex == null)
                    {
                        break;
                    }

                    if (int.TryParse(m_PlayerData.nItemIndex[i], out int nIndexNum))
                    {
                        if (m_ItemData[nIndexNum].sRank == "Epic")
                        {
                            epicNum -= 1;

                            if (epicNum <= 0)
                            {
                                return -1;
                            }
                        }
                    }                       
                }

                Debug.Log(EpicDeck.Count);
                selectNum = Random.Range(0, EpicDeck.Count);

                if (_slot > 0)
                {
                    if (EpicDeck[selectNum].nIndex == Slots[_slot - 1].nIndex.ToString())
                    {
                        EpicDeck.RemoveAt(selectNum);
                        Debug.Log("에픽1");
                        selectNum = Random.Range(0, EpicDeck.Count);
                    }
                }

                Debug.Log(selectNum);
                if (int.TryParse(EpicDeck[selectNum].nIndex, out int nResult2))
                {
                    return nResult2;
                }
                else
                {
                    return -1;
                }

            case 3:

                for (int i = 0; i < m_PlayerData.nItemIndex.Length; i++)
                {
                    if (m_PlayerData.nItemIndex[i] == "" || m_PlayerData.nItemIndex[i] == null)
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
                    if (m_PlayerData.nItemIndex[i] == "" || m_PlayerData.nItemIndex[i] == null)
                    {
                        break;
                    }

                    if (int.TryParse(m_PlayerData.nItemIndex[i], out int nIndexNum))
                    {
                        if (m_ItemData[nIndexNum].sRank == "Legend")
                        {
                            legendNum -= 1;

                            if (legendNum <= 0)
                            {
                                return -1;
                            }
                        }
                    }
                }

                Debug.Log(LegendDeck.Count);
                selectNum = Random.Range(0, LegendDeck.Count);

                if (_slot > 0)
                {
                    if (LegendDeck[selectNum].nIndex == Slots[_slot - 1].nIndex.ToString())
                    {
                        LegendDeck.RemoveAt(selectNum);
                        Debug.Log("레전드1");
                        selectNum = Random.Range(0, LegendDeck.Count);
                    }
                }

                Debug.Log(selectNum);
                if (int.TryParse(LegendDeck[selectNum].nIndex, out int nResult3))
                {
                    return nResult3;
                }
                else
                {
                    return -1;
                }

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
