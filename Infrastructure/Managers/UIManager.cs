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
        private readonly Stack<UI_Window> _activeWindows;

        private Dictionary<UIWindowTypes, UI_Window> _uiWindows;

        public UIManager(IUIInput uiInput, IGameFactory gameFactory)
        {
            _uiInput = uiInput;
            _gameFactory = gameFactory;
            _activeWindows = new Stack<UI_Window>();
            _uiWindows = new Dictionary<UIWindowTypes, UI_Window>();

            _uiInput.CanceledPressed += OnCanceledPressed;
            _uiInput.OpenSettingsPressed += OnOpenSettingsPressed;
        }

        private void Open(UIWindowTypes type)
        {
            if (_uiWindows.TryGetValue(type, out UI_Window ui))
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
            if (_uiWindows.TryGetValue(type, out UI_Window ui))
                ui.Close();
            else
                throw new ArgumentOutOfRangeException($"UI type: {type} was not found");
        }

        private void OnCanceledPressed()
        {
            if (_activeWindows.Count < 1)
                return;

            UI_Window ui = _activeWindows.Pop();
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
            UI_Window[] uiWindows = _gameFactory.CreateUI();

            foreach (UI_Window ui in uiWindows)
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
