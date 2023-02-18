using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonFinishPortal : Interactable
{
    public override void Interaction(GameObject eventPlayer)
    {
        GetComponentInParent<Dungeon>().FinishDungeon();
        IsActive = false;
    }
}
