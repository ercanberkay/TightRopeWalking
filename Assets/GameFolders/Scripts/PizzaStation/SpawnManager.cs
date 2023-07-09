using System.Collections.Generic;
using PoolSystem;
using PizzaSystem;
using LevelSystem;
using UnityEngine;

namespace Managers
{
    public class SpawnManager : MonoBehaviour
    {
        public static SpawnManager instance;

        [SerializeField] private List<string> spawnableObjectTypes;

        private int _distance = 25;

        private void Awake()
        {
            instance = this;
        }
        
        public PizzaBase SpawnObjectAndSetPosition(int stackCount)
        {
            if (CharacterManager.instance.player.transform.position.z + _distance < LevelManager.instance.level.gameAreas[^1].transform.position.z)
            {
                var newObjectHolder = ObjectPool.instance.GetObject("pizzaBoxHolder", 0).transform;
                newObjectHolder.position = CharacterManager.instance.player.transform.position + new Vector3(0, 0, _distance);
                newObjectHolder.SetParent(FindObjectOfType<LevelController>().transform);

                var tempBase = newObjectHolder.GetComponent<PizzaBase>();
                PrepareObjectHolder(tempBase, stackCount);
                return tempBase;
            }
            else
            {
                return null;
            }
        }

        private void PrepareObjectHolder(PizzaBase pizzaBase, int stackCount)
        {
            pizzaBase.SetPooledObject(spawnableObjectTypes[Random.Range(0, spawnableObjectTypes.Count)], 0, stackCount);
        }
    }
}

