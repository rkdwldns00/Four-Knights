using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Data",menuName = "Scriptable Object/Character Data",order = int.MinValue)]
public class CharacterData : ScriptableObject
{
    [SerializeField] string characterName;
    public string CharacterName
    {
        get { return characterName; }
    }

    [SerializeField] CharacterStat characterStat;
    public CharacterStat CharacterStat
    {
        get { return characterStat; }
    }

    [SerializeField] AttackData[] basicAttacks;
    public AttackData[] BasicAttacks
    {
        get { return basicAttacks; }
    }

    [SerializeField] AttackData skill;
    public AttackData Skill
    {
        get { return skill; }
    }

    [SerializeField] AttackData ultimateSkill;
    public AttackData UltimateSkill
    {
        get { return ultimateSkill; }
    }

    [SerializeField] WeaponType weaponType;
    public WeaponType WeaponType
    {
        get { return weaponType; }
    }
}
