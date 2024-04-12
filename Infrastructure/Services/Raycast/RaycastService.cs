using System;
using System.Collections.Generic;
using UnityEngine;
using Codebase.Logic;
using Codebase.StaticData;
using Codebase.Logic.PlayerComponents;
using Codebase.Logic.EnemyComponents;

namespace Codebase.Infrastructure.Services
{
    public partial class RaycastService
    {
        private readonly int _targetsNumber = 10;
        private readonly LayerMask _playerMask;
        private readonly LayerMask _enemyMask;
        private Collider[] _results;

        public RaycastService(RaycastConfig config) 
        {
            _playerMask = config.PlayerMask;
            _enemyMask = config.EnemyMask;
            _results = new Collider[_targetsNumber];
        }
    }

    public partial class RaycastService : IRaycastService
    {
        public bool IsTargetHit(Actor initiator, float radius, out List<ITarget> targets)
        {
            targets = new List<ITarget>();
            LayerMask layerMask = GetMask(initiator);
            int hits = Physics.OverlapSphereNonAlloc(
                initiator.transform.position, radius, _results, layerMask);

            if (hits <= 0)
            {
                return false;
            }
            else
            {
                for(int i = 0; i < hits; i++)
                    if (_results[i].TryGetComponent(out ITarget target))
                        targets.Add(target);

                if(targets.Count <= 0)
                    return false;

                return true;
            }
        }

        private LayerMask GetMask(Actor initiator)
        {
            if (initiator is Player _)
                return _enemyMask;
            else if(initiator is Enemy _)
                return _playerMask;
            else
                throw new ArgumentOutOfRangeException(nameof(initiator));
        }
    }
}
