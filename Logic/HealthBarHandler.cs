using System;
using System.Collections;
using UnityEngine;

namespace Codebase.Logic
{
    public partial class HealthBarHandler : MonoBehaviour
    {
        [SerializeField] private Transform _healthBarLocation;

        private UI_HealthBar _healthBar;
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

        private IEnumerator UpdateHealthBarLocationAsync()
        {
            while (enabled)
            {
                _healthBar.transform.position = Camera.main.WorldToScreenPoint(_healthBarLocation.position);

                yield return null;
            }
        }

        public void UpdateView(int health, int maxHealth) =>
            _healthBar.UpdateView(health, maxHealth);
    }

    public partial class HealthBarHandler : IInitializable<UI_HealthBar>
    {
        public void Initialize(UI_HealthBar healthBar)
        {
            _healthBar = healthBar;
        }
    }
}
