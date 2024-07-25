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

    //public Image[] cardType;        // 카드 타입
    public Image[] card;            // 카드가 나타내게 하는 대상
    int []randIndex;                  // 랜덤값으로 카드 나타나게 함

    void Awake()
    {
        MessageIndex = 0;

        // 1 - 4 페이즈 메세지
        Messages = new string[] {"이 곳은 이벤트가 발생하는 공간입니다.",       // 0
                                "꿈 속은 4번의 이벤트와\n1번의 공유몽으로 구성되어 있습니다",    // 1
                                "이벤트를 통해 점수와 도덕치를 증가시킬 수 있습니다.",        // 2
                                "위의 카드는 플레이어의 도덕치를 증가 시키는 카드입니다.",       // 3                         
                                "카드를 마우스로 클릭해 보세요.",                            // 4
                                "카드를 선택하면 카드의 내용이 플레이어에게 적용됩니다.",       // 5
                                "위의 카드는 플레이어의 도덕치를 5감소시키는 카드입니다.",      // 6
                                "카드를 마우스로 클릭해 보세요.",                            // 7
                                "이와 같은 카드 선택은 3개 중 1개까지 할 수 있습니다.",         // 8
                                "이제 공유몽으로 넘어가도록 합시다."                           // 9
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

    // 현재 Scene 반환
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
                // 마우스 왼쪽 버튼을 클릭했을 때의 동작을 수행합니다.
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