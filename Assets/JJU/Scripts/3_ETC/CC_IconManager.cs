using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CC_IconManager : MonoBehaviour
{
    #region[버프 아이콘들]
    [Header("버프표시 UI들")]
    [Space(10f)]

    /// <summary>
    /// 공격력 버프 아이콘
    /// </summary>
    public GameObject attackBuffIcon;

    /// <summary>
    /// 공격력 디버프 아이콘
    /// </summary>
    public GameObject attackDeBuffIcon;

    /// <summary>
    /// 방어력 버프 아이콘
    /// </summary>
    public GameObject defenseBuffIcon;

    /// <summary>
    /// 방어력 디버프 아이콘
    /// </summary>
    public GameObject defenseDeBuffIcon;

    /// <summary>
    /// 독 상태이상 아이콘
    /// </summary>
    public GameObject poisonIcon;

    /// <summary>
    /// 침묵 상태이상 아이콘
    /// </summary>
    public GameObject silenceIcon;

    /// <summary>
    /// 암흑 상태이상 아이콘
    /// </summary>
    public GameObject darknessIcon;

    /// <summary>
    /// 둔화 상태이상 아이콘
    /// </summary>
    public GameObject slowIcon;

    /// <summary>
    /// 혼란 상태이상 아이콘
    /// </summary>
    public GameObject confuseIcon;

    /// <summary>
    /// 기절 상태이상 아이콘
    /// </summary>
    public GameObject stunIcon;

    public GameObject magicBuffIcon;
    #endregion

    //버프 지속시간
    public int attackBuffDuration;
    public int attackDebuffDuration;
    public int defenseBuffDuration;
    public int defenseDeBuffDuration;
    public int poisonCC_Duration;
    public int silenceCC_Duration;
    public int darknessCC_Duration;
    public int slowCC_Duration;
    public int stunCC_Duration;
    public int magicBuffDuration;


    public void BuffAndCCUpdate()
    {
        attackBuffDuration = Mathf.Clamp(attackBuffDuration--, 0, 5);
        attackDebuffDuration = Mathf.Clamp(attackDebuffDuration--, 0, 5);
        defenseBuffDuration = Mathf.Clamp(defenseBuffDuration--, 0, 5);
        defenseDeBuffDuration = Mathf.Clamp(defenseDeBuffDuration--, 0, 5);
        poisonCC_Duration = Mathf.Clamp(poisonCC_Duration--, 0, 5);
        silenceCC_Duration = Mathf.Clamp(silenceCC_Duration--, 0, 5);
        darknessCC_Duration = Mathf.Clamp(darknessCC_Duration--, 0, 5);
        slowCC_Duration = Mathf.Clamp(slowCC_Duration--, 0, 5);
        stunCC_Duration = Mathf.Clamp(stunCC_Duration--, 0, 5);

        //공격 버프
        if (attackBuffDuration == 0)
            attackBuffIcon.SetActive(false);
        else
            attackBuffIcon.SetActive(true);

        //공격 디버프
        if (attackDebuffDuration == 0)
            attackDeBuffIcon.SetActive(false);
        else
            attackDeBuffIcon.SetActive(true);


        //방어력 버프
        if (defenseBuffDuration == 0)
            defenseBuffIcon.SetActive(false);
        else
            defenseBuffIcon.SetActive(true);


        //방어력 디버프
        if (defenseDeBuffDuration == 0)
            defenseDeBuffIcon.SetActive(false);
        else
            defenseDeBuffIcon.SetActive(true);


        //독 상태이상
        if (poisonCC_Duration == 0)
            poisonIcon.SetActive(false);
        else
            poisonIcon.SetActive(true);


        //침묵 상태이상
        if (silenceCC_Duration == 0)
            silenceIcon.SetActive(false);
        else
            silenceIcon.SetActive(true);


        //암흑 상태이상
        if (darknessCC_Duration == 0)
            darknessIcon.SetActive(false);
        else
            darknessIcon.SetActive(true);


        //둔화 상태이상
        if (slowCC_Duration == 0)
            slowIcon.SetActive(false);
        else
            slowIcon.SetActive(true);


        //기절 상태이상
        if (stunCC_Duration == 0)
            stunIcon.SetActive(false);
        else
            stunIcon.SetActive(true);
    }

}