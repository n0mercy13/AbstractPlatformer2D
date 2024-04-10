using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;
using Codebase.Infrastructure;
using Codebase.Logic;
using Codebase.UI;

namespace Codebase.Infrastructure
{
    public partial class HealthViewManager
    {
        private readonly IGameFactory _gameFactory;
        private readonly Dictionary<Actor, HealthBarView> _actorsWithHealthView;
        private HealthBarView _healthBar;
        private Vector3 _position;

        public HealthViewManager(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            _actorsWithHealthView = new Dictionary<Actor, HealthBarView>();
        }

        public void Register(Actor actor)
        {
            _healthBar = _gameFactory.CreateHealthBar();
            _healthBar.Initialize(actor);
            _actorsWithHealthView.Add(actor, _healthBar);
        }

        private void Unregister(Actor actor)
        {
            if(_actorsWithHealthView.TryGetValue(actor, out HealthBarView healthBar))
            {
                healthBar.Close();
                _actorsWithHealthView.Remove(actor);
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(actor));
            }
        }

        private void UpdateHealthViewPosition()
        {
            foreach(Actor actor in _actorsWithHealthView.Keys)
            {
                _position = actor.HealthBarPosition;
                _healthBar = _actorsWithHealthView[actor];
                _healthBar.transform.position = Camera.main.WorldToScreenPoint(actor.HealthBarPosition);
            }
        }
    }

    public partial class HealthViewManager : IPostStartable
    {
        public void PostStart()
        {
            foreach (Actor actor in _actorsWithHealthView.Keys)
                actor.Death += Unregister;
        }
    }

    public partial class HealthViewManager : ILateTickable
    {
        public void LateTick() => UpdateHealthViewPosition();
    }

    public partial class HealthViewManager : IDisposable
    {
        public void Dispose()
        {
            foreach (Actor actor in _actorsWithHealthView.Keys)
                actor.Death -= Unregister;
        }
    }
}
