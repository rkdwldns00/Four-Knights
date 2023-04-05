using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugInventoryMonitoring : MonoBehaviour
{
    Text ui;
    ItemStatManager player;

    void Start()
    {
        ui = GetComponent<Text>();
        player = FindObjectOfType<ItemStatManager>();
    }

    // Update is called once per frame
    void Update()
    {
        string[] texts = new string[player.Inventory.Length];
        for (int j = 0; j < player.Inventory.Length; j++)
        {
            texts[j] = "아이템 : " + GameManager.ItemTable[player.Inventory[j].id].ItemName;
            switch (GameManager.ItemTable[player.Inventory[j].id].ItemType)
            {
                case ItemType.Etc:
                case ItemType.Usable:
                    texts[j] = texts[j] + ", 개수 : " + ((EtcUniqueData)player.Inventory[j].uniqueData).count;
                    break;
                case ItemType.Accessories:
                    texts[j] = texts[j] + ", 강화 : " + ((AccessoriesUniqueData)player.Inventory[j].uniqueData).enforce;
                    texts[j] = texts[j] + ", 경험치 : " + ((AccessoriesUniqueData)player.Inventory[j].uniqueData).exp;
                    texts[j] = texts[j] + ", 스탯1 : (" + ((AccessoriesUniqueData)player.Inventory[j].uniqueData).upgradeStat[0].UpgradeStatType+", "+ ((AccessoriesUniqueData)player.Inventory[j].uniqueData).upgradeStat[0].value;
                    texts[j] = texts[j] + ", 스탯2 : (" + ((AccessoriesUniqueData)player.Inventory[j].uniqueData).upgradeStat[1].UpgradeStatType+", "+ ((AccessoriesUniqueData)player.Inventory[j].uniqueData).upgradeStat[1].value;
                    break;
                case ItemType.Weapon:
                    texts[j] = texts[j] + ", 강화 : " + ((WeaponUniqueData)player.Inventory[j].uniqueData).enforce;
                    break;
            }
        }
        string text = "";
        foreach (string b in texts)
        {
            text = text + b + "\n";
        }
        ui.text = text;
    }
}
