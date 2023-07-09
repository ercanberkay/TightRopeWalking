using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using CharacterController = Character.CharacterController;

public class CameraConfigSetter : MonoBehaviour
{
    [SerializeField] private CameraConfig _startConfig;
    [SerializeField] private CameraConfig _gameplayConfig;
    [SerializeField] private CameraConfig _finishConfig;

    private CharacterController _registeredCharacter;

    private void OnEnable()
    {
        GameManager.OnGameInitialized += OnInitialize;
        GameManager.OnGameEnd += OnEnd;
    }

    private void OnDisable()
    {
        GameManager.OnGameInitialized -= OnInitialize;
        GameManager.OnGameEnd -= OnEnd;
    }

    private void OnInitialize()
    {
        RegisterPlayer();
    }

    private void OnEnd()
    {
        UnregisterPlayer();
        SetFinishCamera();
    }

    private void RegisterPlayer()
    {
        _registeredCharacter = CharacterManager.instance.player;
        _registeredCharacter.GameState.OnStateEntered += SetGamePlayCamera;
    }

    private void UnregisterPlayer()
    {
        _registeredCharacter.GameState.OnStateEntered -= SetGamePlayCamera;

    }

    private void SetGamePlayCamera(CharacterController obj)
    {
        CinemachineController.instance.SetConfig(_gameplayConfig);
    }

    private void SetFinishCamera()
    {
        CinemachineController.instance.SetConfig(_finishConfig);
    }
}
