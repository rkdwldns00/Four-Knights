using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] ShopData shopData;

    private void Start()
    {
        if(shopData == null)
        {
            Debug.LogError("���������Ͱ� �����ϴ�.");
        }
        foreach (ShopProductData data in shopData.Products)
        {
            if (GameManager.ItemTable[data.costId].ItemType != ItemType.Etc)
            {
                Debug.LogError("������ �䱸�ƿ��ۿ��� ��Ÿ�����۸� �����Ҽ��ֽ��ϴ�.");
            }
        }
    }

    public bool CheckCanTransaction(ItemStatManager eventPlayer, int index, int count)
    {
        if(eventPlayer.FindEtcItemIndex(index) == -1)
        {
            return false;
        }
        return ((EtcUniqueData)eventPlayer.FindItemInfo(shopData.Products[index].costId).uniqueData).count * count >= shopData.Products[index].costCount * count;
    }

    public void RequestTransaction(ItemStatManager eventPlayer, int index, int count)
    {
        if (count > shopData.Products[index].chance)
        {
            count = shopData.Products[index].chance;
        }

        int have = ((EtcUniqueData)eventPlayer.FindItemInfo(shopData.Products[index].costId).uniqueData).count;
        int cost = shopData.Products[index].costCount * count;
        if (have < cost)
        {
            return;
        }

        eventPlayer.AddItem(new Item { id = shopData.Products[index].costId, uniqueData = new EtcUniqueData { count = -cost } });
        eventPlayer.AddItem(new Item { id = shopData.Products[index].productId, uniqueData = new EtcUniqueData { count = shopData.Products[index].productCount * count } });
    }
}
