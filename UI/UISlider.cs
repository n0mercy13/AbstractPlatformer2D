using System;
using UnityEngine;
using UnityEngine.UI;
using Codebase.Logic;

namespace Codebase.UI
{
    public class UISlider : UIElement
    {
        [SerializeField] private Slider _slider;

        private void OnValidate()
        {
            if(_slider == null)
                throw new ArgumentNullException(nameof(_slider));
        }

        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(OnValueChanged);
        }

        public event Action<UIElementTypes, float> ValueChanged = delegate { };

        private void OnValueChanged(float value)
        {
            ValueChanged.Invoke(Type, value);
        }
    }
}
