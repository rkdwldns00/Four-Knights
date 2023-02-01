using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropedItem : Interactable
{
    public Item Item { get; protected set; }

    public override void Interaction(GameObject eventPlayer)
    {
        //throw new System.NotImplementedException();
        ItemStatManager statManager = eventPlayer.GetComponent<ItemStatManager>();
        if (statManager != null)
        {
            statManager.AddItem(Item);
            Destroy(gameObject);
        }
    }
}
