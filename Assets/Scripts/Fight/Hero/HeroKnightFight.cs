using UnityEngine;

namespace Fight.Hero
{
    public class HeroKnightFight : MonoBehaviour 
    {
        [SerializeField]
        private bool noBlood;

        private const float ROLL_DURATION = 0.5714f;

        private Animator            _animator;

        private bool                _rolling;
        private int                 _currentAttack;
        private float               _timeSinceAttack;
        private float               _delayToIdle;
        private float               _rollCurrentTime;
    
        private void Start ()
        {
            _animator = GetComponent<Animator>();
        }
    
        private void Update ()
        {
            // Increase timer that controls attack combo
            _timeSinceAttack += Time.deltaTime;

            // Increase timer that checks roll duration
            if(_rolling)
                _rollCurrentTime += Time.deltaTime;

            // Disable rolling if timer extends duration
            if(_rollCurrentTime > ROLL_DURATION)
                _rolling = false;

            // -- Handle input and movement --
            float inputX = Input.GetAxis("Horizontal");

            //Death
            if (Input.GetKeyDown("e") && !_rolling)
            {
                _animator.SetBool("noBlood", noBlood);
                _animator.SetTrigger("Death");
            }
            
            //Hurt
            else if (Input.GetKeyDown("q") && !_rolling)
                _animator.SetTrigger("Hurt");

            //Attack
            else if(Input.GetMouseButtonDown(0) && _timeSinceAttack > 0.25f && !_rolling)
            {
                _currentAttack++;

                // Loop back to one after third attack
                if (_currentAttack > 3)
                    _currentAttack = 1;

                // Reset Attack combo if time since last attack is too large
                if (_timeSinceAttack > 1.0f)
                    _currentAttack = 1;

                // Call one of three attack animations "Attack1", "Attack2", "Attack3"
                _animator.SetTrigger("Attack" + _currentAttack);

                // Reset timer
                _timeSinceAttack = 0.0f;
            }

            // Block
            else if (Input.GetMouseButtonDown(1) && !_rolling)
            {
                _animator.SetTrigger("Block");
                _animator.SetBool("IdleBlock", true);
            }

            else if (Input.GetMouseButtonUp(1))
                _animator.SetBool("IdleBlock", false);

            // Roll
            else if (Input.GetKeyDown("left shift") && !_rolling)
            {
                _rolling = true;
                _animator.SetTrigger("Roll");
            }

            //Run
            else if (Mathf.Abs(inputX) > Mathf.Epsilon)
            {
                // Reset timer
                _delayToIdle = 0.05f;
                _animator.SetInteger("AnimState", 1);
            }

            //Idle
            else
            {
                // Prevents flickering transitions to idle
                _delayToIdle -= Time.deltaTime;
                if(_delayToIdle < 0)
                    _animator.SetInteger("AnimState", 0);
            }
        }
    }
}
