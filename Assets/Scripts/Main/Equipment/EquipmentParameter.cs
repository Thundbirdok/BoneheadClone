namespace Main.Equipment
{
    using System;
    using UnityEngine;
    
    [Serializable]
    public class EquipmentParameter
    {
        [field: SerializeField]
        public string Id { get; private set; }
        
        [field: SerializeField]
        public string Name { get; private set; }
        
        [field: SerializeField]
        public int Value { get; private set; }

        public EquipmentParameter(string id, string name, int value)
        {
            Id = id;
            Name = name;
            Value = value;
        }
    }
    
    [Serializable]
    public class EquipmentParameterEditor
    {
        [field: SerializeField]
        public string Id { get; private set; }
        
        [field: SerializeField]
        public string Name { get; private set; }

        [field: SerializeField]
        public int MinValueInclusive { get; private set; }
        
        [field: SerializeField]
        public int MaxValueInclusive { get; private set; }
    }
}
