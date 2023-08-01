namespace Main.Equipment
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "EquipmentTypesHandler", menuName = "Equipment/EquipmentTypesHandler")]
    public class EquipmentTypesHandler : ScriptableObject, IEnumerable
    {
        public int Count => equipments.Count;
        
        [SerializeField]
        private List<EquipmentEditor> equipments;
        
        public IEnumerator GetEnumerator()
        {
            return equipments.GetEnumerator();
        }

        public EquipmentEditor this[int index]
        {
            get => equipments[index];
        }
    }
}
