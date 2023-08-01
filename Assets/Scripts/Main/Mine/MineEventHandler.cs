namespace Main.Mine
{
    using System;
    using Main.Equipment;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "MineEventHandler", menuName = "Mine/MineEventHandler")]
    public class MineEventHandler : ScriptableObject
    {
        public event Action OnMine;
        public event Action<Equipment> OnMined;

        public void InvokeMine() => OnMine?.Invoke();
        public void InvokeMined(Equipment equipment) => OnMined?.Invoke(equipment);
    }
}
