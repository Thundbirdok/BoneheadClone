namespace Main.Mine.Ui
{
    using System;
    using System.Linq;
    using DG.Tweening;
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
        private CanvasGroup background;

        [SerializeField]
        private RectTransform popupWindow;
        
        [SerializeField]
        private float showPopupDuration = 0.25f;
        
        [SerializeField]
        private EquipmentWideSlotUi setSlot;

        [SerializeField]
        private EquipmentWideSlotUi dropSlot;

        [SerializeField]
        private RectTransform setSlotPosition; 
        
        [SerializeField]
        private RectTransform dropSlotPosition;
        
        [SerializeField]
        private float swapDuration = 0.25f;
        
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

        private bool _isInitialized;
        private bool _isSwapped;

        private Vector3 _setSlotPosition;
        private Vector3 _dropSlotPosition;
        
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
            Initialize();

            SetInitialState();

            SetSetSlot(equipment);
            SetDropSlot(equipment);

            SetParametersDelta();
            
            gameObject.SetActive(true);

            var sequence = DOTween.Sequence();

            sequence
                .Append(background.DOFade(1, showPopupDuration))
                .Join(popupWindow.DOScale(1, showPopupDuration));

            sequence.Play();
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        private void Initialize()
        {
            if (_isInitialized)
            {
                return;
            }

            _isInitialized = true;

            _setSlotPosition = setSlotPosition.localPosition;
            _dropSlotPosition = dropSlotPosition.localPosition;
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

        private async void Hide()
        {
            var sequence = DOTween.Sequence();

            sequence
                .Append(background.DOFade(0, showPopupDuration))
                .Join(popupWindow.DOScale(Vector3.zero, showPopupDuration));

            await sequence.Play().AsyncWaitForCompletion();
            
            Disable();
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

            var setTargetPosition = _setSlotPosition;
            var dropTargetPosition = _dropSlotPosition;

            if (_isSwapped == false)
            {
                setTargetPosition = _dropSlotPosition;
                dropTargetPosition = _setSlotPosition;
            }

            var sequence = DOTween.Sequence();

            sequence
                .Append(dropSlot.transform.DOLocalMove(dropTargetPosition, swapDuration))
                .Join(setSlot.transform.DOLocalMove(setTargetPosition, swapDuration));

            sequence.Play();
            
            _isSwapped = !_isSwapped;
        }

        private void SetInitialState()
        {
            _isSwapped = false;

            setSlot.transform.localPosition = _setSlotPosition;
            dropSlot.transform.localPosition = _dropSlotPosition;

            background.alpha = 0;
            popupWindow.localScale = Vector3.zero;
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
