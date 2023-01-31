using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    int AddItem(Item item,int count)
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
                    if (((EtcUniqueData)Inventory[i].uniqueData).count + count != 0)
                    {
                        EtcUniqueData d = (EtcUniqueData)Inventory[i].uniqueData;
                        d.count += count;
                        Debug.Log("etc count 더하기");
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
        switch (GameManager.ItemTable[items[index].id].ItemType)
        {
            case ItemType.Etc:
                items[index].uniqueData = new EtcUniqueData() { count = 1 };
                break;
            case ItemType.Weapon:
                items[index].uniqueData = new WeaponUniqueData() { enforce = 1 };
                break;
            case ItemType.Accessories:
                items[index].uniqueData = new AccessoriesUniqueData() { enforce = 1 };
                break;
        }
        return items;
    }

    public int AddAccessories(Item item,AccessoriesUniqueData uniqueData)
    {
        if (GameManager.ItemTable[item.id].ItemType != ItemType.Accessories)
        {
            Debug.LogError("장신구아이템이 아닌 아이템에 AccessoriesUniqueData 속성이 추가되었습니다.");
        }
        int index = AddItem(item, 1);
        Item[] inven = Inventory;
        inven[index].uniqueData = uniqueData;
        Inventory = inven;
        return index;
    }

    public int AddWeapon(Item item)
    {
        if (GameManager.ItemTable[item.id].ItemType != ItemType.Etc)
        {
            Debug.LogError("무기아이템이 아닌 아이템이 무기추가 함수를 사용하였습니다.");
        }
        return AddItem(item, 1);
    }

    public int AddEtcItem(Item item,int count)
    {
        if (GameManager.ItemTable[item.id].ItemType != ItemType.Etc)
        {
            Debug.LogError("기타아이템이 아닌 아이템에 count 속성이 요청되었습니다.");
        }
        return AddItem(item, count);
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
        return new Item();
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
