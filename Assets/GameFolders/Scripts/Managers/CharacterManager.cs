using Sirenix.OdinInspector;
using UnityEngine;
using CharacterController = Character.CharacterController;

namespace Managers
{
    public class CharacterManager : MonoBehaviour
    {
        public static CharacterManager instance;

        [SerializeField] private CharacterController _playerCharacter;
        [ReadOnly] public CharacterController player;

        private void Awake()
        {
            instance = this;
        }

        private void OnEnable()
        {
            BaseGameManager.OnLevelStart += StartCharacter;
        }

        private void OnDisable()
        {
            BaseGameManager.OnLevelStart -= StartCharacter;
        }

        public void SpawnCharacter()
        {
            player = Instantiate(_playerCharacter, Vector3.zero, Quaternion.identity);
        }

        private void StartCharacter()
        {
            if (player.GameState != null) 
                player.SetState(player.GameState);
        }

        public void DestroyCharacter()
        {
            Destroy(player.gameObject);
        }
    }
}

