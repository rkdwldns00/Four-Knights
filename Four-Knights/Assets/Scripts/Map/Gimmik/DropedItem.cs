using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropedItem : Interactable
{
    [SerializeField] Item item;
    public Item Item
    {
        get { return item; }
        protected set { item = value; }
    }

    private void Start()
    {
        ActiveSymbol = Instantiate(GameManager.ItemTable[item.id].Model, transform);
    }

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
