namespace Main.Currency.Coins.Ui
{
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class CoinsHandlerUi : MonoBehaviour
    {
        [SerializeField]
        private CoinsHandler handler;

        [field: SerializeField]
        public Image CoinIcon { get; private set; }

        [SerializeField]
        private TextMeshProUGUI amountText;
        
        private int _amount;
        
        private bool _isTextUpdateBlocked;
        
        private void OnEnable()
        {
            handler.OnAmountChanged += OnValueChanged;

            _amount = handler.Amount;
            
            SetText();
        }

        private void OnDisable()
        {
            handler.OnAmountChanged -= OnValueChanged;
        }

        public void BlockTextUpdate()
        {
            _isTextUpdateBlocked = true;
        }

        public void UnblockTextUpdate()
        {
            _isTextUpdateBlocked = false;
            
            SetText();

            if (_amount != handler.Amount)
            {
                Debug.LogError("Ui shows wrong value. Check text update block logic. " 
                               + _amount
                               + " " 
                               + handler.Amount);
            }
        }

        public void Add(int value)
        {
            _amount += value;

            SetText();
        }

        private void OnValueChanged()
        {
            if (_isTextUpdateBlocked)
            {
                return;
            }
            
            _amount = handler.Amount;
            
            SetText();
        }
        
        private void SetText()
        {
            amountText.text = handler.Amount.ToString();
        }
    }
}
