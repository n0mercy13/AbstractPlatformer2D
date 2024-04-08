using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Codebase.UI;

namespace Codebase.Logic
{
    public class UI_HealthBar : UI_Window
    {
        [SerializeField] private Image _intermediateHealthBar;
        [SerializeField] private Image _healthBar;
        [SerializeField] private TMP_Text _healthLabel;
        [SerializeField, Range(0.0f, 5.0f)] private float _intermediateHealthUpdateSpeed = 1.0f;

        private Coroutine _updateHealthCoroutine;
        private float _intermediateHealth;
        private int _maxHealth;
        private int _health;

        private void OnValidate()
        {
            if (_intermediateHealthBar == null)
                throw new ArgumentNullException(nameof(_intermediateHealthBar));

            if (_healthBar == null)
                throw new ArgumentNullException(nameof(_healthBar));

            if(_healthLabel == null)
                throw new ArgumentNullException(nameof(_healthLabel));
        }

        private void OnDisable()
        {
            if (_updateHealthCoroutine != null)
                StopCoroutine(_updateHealthCoroutine);
        }

        public void UpdateView(int health, int maxHealth)
        {
            if(_intermediateHealth == 0)
                _intermediateHealth = maxHealth;

            _health = health;
            _maxHealth = maxHealth;
            _healthBar.fillAmount = (float)health / maxHealth;

            if(_updateHealthCoroutine != null)
                StopCoroutine(_updateHealthCoroutine);

            _updateHealthCoroutine = StartCoroutine(UpdateHealthAsync());
        }

        private IEnumerator UpdateHealthAsync()
        {
            while(Mathf.Approximately(_health, _intermediateHealth) == false)
            {
                _intermediateHealth = Mathf.MoveTowards(
                    _intermediateHealth, _health, _intermediateHealthUpdateSpeed * Time.deltaTime);

                _intermediateHealthBar.fillAmount = _intermediateHealth / _maxHealth;
                _healthLabel.text = $"{Mathf.FloorToInt(_intermediateHealth)} / {_maxHealth}";

                yield return null;
            }
        }
    }
}
