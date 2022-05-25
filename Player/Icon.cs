using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Background
{
    Active,
    Choose
}

public class Icon : MonoBehaviour
{
    public Image image;
    public Image background;
    public Player owner;
    public GameObject dealtDamageObject;
    public Text nameText;

    public bool canBeDamaged = false;


    public void ChangeIcon(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void ChangeName(string text)
    {
        nameText.text = text;
    }

    public void ShowBackground(Background bc)
    {
        background.gameObject.SetActive(true);
        if (bc == Background.Choose)
        {
            canBeDamaged = true;
            background.color = new Color32(29, 171, 242, 255);
        }
        else
        {
            background.color = new Color32(241, 26, 228, 255);
        }
    }

    public void CloseBackground()
    {
        canBeDamaged = false;
        background.gameObject.SetActive(false);
    }

    public void EnemyClicked()
    {
        if (canBeDamaged)
        {
            canBeDamaged = false;
            GameObject.Find("GameManager").GetComponent<GameManager>().DamagePlayerClicked(owner);
        }

        if (GameManager.opponentDiscards)
        {
            GameManager.opponentDiscards = false;
            owner.DiscardCard();
        }
    }

    public void DamagedAnimation()
    {
        StartCoroutine(DamageDealtCoroutine());
    }


    private IEnumerator DamageDealtCoroutine()
    {
        dealtDamageObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        dealtDamageObject.SetActive(false);
    }

}
