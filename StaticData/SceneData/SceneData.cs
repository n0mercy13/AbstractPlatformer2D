using Cinemachine;
using System;
using UnityEngine;

namespace Codebase.StaticData
{
    public class SceneData : MonoBehaviour
	{
        [field: SerializeField, Header("Camera")] public CinemachineVirtualCamera _virtualCamera {  get; private set; }
		[field: SerializeField, Header("Markers")] public PlayerMarker PlayerMarker { get; private set; }
		[field: SerializeField] public EnemyMarker[] EnemyMarkers { get; private set; }
		[field: SerializeField] public PickUpMarker[] CoinMarkers { get; private set; }

        private void OnValidate()
        {
            if(_virtualCamera == null)
                throw new ArgumentNullException(nameof(_virtualCamera));

            if(PlayerMarker == null) 
                throw new ArgumentNullException(nameof(PlayerMarker));

            if(EnemyMarkers.Length < 1)
                throw new ArgumentOutOfRangeException(nameof(EnemyMarkers));

            if(CoinMarkers.Length < 1)
                throw new ArgumentOutOfRangeException(nameof(CoinMarkers));
        }
    }
}
