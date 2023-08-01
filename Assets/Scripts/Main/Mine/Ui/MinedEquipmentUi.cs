namespace Main.Mine.Ui
{
    using System;
    using Main.Equipment;
    using Main.Equipment.Ui;
    using Main.Inventory;
    using UnityEngine;
    using UnityEngine.UI;

    public class MinedEquipmentUi : MonoBehaviour
    {
        public event Action<Equipment, Equipment> OnDrop;

        [SerializeField]
        private Inventory inventory;
        
        [SerializeField]
        private EquipmentWideSlotUi setSlot;

        [SerializeField]
        private EquipmentWideSlotUi dropSlot;

        [SerializeField]
        private EquipmentIconsHandler iconsHandler;
        
        [SerializeField]
        private Button equipButton;
        
        [SerializeField]
        private Button dropButton;

        private Equipment _set;
        private Equipment _drop;
        
        private void OnEnable()
        {
            dropButton.onClick.AddListener(Drop);
            equipButton.onClick.AddListener(Equip);
        }

        private void OnDisable()
        {
            dropButton.onClick.RemoveListener(Drop);
            equipButton.onClick.RemoveListener(Equip);
        }

        public void Show(Equipment equipment)
        {
            inventory.TryGet(equipment.Type.Id, out var fromInventory);
            iconsHandler.TryGetIcon(equipment.Type.Id, equipment.SubType.Id, out var setIcon);
            setSlot.Set(fromInventory, setIcon);

            iconsHandler.TryGetIcon(equipment.Type.Id, equipment.SubType.Id, out var dropIcon);
            dropSlot.Set(equipment, dropIcon);
            
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            Disable();
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
        
        private void Drop()
        {
            Hide();
            
            OnDrop?.Invoke(_set, _drop);
        }

        private void Equip()
        {
            
        }
    }
}
