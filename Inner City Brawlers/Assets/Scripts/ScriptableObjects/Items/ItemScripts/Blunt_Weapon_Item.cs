using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Blunt Wepaon", menuName = "Item System/Items/Weapons/Blunt Weapon")]
public class Blunt_Weapon_Item : itemObject
{
    public int weaponDamage;
    public void Awake()
    {
        type = ItemType.Blunt_Weapon;
    }
}
