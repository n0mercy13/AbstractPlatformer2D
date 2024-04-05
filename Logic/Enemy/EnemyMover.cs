using System.Collections;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Codebase.Logic.Enemies
{
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navAgent;
        [SerializeField] private Transform[] _patrolRoute;

        private readonly float _distanceMargin = 0.2f;
        private Coroutine _navigationCoroutine;
        private int _nextWaypointIndex;

        private void OnValidate()
        {
            if (_navAgent == null)
                throw new ArgumentNullException(nameof(_navAgent));

            if (_patrolRoute.Length < 2)
                throw new ArgumentOutOfRangeException(nameof(_patrolRoute));
        }

        private void Start()
        {
            _nextWaypointIndex = 0;
            MoveToNextWaypoint();
        }

        private void OnDisable()
        {
            if (_navigationCoroutine != null)
                StopCoroutine(_navigationCoroutine);
        }

        private void MoveToNextWaypoint()
        {
            _nextWaypointIndex++;

            if (_nextWaypointIndex >= _patrolRoute.Length)
                _nextWaypointIndex = 0;

            _navAgent.destination = _patrolRoute[_nextWaypointIndex].position;
            _navigationCoroutine = StartCoroutine(NextWaypointReachedCheckAsync());
        }

        private IEnumerator NextWaypointReachedCheckAsync()
        {
            while (enabled)
            {
                yield return new WaitUntil(() => IsWaypointReached());

                MoveToNextWaypoint();
            }
        }

        private bool IsWaypointReached()
        {
            if (_navAgent.pathPending == false && _navAgent.remainingDistance < _distanceMargin)
                return true;

            return false;
        }
    }

}