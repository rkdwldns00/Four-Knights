using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteraction : Interactable
{
    public override void Interaction(GameObject eventPlayer)
    {
        IsActive = false;
        eventPlayer.GetComponent<Victim>().TakeDamage(10);
    }
}
