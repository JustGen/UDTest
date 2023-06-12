using CodeBase.Infrastructure;
using UnityEngine;

namespace CodeBase
{
    public class OrbitalMove : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField]
        private Vector3 _zoomValues = new Vector3(0.25f, 10f, 30f); //x - step, y - zoomMin, z - zoomMax
        [SerializeField] private float _xCameraRotate = 30f;

        private Vector3 _offset = Vector3.zero;
        private float _mouseSensitive = 1f;

        private readonly string _mouseScroll = "Mouse ScrollWheel";
        private readonly string _mouseOffsetX = "Mouse X";
        
        private void OnEnable()
        {
            GameBootstrapper.Instance.CameraModeController.CameraParentSetup(true);
            SetupPosition();
        }


        private void SetupPosition()
        {
            _offset = new Vector3(_offset.x, _offset.y, -Mathf.Abs(_zoomValues.z));
            this.transform.position = target.position + _offset;

            SetupRotation();
        }

        private void SetupRotation()
        {
            float xMouseOffset = this.transform.localEulerAngles.y + Input.GetAxis(_mouseOffsetX) * _mouseSensitive;
            this.transform.localEulerAngles = new Vector3(_xCameraRotate, xMouseOffset, 0f);
            this.transform.position = this.transform.localRotation * _offset + target.position;
        }

        private void Update()
        {
            if (Input.GetAxis(_mouseScroll) > 0)
                _offset.z += _zoomValues.x;
            else if (Input.GetAxis(_mouseScroll) < 0)
                _offset.z -= _zoomValues.x;

            _offset.z = Mathf.Clamp(_offset.z, -Mathf.Abs(_zoomValues.z), -Mathf.Abs(_zoomValues.y));

            this.transform.position = this.transform.localRotation * _offset + target.position;
        }

        private void LateUpdate()
        {
            if (Input.GetMouseButton(0) == false) return;
            SetupRotation();
        }
    }
}