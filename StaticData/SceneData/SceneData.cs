using Cinemachine;
using System;
using UnityEngine;

namespace Codebase.StaticData
{
    public class SceneData : MonoBehaviour
	{
        [field: SerializeField, Header("Camera")] public CinemachineVirtualCamera VirtualCamera {  get; private set; }
		[field: SerializeField, Header("Markers")] public PlayerMarker PlayerMarker { get; private set; }
		[field: SerializeField] public EnemyMarker[] EnemyMarkers { get; private set; }
		[field: SerializeField] public PickUpMarker[] CoinMarkers { get; private set; }
        [field: SerializeField, Header("Parent")] public Transform EnemyParent { get; private set; }
        [field: SerializeField] public Transform PickUpParent { get; private set; }

        private void OnValidate()
        {
            if(VirtualCamera == null)
                throw new ArgumentNullException(nameof(VirtualCamera));

            if(PlayerMarker == null) 
                throw new ArgumentNullException(nameof(PlayerMarker));

            if(EnemyMarkers.Length < 1)
                throw new ArgumentOutOfRangeException(nameof(EnemyMarkers));

            if(CoinMarkers.Length < 1)
                throw new ArgumentOutOfRangeException(nameof(CoinMarkers));

            if(EnemyParent == null)
                throw new ArgumentNullException(nameof(EnemyParent));

            if(PickUpParent == null)
                throw new ArgumentNullException(nameof(PickUpParent));
        }
    }
}
