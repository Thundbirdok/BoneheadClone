namespace Main.Hero.Ui
{
    using System.Linq;
    using UnityEngine;
    using Common.Hero;
    using Main.Equipment;

    public class HeroParametersUi : MonoBehaviour
    {
        [SerializeField]
        private HeroParametersHandler handler;

        [SerializeField]
        private EquipmentTypesHandler equipmentTypesHandler;
        
        [SerializeField]
        private HeroParameterUi[] parameters;

        private void OnEnable()
        {
            handler.OnParametersChanged += UpdateParameters;

            UpdateParameters();
        }

        private void OnDisable()
        {
            handler.OnParametersChanged -= UpdateParameters;
        }
        
        private void UpdateParameters()
        {
            foreach (var parameterUi in parameters)
            {
                if (handler.Parameters.TryGetValue(parameterUi.ParameterId, out var parameter))
                {
                    parameterUi.Set(parameter.Name, parameter.Value);
                    
                    continue;
                }

                var parameterExamples = equipmentTypesHandler.SelectMany(x => x.Parameters).ToList();

                if (parameterExamples.Count == 0)
                {
                    parameterUi.gameObject.SetActive(false);

                    continue;
                }

                var parameterExample = parameterExamples.FirstOrDefault(x => x.Id == parameterUi.ParameterId);
                
                if (parameterExample == null)
                {
                    parameterUi.gameObject.SetActive(false);

                    continue;
                }
                
                parameterUi.Set(parameterExample.Name, 0);
            }
        }
    }
}
