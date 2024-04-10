using Codebase.Logic;
using System;
using UnityEngine;

namespace Codebase.UI
{
    public class WindowView : MonoBehaviour
    {
        [field: SerializeField] public UIWindowTypes Type { get; private set; }
        [SerializeField] private CanvasGroup _canvasGroup;

        private void OnValidate()
        {
            if(_canvasGroup == null)
                throw new ArgumentNullException(nameof(_canvasGroup));
        }

        public void Open()
        {
            _canvasGroup.alpha = 1.0f;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public void Close()
        {
            _canvasGroup.alpha = 0.0f;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}
