using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItemMaker : MonoBehaviour
{
    ItemStatManager itemStatManager;

    void Start()
    {
        itemStatManager = GetComponent<ItemStatManager>();
        Debug.Log(GameManager.ItemTable[itemStatManager.FindItem(1).id].ItemName);
        int index = itemStatManager.AddEtcItem(new Item() { id = 1 }, 1);
        Debug.Log(itemStatManager.Inventory.Length);
        Debug.Log(((EtcUniqueData)(itemStatManager.Inventory[index].uniqueData)).count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
