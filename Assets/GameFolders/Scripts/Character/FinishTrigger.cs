using InteractionSystem;
using CharacterController = Character.CharacterController;
using LevelSystem;
using UnityEngine;

public class FinishTrigger : MonoBehaviour ,IBeginInteract
{
    private GameAreaManager _newArea;
    public bool IsInteractable => true;

    private void Awake()
    {
        _newArea = GetComponentInParent<GameAreaManager>();
    }

    public void OnInteractBegin(IInteractor interactor)
    {
        var character = (CharacterController)interactor;
        character.SetState(character.WinState);
        character.GameState.OnStateExited += EnterFinishArea;
    }

    private void EnterFinishArea(CharacterController character)
    {
        character.GameState.OnStateExited -= EnterFinishArea;
        _newArea.OnCharacterEntered(character);

    }
}
