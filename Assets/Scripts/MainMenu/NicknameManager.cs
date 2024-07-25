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
        // 닉네임의 길이를 10글자로 제한
        if(playerNameInput.text.Length > 10)
            playerName = playerNameInput.text.Substring(0, 10);

        else playerName = playerNameInput.text;
    }

    public void SetTrueWarningBox()
    {        
        // 닉네임이 공백일 경우
        if (playerNameInput.text == "닉네임") 
        {
            Debug.Log("Nickname is null");
            warningBox.gameObject.SetActive(true);
        }
        // 닉네임을 적었을 경우
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
