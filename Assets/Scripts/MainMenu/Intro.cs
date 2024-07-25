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

    public float duration = 3f; // ���� ���� �ð�
    public float elapsedTime = 0f; // ��� �ð�

    public Text introText;
    public float fadeSpeed = 0.5f; // ���� ���� �ӵ�

    Color newColor;

    private Color originalColor;
    private float currentAlpha = 0f;

    /*private float currentVolume = 1f;*/

    AudioSource audioSource;

    void Start()
    {
        introIndex = 0;

        intros = new string[] {"�� ���� �� ���� �����Դϴ�.",
                                "�������� ������ �巯�� �� �ִ� ������.",
                                "�� �� �ӿ��� �������� �ൿ�� ����\n���� ���� �������ϴ�.",
                                "��, ���� �������� ������ �����ּ���."};

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

    // ���� ����
    void P_Transparency()
    {
        currentAlpha += fadeSpeed * Time.deltaTime;
        currentAlpha = Mathf.Clamp01(currentAlpha); // ������ 0~1 ���̷� ����

        // �ؽ�Ʈ�� ���� ����
        newColor.a = currentAlpha;
        introText.color = newColor;
    }

    // ���� ����
    void M_Transparency()
    {
        currentAlpha -= fadeSpeed * Time.deltaTime;
        currentAlpha = Mathf.Clamp01(currentAlpha); // ������ 0~1 ���̷� ����

        // �ؽ�Ʈ�� ���� ����
        newColor.a = currentAlpha;
        introText.color = newColor;
    }

    // ��ȭ ���� ����
    void ChangeText()
    {
        introIndex++;
        introText.text = intros[introIndex];
        elapsedTime = 0;
    }

    // �Ҹ� ���̷� ����
    /*
    void DownVolume()
    {
        currentVolume -= fadeSpeed * Time.deltaTime;
        currentVolume = Mathf.Clamp01(currentVolume);

        audioSource.volume = currentVolume;
    }*/

}
