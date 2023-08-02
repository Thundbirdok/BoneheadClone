namespace Main.Hero
{
    using UnityEngine;
    using Common.Hero;
    using Main.Inventory;

    public class HeroParametersHandlerSetInventoryService : MonoBehaviour
    {
        [SerializeField]
        private HeroParametersHandler heroParameters;

        [SerializeField]
        private Inventory inventory;

        private void Awake()
        {
            heroParameters.SetInventory(inventory);
        }

        private void OnDestroy()
        {
            heroParameters.UnsetInventory();
        }
    }
}
