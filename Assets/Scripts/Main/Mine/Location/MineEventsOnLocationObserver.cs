namespace Main.Mine.Location
{
    using Main.Hero;
    using UnityEngine;

    public class MineEventsOnLocationObserver : MonoBehaviour
    {
        [SerializeField]
        private MineEventHandler mineEventHandler;

        [SerializeField]
        private HeroKnightMine heroKnightMine;

        private void OnEnable() => heroKnightMine.OnMine += Mine;

        private void OnDisable() => heroKnightMine.OnMine -= Mine;

        private void Mine() => mineEventHandler.InvokeMine();
    }
}
