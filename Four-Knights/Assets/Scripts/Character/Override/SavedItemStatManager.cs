using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedItemStatManager : ItemStatManager
{
    public override Item[] Inventory
    {
        get
        {
            return DataManager.instance.GetInventory();
        }
        protected set { DataManager.instance.SetInventoryData(value); }
    }
}
