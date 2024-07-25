using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class Loading : MonoBehaviourPunCallbacks
{
    private float updateInterval;      // 플레이어 수 업데이트 간격 (예: 2초마다 업데이트)
    private float loadingTextInterval;
    private int loadingTextValue;
    public Slider progressbar;
    public Text loadingText;
    int playerCount;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        updateInterval = 2f;
        loadingTextInterval = 1000f;
        loadingTextValue = 0;
    }

    public override void OnConnectedToMaster()
    {
        // 마스터 서버에 연결된 후 방에 입장
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 2 }, null);
        StartCoroutine(UpdatePlayerCountCoroutine());
        StartCoroutine(LoadScene());
    }

    public override void OnJoinedRoom()
    {
        
    }

    IEnumerator UpdatePlayerCountCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(updateInterval);

            if (PhotonNetwork.InRoom)
            {
                playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
                Debug.Log("현재 플레이어 수: " + playerCount);
            }
        }
    }

    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync("Shared Dream");
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            yield return null;
            if (progressbar.value < 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 0.9f, Time.deltaTime);
            }
            else if (progressbar.value >= 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime);
                InvokeRepeating("LoadingTextAnimation", 1f, loadingTextInterval);
            }

            if(playerCount >= 2)
            {
                operation.allowSceneActivation = true;
            }
        }
    }    

    void LoadingTextAnimation()
    {
        switch (loadingTextValue)
        {
            case 0:
                loadingText.text = "플레이어를 찾고 있습니다.";
                StartCoroutine(WaitForDelay());
                loadingTextValue++;                
                break;
            case 1:
                loadingText.text = "플레이어를 찾고 있습니다..";
                StartCoroutine(WaitForDelay());
                loadingTextValue++;
                break;
            case 2:
                loadingText.text = "플레이어를 찾고 있습니다...";
                StartCoroutine(WaitForDelay());
                loadingTextValue = 0;
                break;
            default:
                break;
        }
    }

    IEnumerator WaitForDelay()
    {
        yield return new WaitForSeconds(1000f); // 대기 시간만큼 대기
    }
}
