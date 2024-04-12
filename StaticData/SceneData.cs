using System;
using Cinemachine;
using UnityEngine;
using Codebase.Infrastructure;

namespace Codebase.StaticData
{
    public class SceneData : MonoBehaviour
	{
        [field: SerializeField, Header("Infrastructure")] public CoroutineRunner CoroutineRunner { get; private set; }
        [field: SerializeField, Header("Video")] public CinemachineVirtualCamera VirtualCamera {  get; private set; }
        [field: SerializeField, Header("UI")] public RectTransform UIRoot {  get; private set; }
        [SerializeField, Header("Audio")] private AudioPlayer _audioPlayer;
        public IAudioPlayer AudioPlayer => _audioPlayer as IAudioPlayer;
		[field: SerializeField, Header("Markers")] public PlayerMarker PlayerMarker { get; private set; }
		[field: SerializeField] public EnemyMarker[] EnemyMarkers { get; private set; }
		[field: SerializeField] public PickUpMarker[] CoinMarkers { get; private set; }
		[field: SerializeField] public PickUpMarker[] MedicalKitMarkers { get; private set; }
        [field: SerializeField, Header("Parents")] public Transform EnemyParent { get; private set; }
        [field: SerializeField] public Transform PickUpParent { get; private set; }

        private void OnValidate()
        {
            if(CoroutineRunner == null)
                throw new ArgumentNullException(nameof(CoroutineRunner));

            if (VirtualCamera == null)
                throw new ArgumentNullException(nameof(VirtualCamera)); 
            
            if (UIRoot == null)
                throw new ArgumentNullException(nameof(UIRoot));

            if (_audioPlayer == null || (_audioPlayer is IAudioPlayer _) == false)
                throw new InvalidOperationException(nameof(AudioPlayer));

            if(PlayerMarker == null) 
                throw new ArgumentNullException(nameof(PlayerMarker));

            if(EnemyMarkers.Length < 1)
                throw new ArgumentOutOfRangeException(nameof(EnemyMarkers));

            if(CoinMarkers.Length < 1)
                throw new ArgumentOutOfRangeException(nameof(CoinMarkers));
            
            if(MedicalKitMarkers.Length < 1)
                throw new ArgumentOutOfRangeException(nameof(MedicalKitMarkers));

            if(EnemyParent == null)
                throw new ArgumentNullException(nameof(EnemyParent));

            if(PickUpParent == null)
                throw new ArgumentNullException(nameof(PickUpParent));
        }
    }
}
