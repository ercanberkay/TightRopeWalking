using System.Collections.Generic;
using PizzaSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using CharacterController = Character.CharacterController;

public enum StackSide
{
    NoWhere,
    Left,
    Right
}
namespace StackSystem
{
    public class StackController : MonoBehaviour
    {
        public static StackController instance;
        private StackVisualController _stackVisual;
        private CharacterController _controller;
        //public event Action<Transform, Transform> OnStackAdded;
        //public event Action<int,int> OnStackUsed;

        public static StackSide stackSide;
        public int LeftStackCount => _leftStickObjectsCount;
        private int _leftStickObjectsCount;
        public int RightStackCount => _rightStickObjectsCount;
        private int _rightStickObjectsCount;

        [SerializeField] private Transform _rightStackParent;
        [SerializeField] private Transform _leftStackParent;
        public Transform stickObject;
        public Quaternion stickObjectRotation;
        private readonly float _maxRot = 25f;
        private readonly float _maxTime = 3;
        private float _timer;

        public List<Rigidbody> ragdollRigidbodies;



        public Transform StackParent => StackSideSelector();

        [ShowInInspector,ReadOnly,PropertyOrder(-1)] public int Stack { get; private set; }

        private void Awake()
        {
            instance = this;
            _stackVisual = GetComponent<StackVisualController>();
            _controller = GetComponent<CharacterController>();
            stickObjectRotation = stickObject.localRotation;
        }

        private void Update()
        {
            GettingPizzaBoxes();
        }
        public void GettingPizzaBoxes()
        {
            if (PizzaHolderManager.instance.CurrentObject == null) return;
            if (PizzaHolderManager.instance.CurrentObject.IsUsed) return;
            GetPizza();
        }

        private void GetPizza()
        {
            var amount = GetObjectAmount();
            if(amount != 0)
            {
                if (stackSide == StackSide.NoWhere) return;
                if (StackParent == _leftStackParent) _leftStickObjectsCount += amount;
                if (StackParent == _rightStackParent) _rightStickObjectsCount += amount;
                PizzaHolderManager.instance.CurrentObject.PizzaHolderToPlayerMove(amount, StackParent);

                if (PizzaHolderManager.instance.CurrentObject.ObjectCount < 0)
                    PizzaLost(amount);
            }
            _stackVisual.UpdateStackPosition(_leftStackParent, _rightStackParent);
            _stackVisual.UpdateVisualUsed(LeftStackCount, RightStackCount);
            _stackVisual.AnimTrigger(LeftStackCount,RightStackCount);
            //OnStackUsed?.Invoke(LeftStackCount,RightStackCount);
            //OnStackAdded?.Invoke(_leftStackParent, _rightStackParent);
        }

        private void PizzaLost(int count)
        {
            Transform child = null;
            var removedList = new List<Transform>();
            for (int i = 0; i < Mathf.Abs(count); i++)
            {
                if (stackSide == StackSide.Left)
                {
                    child = _leftStackParent.GetChild(i);
                    child.GetComponent<Pizza>().SetLost();
                }
                else if (stackSide == StackSide.Right)
                {
                    child = _rightStackParent.GetChild(i);
                    child.GetComponent<Pizza>().SetLost();
                }
                removedList.Add(child);
            }
            if (stackSide == StackSide.Left)
            {
                for (int i = 0; i < removedList.Count; i++)
                {
                    removedList[i].SetParent(null);
                }
            }
            else if (stackSide == StackSide.Right)
            {
                for (int i = 0; i < removedList.Count; i++)
                {
                    removedList[i].SetParent(null);
                }
            }
            removedList.Clear();
        }
        private int GetObjectAmount()
        {
            var givenObj = 0;
            if (PizzaHolderManager.instance.CurrentObject.ObjectCount > 0)
                givenObj = PizzaHolderManager.instance.CurrentObject.ObjectCount;
            else
            {
                if (stackSide == StackSide.Left)
                {
                    if (Mathf.Abs(PizzaHolderManager.instance.CurrentObject.ObjectCount) >= _leftStickObjectsCount)
                        givenObj = -_leftStickObjectsCount;
                    else
                        givenObj = PizzaHolderManager.instance.CurrentObject.ObjectCount;
                }
                else if(stackSide == StackSide.Right)
                {
                    if (Mathf.Abs(PizzaHolderManager.instance.CurrentObject.ObjectCount) >= _rightStickObjectsCount)
                        givenObj = -_rightStickObjectsCount;
                    else
                        givenObj = PizzaHolderManager.instance.CurrentObject.ObjectCount;
                }
            }
            return givenObj;
        }

        public void StacksBalanceSystem()
        {
            var isLeft = LeftStackCount > RightStackCount;
            var i = isLeft ? 1 : -1;
            var dif = Mathf.Abs(LeftStackCount - RightStackCount);
            var lerpRotX = Mathf.Lerp(0, 35 * i, dif / 30f);
            stickObject.localRotation = Quaternion.AngleAxis(lerpRotX, Vector3.forward) * stickObjectRotation;

            var sRot = stickObject.localRotation.eulerAngles;
            var relX = sRot.x - 90;
            var rot = transform.rotation.eulerAngles;
            rot.z = relX / 2f;
            if (sRot.z > 0) rot.z = -relX / 2f;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(rot), 5 * Time.deltaTime);
            if (Mathf.Abs(lerpRotX) > _maxRot)
            {
                _timer += Time.fixedDeltaTime;
                if (_timer > _maxTime)
                {
                    _controller.SetState(_controller.FailState);
                } 
            }
            else _timer = 0;
        }

        public void FailRagdollEffect()
        {
            foreach (var item in ragdollRigidbodies)
            {
                item.isKinematic = false;
            }
        }

        public void WhenItsFail()
        {
            stickObject.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
            for (int i = 0; i < _leftStackParent.childCount; i++)
            {
                _leftStackParent.GetChild(i).GetComponent<Pizza>().SetLost();
            }
            for (int i = 0; i < _rightStackParent.childCount; i++)
            {
                _rightStackParent.GetChild(i).GetComponent<Pizza>().SetLost();
            }
        }
        public Transform StackSideSelector()
        {
            if (stackSide == StackSide.Left)
            {
                return _leftStackParent;
            }
            return _rightStackParent;
        }

//#if UNITY_EDITOR
//        [SerializeField, BoxGroup("Box", false), HorizontalGroup("Box/Debug", .5f), LabelWidth(48)] private int _amount;
//        [HorizontalGroup("Box/Debug"), Button, DisableInEditorMode, LabelText("Add Stack")]
//        public void Editor_AddStack()
//        {
//            var interactor = GetComponentInParent<CharacterController>();
//            for (var i = 0; i < _amount; i++)
//            {
//                //var obj = PizzaManager.instance.SpawnObject();
//                //obj.OnInteractBegin(interactor);
//            }
//        }
//#endif
    }
}

