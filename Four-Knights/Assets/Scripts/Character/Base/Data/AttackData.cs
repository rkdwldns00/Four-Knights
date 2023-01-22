using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack Data",menuName ="Scriptable Object/Attack Data",order =int.MinValue)]
public class AttackData : ScriptableObject
{
    [SerializeField] GameObject prefab;
    public GameObject Prefab
    {
        get { return prefab; }
    }

    [SerializeField] float coolTime;
    public float CoolTime
    {
        get { return coolTime; }
    }
}
