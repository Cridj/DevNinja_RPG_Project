using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCollection : Collection<Hero>
{

}

public class Collection<T> : MonoBehaviour 
{
    public List<T> collection = new List<T>();
    
    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        int nCount = transform.childCount;
        //int nCount = ;
        for (int i = 0; i < nCount; i++)
        {
            if(!transform.GetChild(i).gameObject)
            {
                continue;
            }
            Transform PlayerTm = transform.GetChild(i);
            collection.Add(PlayerTm.GetComponent<T>());
        }
    }

    public void Init2()
    {
        int nCount = transform.childCount;
        //int nCount = ;
        print(nCount);
        for (int i = 6; i < nCount; i++)
        {
            if (!transform.GetChild(i).gameObject.activeSelf)
            {
                continue;
            }
            Transform PlayerTm = transform.GetChild(i);
            if (PlayerTm == null)
                continue;
            collection.Add(PlayerTm.GetComponent<T>());
        }
    }



}