using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillInfo : MonoBehaviour
{
    public TextMeshProUGUI skillName;
    public TextMeshProUGUI skillSummary;
    public int skillNum;

    private void Awake()
    {
        Hero turnHero = BattleManager.I.turnUnit.GetComponent<Hero>();
        switch (skillNum)
        {
            case 1:
                skillName.text = GameInstance.Instance.SkillInfoData[turnHero.skill_set1.ToString()].name;
                skillSummary.text = GameInstance.Instance.SkillInfoData[turnHero.skill_set1.ToString()].skillSummary;
                break;
            case 2:
                skillName.text = GameInstance.Instance.SkillInfoData[turnHero.skill_set2.ToString()].name;
                skillSummary.text = GameInstance.Instance.SkillInfoData[turnHero.skill_set1.ToString()].skillSummary;
                break;
            case 3:
                skillName.text = GameInstance.Instance.SkillInfoData[turnHero.skill_set2.ToString()].name;
                skillSummary.text = GameInstance.Instance.SkillInfoData[turnHero.skill_set1.ToString()].skillSummary;
                break;
        }

    }


}
