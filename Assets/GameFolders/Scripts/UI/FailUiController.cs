using Managers;
using Managers.GameModes;
using UnityEngine;
using UnityEngine.UI;

namespace game.UI
{
    public class FailUiController : UIController<FailUiController>
    {
        [SerializeField] private Button _retryButton;
        [SerializeField] private GameMode _gameMode;

        private void OnEnable()
        {
            _retryButton.onClick.AddListener(RetryButtonPressed);
        }

        private void OnDisable()
        {
            _retryButton.onClick.RemoveListener(RetryButtonPressed);
        }

        private void RetryButtonPressed()
        {
            GameManager.instance.InitializeGameMode(_gameMode);
            HideInstant();
        }
    }
}