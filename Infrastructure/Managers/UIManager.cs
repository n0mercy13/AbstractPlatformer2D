using System;
using System.Collections.Generic;
using Codebase.Logic;
using Codebase.UI;

namespace Codebase.Infrastructure
{
    public partial class UIManager
    {
        private readonly IUIInput _uiInput;
        private readonly IGameFactory _gameFactory;
        private readonly Stack<WindowView> _activeWindows;

        private Dictionary<UIWindowTypes, WindowView> _uiWindows;

        public UIManager(IUIInput uiInput, IGameFactory gameFactory)
        {
            _uiInput = uiInput;
            _gameFactory = gameFactory;
            _activeWindows = new Stack<WindowView>();
            _uiWindows = new Dictionary<UIWindowTypes, WindowView>();

            _uiInput.CanceledPressed += OnCanceledPressed;
            _uiInput.OpenSettingsPressed += OnOpenSettingsPressed;
        }

        private void Open(UIWindowTypes type)
        {
            if (_uiWindows.TryGetValue(type, out WindowView ui))
            {
                ui.Open();
                _activeWindows.Push(ui);
            }
            else
            {
                throw new ArgumentOutOfRangeException($"UI type: {type} was not found");
            }
        }

        private void Close(UIWindowTypes type)
        {
            if (_uiWindows.TryGetValue(type, out WindowView ui))
                ui.Close();
            else
                throw new ArgumentOutOfRangeException($"UI type: {type} was not found");
        }

        private void OnCanceledPressed()
        {
            if (_activeWindows.Count < 1)
                return;

            WindowView ui = _activeWindows.Pop();
            ui.Close();
        }

        private void OnOpenSettingsPressed()
        {
            Open(UIWindowTypes.UI_Settings_Sound);
        }
    }

    public partial class UIManager : IInitializable
    {
        public void Initialize()
        {
            WindowView[] uiWindows = _gameFactory.CreateUI();

            foreach (WindowView ui in uiWindows)
                _uiWindows.Add(ui.Type, ui);
        }
    }

    public partial class UIManager : IDisposable
    {
        public void Dispose()
        {
            _uiInput.CanceledPressed -= OnCanceledPressed;
            _uiInput.OpenSettingsPressed -= OnOpenSettingsPressed;
        }
    }
}
