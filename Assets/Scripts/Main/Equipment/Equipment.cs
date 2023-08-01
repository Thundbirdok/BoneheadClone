namespace Main.Equipment
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [Serializable]
    public class Equipment
    {
        [field: SerializeField]
        public Type Type { get; private set; }

        [field: SerializeField]
        public SubType SubType { get; private set; }

        [SerializeField]
        private EquipmentParameter[] parameters; 
        public IReadOnlyList<EquipmentParameter> Parameters => parameters;

        public Equipment
        (
            Type type,
            SubType subType,
            EquipmentParameter[] equipmentParameters
        )
        {
            Type = type;
            SubType = subType;
            parameters = equipmentParameters;
        }
    }
    
    [Serializable]
    public class EquipmentEditor
    {
        [field: SerializeField]
        public Type Type { get; private set; }

        [field: SerializeField]
        public SubType[] SubTypes { get; private set; }

        [field: SerializeField]
        public EquipmentParameterEditor[] Parameters { get; private set; }
    }

    [Serializable]
    public class Type
    {
        [field: SerializeField]
        public string Id { get; private set; }
        
        [field: SerializeField]
        public string Name { get; private set; }

        public Type(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
    
    [Serializable]
    public class SubType
    {
        [field: SerializeField]
        public string Id { get; private set; }
        
        [field: SerializeField]
        public string Name { get; private set; }

        public SubType(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
