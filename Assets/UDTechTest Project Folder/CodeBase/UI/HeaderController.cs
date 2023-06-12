using System;
using CodeBase.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class HeaderController : MonoBehaviour
    {
        [SerializeField] private Button _standaloneModeButton;
        [SerializeField] private Button _orbitalModeButton;

        public event Action<TypeMode> OnClickButton = delegate(TypeMode mode) {  };
        
        private void OnEnable()
        {
            _standaloneModeButton.onClick.AddListener(OnStandaloneModeClick);
            _orbitalModeButton.onClick.AddListener(OnOrbitalModeClick);

            ChangeButtonColor(true);
        }

        private void OnDisable()
        {
            _standaloneModeButton.onClick.RemoveListener(OnStandaloneModeClick);
            _orbitalModeButton.onClick.RemoveListener(OnOrbitalModeClick);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                OnStandaloneModeClick();
            
            if (Input.GetKeyDown(KeyCode.E))
                OnOrbitalModeClick();
        }

        private void OnStandaloneModeClick()
        {
            GameBootstrapper.Instance.CameraModeController.ChangeMode(TypeMode.FirstPerson);
            ChangeButtonColor(false);
            OnClickButton?.Invoke(TypeMode.FirstPerson);
        }

        private void OnOrbitalModeClick()
        {
            GameBootstrapper.Instance.CameraModeController.ChangeMode(TypeMode.Orbital);
            ChangeButtonColor(true);
            OnClickButton?.Invoke(TypeMode.Orbital);
        }
        
        private void ChangeButtonColor(bool isOrbitalMode)
        {
            if (isOrbitalMode == false)
            {
                _orbitalModeButton.GetComponent<Image>().color = new Color(0f,0f,0f, 0.5686275f);
                _standaloneModeButton.GetComponent<Image>().color = new Color(0f,0.5882353f,0f, 0.5686275f);
            }
            else
            {
                _standaloneModeButton.GetComponent<Image>().color = new Color(0f,0f,0f, 0.5686275f);
                _orbitalModeButton.GetComponent<Image>().color = new Color(0f,0.5882353f,0f, 0.5686275f);
            }
        }
    }
}