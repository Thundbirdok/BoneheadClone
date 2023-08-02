namespace Main.Mine.Ui
{
    using System;
    using System.Linq;
    using Main.Equipment;
    using Main.Equipment.Ui;
    using Main.Inventory;
    using TMPro;
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

        [SerializeField]
        private Image arrow;

        [SerializeField]
        private Color positiveArrowColor = Color.green;
        
        [SerializeField]
        private Color neutralArrowColor = Color.white;
        
        [SerializeField]
        private Color negativeArrowColor = Color.red;
        
        [SerializeField]
        private TextMeshProUGUI parametersDeltaText;
        
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

            SetParametersDelta();
            
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

            SetParametersDelta();
            
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

        private void SetParametersDelta()
        {
            var dropValue =  _drop == null ? 0 : _drop.Parameters.Sum(parameter => parameter.Value);
            var setValue = _set == null ? 0 : _set.Parameters.Sum(parameter => parameter.Value);

            var parametersDelta = dropValue - setValue;

            var parametersDeltaTextValue = Mathf.Abs(parametersDelta).ToString();

            switch (parametersDelta)
            {
                case > 0:
                    parametersDeltaTextValue = "+" + parametersDeltaTextValue;

                    arrow.transform.rotation = Quaternion.identity;
                    arrow.color = positiveArrowColor;

                    break;

                case < 0:
                    parametersDeltaTextValue = "-" + parametersDeltaTextValue;
                
                    arrow.transform.rotation = Quaternion.Euler(0, 0, 180);
                    arrow.color = negativeArrowColor;

                    break;

                default:
                    arrow.transform.rotation = Quaternion.Euler(0, 0, -90);
                    arrow.color = neutralArrowColor;

                    break;
            }
            
            parametersDeltaText.text = parametersDeltaTextValue;
        }
    }
}
