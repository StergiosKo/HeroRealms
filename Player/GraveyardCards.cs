using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveyardCards : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform contentHolder;
    public Player player;

    public void CreateCard(Card card)
    {
        GameObject anObject = GameObject.Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        anObject.GetComponent<CardHolder>().SetCard(card);
        anObject.transform.SetParent(contentHolder);
        anObject.GetComponent<Sacrificable>().player = player;
        anObject.GetComponent<Returnable>().player = player;
    }

    public void DestroyCards()
    {
        foreach(Transform child in contentHolder)
        {
            Destroy(child.gameObject);
        }
    }
}
