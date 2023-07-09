using InteractionSystem;
using Character.StateMachine;
using StackSystem;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Character
{
    public class CharacterController : MonoBehaviour,IInteractor
    {
        [SerializeReference, BoxGroup("Idle", false), HorizontalGroup("Idle/Group")] public State IdleState;
        [SerializeReference, BoxGroup("Game", false), HorizontalGroup("Game/Group")] public GameState GameState;
        [SerializeReference, BoxGroup("Finish", false), HorizontalGroup("Finish/Group")] public FinishState FinishState;
        [SerializeReference, BoxGroup("Win", false), HorizontalGroup("Win/Group")] public State WinState;
        [SerializeReference, BoxGroup("Fail", false), HorizontalGroup("Fail/Group")] public State FailState;
        [ShowInInspector,ReadOnly,BoxGroup("States",false)] public State CurrentState { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        public Interactor Interactor { get; private set; }
        public CharacterMovement Movement { get; private set; }
        public CharacterAnimationController Animation { get; private set; }
        
        public StackController StackController { get; private set; }


        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Movement = GetComponent<CharacterMovement>();
            Animation = GetComponent<CharacterAnimationController>();
            Interactor = GetComponentInChildren<Interactor>();
            StackController = GetComponent<StackController>();
            
            SetState(IdleState);
        }

        private void FixedUpdate()
        {
            CurrentState?.OnStateFixedUpdate(this);
        }

        public void ExitState()
        {
            CurrentState?.StateExit(this);
        }

        public void SetState(State newState)
        {
            ExitState();
            CurrentState = newState;
            CurrentState.StateEnter(this);
        }
    }
}

