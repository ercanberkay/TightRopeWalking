using System;
using System.Collections.Generic;
using LevelSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager instance;
        public static event Action<LevelController> OnLevelSpawned;
        [SerializeField] private GameObject _pizzaPool;

        [ReadOnly] public LevelController level;

        private void Awake()
        {
            instance = this;
        }

        [Button]
        public void SpawnLevel(LevelPart[] parts)
        {
            level = new GameObject("Level").AddComponent<LevelController>();
            var gameAreas = new List<GameAreaManager>();
            var prevArea = parts[0].SetupPart(level.transform);
            gameAreas.Add(prevArea);

            for (int i = 1; i < parts.Length; i++)
            {
                var area = parts[i].SetupPart(level.transform, prevArea);
                gameAreas.Add(area);
                prevArea = area;
            }

            level.gameAreas = gameAreas.ToArray();
            OnLevelSpawned?.Invoke(level);
        }

        public void SpawnPizzaPool()
        {
            var pizzaPool = Instantiate(_pizzaPool);
            pizzaPool.transform.SetParent(FindObjectOfType<LevelController>().transform);
        }

        public void DestroyLevel()
        {
            if (level != null) Destroy(level.gameObject);
        }
    }
}

