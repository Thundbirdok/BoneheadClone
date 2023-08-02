namespace Common.Hero
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Main.Equipment;
    using Main.Inventory;

    [CreateAssetMenu(fileName = "HeroParametersHandler", menuName = "Hero/ParametersHandler", order = 0)]
    public class HeroParametersHandler : ScriptableObject
    {
        public event Action OnParametersChanged;
        
        public Dictionary<string, HeroParameter> Parameters { get; private set; } =
            new Dictionary<string, HeroParameter>();

        private Inventory _inventory;
        
        public void SetInventory(Inventory inventory)
        {
            _inventory = inventory;
            
            _inventory.OnAddEquipment += CalculateParameters;
        }

        public void UnsetInventory()
        {
            _inventory.OnAddEquipment -= CalculateParameters;
        }

        private void CalculateParameters(Equipment _)
        {
            Parameters.Clear();

            SumAllParameters();
            
            OnParametersChanged?.Invoke();
        }

        private void SumAllParameters()
        {
            foreach (var equipment in _inventory)
            {
                foreach (var parameter in equipment.Parameters)
                {
                    if (Parameters.TryGetValue(parameter.Id, out var containedParameter))
                    {
                        containedParameter.Value += parameter.Value;

                        continue;
                    }

                    Parameters.Add
                    (
                        parameter.Id,
                        new HeroParameter()
                        {
                            Id = parameter.Id,
                            Name = parameter.Name,
                            Value = parameter.Value
                        }
                    );
                }
            }
        }

        public class HeroParameter
        {
            public string Id;
            public string Name;
            public int Value;
        }
    }
}
