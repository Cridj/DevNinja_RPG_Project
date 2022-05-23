using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LYCTesting : MonoBehaviour
{
    public ShopManager m_ShopManager;

    public GameObject test1_Obj;
    public GameObject test2_Obj;
    public GameObject test3_Obj;

    public void OpenShop()
    {
        m_ShopManager.OpenShop();
        test1_Obj.SetActive(true);
    }

    public void CloseShop()
    {
        test1_Obj.SetActive(false);
    }

    public void OpenAnswer()
    {
        test2_Obj.SetActive(true);
    }

    public void CloseAnswer()
    {
        test2_Obj.SetActive(false);
    }

    public void OpenClose()
    {
        test3_Obj.SetActive(true);
    }

    public void CloseClose()
    {
        test3_Obj.SetActive(false);
    }

    public void GotoLobby()
    {
        SceneManager.LoadScene("2_LobbyScene");
    }
}
