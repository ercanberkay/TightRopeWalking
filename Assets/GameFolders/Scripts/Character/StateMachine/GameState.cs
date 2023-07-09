namespace Character.StateMachine
{
    public class GameState : State
    {
        protected override void OnStateEnter(CharacterController controller)
        {
            controller.Animation.TriggerMove();
        }
    }
}

