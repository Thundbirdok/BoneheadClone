namespace Main.Mine.Controllers
{
    using System;
    using Main.Equipment;
    using UnityEngine;

    public class MineService : MonoBehaviour
    {
        [SerializeField]
        private MineEventHandler mineEventHandler;

        [SerializeField]
        private EquipmentTypesHandler equipmentTypesHandler;

        private string _previousMinedTypeId;
        private int _previousMinedCount;
        
        private void OnEnable()
        {
            mineEventHandler.OnMine += Mine;
        }

        private void OnDisable()
        {
            mineEventHandler.OnMine -= Mine;
        }

        private void Mine()
        {
            if (equipmentTypesHandler.Count == 0)
            {
                Debug.LogError("No equipment types set");
                
                return;
            }
            
            var equipmentIndex = RollEquipment();

            var equipmentEditor = equipmentTypesHandler[equipmentIndex];

            _previousMinedTypeId = equipmentEditor.Type.Id;
            _previousMinedCount++;

            var equipment = GetEquipment(equipmentEditor);
            
            mineEventHandler.InvokeMined(equipment);
        }

        private int RollEquipment()
        {
            int equipmentIndex;

            while (true)
            {
                equipmentIndex = UnityEngine.Random.Range(0, equipmentTypesHandler.Count);

                if (IsNeedReRoll(equipmentIndex) == false)
                {
                    break;
                }

                _previousMinedCount = 0;
            }

            return equipmentIndex;
        }

        private bool IsNeedReRoll(int equipmentIndex)
        {
            var equipment = equipmentTypesHandler[equipmentIndex];

            var equipmentTypeId = equipment.Type.Id;

            var isSameAsPrevious = string.Equals
            (
                equipmentTypeId,
                _previousMinedTypeId,
                StringComparison.InvariantCulture
            );

            if (isSameAsPrevious == false)
            {
                return false;
            }

            return _previousMinedCount >= 3;
        }

        private static Equipment GetEquipment(EquipmentEditor equipmentEditor)
        {
            var parameters = GetEquipmentParameters(equipmentEditor);

            var subType = GetSubTypeId(equipmentEditor);

            return new Equipment
            (
                equipmentEditor.Type,
                subType,
                parameters
            );
        }

        private static SubType GetSubTypeId(EquipmentEditor equipmentEditor)
        {
            var subTypeIdIndex = UnityEngine.Random.Range
            (
                0,
                equipmentEditor.SubTypes.Length
            );

            var subType = equipmentEditor.SubTypes[subTypeIdIndex];

            return subType;
        }

        private static EquipmentParameter[] GetEquipmentParameters(EquipmentEditor equipmentEditor)
        {
            var parameters = new EquipmentParameter[equipmentEditor.Parameters.Length];

            for (var i = 0; i < equipmentEditor.Parameters.Length; i++)
            {
                var parameterEditor = equipmentEditor.Parameters[i];

                var parameterValue = UnityEngine.Random.Range
                (
                    parameterEditor.MinValueInclusive,
                    parameterEditor.MaxValueInclusive + 1
                );

                var parameter = new EquipmentParameter
                    (parameterEditor.Id, parameterEditor.Name, parameterValue);

                parameters[i] = parameter;
            }

            return parameters;
        }
    }
}
