using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CardManager : MonoBehaviour
{
    [SerializeField] List<GameObject> prefabs;     // �����յ��� ���� �迭    
    [SerializeField] List<GameObject> cards;    // ������ ī����� �������� 12���� �����Ͽ� List�� ��� ��
    private Vector3[] vectorArray = new Vector3[3];     // ī����� ���� ��ġ

    public Canvas canvas;    
    public static bool clickTrigger;    // Ŭ���� �� ������ true�� �ٲ�
    public static bool tmpTrigger;    // Ŭ���� �� ������ true�� �ٲ�
    public static int clickCount;       // Ŭ���� �� ������ ����
    

    void Start()
    {
        clickTrigger = false;
        tmpTrigger = false;
        clickCount = 0;
        cards = new List<GameObject>();

        // �迭�� �� ��Ҹ� �ʱ�ȭ
        vectorArray[0] = new Vector3(-300f, 0f, 0f);
        vectorArray[1] = new Vector3(0f, 0f, 0f);
        vectorArray[2] = new Vector3(300f, 0f, 0f);

        // ����Ʈ�� ������ 12���� �߰�
        for (int i = 0; i < 12; i++)
        {
            int randomIndex = Random.Range(0, prefabs.Count); // �������� �ε��� ����
            GameObject selectedPrefab = prefabs[randomIndex]; // ���õ� ������ ������
            cards.Add(selectedPrefab);      // ����Ʈ�� �߰�
            prefabs.RemoveAt(randomIndex);           
        }
    }

    void Update()
    {
        // Update �ȿ��� �ֱ������� ����ǵ��� ����
        if (prefabs != null)
        {
            // ī�带 ������ ĵ������ ��ġ�� �ʱ�ȭ
            canvas.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

            // Ŭ�� �� ������ ����
            if (clickTrigger == false && clickCount < 4) {
                for (int i = 0; i < 3; i++)
                {
                    GameObject InstPrefab = Instantiate(cards[i], canvas.transform);    // ī�� ����                  
                    InstPrefab.transform.position = vectorArray[i];                     // ī�� ��ġ ����                   
                }

                for (int j = 0; j < 3; j++)
                {
                    cards.RemoveAt(0);      // ����Ʈ���� ������ ī�� ��� ����
                }
                clickTrigger = true;
                tmpTrigger = true;
                //Invoke("CreateCards", 2f);      
                //Debug.Log(clickCount);
            }

            // 4���� ī�� ������ ������ ���������� �̵�
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
