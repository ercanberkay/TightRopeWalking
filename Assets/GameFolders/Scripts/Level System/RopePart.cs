using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelSystem
{
    public class RopePart : LevelPart
    {
        [SerializeField] private RopeAreaManager _ropeArea;

        public override GameAreaManager SetupPart(Transform parent, GameAreaManager previousArea = null)
        {
            var manager = Object.Instantiate(_ropeArea, parent);
            if (previousArea == null) return manager;

            var position = previousArea.GetNextAreaPosition();
            manager.MoveArea(position);
            return manager;
        }
    }
}

