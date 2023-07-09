using DG.Tweening;
using Managers;

namespace Character.StateMachine
{
    public class FailState : State
    {
        protected override void OnStateEnter(CharacterController controller)
        {
            controller.DOKill();
            controller.Animation.TriggerRagdoll() ;
            controller.StackController.FailRagdollEffect();
            controller.StackController.WhenItsFail();
            controller.Rigidbody.isKinematic = false;
            controller.Rigidbody.useGravity = true;
            controller.Rigidbody.constraints = UnityEngine.RigidbodyConstraints.None;
            GameManager.instance.FailGameMode();
        }
    }
}


