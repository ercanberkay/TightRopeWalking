using Managers;
using UnityEngine;

namespace game.UI
{
    public class InputHandleUiController : MonoBehaviour
    {
        private Canvas _canvas;
        private bool _isActive;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
        }

        //private void Start()
        //{
        //    UncoGameManager.OnLevelStart += EnableHandle;
        //    GameManager.OnGameEnded += DisableHandle;
        //    Manager.InputManager.Module.OnMouseDown += ShowHandle;
        //    Manager.InputManager.Module.OnMouseHold += UpdateHandle;
        //    Manager.InputManager.Module.OnMouseUp += HideHandle;
        //}

        //private void OnDestroy()
        //{
        //    UncoGameManager.OnLevelStart -= EnableHandle;
        //    GameManager.OnGameEnded -= DisableHandle;
        //    Manager.InputManager.Module.OnMouseDown -= ShowHandle;
        //    Manager.InputManager.Module.OnMouseHold -= UpdateHandle;
        //    Manager.InputManager.Module.OnMouseUp -= HideHandle;
        //}

        private void EnableHandle()
        {
            _isActive = true;
        }
        
        private void DisableHandle()
        {
            _isActive = false;
            HideHandle();
        }

        private void ShowHandle()
        {
            if(! _isActive) return;
            _canvas.enabled = true;
        }


        private void HideHandle()
        {
            _canvas.enabled = false;
        }
    }
}
