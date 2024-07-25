using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
    public Player player;
    public Text Message;    
    string[] Messages;
    public int MessageIndex;


    // Start is called before the first frame update
    void Awake()
    {
        MessageIndex = 0;

        Messages = new string[] { "�� ���� �������Դϴ�.",                   // 0
                                   "�ٸ� �������� ���� ����� ������.",       // 1
                                   "�� �������� ���� ��ǥ�� ��뺸�� ���� Ż�ⱸ�� ã�ų�\n" + 
                                   "������ HP�� 0���� ����� ���Դϴ�.",        // 2
                                   "ȭ�鿡 ���̴� ������ ȭ��ǥ�� ���� ������",   // 3
                                   "�ڽ��� �ϸ��� ȭ��ǥ�� ���� ���� �̵��ϰų�\n" +
                                   "������� �׼��� ���� �� �ֽ��ϴ�.",      // 4
                                   "�ڽ��� ���� �����ٸ� ������ ���� ���۵˴ϴ�.",   // 5
                                   "������ ���� �������ϴ�.\n" +
                                   "�ٽ� �� �� ������ ȭ��ǥ�� ���� ������.",       // 6
                                   "����� ������ �ȴٸ�\n�λ�, ���� ���� �ൿ�� �� �� �ֽ��ϴ�.", // 7
                                   "������ ������ ���Դϴ�.",        // 8
                                   "������ ������ �߽��ϴ�.\n" +
                                   "������ �ް� �Ǹ� HP�� �پ��ϴ�.",      // 9
                                   "�λ� ��ư�� ���� ������.",        // 10
                                   "�λ� ��ư�� �����ٸ� ����ġ�� �����մϴ�.",   // 11
                                   "���� ��ư�� ���� ������.",                // 12
                                   "���� ��ư�� �����ٸ� ����ġ�� �����ϰ�\n" +
                                   "������ HP�� �پ��ϴ�.",               // 13
                                   "�����ϱ�� �λ��ϱ�� ���� ���� ��Ÿ���� �ֽ��ϴ�.",      // 14
                                   "�Ʒ� ȭ��ǥ�� ���� ������.",                           // 15
                                   "Ż�ⱸ�� ���������� Ż���� �� �ִ� �ⱸ�Դϴ�.",            // 16
                                   "Ż�ⱸ�� ���溸�� ���� �߰��Ͽ�\n" +
                                   "Ż���ϰ� �ȴٸ� ������ �߰��˴ϴ�.",                    // 17
                                   "Ż���ư�� ���� ���������� Ż���ϼ���.",                // 18
                                   "",
                                   ""
        };

        Message.text = Messages[MessageIndex];
    }

    bool IsMyTurn()
    {
        switch (MessageIndex)
        {
            case 3: return true;
            case 6: return true;
            case 10: return true;
            case 12: return true;
            case 15: return true;
            case 18: return true;
            default:
                return false;
        }
    }

    bool TutorialClick()
    {
        return Input.GetMouseButtonDown(0) &&
            (MessageIndex != 3 && MessageIndex != 6 && MessageIndex != 10 &&
            MessageIndex != 12 && MessageIndex != 15 && MessageIndex != 18);
    }

    public void UpdateMessage()
    {
        Message.text = Messages[MessageIndex];
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (TutorialClick())
            {
                // ���콺 ���� ��ư�� Ŭ������ ���� ������ �����մϴ�.
                MessageIndex++;
                UpdateMessage();
            }
            if(IsMyTurn())
            {
                Player.myturn = true;
            }

            if (MessageIndex == 9) Player.hp = 8;
        }

        catch (IndexOutOfRangeException)
        {
            SceneManager.LoadScene("Main_Menu", LoadSceneMode.Single);
        }
    }
}
