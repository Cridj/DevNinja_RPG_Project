using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MHomiLibrary;


public class GameManager : HSingleton<GameManager>
{
    public PathFinding pathFinding;

    /// <summary>
    /// 메인 카메라
    /// </summary>
    public Camera cam;

    //public GameObject player;
    //public Point targetPoint;
    //public Animator animator;


    private void Awake()
    {
        //player = GameObject.Find("Player");

        ////플레이어 캐릭터 애니메이터 컴포넌트 넣어주기
        //animator = player.transform.GetChild(0).transform.GetChild(0).GetComponent<Animator>();

        //길찾기 컴포넌트 넣어주기
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
