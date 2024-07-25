using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCard : MonoBehaviour
{
    private AudioSource audio;
    public AudioClip Sound;
    public Image card;
    public Choice choiceScene;


    void Awake()
    {
        this.audio = this.gameObject.AddComponent<AudioSource>();
        this.audio.clip = this.Sound;
        this.audio.loop = false;
    }

    // 카드 클릭을 활성화 상태로 만드는 함수
    bool Click_Able()
    {
        return choiceScene.MessageIndex == 4 || choiceScene.MessageIndex == 7 ? true : false;
    }

    // 카드 클릭과 함께 메세지 창 내용 변경
    void ClickAction()
    {
        choiceScene.MessageIndex += 1;
        choiceScene.Message.text = choiceScene.Messages[choiceScene.MessageIndex];
    }

    // 카드를 클릭했을 때 작동하는 함수
    public void CardChoice()
    {
        if (Click_Able())
        {
            audio.Play();
            StartCoroutine(DisableCard());
            ClickAction();
        }        
    }

    // 카드를 비활성화 하는 함수
    IEnumerator DisableCard()
    {
        yield return new WaitForSeconds(audio.clip.length - 1f); // 음악 재생 시간만큼 대기
        gameObject.SetActive(false);
    }
}
