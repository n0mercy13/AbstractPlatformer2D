using Codebase.Infrastructure;
using Codebase.StaticData;
using System;
using System.Collections;
using UnityEngine;
using VContainer;

namespace Codebase.Logic.PlayerComponents
{
    public class PlayerHorizontalMover : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;

        private IGameplayInput _input;
        private Coroutine _moveCoroutine;
        private float _walkSpeed;
        private float _walkModifier;
        private float _runModifier;
        private float _inAirModifier;
        private bool _isRunning;

        [Inject]
        private void Construct(PlayerConfig playerConfig, IGameplayInput input)
        {
            _walkSpeed = playerConfig.WalkSpeed;
            _walkModifier = playerConfig.WalkModifier;
            _runModifier = playerConfig.RunModifier;
            _inAirModifier = playerConfig.InAirModifier;

            _input = input;
            _input.HorizontalDirectionChanged += OnHorizontalDirectionChanged;
            _input.IsRunPressed += OnIsRunPressed;
        }

        private void OnValidate()
        {
            if (_characterController == null)
                throw new ArgumentNullException(nameof(_characterController));
        }

        private void OnDisable()
        {
            if (_moveCoroutine != null)
                StopCoroutine(_moveCoroutine);

            _input.HorizontalDirectionChanged -= OnHorizontalDirectionChanged;
            _input.IsRunPressed -= OnIsRunPressed;
        }

        public event Action<float> HorizontalVelocityChanged = delegate { };

        private float _relativeHorizontalSpeed => 
            Mathf.Abs(_characterController.velocity.x) / (_walkSpeed * _runModifier);

        private void OnHorizontalDirectionChanged(float direction)
        {
            if (Mathf.Abs(direction) <= float.Epsilon
                && _moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
            }
            else
            {
                _moveCoroutine = StartCoroutine(MoveAsync(direction));
            }
        }

        private void OnIsRunPressed(bool isRunPressed) =>
            _isRunning = isRunPressed;

        private IEnumerator MoveAsync(float direction)
        {
            Vector3 velocity = new Vector3();
            float moveModifier = 0;

            while (enabled)
            {
                moveModifier = GetMoveModifier();
                velocity.x = direction * _walkSpeed * moveModifier * Time.deltaTime;
                _characterController.Move(velocity);

                HorizontalVelocityChanged.Invoke(_relativeHorizontalSpeed);

                yield return null;
            }
        }

        private float GetMoveModifier()
        {
            if (_characterController.isGrounded == false)
            {
                return _inAirModifier;
            }
            else if (_isRunning)
            {
                return _runModifier;
            }
            else
            {
                return _walkModifier;
            }
        }
    }
}