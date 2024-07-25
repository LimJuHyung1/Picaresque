using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CardManager : MonoBehaviour
{
    [SerializeField] List<GameObject> prefabs;     // 프리팹들을 담을 배열    
    [SerializeField] List<GameObject> cards;    // 프리팹 카드들을 무작위로 12개를 선정하여 List에 담아 둠
    private Vector3[] vectorArray = new Vector3[3];     // 카드들이 놓일 위치

    public Canvas canvas;    
    public static bool clickTrigger;    // 클릭을 할 때마다 true로 바뀜
    public static bool tmpTrigger;    // 클릭을 할 때마다 true로 바뀜
    public static int clickCount;       // 클릭을 할 때마다 증가
    

    void Start()
    {
        clickTrigger = false;
        tmpTrigger = false;
        clickCount = 0;
        cards = new List<GameObject>();

        // 배열의 각 요소를 초기화
        vectorArray[0] = new Vector3(-300f, 0f, 0f);
        vectorArray[1] = new Vector3(0f, 0f, 0f);
        vectorArray[2] = new Vector3(300f, 0f, 0f);

        // 리스트에 프리펩 12개를 추가
        for (int i = 0; i < 12; i++)
        {
            int randomIndex = Random.Range(0, prefabs.Count); // 무작위로 인덱스 선택
            GameObject selectedPrefab = prefabs[randomIndex]; // 선택된 무작위 프리팹
            cards.Add(selectedPrefab);      // 리스트에 추가
            prefabs.RemoveAt(randomIndex);           
        }
    }

    void Update()
    {
        // Update 안에서 주기적으로 실행되도록 설정
        if (prefabs != null)
        {
            // 카드를 보여줄 캔버스의 위치를 초기화
            canvas.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

            // 클릭 할 때마다 실행
            if (clickTrigger == false && clickCount < 4) {
                for (int i = 0; i < 3; i++)
                {
                    GameObject InstPrefab = Instantiate(cards[i], canvas.transform);    // 카드 생성                  
                    InstPrefab.transform.position = vectorArray[i];                     // 카드 위치 설정                   
                }

                for (int j = 0; j < 3; j++)
                {
                    cards.RemoveAt(0);      // 리스트에서 생성된 카드 목록 제거
                }
                clickTrigger = true;
                tmpTrigger = true;
                //Invoke("CreateCards", 2f);      
                //Debug.Log(clickCount);
            }

            // 4번의 카드 선택이 끝나면 공유몽으로 이동
            if (clickCount == 4)
            {
                Invoke("WaitAndLoadScene", 1.3f);
            }
        }
        else
        {
            Debug.Log("prefabs are not existed!");
        }

    }

    void WaitAndLoadScene()
    {
        SceneManager.LoadScene("Shared Dream", LoadSceneMode.Single);
    }
}
