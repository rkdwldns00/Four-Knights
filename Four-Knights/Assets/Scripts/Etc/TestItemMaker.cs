using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItemMaker : MonoBehaviour
{
    ItemStatManager itemStatManager;

    void Start()
    {
        itemStatManager = GetComponent<ItemStatManager>();
        //itemStatManager.AddEtcItem(new Item() { id = 1 }, 1);
        //itemStatManager.AddEtcItem(new Item() { id = 1 }, -2);
        
        Debug.Log("읽은개수 : "+((EtcUniqueData)(itemStatManager.FindItemInfo(1).uniqueData)).count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
