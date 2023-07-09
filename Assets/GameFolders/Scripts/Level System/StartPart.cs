using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LevelSystem
{
    [Serializable]
    public class StartPart : LevelPart
    {
        [SerializeField] private StartAreaManager _manager;
        public override GameAreaManager SetupPart(Transform parent, GameAreaManager previousArea = null)
        {
            var manager = Object.Instantiate(_manager, parent);
            if (previousArea != null)
            {
                var position = previousArea.GetNextAreaPosition();
                manager.MoveArea(position);
            }
            //manager.InitializeStartArea();
            return manager;
        }
    }
}

