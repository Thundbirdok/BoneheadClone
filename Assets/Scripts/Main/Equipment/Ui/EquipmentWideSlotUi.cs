namespace Main.Equipment.Ui
{
    using System.Text;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class EquipmentWideSlotUi : MonoBehaviour
    {
        [SerializeField]
        private Image icon;

        [SerializeField]
        private TextMeshProUGUI equipmentName;
        
        [SerializeField]
        private TextMeshProUGUI parameters;

        public void Set(Equipment equipment)
        {
            if (equipment == null)
            {
                icon.gameObject.SetActive(false);
                equipmentName.text = "Empty";
                parameters.gameObject.SetActive(false);

                return;
            }
            
            equipmentName.text = equipment.Name;
            
            parameters.text = GetParametersText(equipment);
        }

        private static string GetParametersText(Equipment equipment)
        {
            var stringBuilder = new StringBuilder();
            
            foreach (var parameter in equipment.Parameters)
            {
                stringBuilder.Append(parameter.Name);
                stringBuilder.Append(": ");
                stringBuilder.Append(parameter.Value);
                stringBuilder.Append("\n");
            }

            return stringBuilder.ToString();
        }
    }
}
