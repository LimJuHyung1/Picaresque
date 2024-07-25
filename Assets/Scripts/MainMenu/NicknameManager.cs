using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class NicknameManager : MonoBehaviour
{
    public InputField playerNameInput;
    public Image warningBox;
    public static string playerName;

    public void GetNickname()
    {
        // �г����� ���̸� 10���ڷ� ����
        if(playerNameInput.text.Length > 10)
            playerName = playerNameInput.text.Substring(0, 10);

        else playerName = playerNameInput.text;
    }

    public void SetTrueWarningBox()
    {        
        // �г����� ������ ���
        if (playerNameInput.text == "�г���") 
        {
            Debug.Log("Nickname is null");
            warningBox.gameObject.SetActive(true);
        }
        // �г����� ������ ���
        else 
        {
            Debug.Log("Load Scene");
            SceneManager.LoadScene("Loading", LoadSceneMode.Single);
        }             
    }

    public void SetFalseWarningBox()
    {
        warningBox.gameObject.SetActive(false);
    }
}
