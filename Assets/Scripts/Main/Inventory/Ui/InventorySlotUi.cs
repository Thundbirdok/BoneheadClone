namespace Main.Inventory.Ui
{
    using System;
    using Main.Equipment;
    using UnityEngine;
    using UnityEngine.UI;

    public class InventorySlotUi : MonoBehaviour
    {
        [SerializeField]
        private Image icon;

        [SerializeField]
        private string id;
        
        public void Set(Equipment equipment)
        {
            if (IsSameId(equipment) == false)
            {
                return;
            }
        }

        public bool IsSameId(Equipment equipment)
        {
            return equipment.Id.Equals(id, StringComparison.InvariantCulture);
        }
    }
}
