using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public DataManager m_Datamanager;

    public PlayerData m_PlayerData;
    public OptionData m_OptionData;

    public List<MapData> m_MapData;
    public List<CharacterData> m_CharacterData;
    public List<CharacterSkillData> m_CharacterSkillData;
    public List<MonsterData> m_MonsterData;
    public List<MonsterSkillData> m_MonsterSkillData;
    public List<ItemData> m_ItemData;

    private void Start()
    {
        m_Datamanager = GameObject.FindGameObjectWithTag("Info").GetComponent<DataManager>();

        m_Datamanager.m_MapData = m_MapData;
        m_Datamanager.m_PlayerData = m_PlayerData;
        m_Datamanager.m_OptionData = m_OptionData;
        m_Datamanager.MyCharacterList = m_CharacterData;
        m_Datamanager.MyCharacterSkillList = m_CharacterSkillData;
        m_Datamanager.MyMonsterList = m_MonsterData;
        m_Datamanager.MyMonsterSkillList = m_MonsterSkillData;
        m_Datamanager.MyItemList = m_ItemData;
    }
}
