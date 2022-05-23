using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemRank {Normal,Rair,Unique,Legend}

public enum ITEMTYPE { KNIFE,AXE,BIBLE,ARMOR,SHIELD}

[Serializable]
public class HeroItem
{
    //Var
    [SerializeField]
    private ItemRank itemRank;

    [SerializeField]
    private ITEMTYPE itemType;

    [SerializeField]
    private string itemName;

    [SerializeField]
    private int HpStat;

    [SerializeField]
    private int AttackStat;

    [SerializeField]
    private int MagicalAttackStat;

    [SerializeField]
    private int DefenseStat;

    [SerializeField]
    private int SpeedStat;

    [SerializeField]
    private Sprite itemSprite;


    //Property

    public ITEMTYPE ItemType { get { return itemType; } set { itemType = value; } }

    public ItemRank ItemRank
    {
        get { return itemRank; }
        set { itemRank = value; }
    }

    public string ItemName
    {
        get { return itemName; }
        set { itemName = value; }   
    }
    public int HP
    {
        get { return HpStat; }
        set { HpStat = value; }
    }

    public int Attack
    {
        get { return AttackStat; }
        set { AttackStat = value; }
    }

    public int MagicalAttack
    {
        get { return MagicalAttackStat; }
        set { MagicalAttackStat = value; }
    }

    public int Defense
    {
        get { return DefenseStat; }
        set { DefenseStat = value; }
    }

    public int Speed
    {
        get { return SpeedStat; }
        set { SpeedStat = value; }
    }

    public Sprite ItemSprite
    {
        get { return itemSprite; }
        set { itemSprite = value; }
    }
}


