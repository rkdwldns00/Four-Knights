using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStatManager : StatManager
{
    public Item[] inventory;

    protected Item weapon;
    public Item Weapon
    {
        set
        {
            if (GameManager.itemTable.Table[value.id].ItemType == ItemType.Weapon)
            {
                if (((WeaponStaticData)GameManager.itemTable.Table[value.id]).WeaponType == characterData.WeaponType)
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
                    if (GameManager.itemTable.Table[item.id].ItemType != ItemType.Accessories)
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
        foreach (UpgradeStatWithValue upgradeStat in ((WeaponStaticData)GameManager.itemTable.Table[weapon.id]).UpgradeStat)
        {
            if (upgradeStat.UpgradeStatType == type)
            {
                value += upgradeStat.value * ((WeaponUniqueData)weapon.uniqueData).enforce;
            }
        }
        foreach (Item item in accessories)
        {

            foreach (UpgradeStatWithValue upgradeStat in ((AccessoriesUniqueData)(item.uniqueData)).upgradeStat)
            {
                value += upgradeStat.value * ((AccessoriesUniqueData)(item.uniqueData)).enforce;
            }
        }
        return value;
    }
}
