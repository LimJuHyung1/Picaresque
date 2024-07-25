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

    // ī�� Ŭ���� Ȱ��ȭ ���·� ����� �Լ�
    bool Click_Able()
    {
        return choiceScene.MessageIndex == 4 || choiceScene.MessageIndex == 7 ? true : false;
    }

    // ī�� Ŭ���� �Բ� �޼��� â ���� ����
    void ClickAction()
    {
        choiceScene.MessageIndex += 1;
        choiceScene.Message.text = choiceScene.Messages[choiceScene.MessageIndex];
    }

    // ī�带 Ŭ������ �� �۵��ϴ� �Լ�
    public void CardChoice()
    {
        if (Click_Able())
        {
            audio.Play();
            StartCoroutine(DisableCard());
            ClickAction();
        }        
    }

    // ī�带 ��Ȱ��ȭ �ϴ� �Լ�
    IEnumerator DisableCard()
    {
        yield return new WaitForSeconds(audio.clip.length - 1f); // ���� ��� �ð���ŭ ���
        gameObject.SetActive(false);
    }
}
