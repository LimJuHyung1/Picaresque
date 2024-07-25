using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public enum ButtonType
{
    GameStart,
    Exit
}

public class Main_UI_Button_Actions : MonoBehaviour, IPointerEnterHandler ,IPointerExitHandler
{
    public ButtonType type; //UI 버튼 인스턴스
    public Transform Button_Scale;
    public NicknameManager nicknameManager;

    Vector3 defaultScale;

    void Start()
    {
        defaultScale = Button_Scale.localScale; //처음 디폴트 버튼 크기 값 고정적으로 저장
        Screen.SetResolution(1280, 960, false);
    }

    public void ClickButton()
    {
        switch(type)
        {
            case ButtonType.GameStart:
                nicknameManager.SetTrueWarningBox();
                break;
            case ButtonType.Exit:
                Application.Quit();
                break;
        }
    }

    // 커서를 버튼에 올렸을 때 이벤트

    public void OnPointerEnter(PointerEventData eventData)  // 버튼 크기 확대
        => Button_Scale.localScale = defaultScale * 1.2f; 

    public void OnPointerExit(PointerEventData eventData)   // 버튼 크기 축소
        => Button_Scale.localScale = defaultScale; 
}
