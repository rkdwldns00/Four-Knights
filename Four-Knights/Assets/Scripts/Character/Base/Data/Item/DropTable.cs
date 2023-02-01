using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Drop Table",menuName ="Scriptable Object/Drop Table",order = int.MinValue)]
public class DropTable : ScriptableObject
{
    public DropTagData[] dropTags;
}
