using InteractionSystem;
using CharacterController = Character.CharacterController;

namespace PizzaSystem
{
    public class PizzaHolder : PizzaBase, IBeginInteract
    {
        public bool IsInteractable => true;

        public void OnInteractBegin(IInteractor interactor)
        {
            var controller = (CharacterController)interactor;
            controller.SetState(controller.FailState);
        }
    }
}

