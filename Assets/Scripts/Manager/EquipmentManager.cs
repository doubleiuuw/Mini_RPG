using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singletion

    public static EquipmentManager instance;

    void Awake ()
    {
        instance = this;
    }

    #endregion

    public GameObject targetObject;
    Equipment[] currentEquipment;
    GameObject[] currentMeshes;
    

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    Inventory inventory;

    void Start()
    {
        inventory = Inventory.instance;
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        currentMeshes = new GameObject[numSlots];

    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = null;

        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        currentEquipment[slotIndex] = newItem;

        GameObject newMesh = Instantiate<GameObject>(newItem.mesh);
        newMesh.transform.parent = targetObject.transform;

        newMesh = targetObject;
        currentMeshes[slotIndex] = newMesh;
    }

    public void Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
        }
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.U))
            UnequipAll();    
    }
}
