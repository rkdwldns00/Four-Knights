using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop Data", menuName = "Scriptable Object/Shop Data", order = int.MinValue)]
public class ShopData : ScriptableObject
{
    [SerializeField] ShopProductData[] products;
    public ShopProductData[] Products
    {
        get { return products; }
    }
}
