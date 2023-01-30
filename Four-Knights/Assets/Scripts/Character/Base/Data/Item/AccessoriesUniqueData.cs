using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct AccessoriesUniqueData : ItemUniqueData
{
    public int enforce;
    public UpgradeStatWithValue[] upgradeStat;
}
