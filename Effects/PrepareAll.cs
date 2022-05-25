using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PrepareAll", menuName = "ScriptableObjects/Effects/PrepareAll")]
public class PrepareAll : BaseEffect
{
    public override void Activate(Player player, int amount)
    {
        player.board.ReadyBoard();
    }

}
