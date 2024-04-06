using System;
using UnityEngine;

namespace Codebase.Logic.EnemyComponents
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private EnemyMover _mover;
        [SerializeField] private PlayerDetector _detector;

        private Transform[] _patrolRoute;

        private void OnValidate()
        {
            if(_mover == null)
                throw new ArgumentNullException(nameof(_mover));

            if(_detector == null) 
                throw new ArgumentNullException(nameof(_detector));
        }

        private void OnEnable()
        {
            _detector.PlayerDetected += OnPlayerDetected;
            _detector.PlayerLost += OnPlayerLost;
        }

        private void Start()
        {
            Patrol();
        }

        private void OnDisable()
        {
            _detector.PlayerDetected -= OnPlayerDetected;
            _detector.PlayerLost -= OnPlayerLost;
        }

        public void SetPatrolRoute(Transform[] patrolRoute) =>
            _patrolRoute = patrolRoute;

        public void Patrol() =>
            _mover.Patrol(_patrolRoute);

        private void OnPlayerDetected(Transform player)
        {
            _mover.Pursue(player);
        }

        private void OnPlayerLost()
        {
            Patrol();
        }
    }
}