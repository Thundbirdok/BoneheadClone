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

        private bool _isSwapped;
        
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
            SetInitialState();

            SetSetSlot(equipment);
            SetDropSlot(equipment);

            gameObject.SetActive(true);
        }

        private void SetSetSlot(Equipment equipment)
        {
            Sprite setIcon = null;

            if (inventory.TryGet(equipment.Type.Id, out _set))
            {
                iconsHandler.TryGetIcon(_set.Type.Id, _set.SubType.Id, out setIcon);
            }

            setSlot.Set(_set, setIcon);
        }

        private void SetDropSlot(Equipment equipment)
        {
            _drop = equipment;

            iconsHandler.TryGetIcon(_drop.Type.Id, _drop.SubType.Id, out var dropIcon);
            dropSlot.Set(_drop, dropIcon);
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
            (_set, _drop) = (_drop, _set);

            if (_isSwapped)
            {
                setSlot.transform.SetAsFirstSibling();
            }
            else
            {
                setSlot.transform.SetAsLastSibling();
            }

            _isSwapped = !_isSwapped;
        }

        private void SetInitialState()
        {
            _isSwapped = false;
            setSlot.transform.SetAsFirstSibling();
            setSlot.transform.SetAsFirstSibling();
        }
    }
}
