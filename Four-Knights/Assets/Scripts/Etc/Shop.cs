using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] ShopData shopData;

    private void Start()
    {
        if (shopData == null)
        {
            Debug.LogError("상점데이터가 없습니다.");
        }
        foreach (ShopProductData data in shopData.Products)
        {
            if (GameManager.ItemTable[data.costId].ItemType != ItemType.Etc)
            {
                Debug.LogError("상점의 요구아에템에는 기타아이템만 적용할수있습니다.");
            }
        }
    }

    public bool CheckCanTransaction(ItemStatManager eventPlayer, int index, int count)
    {
        if (eventPlayer.FindEtcItemIndex(index) == -1)
        {
            return false;
        }
        return ((EtcUniqueData)eventPlayer.FindItemInfo(shopData.Products[index].costId).uniqueData).count * count >= shopData.Products[index].costCount * count;
    }

    public void RequestTransaction(ItemStatManager eventPlayer, int index, int count)
    {
        int have;
        if (eventPlayer.FindItemInfo(shopData.Products[index].costId).id == 0)
        {
            have = 0;
        }
        else
        {
            have = ((EtcUniqueData)eventPlayer.FindItemInfo(shopData.Products[index].costId).uniqueData).count;
        }

        int cost = shopData.Products[index].costCount * count;
        if (have < cost)
        {
            return;
        }

        eventPlayer.AddItem(new Item { id = shopData.Products[index].costId, uniqueData = new EtcUniqueData { count = -cost } });
        eventPlayer.AddItem(new Item { id = shopData.Products[index].productId, uniqueData = new EtcUniqueData { count = shopData.Products[index].productCount * count } });
    }
}
