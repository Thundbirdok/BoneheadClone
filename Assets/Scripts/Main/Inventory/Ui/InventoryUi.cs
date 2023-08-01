namespace Main.Inventory.Ui
{
    using System.Linq;
    using Main.Equipment;
    using UnityEngine;
    
    public class InventoryUi : MonoBehaviour
    {
        [SerializeField]
        private Inventory inventory;
        
        [SerializeField]
        private InventorySlotUi[] slots;

        [SerializeField]
        private EquipmentIconsHandler iconsHandler;
        
        private void OnEnable()
        {
            inventory.OnAddEquipment += AddToSlot;
        }

        private void OnDisable()
        {
            inventory.OnAddEquipment -= AddToSlot;
        }

        private void AddToSlot(Equipment equipment)
        {
            var slot = slots.FirstOrDefault(slot => slot.IsSameId(equipment));

            if (slot == null)
            {
                return;
            }
            
            iconsHandler.TryGetIcon(equipment.Type.Id, equipment.SubType.Id, out var iconSprite);
            
            slot.Set(equipment, iconSprite);
        }
    }
}
