using System;
using UnityEngine;
using VContainer;
using Codebase.StaticData;

namespace Codebase.Logic.EnemyComponents
{
    public partial class Enemy : Actor
    {
        [SerializeField] private EnemyAI _ai;

        [Inject]
        private void Construct(EnemyConfig config)
        {
            Health.Initialize(config.MaxHealth);
        }

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
