using System;
using UnityEngine;

namespace CodeBase
{
    public class CameraModeController : MonoBehaviour, ICameraMode
    {
        [Header("Components")]
        [SerializeField] private OrbitalMove _orbitalMoveComponent;
        [SerializeField] private StandaloneFPMove _standaloneFpMoveComponent;
        [Space]
        [Header("GameObjects")]
        [SerializeField] private GameObject _camera;
        [SerializeField] private GameObject _parentStandaloneMode;
        [SerializeField] private GameObject _parentOrbitalMode;

        private TypeMode _activeMode;

        private void Awake()
        {
            OrbitalMode();
        }

        public void ChangeMode(TypeMode mode)
        {
            if (_activeMode == mode) return;
            
            switch (mode)
            {
                case TypeMode.Empty:
                    break;
                case TypeMode.FirstPerson:
                    FirstPersonMode();
                    break;
                case TypeMode.Orbital:
                    OrbitalMode();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

        private void OrbitalMode()
        {
            _standaloneFpMoveComponent.enabled = false;
            _orbitalMoveComponent.enabled = true;
            
            _activeMode = TypeMode.Orbital;
        }

        public void CameraParentSetup(bool isOrbital)
        {
            if (isOrbital == false)
            {
                _camera.transform.SetParent(_parentStandaloneMode.transform, false);
            }
            else
            {
                _camera.transform.SetParent(_parentOrbitalMode.transform, false);
            }
        }

        private void FirstPersonMode()
        {
            _orbitalMoveComponent.enabled = false;
            _standaloneFpMoveComponent.enabled = true;
            
            _activeMode = TypeMode.FirstPerson;
        }
    }
}