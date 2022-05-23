using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    DataManager m_DataManager;
    [SerializeField]
    PlayerData m_PlayerData;
    [SerializeField]
    List<ItemData> m_ItemData;
    [SerializeField]
    List<CharacterData> m_CharacterData;

    public PlatformScript m_PlatformScript;
    public TradeManager m_TradeManager;
    public ReinforceManager m_ReinforceManager;

    public GameObject Trade_Obj;

    public GameObject Reinforce_Obj;

    public Button Trade_Btn;
    public RectTransform Trade_RT;
    public Button Reinforce_Btn;
    public RectTransform Reinforce_Rt;
    public Button Buy_Btn;

    public Text Result_Txt;
    public Text AnswerItem_Txt;

    [SerializeField]
    private ColorBlock SelectColorBlock;
    [SerializeField]
    private ColorBlock UnselectColorBlock;

    public int nIndex;
    public int nIndexPrev;
    public int nSlot;
    public int nSlotPrev;
    public int nSelect;

    // Start is called before the first frame update
    void Start()
    {
        m_DataManager = GameObject.FindGameObjectWithTag("Info").GetComponent<DataManager>();
        m_PlayerData = m_PlatformScript.m_PlayerData;
        m_ItemData = m_PlatformScript.m_ItemData;
        m_CharacterData = m_PlatformScript.m_CharacterData;

        SelectColorBlock = Trade_Btn.colors;
        UnselectColorBlock = Reinforce_Btn.colors;

        Result_Txt.text = "가격 : 0 콜 | 결제 후 : 0 콜";

        //m_ReinforceManager.SetReinforceChar();
        //m_TradeManager.SetTradeItem();
    }

    public void OpenShop()
    {
        m_TradeManager.SetTradeItem();
        m_ReinforceManager.SetReinforceChar();
    }

    public void OpenTradeTap()
    {
        if(nSelect==0)
        {
            return;
        }

        nSelect = 0;
        nIndex = -1;
        nSlot = -1;

        Trade_Btn.colors = SelectColorBlock;
        Reinforce_Btn.colors = UnselectColorBlock;

        Trade_RT.anchoredPosition = new Vector2(5, Trade_RT.anchoredPosition.y);
        Reinforce_Rt.anchoredPosition = new Vector2(35, Reinforce_Rt.anchoredPosition.y);
        Reinforce_Obj.SetActive(false);
        Trade_Obj.SetActive(true);

        SelectSlot(-1, -1);
    }

    public void OpenReinforceTap()
    {
        if(nSelect==1)
        {
            return;
        }

        nSelect = 1;
        nIndex = -1;
        nSlot = -1;

        Trade_Btn.colors = UnselectColorBlock;
        Reinforce_Btn.colors = SelectColorBlock;

        Trade_RT.anchoredPosition = new Vector2(35, Trade_RT.anchoredPosition.y);
        Reinforce_Rt.anchoredPosition = new Vector2(5, Reinforce_Rt.anchoredPosition.y);
        Trade_Obj.SetActive(false);
        Reinforce_Obj.SetActive(true);

        SelectSlot(-1, -1);
    }

    public void SelectSlot(int _nindex, int _nslot)
    {
        nIndex = _nindex;
        nSlot = _nslot;

        if(nIndex <= -1 || nSlot <= -1)
        {
            Result_Txt.text = "가격 : 0 콜 | 결제 후 : 0 콜";

            return;
        }

        switch(nSelect)
        {
            case 0:
                Result_Txt.text = "가격 : " + m_ItemData[_nindex].nCost + " | 결제 후 : " + (m_PlayerData.nCoin - int.Parse(m_ItemData[_nindex].nCost));
                Buy_Btn.interactable = true;

                Debug.Log(nIndex);
                if ((m_PlayerData.nCoin - int.Parse(m_ItemData[_nindex].nCost))<0)
                {
                    Buy_Btn.interactable = false;
                }
                break;
            case 1:
                Result_Txt.text = "가격 : " + (m_PlayerData.nRank[_nindex] * 50) + " | 결제 후 : " + (m_PlayerData.nCoin - (m_PlayerData.nRank[_nindex] * 50));
                Buy_Btn.interactable = true;
                if(m_PlayerData.nRank[nIndex]>=m_PlayerData.nLevel[nIndex])
                {
                    Buy_Btn.interactable = false;
                    m_ReinforceManager.ReinforceMax(nSlot);
                }

                if(m_PlayerData.nCoin - (m_PlayerData.nRank[_nindex] * 50)<0)
                {
                    Buy_Btn.interactable = false;
                }
                break;
            default:
                break;
        }

        nIndexPrev = nIndex;
        nSlotPrev = nSlot;
    }

    public void AnswerBuy()
    {
        switch(nSelect)
        {
            case 0:
                m_TradeManager.ItemSelect(nSlotPrev);
                AnswerItem_Txt.text = m_ItemData[nIndexPrev].sName + "을 구매하시겠습니까?";
                Result_Txt.text = "가격 : " + m_ItemData[nIndexPrev].nCost + " | 결제 후 : " + (m_PlayerData.nCoin - int.Parse(m_ItemData[nIndexPrev].nCost));
                break;
            case 1:
                m_ReinforceManager.RenewalInfo(nSlotPrev);
                AnswerItem_Txt.text = m_CharacterData[nIndexPrev].sName + "을 강화하시겠습니까?";
                Result_Txt.text = "가격 : " + (m_PlayerData.nRank[nIndexPrev] * 50) + " | 결제 후 : " + (m_PlayerData.nCoin - (m_PlayerData.nRank[nIndexPrev] * 50));
                break;
        }
    }


    public void PayCheck()
    {
        switch(nSelect)
        {
            case 0:
                m_PlayerData.nCoin -= int.Parse(m_ItemData[nIndexPrev].nCost);
                //for문 돌려서 빈 공간에다가 아이템 정보 넣은 후 nindex -1로 초기화하고 SelectSlot 부르기
                for(int i = 0; i < m_PlayerData.nItemIndex.Length; i++)
                {
                    if(m_PlayerData.nItemIndex[i]=="")
                    {
                        m_PlayerData.nItemIndex[i] = nIndexPrev.ToString();
                        m_PlayerData.sItemName[i] = m_ItemData[nIndexPrev].sName;
                        m_ItemData[nIndexPrev].bUnlock = true;
                        nIndexPrev = -1;
                        break;
                    }                   
                }
                m_TradeManager.ItemSold(nSlotPrev);

                SelectSlot(nIndexPrev, nSlotPrev);

                nIndexPrev = nIndex;
                nSlotPrev = nSlot;

                Buy_Btn.interactable = false;

                m_DataManager.SavePlayerDataToJson();
                m_DataManager.SaveListDataToJson();
                break;
            case 1:
                m_PlayerData.nCoin -= (m_PlayerData.nRank[nIndexPrev] * 50);
                //캐릭터 성능 업그레이드 후 그대로 SelectSlot 부르기
                m_PlayerData.nRank[nIndexPrev] += 1;
                m_PlayerData.fHealth[nIndexPrev] += float.Parse(m_CharacterData[int.Parse(m_PlayerData.nCharIndex[nIndexPrev])].fHealth);
                m_PlayerData.fAttack[nIndexPrev] += float.Parse(m_CharacterData[int.Parse(m_PlayerData.nCharIndex[nIndexPrev])].fAttack);
                m_PlayerData.fMagic[nIndexPrev] += float.Parse(m_CharacterData[int.Parse(m_PlayerData.nCharIndex[nIndexPrev])].fMagic);
                m_PlayerData.fDefense[nIndexPrev] += float.Parse(m_CharacterData[int.Parse(m_PlayerData.nCharIndex[nIndexPrev])].fDefense);
                m_PlayerData.fSpeed[nIndexPrev] += float.Parse(m_CharacterData[int.Parse(m_PlayerData.nCharIndex[nIndexPrev])].fSpeed);

                nIndex = nIndexPrev;
                nSlot = nSlotPrev;

                m_ReinforceManager.RenewalInfo(nSlotPrev);

                SelectSlot(nIndex, nSlot);

                m_DataManager.SavePlayerDataToJson();
                m_DataManager.SaveListDataToJson();
                break;
            default:
                break;
        }
    }

    public void UnSelect()
    {
        
        switch(nSelect)
        {
            case 0:
                m_TradeManager.ItemUnselect(nSlotPrev);
                break;

            case 1:
                m_ReinforceManager.UnSelect(nSlotPrev);
                break;
        }
    }

    public void CloseShop()
    {
        OpenShop();
    }
}
