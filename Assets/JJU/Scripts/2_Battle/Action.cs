using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface UnitAction
{
    //����
    void Attack(float Damage);

    //����
    void Gaurd(float Damage);

    //��ų
    void Skill(float Damage);

}