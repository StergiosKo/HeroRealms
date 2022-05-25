using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{
    Human,
    AI
}

[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/Player")]
public class PlayerSO : ScriptableObject
{

    public string playerName;
    public Sprite playerSprite;

    public PlayerType playerType;

    public bool isActive;

    private void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }

}
