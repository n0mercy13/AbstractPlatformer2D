using System;
using System.Collections;
using UnityEngine;
using VContainer;
using Codebase.StaticData;
using Codebase.Infrastructure;

namespace Codebase.Logic.PlayerComponents
{
    public class PlayerVerticalMover : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;

        private IGameplayInput _input;
        private Coroutine _gravityCoroutine;
        private Coroutine _jumpCoroutine;
        private float _gravityModifier;
        private float _jumpVelocity;
        private float _jumpDuration;

        [Inject]
        private void Construct(IGameplayInput input, PlayerConfig playerConfig)
        {
            _gravityModifier = playerConfig.GravityModifier;
            _jumpVelocity = playerConfig.JumpVelocity;
            _jumpDuration = playerConfig.JumpDuration;

            _input = input;
            _input.JumpPressed += OnJumpPressed;
        }

        private void OnValidate()
        {
            if (_characterController == null)
                throw new ArgumentNullException(nameof(_characterController));
        }

        private void OnEnable()
        {
            _gravityCoroutine = StartCoroutine(GravityPullAsync());
        }

        private void OnDisable()
        {
            if (_gravityCoroutine != null)
                StopCoroutine(_gravityCoroutine);

            if (_jumpCoroutine != null)
                StopCoroutine(_jumpCoroutine);
        }

        private void OnJumpPressed()
        {
            if (_characterController.isGrounded == false)
                return;

            _jumpCoroutine = StartCoroutine(JumpAsync());
        }

        private IEnumerator JumpAsync()
        {
            Vector3 velocity = new Vector3();
            float timer = 0.0f;

            do
            {
                timer += Time.deltaTime;
                velocity.y = _jumpVelocity * Time.deltaTime;
                _characterController.Move(velocity);

                yield return null;
            }
            while (timer <= _jumpDuration);
        }

        private IEnumerator GravityPullAsync()
        {
            Vector3 velocity = new Vector3();

            while (enabled)
            {
                velocity.y = Physics.gravity.y * _gravityModifier * Time.deltaTime;
                _characterController.Move(velocity);

                yield return null;
            }
        }

    }
}