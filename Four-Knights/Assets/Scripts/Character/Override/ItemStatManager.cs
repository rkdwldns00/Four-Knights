using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemStatManager : StatManager
{
    Item[] inventory = new Item[0];
    public virtual Item[] Inventory
    {
        get { return inventory; }
        protected set { inventory = value; Debug.Log("로컬 인벤토리 설정"); }
    }

    protected Item weapon;
    public Item Weapon
    {
        set
        {
            if (GameManager.ItemTable[value.id].ItemType == ItemType.Weapon)
            {
                if (((WeaponStaticData)GameManager.ItemTable[value.id]).WeaponType == CharacterData.WeaponType)
                {
                    weapon = value;
                }
                else
                {
                    Debug.LogError("지정된 무기타입과 다른 타입의 무기를 장착시도하였습니다.");
                }
            }
            else
            {
                Debug.LogError("무기가아닌 장비를 장착시도하였습니다.");
            }
        }
    }

    protected Item[] accessories = new Item[5];
    public Item[] Accessories
    {
        set
        {
            if (accessories.Length == value.Length)
            {
                bool c = true;
                foreach (Item item in value)
                {
                    if (GameManager.ItemTable[item.id].ItemType != ItemType.Accessories)
                    {
                        c = false;
                        break;
                    }
                }
                if (c)
                {
                    accessories = value;
                }
                else
                {
                    Debug.LogError("장신구가아닌 아이템을 장신구칸에 장착시도하였습니다.");
                }
            }
            else
            {
                Debug.LogError("성유물칸의 개수가 맞지않습니다!");
            }
        }
    }

    protected override float FindUpgradeStat(UpgradeStatType type)
    {
        float value = base.FindUpgradeStat(type);
        if (GameManager.ItemTable[weapon.id].ItemType == ItemType.Weapon)
        {
            foreach (UpgradeStatWithValue upgradeStat in ((WeaponStaticData)GameManager.ItemTable[weapon.id]).UpgradeStat)
            {
                if (upgradeStat.UpgradeStatType == type)
                {
                    value += upgradeStat.value * ((WeaponUniqueData)weapon.uniqueData).enforce;
                }
            }
        }
        foreach (Item item in accessories)
        {
            if (GameManager.ItemTable[item.id].ItemType == ItemType.Accessories)
            {
                foreach (UpgradeStatWithValue upgradeStat in ((AccessoriesUniqueData)(item.uniqueData)).upgradeStat)
                {
                    value += upgradeStat.value * ((AccessoriesUniqueData)(item.uniqueData)).enforce;
                }
            }
        }
        return value;
    }

    public int AddItem(Item item)
    {
        if (GameManager.ItemTable[item.id].ItemType != ItemType.Etc || FindItemInfo(item.id).id == 0)
        {
            int nullIndex = -1;
            for (int i = 0; i < Inventory.Length; i++)
            {
                if (Inventory[i].id == 0)
                {
                    nullIndex = i;
                    break;
                }
            }

            if (nullIndex == -1)
            {
                Item[] items = new Item[Inventory.Length + 1];
                for (int i = 0; i < Inventory.Length; i++)
                {
                    items[i] = Inventory[i];
                }
                items[items.Length - 1] = item;
                items = CreateUniqueData(items, items.Length - 1);

                Inventory = items;
                return Inventory.Length - 1;
            }
            else
            {
                Item[] items = Inventory;
                items[nullIndex] = item;
                items = CreateUniqueData(items,nullIndex);
                Inventory = items;
                return nullIndex;
            }
        }
        else
        {
            for(int i = 0; i < Inventory.Length; i++)
            {
                if (Inventory[i].id == item.id)
                {
                    if (item.uniqueData == null)
                    {
                        item = CreateUniqueData(item);
                    }
                    if (((EtcUniqueData)Inventory[i].uniqueData).count + ((EtcUniqueData)item.uniqueData).count != 0)
                    {
                        EtcUniqueData d = (EtcUniqueData)Inventory[i].uniqueData;
                        d.count += ((EtcUniqueData)item.uniqueData).count;
                        Item[] inven = Inventory;
                        inven[i].uniqueData = d;
                        Inventory = inven;
                        return i;
                    }
                    else
                    {
                        DeleteItem(i);
                        return -1;
                    }
                }
            }
        }
        return -1;
    }

    Item[] CreateUniqueData(Item[] items,int index)
    {
        items[index] = CreateUniqueData(items[index]);
        return items;
    }

    Item CreateUniqueData(Item item)
    {
        switch (GameManager.ItemTable[item.id].ItemType)
        {
            case ItemType.Etc:
                if (item.uniqueData == null || ((EtcUniqueData)item.uniqueData).count == 0)
                {
                    item.uniqueData = new EtcUniqueData() { count = 1 };
                }
                break;
            case ItemType.Weapon:
                if (item.uniqueData == null || ((WeaponUniqueData)item.uniqueData).enforce == 0)
                {
                    item.uniqueData = new WeaponUniqueData() { enforce = 1 };
                }
                break;
            case ItemType.Accessories:
                if (item.uniqueData == null || ((AccessoriesUniqueData)item.uniqueData).enforce == 0)
                {
                    AccessoriesUniqueData unique = new AccessoriesUniqueData() { enforce = 1 };
                    UpgradeStatWithValue[] randomList = ((AccssoriesStaticData)GameManager.ItemTable[item.id]).MaxUpgradeStatList;
                    unique.upgradeStat = new UpgradeStatWithValue[2];
                    unique.upgradeStat[0] = randomList[UnityEngine.Random.Range(0, randomList.Length)];
                    unique.upgradeStat[0].value *= UnityEngine.Random.Range(0.5f, 1f);
                    unique.upgradeStat[1] = randomList[UnityEngine.Random.Range(0, randomList.Length)];
                    unique.upgradeStat[1].value *= UnityEngine.Random.Range(0.5f, 1f);

                    item.uniqueData = unique;
                }
                break;
        }
        return item;
    }

    public void DeleteItem(int index)
    {
        if (index == Inventory.Length - 1)
        {
            int lastIndex = Inventory.Length - 1;
            for(int i = Inventory.Length - 1; i > 0; i--)
            {
                if(Inventory[i].id != 0)
                {
                    break;
                }
            }

            Item[] items = new Item[lastIndex];
            for(int i = 0; i < lastIndex; i++)
            {
                items[i] = Inventory[i];
            }
            Inventory = items;
        }
        else
        {
            Item[] inven = Inventory;
            inven[index] = new Item()
            {
                id = 0,
                uniqueData = new EtcUniqueData()
            };
            Inventory = inven;
        }
    }

    public Item FindItemInfo(int id)
    {
        foreach(Item item in Inventory)
        {
            if(item.id == id)
            {
                return item;
            }
        }

        Item nullItem = new Item();
        if (GameManager.ItemTable[id].ItemType == ItemType.Etc)
        {
            nullItem.uniqueData = new EtcUniqueData() { count = 0 };
        }
        
        return nullItem;
    }

    public int FindEtcItemIndex(int id)
    {
        if (GameManager.ItemTable[id].ItemType != ItemType.Etc)
        {
            return -1;
        }
        for(int i =0;i<Inventory.Length;i++)
        {
            if (Inventory[i].id == id)
            {
                return i;
            }
        }
        return -1;
    }
}
