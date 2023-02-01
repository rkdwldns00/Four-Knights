using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct DropTagData
{
    public float dropPercent;
    public KeyValuePair<Item, float>[] items;
}
