using System;
using LevelSystem;
using game.UI;
using DG.Tweening;
using UnityEngine;

namespace Managers.GameModes
{
    [CreateAssetMenu(menuName = "Game/GameMode/DefaultGameMode", fileName = "DefaultGameMode", order = -399)]
    public class DefaultGameMode : GameMode
    {
        public LevelConfig[] Levels;
        [SerializeField] private CameraConfig _startConfig;

        public override void InitializeGameMode()
        {
            var config = Levels[0]; //tek level oldugu icin
            LevelManager.instance.SpawnLevel(config.parts);
            CharacterManager.instance.SpawnCharacter();
            var startArea = LevelManager.instance.level.gameAreas[0];
            startArea.OnCharacterEntered(CharacterManager.instance.player);
            var target = new GameObject("cameraTarget").AddComponent<CameraFollowTarget>();
            target.transform.SetParent(GameManager.instance.transform);
            CinemachineController.instance.SetTarget(target);
            CinemachineController.instance.SetConfig(_startConfig);
            LevelManager.instance.SpawnPizzaPool();
            IntroUiController.instance.ShowInstant();
        }

        public override void StartGameMode(Action levelStart)
        {
            IntroUiController.instance.Hide();
            StartGame();

            void StartGame()
            {
                CinemachineController.instance.SetTarget(CharacterManager.instance.player.GetComponent<CameraFollowTarget>());
                levelStart.Invoke();
            }

        }
        public override void CompleteGameMode()
        {
            DOVirtual.DelayedCall(1f, WinUiController.instance.Show, false);
        }
        public override void FailGameMode()
        {
            DOVirtual.DelayedCall(2f, FailUiController.instance.Show, false);
        }

        public override void DeinitializeGameMode()
        {
            LevelManager.instance.DestroyLevel();
            CharacterManager.instance.DestroyCharacter();
        }
        
        public override void SkipGameMode()
        {
        }

        
    }
}

