namespace Main.Inventory.Ui
{
    using System;
    using Main.Equipment;
    using UnityEngine;
    using UnityEngine.UI;

    public class InventorySlotUi : MonoBehaviour
    {
        [SerializeField]
        private Image icon;

        [SerializeField]
        private string typeId;

        private void Start()
        {
            Set(null, null);
        }

        public void Set(Equipment equipment, Sprite iconSprite)
        {
            if (equipment == null)
            {
                icon.gameObject.SetActive(false);   
            }
            
            if (IsSameId(equipment) == false)
            {
                return;
            }

            if (iconSprite == null)
            {
                icon.gameObject.SetActive(false);
                
                return;
            }
            
            icon.sprite = iconSprite;
            icon.gameObject.SetActive(true);
        }

        public bool IsSameId(Equipment equipment)
        {
            if (equipment == null)
            {
                return false;
            }

            return equipment.Type.Id.Equals(typeId, StringComparison.InvariantCulture);
        }
    }
}
