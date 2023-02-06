using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Usable Item Data",menuName ="Scriptable Object/Usable Item Data",order =int.MinValue)]
public class UsableItemStaticData : ItemStaticData
{
    [SerializeField] BuffWithTime buff;
    public BuffWithTime Buff
    {
        get { return buff; }
    }
}
