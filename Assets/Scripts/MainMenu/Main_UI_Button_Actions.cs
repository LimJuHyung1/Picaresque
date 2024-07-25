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
    public ButtonType type; //UI ��ư �ν��Ͻ�
    public Transform Button_Scale;
    public NicknameManager nicknameManager;

    Vector3 defaultScale;

    void Start()
    {
        defaultScale = Button_Scale.localScale; //ó�� ����Ʈ ��ư ũ�� �� ���������� ����
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

    // Ŀ���� ��ư�� �÷��� �� �̺�Ʈ

    public void OnPointerEnter(PointerEventData eventData)  // ��ư ũ�� Ȯ��
        => Button_Scale.localScale = defaultScale * 1.2f; 

    public void OnPointerExit(PointerEventData eventData)   // ��ư ũ�� ���
        => Button_Scale.localScale = defaultScale; 
}
