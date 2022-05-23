using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MHomiLibrary;
using System;
using UnityEngine.Rendering;
using UnityEngine.UI;

[Serializable]
public class PrefabDic : SerializableDictionary<string, PrefabDataClass> { }

[Serializable]
public class SkilPrefabDic : SerializableDictionary<string, SkillInfoClass> { }

[Serializable]
public class HeroItemListPrefabDic : SerializableDictionary<string, HeroItem> { }

[Serializable]
public class ParticlePrefabDic : SerializableDictionary<string, GameObject> { }

[Serializable]
public class SoundPrefabDic : SerializableDictionary<string, AudioClip> { }


[Serializable]
public class SkillInfoClass
{
    public Sprite icon;
    public string name;
    public string skillSummary;    
}

[Serializable]
public class PrefabDataClass
{
    public GameObject prefab;
    public Sprite Portrait;
}

public class GameInstance : MonoBehaviour
{

    [Header("변수들")]
    [Space(20f)]

    public GameData gamdData;
    private static GameInstance instance = null;
    public ENEMY_TYPE enemy1;
    public ENEMY_TYPE enemy2;
    public ENEMY_TYPE enemy3;
    public ENEMY_TYPE eliteENum;
    public ENEMY_TYPE[] enemies;
    public ENEMY_TYPE eliteEnemy;




    public int nowStage;

    [Space(20f)]

    [Header("프리팹 저장용")]
    [Space(20f)]

    public SoundPrefabDic soundPrefab;

    public PrefabDic EnemyPrefab;
    [Space(10f)]

    public PrefabDic PlayerPrefab;
    [Space(10f)]

    public SkilPrefabDic SkillInfoData;
    [Space(10f)]

    public ParticlePrefabDic particlePrefab;
    [Space(10f)]

    [Header("아이템 저장용")]
    [Space(10f)]
    public HeroItemListPrefabDic ItemDatas;

    



    private void Awake()
    {
        //싱글톤 선언
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static GameInstance Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 프리펩 만들기
    /// </summary>
    /// <param name="type">딕셔너리 인덱스</param>
    /// <param name="PosV3">생성위치</param>
    /// <param name="ScaleV3">생성 크기</param>
    /// <param name="Rot">생성시 회전값</param>
    /// <returns></returns>
    public GameObject CreateEnemyPrefab(string type,                                   
                                   Vector3 PosV3,
                                   Vector3 ScaleV3,
                                   Quaternion Rot)
    {
        GameObject prefab = EnemyPrefab[type].prefab;
    
        if(type == "SKELETON")
        {
            Rot = Quaternion.Euler(Rot.x, 180f, Rot.z);
            ScaleV3 = Vector3.one;
        }

        if (prefab)
        {            
            GameObject obj = Instantiate(prefab, PosV3, Rot);
            obj.GetComponent<Unit>().unitSprite = EnemyPrefab[type].Portrait;
            obj.transform.localScale = ScaleV3;
            if (obj.GetComponent<SortingGroup>())
            {
                obj.GetComponent<SortingGroup>().sortingOrder = 3;
            }
            Enemy enemy = obj.GetComponent<Enemy>();

            if (PosV3.y == 1f)
            {
                enemy.SortLayerOrder = 200;
                enemy.sortingGroup.sortingOrder = 200;
                print("??");
            }
            else if(PosV3.y == -1f)
            {
                enemy.SortLayerOrder = 205;
                enemy.sortingGroup.sortingOrder = 205;
            }
            else
            {
                enemy.SortLayerOrder = 210;
                enemy.sortingGroup.sortingOrder = 210;
            }
            return obj;
        }
        return null;
    }

    /// <summary>
    /// 프리펩 만들기
    /// </summary>
    /// <param name="type">딕셔너리 인덱스</param>
    /// <param name="PosV3">생성위치</param>
    /// <param name="ScaleV3">생성 크기</param>
    /// <param name="Rot">생성시 회전값</param>
    /// <returns></returns>
    public GameObject CreateHeroPrefab(string type,
                                   Vector3 PosV3,
                                   Vector3 ScaleV3,
                                   Quaternion Rot)
    {
        GameObject prefab = PlayerPrefab[type].prefab;

        if (prefab)
        {
            GameObject obj = Instantiate(prefab, PosV3, Rot);
            obj.GetComponent<Unit>().unitSprite = PlayerPrefab[type].Portrait;
            obj.transform.localScale = ScaleV3;
            return obj;
        }
        return null;
    }
}
