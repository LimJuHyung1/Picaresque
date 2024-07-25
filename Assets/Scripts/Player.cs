using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using static UnityEngine.UI.Image;
//using static UnityEditor.PlayerSettings;

public class Player : MonoBehaviourPunCallbacks // 게임 로직 구현에 필요한 클래스
{
    public static int hp;
    public static int damage;      // 상대방에게 입히는 데미지 수치    
    public static int power;
    public static string rank;      // power 에 따라 rank 증가
    public static bool isOtherPlayerHere;
    public static bool myturn = true;    
    //-----------------------------------//
    public Vector2 inputVec;
    public float speed;
    //-----------------------------------//
    public PhotonView PV;
    //-----------------------------------//
    Rigidbody2D rigid;
    BoxCollider2D collider;
    SpriteRenderer spriter;
    Animator anim;
    public AnimatorOverrideController externAnimator;   // 유저 캐릭터를 구별하기 위해 설정 


    void Awake()
    {
        hp = 10;
        damage = 2;
        speed = 3;        
        isOtherPlayerHere = false;

        rigid = GetComponent<Rigidbody2D>(); 
        collider = GetComponent<BoxCollider2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }   

    // Update is called once per frame
    void Update()
    {
        power = MoralityManager.SendMorality();

        // 화살표 키로 이동
        if (PV.IsMine)
        {
            inputVec.x = Input.GetAxisRaw("Horizontal");
            inputVec.y = Input.GetAxisRaw("Vertical");
        }        
    }

    void FixedUpdate()
    {
        if (PV.IsMine)
        {
            Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
            rigid.MovePosition(rigid.position + nextVec);
        }        
    }

    // 프레임이 종료 되기 전 실행되는 생명주기 함수
    void LateUpdate()
    {
        if (PV.IsMine)
        {
            anim.SetFloat("Speed", inputVec.magnitude);

            float axisX = Input.GetAxisRaw("Horizontal");
            float axisY = Input.GetAxisRaw("Vertical");

            if (axisX != 0)
            {                
                PV.RPC("FlipXRPC", RpcTarget.AllBuffered, axisX);                   
            }

            if (axisX != 0 || axisY != 0)
            {
                anim.SetFloat("Speed", 1);
            }
            else anim.SetFloat("Speed", 0);

            if (!IsAlive())
            {
                anim.SetBool("Dead", true);
            }           

            if (!PhotonNetwork.IsMasterClient)
                anim.runtimeAnimatorController = externAnimator;
        }

        // 또 다른 애니메이터 설정
        if(!PV.IsMine && PhotonNetwork.IsMasterClient)
        {
            anim.runtimeAnimatorController = externAnimator;
        }
    }

    [PunRPC]
    void FlipXRPC(float axis)
    {
        spriter.flipX = axis == -1;
    }

    bool IsAlive()
    {
        return hp > 0 ? true : false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isOtherPlayerHere = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Staying in trigger zone with: " + other.gameObject.name);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isOtherPlayerHere = false;
        }
    }
}
