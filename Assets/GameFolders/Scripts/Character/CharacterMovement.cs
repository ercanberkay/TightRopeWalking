using UnityEngine;
using StackSystem;

namespace Character
{
    public class CharacterMovement : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private Vector3 _mouseStartPos;
        private Vector3 _mouseEndPos;

        private readonly float _desiredSwipeValue = 100f;

        public float MoveSpeed;
        public bool IsActive;
        private bool MouseDown => Input.GetMouseButtonDown(0);
        private bool MouseUp => Input.GetMouseButtonUp(0);

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        public void Move()
        {
            if (!IsActive) return;
            var movement = Vector3.forward * MoveSpeed;
            _rigidbody.velocity = movement;
        }

        private void Update()
        {
            GetInput();
        }
        private void GetInput()
        {
            if (MouseDown)
            {
                _mouseStartPos = Input.mousePosition;
            }
            if (MouseUp)
            {
                _mouseEndPos = Input.mousePosition;
                if (Mathf.Abs(_mouseStartPos.x - _mouseEndPos.x) > _desiredSwipeValue)
                {
                    if (_mouseStartPos.x > _mouseEndPos.x)
                    {
                        StackController.stackSide = StackSide.Left;
                    }
                    if (_mouseStartPos.x < _mouseEndPos.x)
                    {
                        StackController.stackSide = StackSide.Right;
                    }
                }
            }
        }

        public void Look()
        {
            if (!IsActive) return;

            var rotation = Quaternion.Lerp(_rigidbody.rotation, Quaternion.LookRotation(Vector3.back), .2f);
            _rigidbody.MoveRotation(rotation);
        }
    }
}

