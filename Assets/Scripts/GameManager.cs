using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;
//using PPlayer = Photon.Realtime.Player;

//using Mono.CompilerServices.SymbolWriter;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject player;       // 플레이어 객체
    public static int turn;         // 턴제에 활용할 변수
    private int spawnIndex;         // 스폰 배열중 하나를 랜덤으로 가져올 변수
    private bool isSpwaned = false; // 플레이어 프리팹이 생성 되었는지 확인하는 변수

    public Text playerName;
    public Text playerHP;
    public Text playerMorality;
    public new Camera camera;

    Vector3[] spawnArr = new Vector3[16];

    void Awake()
    {
        turn = 1;

        playerName.text = NicknameManager.playerName;

        // 스폰 지역 배열 생성
        int y = 27;
        int x = -18;

        for (int i = 0; i < 16; i++)
        {
            if (i % 4 == 0 && i != 0)
            {
                x = -18;
                y -= 18;
            }                
            spawnArr[i] = new Vector3(x, y, 0);
            //Debug.Log(i + " : " + spawnArr[i]);
            x += 12;
        }

        // 랜덤으로 스폰 배열에 접근
        spawnIndex = Random.Range(0, spawnArr.Length);

        if(isSpwaned == false)      // 스폰 중첩을 막기위해 조건문 설정
        {
            // 플레이어 생성
            player = PhotonNetwork.Instantiate
                ("Player", spawnArr[spawnIndex], Quaternion.identity);

            // 카메라 위치를 플레이어에 맞게 조정
            camera.transform.position = player.transform.position + new Vector3(0, 0, -10);

            isSpwaned = true;
        }
    }

    void Update()
    {
        playerHP.text = Player.hp.ToString();
        playerMorality.text = Player.power.ToString();        
    }

    public void ExitGame()
    {
        Application.Quit();
    }     

    private void OnObjectInstantiated(GameObject spawnedObject, PhotonMessageInfo info)
    {
        Debug.Log("Object instantiated by: " + info.Sender.NickName);
        Debug.Log("Object name: " + spawnedObject.name);
    }    
}
