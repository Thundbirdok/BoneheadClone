namespace Main.Equipment
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "EquipmentTypesHandler", menuName = "Equipment/EquipmentTypesHandler")]
    public class EquipmentTypesHandler : ScriptableObject, IEnumerable<EquipmentEditor>
    {
        public int Count => equipments.Count;
        
        [SerializeField]
        private List<EquipmentEditor> equipments;

        IEnumerator<EquipmentEditor> IEnumerable<EquipmentEditor>.GetEnumerator()
        {
            return equipments.GetEnumerator();
        }

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
