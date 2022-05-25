using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrePlayer : MonoBehaviour
{
    public PlayerSO playerSO;

    public Image icon;
    public InputField nameText;

    public Image humanButton;
    public Image aiButton;

    public GameObject background;

    public PrePlayerManager prePlayerManager;

    private void Start()
    {
        SetStats();
    }

    public void SetStats()
    {
        icon.sprite = playerSO.playerSprite;
        nameText.text = playerSO.playerName;
        playerSO.isActive = true;
        if (playerSO.playerType == PlayerType.Human) HumanPressed();
        else AIPressed();
    }

    public void IconChanged(Sprite sprite)
    {
        icon.sprite = sprite;
        playerSO.playerSprite = sprite;
    }
    public void NameChanged()
    {
        playerSO.playerName = nameText.text;
    }

    public void HumanPressed()
    {
        playerSO.playerType = PlayerType.Human;
        ButtonActive(aiButton, false);
        ButtonActive(humanButton, true);
    }
    public void AIPressed()
    {
        playerSO.playerType = PlayerType.AI;
        ButtonActive(humanButton, false);
        ButtonActive(aiButton, true);
    }

    private void ButtonActive(Image image, bool flag)
    {
        if (flag) image.color = new UnityEngine.Color(255, 255, 255, 255);
        else image.color = new UnityEngine.Color32(156, 136, 136, 255);
    }

    public void IconPressed()
    {
        prePlayerManager.ChangeIconsObject(this);
    }

    public void TrashPressed()
    {
        playerSO.isActive = false;
        background.SetActive(false);
    }

    public void NewPlayerPressed()
    {
        playerSO.isActive = true;
        background.SetActive(true);
    }
}
