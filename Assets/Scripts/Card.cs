using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class Card : MonoBehaviourPunCallbacks, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public AudioClip clickSound; // 버튼 클릭 사운드
    private AudioSource audioSource;
    //----------------------------------//
    CardManager cardmanager;        // 카드 매니저 스크립트 정보 가져옴
    string Name;                    // Card 스크립트를 가지게 되는 오브젝트의 이름을 가져옴
    //----------------------------------//
    public static bool deja_Vu_Trigger;
    public static bool dream_Bundle_Trigger;
    public static bool lucid_Dreaming_Trigger;
    //public static bool memory_Playback_Trigger;
    public static bool sink_Hole_Trigger;
    //----------------------------------//
    private List<Image> imagesToFade;   // 부모 오브젝트와 자식 오브젝트를 같이 포함하기 위해 사용
    public float fadeSpeed = 1f; // 페이드 속도 (원하는 값으로 조절)
    private bool isClick;           // 카드가 클릭 되었을 때 투명도를 감소시킬 용도로 사용
    private float currentAlpha;


    void Awake()
    {
        // AudioSource 컴포넌트 가져오기 (버튼 오브젝트나 부모 오브젝트에 있어야 함)
        audioSource = GetComponent<AudioSource>();

        Name = this.gameObject.name;

        deja_Vu_Trigger = false;
        dream_Bundle_Trigger = false;
        lucid_Dreaming_Trigger = false;
        // memory_Playback_Trigger = false;
        sink_Hole_Trigger = false;

        // 부모 오브젝트와 그 하위 자식 오브젝트들에 있는 모든 Image 컴포넌트를 가져오기
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

    // 카드를 클릭했을 때 작동하는 함수
    public void CardChoice()
    {
        if(CardManager.tmpTrigger == true)
        {
            Transform blindCard = transform.Find("blindCard");      // 카드 가리는 자식 오브젝트

            if (CardManager.clickCount < 4 && blindCard != null)
            {
                switch (Name)       // Card 스크립트를 가지고 있는 오브젝트의 이름에 따라 다르게 실행
                {
                    // 도덕치 증감
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
                CardManager.clickCount += 1;            // 카드 선택 카운트
                Debug.Log("Player morality : " + MoralityManager.PrintMorality());
                DisableCard();      // 카드 비활성화 함수(미완성)

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

    // 아이템 카드 클릭 시 효과
    public void ItemCardChoice()        
    {
        switch (Name) 
        {            
            case "IC(Deja_Vu)(Clone)":       // 재사용
                Debug.Log("카드 재사용");
                //ItemCardManager.PopAct[0](0, "IC(Deja_Vu)");
                photonView.RPC("OnTriggers", RpcTarget.AllBuffered, Name);
                break;
            case "IC(Dream_Bundle)(Clone)":  // 데미지 반감
                Debug.Log("데미지 반감");
                //ItemCardManager.PopAct[1](1, "IC(Dream_Bundle)");
                photonView.RPC("OnTriggers", RpcTarget.AllBuffered, Name);
                break;                
            case "IC(Lucid_Dreaming)(Clone)":    // 두 턴 연속 행동 - 성공
                Debug.Log("두 턴 연속 행동");
               // ItemCardManager.PopAct[2](2, "IC(Lucid_Dreaming)");
                photonView.RPC("OnTriggers", RpcTarget.AllBuffered, Name);
                break;
            case "IC(Memory_Playback)(Clone)":   // 체력 3 회복
                Debug.Log("체력 3 회복");
                //ItemCardManager.PopAct[3](3, "IC(Memory_Playback)");
                photonView.RPC("OnTriggers", RpcTarget.AllBuffered, Name);
                Player.hp += 3;
                break;
            case "IC(Sink_Hole)(Clone)":         // 싱크홀 설치
                Debug.Log("싱크홀 설치");
                //ItemCardManager.PopAct[4](4, "IC(Sink_Hole)");
                photonView.RPC("OnTriggers", RpcTarget.AllBuffered, Name);
                //sink_Hole_Trigger = true;
                break;
            default:
                break;
        }
        isClick = true;     // 카드를 사라지도록
    }



    public void FadeCard()
    {
        // 페이드 효과 적용
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
        // 투명도가 0이면 오브젝트 비활성화
        if (currentAlpha <= 0f)
        {
            gameObject.SetActive(false);
            //ItemCardManager.DeleteEffect();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 버튼이 클릭되었을 때 오디오 재생
        if (clickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }

    // 마우스 포인터가 오브젝트 위에 올라갔을 때 호출됩니다.
    public void OnPointerEnter(PointerEventData eventData)
    {
        switch (Name)
        {
            case "IC(Deja_Vu)(Clone)":       // 재사용
               // ItemCardManager.ShowEffect[0](0, "IC(Deja_Vu)");
                break;
            case "IC(Dream_Bundle)(Clone)":  // 데미지 반감
                //ItemCardManager.ShowEffect[1](1, "IC(Dream_Bundle)");
                break;
            case "IC(Lucid_Dreaming)(Clone)":    // 두 턴 연속 행동 - 성공
                //ItemCardManager.ShowEffect[2](2, "IC(Lucid_Dreaming)");
                break;
            case "IC(Memory_Playback)(Clone)":   // 체력 3 회복
                //ItemCardManager.ShowEffect[3](3, "IC(Memory_Playback)");
                break;
            case "IC(Sink_Hole)(Clone)":         // 싱크홀 설치
                //ItemCardManager.ShowEffect[4](4, "IC(Sink_Hole)");
                break;
            default:
                break;        
        }        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Debug.Log("카드 설명 지워짐");
        //ItemCardManager.DeleteEffect();
    }

    // 카드 클릭 후 CardManager의 trigger 변수를 false로 설정
    private void OnClickTrigger()       
    {
        CardManager.clickTrigger = false;       
    }

    // 카드를 비활성화 하는 함수
    IEnumerator DisableCard()
    {
        yield return new WaitForSeconds(audioSource.clip.length - 1f); // 음악 재생 시간만큼 대기
        // gameObject.SetActive(false);
    }
}
