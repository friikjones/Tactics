using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

[Serializable]
public class CharacterHolder
{
    public int ID;
    public string char_name;
    public CharClass type;
    public CharElement element;
    public float stat_HP;
    public float stat_armor;
    public float stat_regen;
    public float stat_mana;
    public float stat_spirit;
    public float stat_will;
    public int move_Range;
    public float basic_dmg;
    public int basic_range;




    public void Clone(CharacterHolder target)
    {
        ID = target.ID;
        char_name = target.char_name;
        type = target.type;
        element = target.element;
        stat_HP = target.stat_HP;
        stat_armor = target.stat_armor;
        stat_regen = target.stat_regen;
        stat_mana = target.stat_mana;
        stat_spirit = target.stat_spirit;
        stat_will = target.stat_will;
        move_Range = target.move_Range;
        basic_dmg = target.basic_dmg;
        basic_range = target.basic_range;
    }
}

public enum CharElement
{
    fire,
    water,
    earth,
    air
}

public enum CharClass
{
    assassin,
    tank,
    support
}