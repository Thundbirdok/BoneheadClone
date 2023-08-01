namespace Main.Equipment
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "EquipmentIconsHandler", menuName = "Equipment/EquipmentIconsHandler")]
    public class EquipmentIconsHandler : ScriptableObject
    {
        [SerializeField]
        private List<TypeSubTypes> equipmentIcons;

        public bool TryGetIcon(string typeId, string subTypeId, out Sprite sprite)
        {
            var typeSubType = equipmentIcons
                .FirstOrDefault(x => x.TypeId == typeId);
            
            if (typeSubType == null)
            {
                sprite = null;
                
                return false;
            }

            var subType = typeSubType.SubTypes.FirstOrDefault(x => x.SubTypeId == subTypeId);
            
            if (subType == null)
            {
                sprite = typeSubType.SubTypes[0].Sprite;
                
                return true;
            }
            
            sprite = subType.Sprite;
            
            return true;
        }

        [Serializable]
        public class TypeSubTypes
        {
            [field: SerializeField]
            public string TypeId { get; private set; }

            [field: SerializeField]
            public List<SubTypeSprite> SubTypes { get; private set; }
        }
        
        [Serializable]
        public class SubTypeSprite
        {
            [field: SerializeField]
            public string SubTypeId { get; private set; }

            [field: SerializeField]
            public Sprite Sprite { get; private set; }
        }
    }
}
