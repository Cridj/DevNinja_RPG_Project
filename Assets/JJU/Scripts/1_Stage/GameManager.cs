using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MHomiLibrary;


public class GameManager : HSingleton<GameManager>
{
    public PathFinding pathFinding;

    /// <summary>
    /// ���� ī�޶�
    /// </summary>
    public Camera cam;

    //public GameObject player;
    //public Point targetPoint;
    //public Animator animator;


    private void Awake()
    {
        //player = GameObject.Find("Player");

        ////�÷��̾� ĳ���� �ִϸ����� ������Ʈ �־��ֱ�
        //animator = player.transform.GetChild(0).transform.GetChild(0).GetComponent<Animator>();

        //��ã�� ������Ʈ �־��ֱ�
        //pathFinding = GameObject.Find("PathFinding").GetComponent<PathFinding>();
    }

    private void Start()
    {
        //player.transform.position = GameInstance.Instance.gamdData.PlayerPos;
    }


    public void GotoTestScene()
    {
        SceneLoad.LoadScene("TestBattle");
    }
}
