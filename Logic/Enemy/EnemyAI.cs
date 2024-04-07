using Codebase.Infrastructure;
using System;
using UnityEngine;
using VContainer;

namespace Codebase.Logic.EnemyComponents
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private EnemyMover _mover;
        [SerializeField] private PlayerDetector _detector;
        [SerializeField] private EnemyAttackHandler _attack;

        private IAudioService _audioService;
        private Transform[] _patrolRoute;

        [Inject]
        private void Construct(IAudioService audioService)
        {
            _audioService = audioService;
        }

        private void OnValidate()
        {
            if(_mover == null)
                throw new ArgumentNullException(nameof(_mover));

            if(_detector == null) 
                throw new ArgumentNullException(nameof(_detector));

            if(_attack == null)
                throw new ArgumentNullException(nameof(_attack));
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

        public void Patrol()
        {
            _mover.Patrol(_patrolRoute);
        }

        private void OnPlayerDetected(Transform player)
        {
            _mover.Pursue(player);
            _attack.AttackAsync();
            _audioService.PlaySFX(AudioElementTypes.SFX_Enemy_PlayerDetected);
        }

        private void OnPlayerLost()
        {
            Patrol();
            _attack.StopAttack();
            _audioService.PlaySFX(AudioElementTypes.SFX_Enemy_PlayerLost);
        }
    }
}