namespace Main.Hero.Ui
{
    using UnityEngine;
    using TMPro;

    public class HeroParameterUi : MonoBehaviour
    {
        [field: SerializeField]
        public string ParameterId { get; private set; }
        
        [SerializeField]
        private TextMeshProUGUI parameterNameText;
        
        [SerializeField]
        private TextMeshProUGUI valueText;

        public void Set(string parameterName, int value)
        {
            parameterNameText.text = parameterName;
            valueText.text = value.ToString();
        }
    }
}
