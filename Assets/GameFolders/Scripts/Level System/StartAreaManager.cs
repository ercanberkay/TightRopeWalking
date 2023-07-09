using Managers;
using CharacterController = Character.CharacterController;


namespace LevelSystem
{
    public class StartAreaManager : GameAreaManager
    {
        public override void OnCharacterEntered(CharacterController controller)
        {
            if (!GameManager.instance.IsPlaying) return;
            controller.SetState(controller.GameState);
        }
    }
}

