using Managers;
using UnityEngine;
using StackSystem;
using Random = UnityEngine.Random;

namespace PizzaSystem
{
    public class PizzaHolderManager : MonoBehaviour
    {
        public static PizzaHolderManager instance;


        [SerializeField] private PizzaBase _currentObject;
        public PizzaBase CurrentObject { get => _currentObject; private set => _currentObject = value; }

        private void Awake()
        {
            instance = this;
        }

        private void OnEnable()
        {
            SpawnNewObjectHolder();
        }

        public void SpawnNewObjectHolder()
        {
            var randomHolderCount = 0;
            if ((Random.Range(0, 100) < 35 && (StackController.instance.LeftStackCount > 0 || StackController.instance.RightStackCount > 0)) || StackController.instance.LeftStackCount + StackController.instance.RightStackCount > 25)
            {
                randomHolderCount = Random.Range(Mathf.Clamp(-25, -Mathf.Abs(StackController.instance.LeftStackCount + StackController.instance.RightStackCount), -1), -1);
            }
            else
            {
                randomHolderCount = Mathf.Clamp(Random.Range(1, Mathf.Min(StackController.instance.LeftStackCount, StackController.instance.RightStackCount) + 8), 1, 22);
            }
            CurrentObject = SpawnManager.instance.SpawnObjectAndSetPosition(randomHolderCount);
        }
    }
}

