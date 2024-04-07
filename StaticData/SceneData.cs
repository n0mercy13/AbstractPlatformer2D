﻿using System;
using Cinemachine;
using UnityEngine;
using Codebase.Infrastructure;

namespace Codebase.StaticData
{
    public class SceneData : MonoBehaviour
	{
        [field: SerializeField, Header("Video")] public CinemachineVirtualCamera VirtualCamera {  get; private set; }
        [SerializeField, Header("Audio")] private AudioPlayer _audioPlayer;
        public IAudioPlayer AudioPlayer => _audioPlayer as IAudioPlayer;
		[field: SerializeField, Header("Markers")] public PlayerMarker PlayerMarker { get; private set; }
		[field: SerializeField] public EnemyMarker[] EnemyMarkers { get; private set; }
		[field: SerializeField] public PickUpMarker[] CoinMarkers { get; private set; }
		[field: SerializeField] public PickUpMarker[] MedicalKitMarkers { get; private set; }
        [field: SerializeField, Header("Parent")] public Transform EnemyParent { get; private set; }
        [field: SerializeField] public Transform PickUpParent { get; private set; }

        private void OnValidate()
        {
            if(VirtualCamera == null)
                throw new ArgumentNullException(nameof(VirtualCamera));

            if(_audioPlayer == null || (_audioPlayer is IAudioPlayer _) == false)
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
