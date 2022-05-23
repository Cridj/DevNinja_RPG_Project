using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSlot : MonoBehaviour
{
    [SerializeField]
    DataManager m_DataManager;
    public PlatformScript m_PlatformScript;
    public List<MonsterData> m_MonsterData;
    public MapManager m_MapManager;
    public Image Mons_Img;
    public Text Mons_Txt;

    public int nIndex;
    public bool bUnlock;

    public void UnlockSlot(int _nIndex, bool _bUnlock)
    {
        m_DataManager = GameObject.FindGameObjectWithTag("Info").GetComponent<DataManager>();

        m_MonsterData = m_PlatformScript.m_MonsterData;

        nIndex = _nIndex;
        bUnlock = _bUnlock;

        if(_bUnlock==true)
        {
            Mons_Img.sprite = m_DataManager.Mons_Spr[nIndex];
            Mons_Img.gameObject.SetActive(true);
            Mons_Txt.text = m_MonsterData[nIndex].sName;
        }
        else
        {
            Mons_Img.sprite = m_DataManager.Mons_Spr[nIndex];
            Mons_Img.gameObject.SetActive(false);
            Mons_Txt.text = "???";
        }
                
    }
}
