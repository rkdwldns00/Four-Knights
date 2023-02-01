using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropedItem : Interactable
{
    public Item item;
    [SerializeField] GameObject basicModelPrefab;

    private void Start()
    {
        if (showedName == null || showedName == "")
        {
            showedName = GameManager.ItemTable[item.id].ItemName;
        }
        if (GameManager.ItemTable[item.id].Model != null)
        {
            ActiveSymbol = Instantiate(GameManager.ItemTable[item.id].Model, transform);
        }
        else
        {
            ActiveSymbol = Instantiate(basicModelPrefab, transform);
        }
    }

    public override void Interaction(GameObject eventPlayer)
    {
        //throw new System.NotImplementedException();
        ItemStatManager statManager = eventPlayer.GetComponent<ItemStatManager>();
        if (statManager != null)
        {
            statManager.AddItem(item);
            Destroy(gameObject);
        }
    }
}
