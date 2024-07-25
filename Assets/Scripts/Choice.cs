using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Choice : MonoBehaviour
{
    public Text Message;
    public string[] Messages;
    public int MessageIndex;

    //public Image[] cardType;        // ī�� Ÿ��
    public Image[] card;            // ī�尡 ��Ÿ���� �ϴ� ���
    int []randIndex;                  // ���������� ī�� ��Ÿ���� ��

    void Awake()
    {
        MessageIndex = 0;

        // 1 - 4 ������ �޼���
        Messages = new string[] {"�� ���� �̺�Ʈ�� �߻��ϴ� �����Դϴ�.",       // 0
                                "�� ���� 4���� �̺�Ʈ��\n1���� ���������� �����Ǿ� �ֽ��ϴ�",    // 1
                                "�̺�Ʈ�� ���� ������ ����ġ�� ������ų �� �ֽ��ϴ�.",        // 2
                                "���� ī��� �÷��̾��� ����ġ�� ���� ��Ű�� ī���Դϴ�.",       // 3                         
                                "ī�带 ���콺�� Ŭ���� ������.",                            // 4
                                "ī�带 �����ϸ� ī���� ������ �÷��̾�� ����˴ϴ�.",       // 5
                                "���� ī��� �÷��̾��� ����ġ�� 5���ҽ�Ű�� ī���Դϴ�.",      // 6
                                "ī�带 ���콺�� Ŭ���� ������.",                            // 7
                                "�̿� ���� ī�� ������ 3�� �� 1������ �� �� �ֽ��ϴ�.",         // 8
                                "���� ���������� �Ѿ���� �սô�."                           // 9
        };
        
        Message.text = Messages[MessageIndex];


        //cardType = new Image[2];
        //card = new Image[2];
        for(int i = 0; i < card.Length; i++)
        {
            //randIndex[i] = UnityEngine.Random.Range(0, 1);
            card[i].gameObject.SetActive(false);
        }              
    }

    // ���� Scene ��ȯ
    string CurrentScene()
    {
        return SceneManager.GetActiveScene().name;
    }

    bool IsClickPoss()
    {
        return MessageIndex != 4 && MessageIndex != 7 ? true : false;
    }

    void Update()
    {        
        try
        {
            if (Input.GetMouseButtonDown(0) && IsClickPoss())
            {
                // ���콺 ���� ��ư�� Ŭ������ ���� ������ �����մϴ�.
                MessageIndex++;
                Message.text = Messages[MessageIndex];
            }    
            if(MessageIndex == 3)
            {
                card[0].gameObject.SetActive(true);                
            }
            else if (MessageIndex == 6)
            {
                card[1].gameObject.SetActive(true);
            }


        }
        catch (IndexOutOfRangeException)
        {
            SceneManager.LoadScene("Tutorial_Shared Dream", LoadSceneMode.Single);          
        }
    }

}