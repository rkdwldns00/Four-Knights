using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropedItem : Interactable
{
    public Item Item { get; protected set; }
    public int Count { get; protected set; }

    public override void Interaction(GameObject eventPlayer)
    {
        //throw new System.NotImplementedException();
        ItemStatManager statManager = eventPlayer.GetComponent<ItemStatManager>();
        if (statManager != null)
        {
            switch (GameManager.ItemTable[Item.id].ItemType)
            {
                case ItemType.Weapon:
                    statManager.AddWeapon(Item);
                    break;
                case ItemType.Accessories:
                    statManager.AddAccessories(Item);
                    break;
                case ItemType.Etc:
                    statManager.AddEtcItem(Item,Count);
                    break;
            }
        }
    }
}
