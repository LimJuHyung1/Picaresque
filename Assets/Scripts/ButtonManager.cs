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
    0�� ��ư : ������ �̵�
    1�� ��ư : �Ʒ��� �̵�
    2�� ��ư : ���� �̵�
    3�� ��ư : ���� �̵�
    
    4�� ��ư : ���� ��ư
    5�� ��ư : Ż�� ��ư   
    */

    public new Camera camera;
    private GameManager gameManager;
    public int healHp;
    //-----------------------------------//
    private bool isMasterAtk;        // ������ �÷��̾��� ���� Ʈ����
    private bool isntMasterAtk;      // �����Ͱ� �ƴ� �÷��̾��� ���� Ʈ����
    //-----------------------------------//
    // ��ư Ŭ�� �Ҹ�
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

        portal = this.GetComponent<AudioSource>();          // ȭ��ǥ �Ҹ�
        attack = buttons[4].GetComponent<AudioSource>();    // ���� ��ư �Ҹ�
        escape = buttons[5].GetComponent<AudioSource>();    // Ż�� ��ư �Ҹ�
    }

    void Update()
    {
        // ��ư ��Ÿ���� �����ؾ� ��

        
    }

    bool RightUnactive()            // ������ �̵� ��ư ��Ȱ��ȭ
    {
        return gameManager.player.transform.position.x > 13
            && gameManager.player.transform.position.x < 23 ? true : false;
    }
    bool DownUnactive()             // �Ʒ��� �̵� ��ư ��Ȱ��ȭ
    {
        return gameManager.player.transform.position.y > -32
            && gameManager.player.transform.position.y < -22 ? true : false;
    }
    bool LeftUnactive()             // ���� �̵� ��ư ��Ȱ��ȭ
    {
        return gameManager.player.transform.position.x > -23
            && gameManager.player.transform.position.x < -13 ? true : false;
    }
    bool UpUnactive()               // ���� �̵� ��ư ��Ȱ��ȭ
    {
        return gameManager.player.transform.position.y > 22
            && gameManager.player.transform.position.y < 32 ? true : false;
    }

    // Update ���� ȭ��ǥ�� Ȱ��ȭ/��Ȱ��ȭ ����
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
        buttons[3].gameObject.SetActive(!UpUnactive());          // ���� ȭ��ǥ Ȱ��ȭ
    }

    public void AllButtonActive()        // ��� ��ư Ȱ��ȭ
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(true);
        }
    }

    public void AllButtonUnActive()        // ��� ��ư ��Ȱ��ȭ
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
    }

    void GrtAtkUnactive()            // ���� ��ư Ȱ��ȭ/��Ȱ��ȭ ����
    {
        if(Player.isOtherPlayerHere == true) buttons[4].gameObject.SetActive(true);
        else buttons[4].gameObject.SetActive(false);        
    }


    public void MoveRight()         // �÷��̾� ������ �̵� ��ư - OnClick()
    {
        Vector3 movement = Vector3.right * 12f;
        Vector3 cameraMove = Vector3.right * 12f;

        gameManager.player.transform.position += movement;
        camera.transform.position += cameraMove;
        portal.Play();
    }
    public void MoveDown()          // �÷��̾� �Ʒ��� �̵� ��ư - OnClick()
    {
        Vector3 movement = Vector3.down * 18f;
        Vector3 cameraMove = Vector3.down * 18f;

        gameManager.player.transform.position += movement;
        camera.transform.position += cameraMove;
        portal.Play();
    }
    public void MoveLeft()          // �÷��̾� ���� �̵� ��ư - OnClick()
    {
        Vector3 movement = Vector3.left * 12f;
        Vector3 cameraMove = Vector3.left * 12f;

        gameManager.player.transform.position += movement;
        camera.transform.position += cameraMove;
        portal.Play();
    }
    public void MoveUp()            // �÷��̾� ���� �̵� ��ư - OnClick()
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

    public void Attack()            // ���� ��ư - AttackButton - OnClick()
    {
        attack.Play();
        MoralityManager.ModifyMorality(MoralityManager.AttackMorality);
        AtkTrigger();        
        photonView.RPC("AtkRPC", RpcTarget.AllBuffered);
    }
        

    public void Escape()            // Ż�� ��ư - EscapeButton - OnClick()
    {
        // Ż�� ���� ����
        SceneManager.LoadScene("Main_Menu", LoadSceneMode.Single);
    }

}
