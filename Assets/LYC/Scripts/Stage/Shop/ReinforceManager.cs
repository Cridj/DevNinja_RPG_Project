using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReinforceManager : MonoBehaviour
{
    [SerializeField]
    DataManager m_DataManager;

    public ShopManager m_ShopManager;
    public PlatformScript m_PlatformScript;
    public PlayerData m_PlayerData;
    public List<CharacterData> m_CharacterData;

    [SerializeField]
    public GameObject SlotBase_Obj;

    [SerializeField]
    private ReinforceSlot[] Slots;

    public ReinforceSlot[] GetSlots() { return Slots; }

    // Start is called before the first frame update
    void Start()
    {
        m_DataManager = GameObject.FindGameObjectWithTag("Info").GetComponent<DataManager>();
        m_PlayerData = m_PlatformScript.m_PlayerData;
        m_CharacterData = m_PlatformScript.m_CharacterData;
    }

    public void SetReinforceChar()
    {
        Start();

        for(int i = 0; i < Slots.Length; i++)
        {
            
            if (m_PlayerData.nCharIndex[i] == "" || m_PlayerData.nCharIndex[i] == null)
            {
                Slots[i].gameObject.SetActive(false);
                break;
            }

            Slots[i].SetCharSlot(int.Parse(m_PlayerData.nCharIndex[i]), i);
            Slots[i].gameObject.SetActive(true);
        }
    }

    public void ReinforceMax(int _slots)
    {
        Slots[_slots].ReinforceMax();
    }

    public void RenewalInfo(int _slots)
    {
        Slots[_slots].RenewalInfo();
    }

    public void UnSelect(int _slots)
    {
        Slots[_slots].CharUnSelect();
    }
}
