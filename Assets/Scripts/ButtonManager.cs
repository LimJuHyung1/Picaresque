using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;

public class ButtonManager : MonoBehaviourPunCallbacks
{
    public Button []buttons;
    /*
    0번 버튼 : 오른쪽 이동
    1번 버튼 : 아래쪽 이동
    2번 버튼 : 왼쪽 이동
    3번 버튼 : 위쪽 이동
    
    4번 버튼 : 공격 버튼
    5번 버튼 : 탈출 버튼   
    */

    public new Camera camera;
    private GameManager gameManager;
    public int healHp;
    //-----------------------------------//
    private bool isMasterAtk;        // 마스터 플레이어의 공격 트리거
    private bool isntMasterAtk;      // 마스터가 아닌 플레이어의 공격 트리거
    //-----------------------------------//
    // 버튼 클릭 소리
    AudioSource portal;
    AudioSource attack;
    AudioSource escape;
    //-----------------------------------//

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        healHp = 1;
        
        isMasterAtk = false;
        isntMasterAtk = false;

        portal = this.GetComponent<AudioSource>();          // 화살표 소리
        attack = buttons[4].GetComponent<AudioSource>();    // 공격 버튼 소리
        escape = buttons[5].GetComponent<AudioSource>();    // 탈출 버튼 소리
    }

    void Update()
    {
        // 버튼 쿨타임을 구현해야 함

        
    }

    bool RightUnactive()            // 오른쪽 이동 버튼 비활성화
    {
        return gameManager.player.transform.position.x > 13
            && gameManager.player.transform.position.x < 23 ? true : false;
    }
    bool DownUnactive()             // 아래쪽 이동 버튼 비활성화
    {
        return gameManager.player.transform.position.y > -32
            && gameManager.player.transform.position.y < -22 ? true : false;
    }
    bool LeftUnactive()             // 왼쪽 이동 버튼 비활성화
    {
        return gameManager.player.transform.position.x > -23
            && gameManager.player.transform.position.x < -13 ? true : false;
    }
    bool UpUnactive()               // 위쪽 이동 버튼 비활성화
    {
        return gameManager.player.transform.position.y > 22
            && gameManager.player.transform.position.y < 32 ? true : false;
    }

    // Update 에서 화살표의 활성화/비활성화 관리
    void RightArrow()
    {
        buttons[0].gameObject.SetActive(!RightUnactive());
    }
    void DownArrow()
    {
        buttons[1].gameObject.SetActive(!DownUnactive());
    }
    void LeftArrow()
    {
        buttons[2].gameObject.SetActive(!LeftUnactive());
    }
    void UpArrow()
    {
        buttons[3].gameObject.SetActive(!UpUnactive());          // 위쪽 화살표 활성화
    }

    public void AllButtonActive()        // 모든 버튼 활성화
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(true);
        }
    }

    public void AllButtonUnActive()        // 모든 버튼 비활성화
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
    }

    void GrtAtkUnactive()            // 공격 버튼 활성화/비활성화 관리
    {
        if(Player.isOtherPlayerHere == true) buttons[4].gameObject.SetActive(true);
        else buttons[4].gameObject.SetActive(false);        
    }


    public void MoveRight()         // 플레이어 오른쪽 이동 버튼 - OnClick()
    {
        Vector3 movement = Vector3.right * 12f;
        Vector3 cameraMove = Vector3.right * 12f;

        gameManager.player.transform.position += movement;
        camera.transform.position += cameraMove;
        portal.Play();
    }
    public void MoveDown()          // 플레이어 아래쪽 이동 버튼 - OnClick()
    {
        Vector3 movement = Vector3.down * 18f;
        Vector3 cameraMove = Vector3.down * 18f;

        gameManager.player.transform.position += movement;
        camera.transform.position += cameraMove;
        portal.Play();
    }
    public void MoveLeft()          // 플레이어 왼쪽 이동 버튼 - OnClick()
    {
        Vector3 movement = Vector3.left * 12f;
        Vector3 cameraMove = Vector3.left * 12f;

        gameManager.player.transform.position += movement;
        camera.transform.position += cameraMove;
        portal.Play();
    }
    public void MoveUp()            // 플레이어 위쪽 이동 버튼 - OnClick()
    {
        Vector3 movement = Vector3.up * 18f;
        Vector3 cameraMove = Vector3.up * 18f;

        gameManager.player.transform.position += movement;
        camera.transform.position += cameraMove;
        portal.Play();
    }

    public void AtkTrigger()
    {
        if (PhotonNetwork.IsMasterClient) isMasterAtk = true;
        else isntMasterAtk = true;
    }

    [PunRPC]
    public void AtkRPC()
    {
        if (isMasterAtk == true)
        {
            Player.hp -= Player.damage;
            Debug.Log("Master's atk");
            isMasterAtk = false;
        }
        else if (isntMasterAtk == true)
        {
            Player.hp -= Player.damage;
            Debug.Log("N_Master's atk");
            isntMasterAtk = false;
        }

    }

    public void Attack()            // 공격 버튼 - AttackButton - OnClick()
    {
        attack.Play();
        MoralityManager.ModifyMorality(MoralityManager.AttackMorality);
        AtkTrigger();        
        photonView.RPC("AtkRPC", RpcTarget.AllBuffered);
    }
        

    public void Escape()            // 탈출 버튼 - EscapeButton - OnClick()
    {
        // 탈출 로직 구현
        SceneManager.LoadScene("Main_Menu", LoadSceneMode.Single);
    }

}
