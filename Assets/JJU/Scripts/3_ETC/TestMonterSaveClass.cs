using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TestMonterSaveClass : MonoBehaviour
{
    public TMP_Dropdown common1;
    public TMP_Dropdown common2;
    public TMP_Dropdown common3;
    public TMP_Dropdown elite;

    public ENEMY_TYPE enemy1;
    public ENEMY_TYPE enemy2;
    public ENEMY_TYPE enemy3;
    public ENEMY_TYPE eliteENum;

    public void Btn()
    {
        SceneLoad.LoadScene("BattleTest");
    }

    private void Update()
    {
        enemy1 = (ENEMY_TYPE)System.Enum.Parse(typeof(ENEMY_TYPE), common1.options[common1.value].text);
        enemy2 = (ENEMY_TYPE)System.Enum.Parse(typeof(ENEMY_TYPE), common2.options[common2.value].text);
        enemy3 = (ENEMY_TYPE)System.Enum.Parse(typeof(ENEMY_TYPE), common3.options[common3.value].text);
        eliteENum = (ENEMY_TYPE)System.Enum.Parse(typeof(ENEMY_TYPE), elite.options[elite.value].text);

        GameInstance.Instance.enemy1 = enemy1;
        GameInstance.Instance.enemy2 = enemy2;
        GameInstance.Instance.enemy3 = enemy3;
        GameInstance.Instance.eliteENum = eliteENum;
    }

}
