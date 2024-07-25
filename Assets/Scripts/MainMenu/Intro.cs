using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    string[] intros;
    public int introIndex;

    public float duration = 3f; // 동작 지속 시간
    public float elapsedTime = 0f; // 경과 시간

    public Text introText;
    public float fadeSpeed = 0.5f; // 투명도 증가 속도

    Color newColor;

    private Color originalColor;
    private float currentAlpha = 0f;

    /*private float currentVolume = 1f;*/

    AudioSource audioSource;

    void Start()
    {
        introIndex = 0;

        intros = new string[] {"이 곳은 꿈 속의 세계입니다.",
                                "여러분의 본성을 드러낼 수 있는 곳이죠.",
                                "이 꿈 속에서 여러분의 행동에 따라\n선과 악이 정해집니다.",
                                "자, 이제 여러분의 선택을 보여주세요."};

        newColor = originalColor;        
        introText.text = intros[introIndex];
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            elapsedTime += Time.deltaTime;

            if (introIndex == intros.Length)
            {
                SceneManager.LoadScene("Shared Dream", LoadSceneMode.Single);
            }

            if (elapsedTime < duration)
                P_Transparency();
            else if (elapsedTime >= duration && elapsedTime < 2 * duration)
                M_Transparency();
            else if (elapsedTime >= 2 * duration)
                ChangeText();

            /* if (introIndex == intros.Length - 1) DownVolume();  */          

        }
       
        catch (IndexOutOfRangeException)
        {
            SceneManager.LoadScene("First Choice", LoadSceneMode.Single);
        }

    }

    // 투명도 증가
    void P_Transparency()
    {
        currentAlpha += fadeSpeed * Time.deltaTime;
        currentAlpha = Mathf.Clamp01(currentAlpha); // 투명도를 0~1 사이로 제한

        // 텍스트의 투명도 조절
        newColor.a = currentAlpha;
        introText.color = newColor;
    }

    // 투명도 감소
    void M_Transparency()
    {
        currentAlpha -= fadeSpeed * Time.deltaTime;
        currentAlpha = Mathf.Clamp01(currentAlpha); // 투명도를 0~1 사이로 제한

        // 텍스트의 투명도 조절
        newColor.a = currentAlpha;
        introText.color = newColor;
    }

    // 대화 내용 변경
    void ChangeText()
    {
        introIndex++;
        introText.text = intros[introIndex];
        elapsedTime = 0;
    }

    // 소리 줄이려 했음
    /*
    void DownVolume()
    {
        currentVolume -= fadeSpeed * Time.deltaTime;
        currentVolume = Mathf.Clamp01(currentVolume);

        audioSource.volume = currentVolume;
    }*/

}
