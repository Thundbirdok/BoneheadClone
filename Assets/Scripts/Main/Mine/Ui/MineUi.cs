namespace Main.Mine.Ui
{
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
        private Inventory inventory;
        
        [SerializeField]
        private MinedEquipmentUi minedEquipmentUi;
        
        private void OnEnable()
        {
            mineEventHandler.OnMined += ShowMinedEquipment;
            minedEquipmentUi.OnDrop += SetAndDropEquipment;

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
        }
    }
}
