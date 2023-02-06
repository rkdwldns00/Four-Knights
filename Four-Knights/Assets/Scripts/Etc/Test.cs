using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    ItemStatManager itemStatManager;
    int index;

    void Start()
    {
        itemStatManager = GetComponent<ItemStatManager>();
        index = itemStatManager.AddItem(new Item { id = 2 });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            itemStatManager.equipAccessories(index, 0);
        }
        Debug.Log(itemStatManager.IsSetSkillActive(SetSkillType.Test, 1));
    }
}
