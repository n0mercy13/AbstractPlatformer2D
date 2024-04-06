using System;
using UnityEngine;
using Codebase.StaticData;

namespace Codebase.Logic.PlayerComponents
{
    public class PlayerAnimationHandler : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private PlayerHorizontalMover _horizontalMover;

        private int _speedHash;

        private void OnValidate()
        {
            if (_animator == null)
                throw new ArgumentNullException(nameof(_animator));

            if (_horizontalMover == null)
                throw new ArgumentNullException(nameof(_horizontalMover));
        }

        private void OnEnable()
        {
            _speedHash = Animator.StringToHash(Constants.Animation.Player.Speed);

            _horizontalMover.HorizontalVelocityChanged += OnHorizontalVelocityChanged;
        }

        private void OnDisable()
        {
            _horizontalMover.HorizontalVelocityChanged -= OnHorizontalVelocityChanged;
        }

        private void OnHorizontalVelocityChanged(float relativeHorizontalVelocity) => 
            _animator.SetFloat(_speedHash, relativeHorizontalVelocity);
    }
}