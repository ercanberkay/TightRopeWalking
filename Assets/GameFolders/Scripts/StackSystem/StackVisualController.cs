using DG.Tweening;
using UnityEngine;
using TMPro;
using CharacterController = Character.CharacterController;

namespace StackSystem
{
    public class StackVisualController : MonoBehaviour
    {
        private StackController _stackController;
        private CharacterController _controller;

        [SerializeField] private TMP_Text _leftHolderText;
        [SerializeField] private TMP_Text _rightHolderText;

        private void Awake()
        {
            _stackController = GetComponent<StackController>();
            _controller = GetComponent<CharacterController>();
        }


        //private void OnEnable()
        //{
        //    _stackController.OnStackUsed += UpdateVisualUsed;
        //    _stackController.OnStackUsed += AnimTrigger;
        //    _stackController.OnStackAdded += UpdateStackPosition;
        //}

        //private void OnDisable()
        //{
        //    _stackController.OnStackUsed -= UpdateVisualUsed;
        //    _stackController.OnStackUsed -= AnimTrigger;
        //    _stackController.OnStackAdded -= UpdateStackPosition;
        //}

        public void UpdateVisualUsed(int leftHolderCount, int rightHolderCount)
        {
            _leftHolderText.text = $"{leftHolderCount}";
            _rightHolderText.text = $"{rightHolderCount}";
            StackController.stackSide = StackSide.NoWhere;
        }

        public void UpdateStackPosition(Transform leftParent, Transform rightParent)
        {
            DOVirtual.DelayedCall(.5f,()=>{
                for (int i = 0; i < _stackController.LeftStackCount; i++)
                {
                    leftParent.GetChild(i).DOLocalMoveY(.05f * i, .1f);
                }
                for (int i = 0; i < _stackController.RightStackCount; i++)
                {
                    rightParent.GetChild(i).DOLocalMoveY(.05f * i, .1f);
                }
            });
            
        }

        public void AnimTrigger(int leftHolderCount, int rightHolderCount)
        {
            if (leftHolderCount > rightHolderCount) _controller.Animation.TriggerLeftSide();
            if (rightHolderCount > leftHolderCount) _controller.Animation.TriggerRightSide();
            if (rightHolderCount == leftHolderCount) _controller.Animation.TriggerMove();
        }
    }
}

