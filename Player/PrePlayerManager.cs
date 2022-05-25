using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrePlayerManager : MonoBehaviour
{
    public List<PrePlayer> players;

    public GameObject icons;

    private bool iconsOpen = false;

    private PrePlayer currentPlayerChange;

    private void Start()
    {
        foreach(PrePlayer player in players)
        {
            player.SetStats();
        }
    }

    public void ChangeIconsObject(PrePlayer player)
    {
        OpenCloseIconWindow();
        currentPlayerChange = player;

    }

    public void OpenCloseIconWindow()
    {
        iconsOpen = !iconsOpen;
        icons.SetActive(iconsOpen);
    }

    public void IconChanged(Sprite iconSprite)
    {
        currentPlayerChange.IconChanged(iconSprite);
    }

    public void StartButtonPressed()
    {
        gameObject.GetComponent<MoveSceen>().Pressed("GameScene");
    }

}
