using System;
using System.Collections;
using UnityEngine;
using Codebase.UI;

namespace Codebase.Logic
{
    public partial class HealthBarHandler : MonoBehaviour
    {
        [SerializeField] private Transform _healthBarLocation;

        private HealthBarView _healthBar;
        private Coroutine _setHealthBarLocation;

        private void OnValidate()
        {
            if (_healthBarLocation == null)
                throw new ArgumentNullException(nameof(_healthBarLocation));
        }

        private void Start()
        {
            _setHealthBarLocation = StartCoroutine(UpdateHealthBarLocationAsync());
        }

        private void OnDisable()
        {
            if (_setHealthBarLocation != null)
                StopCoroutine(_setHealthBarLocation);

        }

        private void OnDestroy()
        {
            Destroy(_healthBar.gameObject);
        }

        private IEnumerator UpdateHealthBarLocationAsync()
        {
            while (enabled)
            {
                _healthBar.transform.position = Camera.main.WorldToScreenPoint(_healthBarLocation.position);

                yield return null;
            }
        }

        public void Refresh(int health, int maxHealth) =>
            _healthBar.Refresh(health, maxHealth);
    }

    public partial class HealthBarHandler : IInitializable<HealthBarView>
    {
        public void Initialize(HealthBarView healthBar)
        {
            _healthBar = healthBar;
        }
    }
}
