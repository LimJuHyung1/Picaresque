using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEditor.VersionControl;

public class TutorialButtonManager : MonoBehaviour
{
    public Button []buttons;
    public Player player;
    public MessageManager messageManager;
    AudioSource greet;
    AudioSource attack;
    AudioSource escape;

    bool greetAct;
    bool attackAct;
    bool escapeAct;
    bool tmp1;
    bool tmp2;


    bool RightUnactive()
    {
        return player.transform.position.x > 1
            && player.transform.position.x < 11 ? true : false;
    }
    bool DownUnactive()
    {
        return player.transform.position.y > -14
            && player.transform.position.y < -5 ? true : false;
    }
    bool LeftUnactive()
    {
        return player.transform.position.x > -23
            && player.transform.position.x < -13 ? true : false;
    }
    bool UpUnactive()
    {
        return player.transform.position.y > 22
            && player.transform.position.y < 32 ? true : false;
    }

    void Awake()
    {
        greet = buttons[6].GetComponent<AudioSource>();
        attack = buttons[7].GetComponent<AudioSource>();
        escape = buttons[8].GetComponent<AudioSource>();

        greetAct = false;
        attackAct = false;
        escapeAct = false;
        tmp1 = false;
        tmp2 = false;
    }

    /*void RightArrow(bool act)
    {
        if (RightUnactive() && act == false)
        {
            buttons[0].gameObject.SetActive(false);
        }       // ������ ȭ��ǥ ��Ȱ��ȭ
        else if (!RightUnactive() && act == true)
        {
            buttons[0].gameObject.SetActive(true);
        }      // ������ ȭ��ǥ Ȱ��ȭ
    }

    void DownArrow(bool act)
    {
        if (DownUnactive() && act == false)
        {
            buttons[1].gameObject.SetActive(false);
        }        // �Ʒ��� ȭ��ǥ ��Ȱ��ȭ
        else if (!DownUnactive() && act == true)
        {
            buttons[1].gameObject.SetActive(true);
        }        // �Ʒ��� ȭ��ǥ Ȱ��ȭ
    }

    void LeftArrow(bool act)
    {
        if (LeftUnactive() && act == false)
        {
            buttons[2].gameObject.SetActive(false);
        }        // ���� ȭ��ǥ ��Ȱ��ȭ
        else if (!LeftUnactive() && act == true)
        {
            buttons[2].gameObject.SetActive(true);
        }        // ���� ȭ��ǥ Ȱ��ȭ
    }

    void UpArrow(bool act)
    {
        if (UpUnactive() && act == false)
        {
            buttons[3].gameObject.SetActive(false);
        }          // ���� ȭ��ǥ ��Ȱ��ȭ
        else if (!UpUnactive() && act == true)
        {
            buttons[3].gameObject.SetActive(true);
        }          // ���� ȭ��ǥ Ȱ��ȭ
    }*/


    void Update()
    {        
        switch (messageManager.MessageIndex)
        {
            default:
                tmp1 = false;
                tmp2 = false;
                buttons[2].gameObject.SetActive(false);
                buttons[3].gameObject.SetActive(false);
                break;
            case 3:
                tmp1 = true;
                break;
            //case 4:
            //    tmp1 = true;
            //    break;
            case 6:
                tmp1 = true;
                break;
            //case 7:
            //    tmp1 = true;
            //    break;
            case 10:
                greetAct = true;
                break;
            case 11:
                greetAct = true;
                break;
            case 12:
                attackAct = true;
                break;
            case 13:
                attackAct = true;
                break;
            case 15:
                tmp2 = true;
                break;
            //case 16:
            //    tmp2 = true;
            //    break;
            case 18:
                escapeAct = true;
                break;
            case 19:
                escapeAct = true;
                break;
        }
        RightArrow(tmp1);
        DownArrow(tmp2);

        GreetUnactive();
        AttackUnactive();
        EscapeUnactive();
        if (Player.myturn == true)
        {
            // �� ���� �� ��� �� ��ư ��Ȱ��ȭ
            buttons[5].gameObject.SetActive(false);
        }
        else
        {
            // ��� ���� �� ��� �� ��ư Ȱ��ȭ
            buttons[5].gameObject.SetActive(true);
        }
    }

    public void Greet()
    {
        //Player.morality += 2;
        Player.myturn = false;
        greet.Play();
        messageManager.MessageIndex += 1;
        messageManager.UpdateMessage();
    }

    public void Attack()
    {
        //Player.morality -= 2;
        Player.myturn = false;
        attack.Play();
        messageManager.MessageIndex += 1;
        messageManager.UpdateMessage();
        // player[1].hp -= 1;        
    }

    public void Escape()
    {
        SceneManager.LoadScene("Main_Menu", LoadSceneMode.Single);
    }

    void RightArrow(bool act)
    {
        if (act == false)
        {
            buttons[0].gameObject.SetActive(false);
        }       // ������ ȭ��ǥ ��Ȱ��ȭ
        else
        {
            buttons[0].gameObject.SetActive(true);
        }      // ������ ȭ��ǥ Ȱ��ȭ
    }

    void DownArrow(bool act)
    {
        if (act == false)
        {
            buttons[1].gameObject.SetActive(false);
        }        // �Ʒ��� ȭ��ǥ ��Ȱ��ȭ
        else
        {
            buttons[1].gameObject.SetActive(true);
        }        // �Ʒ��� ȭ��ǥ Ȱ��ȭ
    }

    void GreetUnactive() { 
        if(greetAct == false)
        {
            buttons[6].gameObject.SetActive(false);
        }
        else
        {
            buttons[6].gameObject.SetActive(true);
        }
    }
    void AttackUnactive()
    { 
        if(attackAct == false)
        {
            buttons[7].gameObject.SetActive(false);
        }
        else
        {
            buttons[7].gameObject.SetActive(true);
        }
    }
    void EscapeUnactive()
    { 
        if(escapeAct == false)
        {
            buttons[8].gameObject.SetActive(false);
        }
        else
        {
            buttons[8].gameObject.SetActive(true);
        }
    }
}
