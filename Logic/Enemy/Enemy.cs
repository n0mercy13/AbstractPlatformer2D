using System;
using UnityEngine;
using Codebase.Logic.Enemies;

namespace Codebase.Logic.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyMover _mover;

        private Transform[] _patrolRoute;

        private void OnValidate()
        {
            if(_mover == null)
                throw new ArgumentNullException(nameof(_mover));
        }

        public void Patrol(Transform[] patrolRoute = null)
        {
            if (patrolRoute == null && _patrolRoute == null)
                throw new InvalidOperationException("No valid route to patrol");

            if(patrolRoute != null)
                _patrolRoute = patrolRoute;

            _mover.Patrol(_patrolRoute);
        }
    }
}
