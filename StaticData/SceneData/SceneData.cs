using System;
using UnityEngine;

namespace Codebase.StaticData
{
    public class SceneData : MonoBehaviour
	{
		[field: SerializeField] public PlayerMarker PlayerMarker { get; private set; }
		[field: SerializeField] public EnemyMarker[] EnemyMarkers { get; private set; }
		[field: SerializeField] public PickUpMarker[] CoinMarkers { get; private set; }

        private void OnValidate()
        {
            if(PlayerMarker == null) 
                throw new ArgumentNullException(nameof(PlayerMarker));

            if(EnemyMarkers.Length < 1)
                throw new ArgumentOutOfRangeException(nameof(EnemyMarkers));

            if(CoinMarkers.Length < 1)
                throw new ArgumentOutOfRangeException(nameof(CoinMarkers));
        }
    }
}
