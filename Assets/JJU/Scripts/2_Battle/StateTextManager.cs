using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTextManager : MonoBehaviour
{
    Animator animator;

    public GameObject AttackUp;
    public GameObject DefenseUp;
    public GameObject AttackDown;
    public GameObject DefenseDown;
    public GameObject MagicUp;
    public GameObject MagicDown;
    public GameObject Poison;
    public GameObject Silent;
    public GameObject Darkness;
    public GameObject Slow;
    public GameObject Stun;
    public GameObject Taunt;

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void DeActivate()
    {
        AttackUp.SetActive(false);
        AttackDown.SetActive(false);
        DefenseDown.SetActive(false);
        DefenseUp.SetActive(false);
        MagicUp.SetActive(false);
        MagicDown.SetActive(false);
        Poison.SetActive(false);
        Silent.SetActive(false);
        Darkness.SetActive(false);
        Slow.SetActive(false);
        Stun.SetActive(false);
        Taunt.SetActive(false);
    }

    public void ActivateState(string[] states)
    {
        foreach(string state in states)
        {
            switch (state)
            {
                case "AttackUp":
                    AttackUp.SetActive(true);
                    break;
                case "AttackDown":
                    AttackDown.SetActive(true);
                    break;
                case "DefenseUp":
                    DefenseUp.SetActive(true);
                    break;
                case "DefenseDown":
                    DefenseDown.SetActive(true);
                    break;
                case "MagicUp":
                    MagicUp.SetActive(true);
                    break;
                case "MagicDown":
                    MagicDown.SetActive(true);
                    break;
                case "Poison":
                    Poison.SetActive(true);
                    break;
                case "Silent":
                    Silent.SetActive(true);
                    break;
                case "Darkness":
                    Darkness.SetActive(true);
                    break;
                case "Slow":
                    Slow.SetActive(true);
                    break;
                case "Stun":
                    Stun.SetActive(true);
                    break;
                case "Taunt":
                    Taunt.SetActive(true);
                    break;
            }
        }
        animator.SetTrigger("StateOn");
    }


}
