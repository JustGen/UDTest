using System;
using UnityEngine;

namespace CodeBase.UI
{
    public class FooterController : MonoBehaviour
    {
        [SerializeField] private HeaderController _headerController;
        [SerializeField] private GameObject _standaloneModePanel;
        [SerializeField] private GameObject _orbitalModePanel;

        private void OnEnable()
        {
            _headerController.OnClickButton += ChangeFaqMode;

            ChangeVisibilityFaqPanel(true);
        }

        private void OnDisable() => _headerController.OnClickButton -= ChangeFaqMode;

        private void ChangeFaqMode(TypeMode typeMode)
        {
            switch (typeMode)
            {
                case TypeMode.Empty:
                    break;
                case TypeMode.FirstPerson:
                    ChangeVisibilityFaqPanel(false);
                    break;
                case TypeMode.Orbital:
                    ChangeVisibilityFaqPanel(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeMode), typeMode, null);
            }
        }

        private void ChangeVisibilityFaqPanel(bool isOrbital)
        {
            _orbitalModePanel.SetActive(isOrbital);
            _standaloneModePanel.SetActive(!isOrbital);
        }
    }
}