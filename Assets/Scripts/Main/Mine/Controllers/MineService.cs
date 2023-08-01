namespace Main.Mine.Controllers
{
    using System;
    using System.Linq;
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
            var equipment = equipmentTypesHandler[equipmentIndex].GetEquipment();
            
            _previousMinedTypeId = equipment.TypeId;
            _previousMinedCount++;

            RollStats(equipment, equipmentEditor);
            
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

            var equipmentTypeId = equipment.TypeId;

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

        private static void RollStats(Equipment equipment, EquipmentEditor equipmentEditor)
        {
            foreach (var parameter in equipment.Parameters)
            {
                var parameterEditor = equipmentEditor.Parameters
                    .FirstOrDefault(x => x.Id == parameter.Id);

                if (parameterEditor == null)
                {
                    continue; 
                }

                parameter.Value = UnityEngine.Random.Range
                (
                    parameterEditor.MinValueInclusive,
                    parameterEditor.MaxValueInclusive + 1
                );
            }
        }
    }
}
