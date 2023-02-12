using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    Shop shop;
    ItemStatManager item;

    void Start()
    {
        shop = FindObjectOfType<Shop>();
        item = GetComponent<ItemStatManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(shop.CheckCanTransaction(item, 0, 1));
        if (Input.GetKeyDown(KeyCode.G))
        {
            item.AddItem(1);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            item.AddItem(new Item { id = 1, uniqueData = new EtcUniqueData { count = -1 } });
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            FindObjectOfType<Shop>().RequestTransaction(item, 0, 1);
        }
    }
}
