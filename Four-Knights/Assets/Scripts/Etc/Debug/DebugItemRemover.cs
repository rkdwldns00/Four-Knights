using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugItemRemover : MonoBehaviour
{
    public InputField inputIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryRemove()
    {
        int index = 0;
        if (int.TryParse(inputIndex.text, out index))
        {
            FindObjectOfType<ItemStatManager>().DeleteItem(index);
        }
    }
}
