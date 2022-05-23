using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CC_IconManager : MonoBehaviour
{
    #region[���� �����ܵ�]
    [Header("����ǥ�� UI��")]
    [Space(10f)]

    /// <summary>
    /// ���ݷ� ���� ������
    /// </summary>
    public GameObject attackBuffIcon;

    /// <summary>
    /// ���ݷ� ����� ������
    /// </summary>
    public GameObject attackDeBuffIcon;

    /// <summary>
    /// ���� ���� ������
    /// </summary>
    public GameObject defenseBuffIcon;

    /// <summary>
    /// ���� ����� ������
    /// </summary>
    public GameObject defenseDeBuffIcon;

    /// <summary>
    /// �� �����̻� ������
    /// </summary>
    public GameObject poisonIcon;

    /// <summary>
    /// ħ�� �����̻� ������
    /// </summary>
    public GameObject silenceIcon;

    /// <summary>
    /// ���� �����̻� ������
    /// </summary>
    public GameObject darknessIcon;

    /// <summary>
    /// ��ȭ �����̻� ������
    /// </summary>
    public GameObject slowIcon;

    /// <summary>
    /// ȥ�� �����̻� ������
    /// </summary>
    public GameObject confuseIcon;

    /// <summary>
    /// ���� �����̻� ������
    /// </summary>
    public GameObject stunIcon;

    public GameObject magicBuffIcon;
    #endregion

    //���� ���ӽð�
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

        //���� ����
        if (attackBuffDuration == 0)
            attackBuffIcon.SetActive(false);
        else
            attackBuffIcon.SetActive(true);

        //���� �����
        if (attackDebuffDuration == 0)
            attackDeBuffIcon.SetActive(false);
        else
            attackDeBuffIcon.SetActive(true);


        //���� ����
        if (defenseBuffDuration == 0)
            defenseBuffIcon.SetActive(false);
        else
            defenseBuffIcon.SetActive(true);


        //���� �����
        if (defenseDeBuffDuration == 0)
            defenseDeBuffIcon.SetActive(false);
        else
            defenseDeBuffIcon.SetActive(true);


        //�� �����̻�
        if (poisonCC_Duration == 0)
            poisonIcon.SetActive(false);
        else
            poisonIcon.SetActive(true);


        //ħ�� �����̻�
        if (silenceCC_Duration == 0)
            silenceIcon.SetActive(false);
        else
            silenceIcon.SetActive(true);


        //���� �����̻�
        if (darknessCC_Duration == 0)
            darknessIcon.SetActive(false);
        else
            darknessIcon.SetActive(true);


        //��ȭ �����̻�
        if (slowCC_Duration == 0)
            slowIcon.SetActive(false);
        else
            slowIcon.SetActive(true);


        //���� �����̻�
        if (stunCC_Duration == 0)
            stunIcon.SetActive(false);
        else
            stunIcon.SetActive(true);
    }

}