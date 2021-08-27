using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equitment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;
    public GameObject mesh;

    public int armorModifier;
    public int damageModifier;

    public override void Use()
    {
        base.Use();
        // Equip the item
        EquipmentManager.instance.Equip(this);
        // Remove it from the inventory
        RemoveFromInventory();
    }
}

public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield, Feet }