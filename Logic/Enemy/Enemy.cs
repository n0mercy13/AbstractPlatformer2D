using System;
using UnityEngine;
using Codebase.Logic.EnemyComponents;

namespace Codebase.Logic.Enemy
{
    public partial class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyAI _ai;

        private void OnValidate()
        {
            if(_ai == null)
                throw new ArgumentNullException(nameof(_ai));
        }
    }

    public partial class Enemy : IInitializable<Transform[]>
    {
        public void Initialize(Transform[] patrolRoute)
        {
            _ai.SetPatrolRoute(patrolRoute);
        }
    }
}
