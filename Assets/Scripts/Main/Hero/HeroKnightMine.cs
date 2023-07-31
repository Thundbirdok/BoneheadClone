namespace Main.Hero
{
    using System;
    using System.Threading.Tasks;
    using Main.Mine;
    using UnityEngine;
    using Random = UnityEngine.Random;

    public class HeroKnightMine : MonoBehaviour, IInteractable
    {
        public event Action OnMine;
        
        [SerializeField]
        private Animator animator;
        
        private const float BLOCK_DURATION = 0.15f;

        private float _time;
        
        private static readonly int BlockHash = Animator.StringToHash("Block");
        private static readonly int IdleBlockHash = Animator.StringToHash("IdleBlock");
        
        private static readonly int[] AttacksHash =
        {
            Animator.StringToHash("Attack1"),
            Animator.StringToHash("Attack2"),
            Animator.StringToHash("Attack3")
        };

        private static readonly int RollHash = Animator.StringToHash("Roll");
        private static readonly int AnimStateHash = Animator.StringToHash("AnimState");

        private void Start()
        {
            Idle();
        }

        public void Interact() => Mine();
        
        private void Mine()
        {
            Roll();
            
            OnMine?.Invoke();
        }

        private void Roll()
        {
            animator.SetTrigger(RollHash);
        }

        private void Attack()
        {
            var attackIndex = Random.Range(0, AttacksHash.Length);
            
            animator.SetTrigger(AttacksHash[attackIndex]);
        }

        private async void Block()
        {
            animator.SetTrigger(BlockHash);
            animator.SetBool(IdleBlockHash, true);
            
            while (_time < BLOCK_DURATION)
            {
                await Task.Yield();
                
                _time += Time.deltaTime;
            }
            
            animator.SetBool(IdleBlockHash, false);
        }

        private void Idle() => animator.SetInteger(AnimStateHash, 0);
    }
}
