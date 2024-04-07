using System;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using Codebase.Infrastructure;
using Codebase.Logic;

namespace Codebase.UI
{
    public class UI_Settings_Sound : UI_Window
    {
        [SerializeField] private Slider _masterSlider;
        [SerializeField] private Slider _sfxSlider;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _muteButton;

        private readonly float _minVolume = -80;

        private IAudioService _audioService;
        private float _lastVolume;
        private bool _isMuted;

        [Inject] 
        private void Construct(IAudioService audioService)
        {
            _audioService = audioService;
            _isMuted = false;
        }

        private void OnValidate()
        {
            if(_masterSlider == null)
                throw new ArgumentNullException(nameof(_masterSlider));

            if(_sfxSlider == null)
                throw new ArgumentNullException(nameof(_sfxSlider));

            if(_musicSlider == null)
                throw new ArgumentNullException(nameof(_musicSlider));

            if(_closeButton == null)
                throw new ArgumentNullException(nameof(_closeButton));

            if(_muteButton == null)
                throw new ArgumentNullException(nameof(_muteButton));
        }

        private void OnEnable()
        {
            _masterSlider.onValueChanged.AddListener(OnMasterValueChanged);
            _sfxSlider.onValueChanged.AddListener(OnSfxValueChanged);
            _musicSlider.onValueChanged.AddListener(OnMusicValueChanged);
            _muteButton.onClick.AddListener(OnMuteButtonPressed);
            _closeButton.onClick.AddListener(OnCloseButtonPressed);
        }

        private void OnDisable()
        {
            _masterSlider.onValueChanged.RemoveListener(OnMasterValueChanged);
            _sfxSlider.onValueChanged.RemoveListener(OnSfxValueChanged);
            _musicSlider.onValueChanged.RemoveListener(OnMusicValueChanged);
            _muteButton.onClick.RemoveListener(OnMuteButtonPressed);
            _closeButton.onClick.RemoveListener(OnCloseButtonPressed);
        }

        private void OnMasterValueChanged(float value) => 
            _audioService.SetVolume(UIElementTypes.UI_Slider_Volume_Master, value);

        private void OnSfxValueChanged(float value) => 
            _audioService.SetVolume(UIElementTypes.UI_Slider_Volume_SFX, value);

        private void OnMusicValueChanged(float value) => 
            _audioService.SetVolume(UIElementTypes.UI_Slider_Volume_Music, value);

        private void OnMuteButtonPressed()
        {
            if(_isMuted)
            {
                _isMuted = false;
                _masterSlider.value = _lastVolume;
            }
            else
            {
                _isMuted = true;
                _lastVolume = _masterSlider.value;
                _masterSlider.value = _minVolume;
            }
        }

        private void OnCloseButtonPressed() => Close();
    }
}
