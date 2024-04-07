using Codebase.Infrastructure;
using System;
using UnityEngine;
using VContainer;

namespace Codebase.Logic.PlayerComponents
{
    [RequireComponent(typeof(SphereCollider))]
    public class PickUpsHandler : MonoBehaviour
    {
        [SerializeField, Range(0f, 10f)] private float _collectionRadius;
        
        private IAudioService _audioService;

        [Inject]
        private void Construct(IAudioService audioService)
        {
            _audioService = audioService;
        }

        private void Awake()
        {
            SphereCollider collider = GetComponent<SphereCollider>();
            collider.isTrigger = true;
            collider.radius = _collectionRadius;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Coin coin))
            {
                CollectCoin(coin);
            }
            else if(other.TryGetComponent(out MedicalKit medicalKit))
            {
                CollectMedicalKit(medicalKit);
            }
        }

        public event Action<int> CoinCollected = delegate { };
        public event Action<int> MedicalKitCollected = delegate { };

        private void CollectCoin(Coin coin)
        {
            int value = coin.Collect();
            CoinCollected.Invoke(value);
            _audioService.PlaySFX(AudioElementTypes.SFX_Player_PickUpItem);
        }

        private void CollectMedicalKit(MedicalKit medicalKit)
        {
            int value = medicalKit.Collect();
            MedicalKitCollected.Invoke(value);
            _audioService.PlaySFX(AudioElementTypes.SFX_Player_PickUpItem);
        }
    }
}