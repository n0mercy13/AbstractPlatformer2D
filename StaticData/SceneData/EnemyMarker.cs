using System;
using UnityEngine;

namespace Codebase.StaticData
{
    public class EnemyMarker : SpawnMarker
	{
		[field: SerializeField] public Transform[] PatrolRoute { get; private set; }

        private void OnValidate()
        {
            if(PatrolRoute.Length < 2)
                throw new ArgumentOutOfRangeException(nameof(PatrolRoute));
        }
    }
}
