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
        protected set { inventory = value; }
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
        Debug.Log("아이템 추가 : "+item.id);
        if (GameManager.ItemTable[item.id].ItemType != ItemType.Etc || FindItem(item.id).id == 0)
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
                
                Inventory = items;
                CreateUniqueData(items.Length - 1);
                return Inventory.Length - 1;
            }
            else
            {
                Inventory[nullIndex] = item;
                CreateUniqueData(nullIndex);
                return nullIndex;
            }
        }
        else
        {
            for(int i = 0; i < Inventory.Length; i++)
            {
                if (Inventory[i].id == item.id)
                {
                    EtcUniqueData d = ((EtcUniqueData)(Inventory[i].uniqueData));
                    d.count += count;
                    Inventory[i].uniqueData = d;
                    return i;
                }
            }
        }
        return -1;
    }

    void CreateUniqueData(int index)
    {
        switch (GameManager.ItemTable[Inventory[index].id].ItemType)
        {
            case ItemType.Etc:
                Inventory[index].uniqueData = new EtcUniqueData() { count = 1 };
                break;
            case ItemType.Weapon:
                Inventory[index].uniqueData = new WeaponUniqueData() { enforce = 1 };
                break;
            case ItemType.Accessories:
                Inventory[index].uniqueData = new AccessoriesUniqueData() { enforce = 1 };
                break;
        }
    }

    public int AddAccessories(Item item,AccessoriesUniqueData uniqueData)
    {
        if (GameManager.ItemTable[item.id].ItemType != ItemType.Accessories)
        {
            Debug.LogError("장신구아이템이 아닌 아이템에 AccessoriesUniqueData 속성이 추가되었습니다.");
        }
        int index = AddItem(item, 1);
        Inventory[index].uniqueData = uniqueData;
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
        }
        else
        {
            Inventory[index] = new Item()
            {
                id = 0,
                uniqueData = new EtcUniqueData()
            };
        }
    }

    public Item FindItem(int id)
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
}
