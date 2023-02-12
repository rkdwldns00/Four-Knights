using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SavedItemStatManager : ItemStatManager
{
    public override Item[] Inventory
    {
        get
        {
            return GetInventory();
        }
        protected set { SetInventoryData(value); }
    }

    public Item[] GetInventory()
    {
        Data data = DataManager.LoadGameData<Data>("Inventory");
        Item[] inventory = new Item[data.inventory.Length];
        for (int i = 0; i < inventory.Length; i++)
        {
            inventory[i] = new Item();
            inventory[i].id = data.inventory[i].id;
            switch (GameManager.ItemTable[data.inventory[i].id].ItemType)
            {
                case ItemType.Weapon:
                    inventory[i].uniqueData = data.weaponUniqueData[data.inventory[i].uniqueIndex];
                    break;
                case ItemType.Accessories:
                    inventory[i].uniqueData = data.accessoriesUniqueData[data.inventory[i].uniqueIndex];
                    break;
                case ItemType.Etc:
                    inventory[i].uniqueData = data.etcUniqueData[data.inventory[i].uniqueIndex];
                    break;
            }
        }
        return inventory;
    }

    public void SetInventoryData(Item[] inventory)
    {
        Data data = new Data();
        data.inventory = new ItemWithUniqueIndex[inventory.Length];
        int weaponIndex = 0;
        int accessoriesIndex = 0;
        int etcIndex = 0;
        for (int i = 0; i < inventory.Length; i++)
        {
            switch (GameManager.ItemTable[inventory[i].id].ItemType)
            {
                case ItemType.Weapon:
                    data.inventory[i].uniqueIndex = weaponIndex;
                    weaponIndex += 1;
                    break;
                case ItemType.Accessories:
                    data.inventory[i].uniqueIndex = accessoriesIndex;
                    accessoriesIndex += 1;
                    break;
                case ItemType.Etc:
                    data.inventory[i].uniqueIndex = etcIndex;
                    etcIndex += 1;
                    break;
            }
        }
        data.weaponUniqueData = new WeaponUniqueData[weaponIndex];
        data.accessoriesUniqueData = new AccessoriesUniqueData[accessoriesIndex];
        data.etcUniqueData = new EtcUniqueData[etcIndex];
        weaponIndex = 0;
        accessoriesIndex = 0;
        etcIndex = 0;
        for (int i = 0; i < inventory.Length; i++)
        {
            switch (GameManager.ItemTable[inventory[i].id].ItemType)
            {
                case ItemType.Weapon:
                    data.weaponUniqueData[weaponIndex++] = (WeaponUniqueData)inventory[i].uniqueData;
                    break;
                case ItemType.Accessories:
                    data.accessoriesUniqueData[accessoriesIndex++] = (AccessoriesUniqueData)inventory[i].uniqueData;
                    break;
                case ItemType.Etc:
                    data.etcUniqueData[etcIndex++] = (EtcUniqueData)inventory[i].uniqueData;
                    break;
            }
        }

        for (int i = 0; i < inventory.Length; i++)
        {
            data.inventory[i].id = inventory[i].id;
        }
        DataManager.SaveGameData("Inventory",data);
    }
}
