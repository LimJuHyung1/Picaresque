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

        Messages = new string[] { "이 곳은 공유몽입니다.",                   // 0
                                   "다른 누군가와 꿈이 연결된 곳이죠.",       // 1
                                   "이 곳에서의 최종 목표는 상대보다 빨리 탈출구를 찾거나\n" + 
                                   "상대방의 HP를 0으로 만드는 것입니다.",        // 2
                                   "화면에 보이는 오른쪽 화살표를 눌러 보세요",   // 3
                                   "자신의 턴마다 화살표를 눌러 방을 이동하거나\n" +
                                   "상대방과의 액션을 취할 수 있습니다.",      // 4
                                   "자신의 턴이 끝난다면 상대방의 턴이 시작됩니다.",   // 5
                                   "상대방의 턴이 끝났습니다.\n" +
                                   "다시 한 번 오른쪽 화살표를 눌러 보세요.",       // 6
                                   "상대방과 만나게 된다면\n인사, 공격 등의 행동을 할 수 있습니다.", // 7
                                   "지금은 상대방의 턴입니다.",        // 8
                                   "상대방이 공격을 했습니다.\n" +
                                   "공격을 받게 되면 HP가 줄어듭니다.",      // 9
                                   "인사 버튼을 눌러 보세요.",        // 10
                                   "인사 버튼을 누른다면 도덕치가 증가합니다.",   // 11
                                   "공격 버튼을 눌러 보세요.",                // 12
                                   "공격 버튼을 누른다면 도덕치가 감소하고\n" +
                                   "상대방의 HP가 줄어듭니다.",               // 13
                                   "공격하기와 인사하기는 일정 턴의 쿨타임이 있습니다.",      // 14
                                   "아래 화살표를 눌러 보세요.",                           // 15
                                   "탈출구는 공유몽에서 탈출할 수 있는 출구입니다.",            // 16
                                   "탈출구를 상대방보다 먼저 발견하여\n" +
                                   "탈출하게 된다면 점수가 추가됩니다.",                    // 17
                                   "탈출버튼을 눌러 공유몽에서 탈출하세요.",                // 18
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
                // 마우스 왼쪽 버튼을 클릭했을 때의 동작을 수행합니다.
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
