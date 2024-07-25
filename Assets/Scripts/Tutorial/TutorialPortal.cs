using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPortal : MonoBehaviour
{
    public Player player;
    private AudioSource audio;
    public AudioClip Sound;
    public MessageManager messageManager;

    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        this.audio = this.gameObject.AddComponent<AudioSource>();
        this.audio.clip = this.Sound;
        this.audio.loop = false;
    }    

    /*public void Move()
    {
        switch (gameObject.tag)
        {
            case "arrow_right":
                player.MoveRight();
                player.myturn = false;
                break;
            case "arrow_down":
                player.MoveDown();
                player.myturn = false;
                break;
            case "arrow_left":
                player.MoveLeft();
                player.myturn = false;
                break;
            case "arrow_up":
                player.MoveUp();
                player.myturn = false;
                break;
            default:
                break;
        }
        messageManager.MessageIndex += 1;
        this.audio.Play();
    } */
}
