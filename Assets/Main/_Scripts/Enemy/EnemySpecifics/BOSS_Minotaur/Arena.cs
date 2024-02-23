using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum boss
{
    boss1,
    boss2, 
}
public class Arena : MonoBehaviour
{
    [SerializeField] private GameObject door;
    public bool isPlayerSurrounding;
    public boss thisBoss;
    private UI_InGame inGameUI;
    private void Awake()
    {
        isPlayerSurrounding = false;
        if (door != null)
            door.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerSurrounding = true;
            
            if (door != null)
            {
                door.gameObject.SetActive(true);
                UI_InGame.instance.isInBossStage = true;
                switch (thisBoss) 
                {
                    case boss.boss1:
                        AudioManager.instance.PlayBGM(3);
                        break;
                        case boss.boss2:
                        AudioManager.instance.PlayBGM(2);
                        break;

                }
            }
               
        }

    }
    public void openDoor()
    {
        door.SetActive(false);
    }
}
