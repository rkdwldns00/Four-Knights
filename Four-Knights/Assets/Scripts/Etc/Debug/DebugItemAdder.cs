using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugItemAdder : MonoBehaviour
{

    public InputField inputId;
    public InputField inputCount;

    void Start()
    {
        //FindObjectOfType<ItemStatManager>().AddItem(new Item() { id = 2 });
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddItem()
    {
        int id = 0, count = 0;
        int.TryParse(inputId.text, out id);
        int.TryParse(inputCount.text, out count);
        FindObjectOfType<ItemStatManager>().AddItem(id, count);
    }
}
