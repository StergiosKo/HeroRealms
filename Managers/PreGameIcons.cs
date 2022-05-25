using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreGameIcons : MonoBehaviour
{

    public PrePlayerManager manager;

    public void Pressed()
    {
        manager.IconChanged(gameObject.GetComponent<Image>().sprite);
    }
}
