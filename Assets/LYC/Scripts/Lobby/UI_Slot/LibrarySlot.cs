using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LibrarySlot : MonoBehaviour
{
    [SerializeField]
    DataManager m_DataManager;
    public PlatformScript m_PlatformScript;
    public LibraryManager m_LibraryManager;
    public PreviewManager m_PreviewManager;
    public Image List_Img;
    public Text Name_Txt;

    public int nType;
    public int nIndex;
    public bool bUnlock;

    private void SetColor(float _alpha)
    {
        Color color = List_Img.color;
        color.a = _alpha;
        List_Img.color = color;
    }

    public void UnlockSlot(int _type, int _index, bool _unlock)
    {
        m_DataManager = GameObject.FindGameObjectWithTag("Info").GetComponent<DataManager>();

        nType = _type;
        nIndex = _index;
        bUnlock = _unlock;
        
        switch (_type)
        {
            case 0:               
                if(_unlock==false)
                {
                    List_Img.sprite = m_DataManager.Unknown_Spr;
                    List_Img.gameObject.SetActive(true);
                    Name_Txt.text = "???";
                }
                else
                {
                    List_Img.sprite = m_DataManager.CharHead_Spr[_index];
                    List_Img.gameObject.SetActive(true);
                    Name_Txt.text = m_LibraryManager.m_CharacterData[_index].sName;
                }
                break;
            case 1:       
                if (_unlock == false)
                {
                    List_Img.sprite = m_DataManager.Unknown_Spr;
                    List_Img.gameObject.SetActive(true);
                    Name_Txt.text = "???";
                }
                else
                {
                    List_Img.sprite = m_DataManager.Mons_Spr[_index];
                    List_Img.gameObject.SetActive(true);
                    Name_Txt.text = m_LibraryManager.m_MonsterData[_index].sName;
                }
                break;
            case 2:       
                if (_unlock == false)
                {
                    List_Img.sprite = m_DataManager.Unknown_Spr;
                    List_Img.gameObject.SetActive(true);
                    Name_Txt.text = "???";
                }
                else
                {
                    List_Img.sprite = m_DataManager.Item_Spr[_index];
                    List_Img.gameObject.SetActive(true);
                    Name_Txt.text = m_LibraryManager.m_ItemData[_index].sName;
                }
                break;

            default:
                break;

        }
    }

    public void ClickTap()
    {
        m_LibraryManager.ClickSlot(nType, nIndex, bUnlock);

        m_PreviewManager.ChangeSlot(nType, nIndex);
    }
}
