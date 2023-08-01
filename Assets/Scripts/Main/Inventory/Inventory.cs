namespace Main.Inventory
{
    using System;
    using System.Collections.Generic;
    using Main.Equipment;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/Inventory")]
    public class Inventory : ScriptableObject
    {
        public event Action<Equipment> OnAddEquipment;
        
        private readonly Dictionary<string, Equipment> _equipments 
            = new Dictionary<string, Equipment>();

        public void Add(Equipment equipment)
        {
            if (equipment == null)
            {
                return;
            }

            var isContains = _equipments.ContainsKey(equipment.Id);

            AddToDictionary(equipment, isContains);
            
            OnAddEquipment?.Invoke(equipment);
        }

        private void AddToDictionary(Equipment equipment, bool isContains)
        {
            if (isContains)
            {
                _equipments[equipment.Id] = equipment;
                
                return;
            }

            _equipments.Add(equipment.Id, equipment);
        }
    }
}
