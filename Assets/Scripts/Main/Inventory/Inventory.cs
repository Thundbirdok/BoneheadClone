namespace Main.Inventory
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Main.Equipment;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/Inventory")]
    public class Inventory : ScriptableObject, IEnumerable<Equipment>
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

            var isContains = _equipments.ContainsKey(equipment.Type.Id);

            AddToDictionary(equipment, isContains);
            
            OnAddEquipment?.Invoke(equipment);
        }

        public bool TryGet(string typeId, out Equipment equipment)
        {
            return _equipments.TryGetValue(typeId, out equipment);
        }
        
        private void AddToDictionary(Equipment equipment, bool isContains)
        {
            if (isContains)
            {
                _equipments[equipment.Type.Id] = equipment;
                
                return;
            }

            _equipments.Add(equipment.Type.Id, equipment);
        }

        public IEnumerator<Equipment> GetEnumerator()
        {
            return _equipments.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
