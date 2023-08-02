namespace Main.Mine.Ui
{
    using System.Linq;
    using Main.Equipment;
    using Main.Inventory;
    using UnityEngine;
    
    public class MineUi : MonoBehaviour
    {
        [SerializeField]
        private MineEventHandler mineEventHandler;

        // [SerializeField]
        // private CoinsView coinsView;

        [SerializeField]
        private DroppedCoinsAnimator droppedCoinsAnimator;
        
        [SerializeField]
        private Inventory inventory;
        
        [SerializeField]
        private MinedEquipmentUi minedEquipmentUi;
        
        private void OnEnable()
        {
            mineEventHandler.OnMined += ShowMinedEquipment;
            minedEquipmentUi.OnDrop += SetAndDropEquipment;

            droppedCoinsAnimator.Initialize();
            
            minedEquipmentUi.Disable();
        }

        private void OnDisable()
        {
            mineEventHandler.OnMined -= ShowMinedEquipment;
            minedEquipmentUi.OnDrop -= SetAndDropEquipment;
        }

        private void ShowMinedEquipment(Equipment equipment)
        {
            minedEquipmentUi.Show(equipment);
        }

        private void SetAndDropEquipment(Equipment set, Equipment drop)
        {
            inventory.Add(set);

            if (drop == null)
            {
                return;
            }
            
            var value = drop.Parameters.Sum(parameter => parameter.Value);

            droppedCoinsAnimator.Drop(value);
        }
    }
}
