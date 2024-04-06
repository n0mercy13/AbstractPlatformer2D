using System;
using UnityEngine;
using VContainer;
using Codebase.StaticData;
using Codebase.Logic.PlayerComponents;

namespace Codebase.Logic.EnemyComponents
{
    public class PlayerDetector : MonoBehaviour
    {
        [SerializeField] private SphereCollider _collider;

        [Inject]
        private void Construct(EnemyConfig config)
        {
            _collider.radius = config.DetectionRadius;
            _collider.isTrigger = true;
        }

        private void OnValidate()
        {
            if(_collider == null)
                throw new ArgumentNullException(nameof(_collider));
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out Player player))
                PlayerDetected.Invoke(player.transform);
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.TryGetComponent(out Player _))
                PlayerLost.Invoke();
        }

        public event Action<Transform> PlayerDetected = delegate { };
        public event Action PlayerLost = delegate { };
    }
}