using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugItemAscender : MonoBehaviour
{
    public InputField inputIndex;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryAscend()
    {
        int index = 0;
        if(int.TryParse(inputIndex.text,out index)){
            FindObjectOfType<ItemStatManager>().UpgradeAscend(index);
        }
    }
}
