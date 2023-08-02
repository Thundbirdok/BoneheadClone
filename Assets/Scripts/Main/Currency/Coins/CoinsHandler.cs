using UnityEngine;

namespace Main.Currency.Coins
{
    using System;

    [CreateAssetMenu(fileName = "CoinsHandler", menuName = "Currency/CoinsHandler")]
    public class CoinsHandler : ScriptableObject
    {
        public event Action OnAmountChanged;
        
        [field: NonSerialized]
        public int Amount { get; private set; }

        public void Add(int value)
        {
            Amount += value;
            
            OnAmountChanged?.Invoke();
        }

        public bool TrySpend(int value)
        {
            if (Amount < value)
            {
                return false;
            }

            Amount -= value;
            
            OnAmountChanged?.Invoke();
            
            return true;
        }
    }
}
