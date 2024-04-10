using Codebase.Logic;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.UI
{
    public partial class HealthBarView : MonoBehaviour
    {
        [SerializeField] private Image _topBar;
        [SerializeField] private Image _bottomBar;
        [SerializeField] private TMP_Text _label;
        [SerializeField, Range(0.0f, 10.0f)] private float _updateSpeedOnAttacked = 5.0f;
        [SerializeField, Range(0.0f, 20.0f)] private float _updateSpeedOnHealed = 10.0f;

        private Actor _actor;
        private Coroutine _updateHealthCoroutine;
        private float _currentHealth;
        private int _targetHealth;
        private int _maxHealth;

        private void OnValidate()
        {
            if (_topBar == null)
                throw new ArgumentNullException(nameof(_topBar));

            if (_bottomBar == null)
                throw new ArgumentNullException(nameof(_bottomBar));

            if (_label == null)
                throw new ArgumentNullException(nameof(_label));
        }

        private void OnDisable()
        {
            if (_updateHealthCoroutine != null)
                StopCoroutine(_updateHealthCoroutine);

            _actor.HealthChanged -= Refresh;
        }

        public void Close() => Destroy(gameObject);

        private void Refresh(int targetHealth, int maxHealth)
        {
            if (_currentHealth == 0)
                _currentHealth = maxHealth;

            _targetHealth = targetHealth;
            _maxHealth = maxHealth;

            if (_updateHealthCoroutine != null)
                StopCoroutine(_updateHealthCoroutine);

            if (_targetHealth < _currentHealth)
            {
                UpdateBar(_topBar, _targetHealth);
                _updateHealthCoroutine = StartCoroutine(UpdateHealthAsync(_bottomBar, _updateSpeedOnAttacked));
            }
            else if (_targetHealth > _currentHealth)
            {
                UpdateBar(_bottomBar, _targetHealth);
                _updateHealthCoroutine = StartCoroutine(UpdateHealthAsync(_topBar, _updateSpeedOnHealed));
            }
            else
            {
                UpdateBar(_topBar, _targetHealth);
                UpdateBar(_bottomBar, _targetHealth);
                UpdateLabel();
            }
        }

        private IEnumerator UpdateHealthAsync(Image bar, float updateSpeed)
        {
            while (Mathf.Approximately(_targetHealth, _currentHealth) == false)
            {
                _currentHealth = Mathf.MoveTowards(
                    _currentHealth, _targetHealth, updateSpeed * Time.deltaTime);

                UpdateBar(bar, _currentHealth);
                UpdateLabel();

                yield return null;
            }
        }

        private void UpdateLabel() =>
            _label.text = $"{Mathf.FloorToInt(_currentHealth)} / {_maxHealth}";

        private void UpdateBar(Image bar, float value) =>
            bar.fillAmount = value / _maxHealth;
    }

    public partial class HealthBarView : IInitializable<Actor>
    {
        public void Initialize(Actor actor)
        {
            _actor = actor;
            _actor.HealthChanged += Refresh;
        }
    }
}
