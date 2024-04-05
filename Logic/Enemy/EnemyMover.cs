using System.Collections;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Codebase.Logic.Enemies
{
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navAgent;

        private readonly float _distanceMargin = 0.2f;
        private Transform[] _patrolRoute;
        private Coroutine _navigationCoroutine;
        private int _nextWaypointIndex;

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
            _nextWaypointIndex = 0;
            MoveToNextWaypoint();
        }

        private void MoveToNextWaypoint()
        {
            _nextWaypointIndex = (_nextWaypointIndex + 1) % _patrolRoute.Length;
            _navAgent.destination = _patrolRoute[_nextWaypointIndex].position;

            if (_navigationCoroutine != null)
                StopCoroutine(_navigationCoroutine);

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
            if (_navAgent.pathPending == false 
                && _navAgent.remainingDistance < _distanceMargin)
                 return true;

            return false;
        }
    }
}