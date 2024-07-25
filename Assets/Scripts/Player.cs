using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using static UnityEngine.UI.Image;
//using static UnityEditor.PlayerSettings;

public class Player : MonoBehaviourPunCallbacks // ���� ���� ������ �ʿ��� Ŭ����
{
    public static int hp;
    public static int damage;      // ���濡�� ������ ������ ��ġ    
    public static int power;
    public static string rank;      // power �� ���� rank ����
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
    public AnimatorOverrideController externAnimator;   // ���� ĳ���͸� �����ϱ� ���� ���� 


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

        // ȭ��ǥ Ű�� �̵�
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

    // �������� ���� �Ǳ� �� ����Ǵ� �����ֱ� �Լ�
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

        // �� �ٸ� �ִϸ����� ����
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
