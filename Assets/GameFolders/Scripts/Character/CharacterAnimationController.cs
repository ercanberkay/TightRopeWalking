using UnityEngine;

namespace Character
{
    public class CharacterAnimationController : MonoBehaviour
    {
        private Animator _animator;


        private static readonly int LeftSide = Animator.StringToHash("LeftSide");
        private static readonly int RightSide = Animator.StringToHash("RightSide");
        private static readonly int Move = Animator.StringToHash("Move");
        private static readonly int Idle = Animator.StringToHash("Idle");

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        public void TriggerMove() => _animator.SetTrigger(Move);
        public void TriggerLeftSide() => _animator.SetTrigger(LeftSide);
        public void TriggerRightSide() => _animator.SetTrigger(RightSide);
        public void TriggerIdle() => _animator.SetTrigger(Idle);
        public void TriggerRagdoll() => _animator.enabled = false;
    }
}

