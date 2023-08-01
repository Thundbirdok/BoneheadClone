namespace Main.Equipment
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [Serializable]
    public class Equipment
    {
        [field: SerializeField]
        public string Id { get; private set; }
        
        [field: SerializeField]
        public string TypeId { get; private set; }

        [field: SerializeField]
        public string Name { get; private set; }

        [field: SerializeField]
        public IReadOnlyList<EquipmentParameter> Parameters { get; private set; }

        public Equipment(string id, string typeId, string name, EquipmentParameter[] parameters)
        {
            Id = id;
            TypeId = typeId;
            Name = name;
            Parameters = parameters;
        }
    }
    
    [Serializable]
    public class EquipmentEditor
    {
        [field: SerializeField]
        public string Id { get; private set; }
        
        [field: SerializeField]
        public string TypeId { get; private set; }

        [field: SerializeField]
        public string Name { get; private set; }

        [field: SerializeField]
        public EquipmentParameterEditor[] Parameters { get; private set; }

        public Equipment GetEquipment()
        {
            var parameters = new EquipmentParameter[Parameters.Length];

            for (var i = 0; i < Parameters.Length; i++)
            {
                parameters[i] = Parameters[i].GetEquipmentParameter();
            }

            return new Equipment(Id, TypeId, Name, parameters);
        }
    }
}
