using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using VContainer;
using Codebase.StaticData;

namespace Codebase.Logic.EnemyComponents
{
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navAgent;

        private readonly float _distanceMargin = 0.2f;
        private Transform[] _patrolRoute;
        private Transform _target;
        private Coroutine _navigationCoroutine;
        private int _nextWaypointIndex;
        private float _patrolSpeed;
        private float _pursueSpeed;
        private bool _isPursuing;

        [Inject]
        private void Construct(EnemyConfig config)
        {
            _patrolSpeed = config.PatrolSpeed;
            _pursueSpeed = config.PursueSpeed;
        }

        private void OnValidate()
        {
            if (_navAgent == null)
                throw new ArgumentNullException(nameof(_navAgent));
        }

        private void OnDisable()
        {
            if (_navigationCoroutine != null)
                StopCoroutine(_navigationCoroutine);
        }

        public void Patrol(Transform[] route)
        {
            _patrolRoute = route;
            _isPursuing = false;
            _navAgent.speed = _patrolSpeed;
            _nextWaypointIndex = 0;

            MoveToPosition(GetTarget());
        }

        public void Pursue(Transform target)
        {
            _target = target;
            _isPursuing = true;
            _navAgent.speed = _pursueSpeed;

            MoveToPosition(GetTarget());
        }

        private Vector3 GetTarget()
        {
            if (_isPursuing)
            {
                return _target.position;
            }
            else
            {
                _nextWaypointIndex = (_nextWaypointIndex + 1) % _patrolRoute.Length;

                return _patrolRoute[_nextWaypointIndex].position;
            }
        }

        private void MoveToPosition(Vector3 position)
        {
            _navAgent.destination = position;

            if (_navigationCoroutine != null)
                StopCoroutine(_navigationCoroutine);

            _navigationCoroutine = StartCoroutine(DestinationReachedCheckAsync());
        }

        private IEnumerator DestinationReachedCheckAsync()
        {
            while (enabled)
            {
                yield return new WaitUntil(() => IsDestinationReached());

                MoveToPosition(GetTarget());
            }
        }

        private bool IsDestinationReached()
        {
            if (_navAgent.pathPending == false 
                && _navAgent.remainingDistance < _distanceMargin)
                 return true;

            return false;
        }
    }
}