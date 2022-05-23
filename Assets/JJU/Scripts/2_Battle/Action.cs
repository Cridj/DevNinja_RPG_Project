using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface UnitAction
{
    //공격
    void Attack(float Damage);

    //가드
    void Gaurd(float Damage);

    //스킬
    void Skill(float Damage);

}