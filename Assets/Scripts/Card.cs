using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class Card : MonoBehaviourPunCallbacks, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public AudioClip clickSound; // ��ư Ŭ�� ����
    private AudioSource audioSource;
    //----------------------------------//
    CardManager cardmanager;        // ī�� �Ŵ��� ��ũ��Ʈ ���� ������
    string Name;                    // Card ��ũ��Ʈ�� ������ �Ǵ� ������Ʈ�� �̸��� ������
    //----------------------------------//
    public static bool deja_Vu_Trigger;
    public static bool dream_Bundle_Trigger;
    public static bool lucid_Dreaming_Trigger;
    //public static bool memory_Playback_Trigger;
    public static bool sink_Hole_Trigger;
    //----------------------------------//
    private List<Image> imagesToFade;   // �θ� ������Ʈ�� �ڽ� ������Ʈ�� ���� �����ϱ� ���� ���
    public float fadeSpeed = 1f; // ���̵� �ӵ� (���ϴ� ������ ����)
    private bool isClick;           // ī�尡 Ŭ�� �Ǿ��� �� ������ ���ҽ�ų �뵵�� ���
    private float currentAlpha;


    void Awake()
    {
        // AudioSource ������Ʈ �������� (��ư ������Ʈ�� �θ� ������Ʈ�� �־�� ��)
        audioSource = GetComponent<AudioSource>();

        Name = this.gameObject.name;

        deja_Vu_Trigger = false;
        dream_Bundle_Trigger = false;
        lucid_Dreaming_Trigger = false;
        // memory_Playback_Trigger = false;
        sink_Hole_Trigger = false;

        // �θ� ������Ʈ�� �� ���� �ڽ� ������Ʈ�鿡 �ִ� ��� Image ������Ʈ�� ��������
        imagesToFade = new List<Image>(GetComponentsInChildren<Image>());
        isClick = false;
    }

    void Update()
    {
        if (isClick) 
        {
            FadeCard();
        }
    }

    // ī�带 Ŭ������ �� �۵��ϴ� �Լ�
    public void CardChoice()
    {
        if(CardManager.tmpTrigger == true)
        {
            Transform blindCard = transform.Find("blindCard");      // ī�� ������ �ڽ� ������Ʈ

            if (CardManager.clickCount < 4 && blindCard != null)
            {
                switch (Name)       // Card ��ũ��Ʈ�� ������ �ִ� ������Ʈ�� �̸��� ���� �ٸ��� ����
                {
                    // ����ġ ����
                    case "CounterCard(Clone)":
                        MoralityManager.ModifyMorality(-20);
                        break;
                    case "RobCard(Clone)":
                        MoralityManager.ModifyMorality(-10);
                        break;
                    case "KindnessCard(Clone)":
                        MoralityManager.ModifyMorality(10);
                        break;
                    case "VolunteerCard(Clone)":
                        MoralityManager.ModifyMorality(20);
                        break;

                    //---------------------------------//

                    default:
                        Debug.Log("Something is wrong!" + Name);
                        break;
                }

                blindCard.gameObject.SetActive(false);
                CardManager.tmpTrigger = false;
                Invoke("OnClickTrigger", 2f);
                CardManager.clickCount += 1;            // ī�� ���� ī��Ʈ
                Debug.Log("Player morality : " + MoralityManager.PrintMorality());
                DisableCard();      // ī�� ��Ȱ��ȭ �Լ�(�̿ϼ�)

            }
        }
    }

    [PunRPC]
    public void OnTriggers(string ic_name)
    {
        switch (ic_name)
        {
            case "IC(Deja_Vu)(Clone)":
                //deja_Vu_Trigger = true;
                break;
            case "IC(Dream_Bundle)(Clone)":
                dream_Bundle_Trigger = true;
                break;
            case "IC(Lucid_Dreaming)(Clone)":
                lucid_Dreaming_Trigger = true;
                break;
            case "IC(Memory_Playback)(Clone)":
                break;
            case "IC(Sink_Hole)(Clone)":
                //sink_Hole_Trigger = true;
                break;
            default:
                break;
        }
    }

    [PunRPC]
    public void OffTriggers(int i)
    {
        switch (i)
        {
            case 0:
                //deja_Vu_Trigger = true;
                break;
            case 1:
                dream_Bundle_Trigger = false;
                break;
            case 2:
                lucid_Dreaming_Trigger = false;
                break;
            case 3:
                break;
            case 4:
                //sink_Hole_Trigger = true;
                break;
            default:
                break;
        }
    }

    // ������ ī�� Ŭ�� �� ȿ��
    public void ItemCardChoice()        
    {
        switch (Name) 
        {            
            case "IC(Deja_Vu)(Clone)":       // ����
                Debug.Log("ī�� ����");
                //ItemCardManager.PopAct[0](0, "IC(Deja_Vu)");
                photonView.RPC("OnTriggers", RpcTarget.AllBuffered, Name);
                break;
            case "IC(Dream_Bundle)(Clone)":  // ������ �ݰ�
                Debug.Log("������ �ݰ�");
                //ItemCardManager.PopAct[1](1, "IC(Dream_Bundle)");
                photonView.RPC("OnTriggers", RpcTarget.AllBuffered, Name);
                break;                
            case "IC(Lucid_Dreaming)(Clone)":    // �� �� ���� �ൿ - ����
                Debug.Log("�� �� ���� �ൿ");
               // ItemCardManager.PopAct[2](2, "IC(Lucid_Dreaming)");
                photonView.RPC("OnTriggers", RpcTarget.AllBuffered, Name);
                break;
            case "IC(Memory_Playback)(Clone)":   // ü�� 3 ȸ��
                Debug.Log("ü�� 3 ȸ��");
                //ItemCardManager.PopAct[3](3, "IC(Memory_Playback)");
                photonView.RPC("OnTriggers", RpcTarget.AllBuffered, Name);
                Player.hp += 3;
                break;
            case "IC(Sink_Hole)(Clone)":         // ��ũȦ ��ġ
                Debug.Log("��ũȦ ��ġ");
                //ItemCardManager.PopAct[4](4, "IC(Sink_Hole)");
                photonView.RPC("OnTriggers", RpcTarget.AllBuffered, Name);
                //sink_Hole_Trigger = true;
                break;
            default:
                break;
        }
        isClick = true;     // ī�带 ���������
    }



    public void FadeCard()
    {
        // ���̵� ȿ�� ����
        foreach (Image image in imagesToFade)
        {
            Color currentColor = image.color;
            currentAlpha = currentColor.a;
            Color newColor = new Color
                (currentColor.r,
                currentColor.g,
                currentColor.b,
                currentColor.a - (fadeSpeed * Time.deltaTime * 3.0f));
            image.color = newColor;            
        }
        // ������ 0�̸� ������Ʈ ��Ȱ��ȭ
        if (currentAlpha <= 0f)
        {
            gameObject.SetActive(false);
            //ItemCardManager.DeleteEffect();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // ��ư�� Ŭ���Ǿ��� �� ����� ���
        if (clickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }

    // ���콺 �����Ͱ� ������Ʈ ���� �ö��� �� ȣ��˴ϴ�.
    public void OnPointerEnter(PointerEventData eventData)
    {
        switch (Name)
        {
            case "IC(Deja_Vu)(Clone)":       // ����
               // ItemCardManager.ShowEffect[0](0, "IC(Deja_Vu)");
                break;
            case "IC(Dream_Bundle)(Clone)":  // ������ �ݰ�
                //ItemCardManager.ShowEffect[1](1, "IC(Dream_Bundle)");
                break;
            case "IC(Lucid_Dreaming)(Clone)":    // �� �� ���� �ൿ - ����
                //ItemCardManager.ShowEffect[2](2, "IC(Lucid_Dreaming)");
                break;
            case "IC(Memory_Playback)(Clone)":   // ü�� 3 ȸ��
                //ItemCardManager.ShowEffect[3](3, "IC(Memory_Playback)");
                break;
            case "IC(Sink_Hole)(Clone)":         // ��ũȦ ��ġ
                //ItemCardManager.ShowEffect[4](4, "IC(Sink_Hole)");
                break;
            default:
                break;        
        }        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Debug.Log("ī�� ���� ������");
        //ItemCardManager.DeleteEffect();
    }

    // ī�� Ŭ�� �� CardManager�� trigger ������ false�� ����
    private void OnClickTrigger()       
    {
        CardManager.clickTrigger = false;       
    }

    // ī�带 ��Ȱ��ȭ �ϴ� �Լ�
    IEnumerator DisableCard()
    {
        yield return new WaitForSeconds(audioSource.clip.length - 1f); // ���� ��� �ð���ŭ ���
        // gameObject.SetActive(false);
    }
}
