using System;
using CodeBase.Infrastructure;
using UnityEngine;

namespace CodeBase
{
    public class StandaloneFPMove : MonoBehaviour
    {
        class CameraState
        {
            public float yaw;
            public float pitch;
            public float roll;
            public float x;
            public float y;
            public float z;

            public void SetFromTransform(Transform t)
            {
                pitch = t.eulerAngles.x;
                yaw = t.eulerAngles.y;
                roll = t.eulerAngles.z;
                x = t.position.x;
                y = t.position.y;
                z = t.position.z;
            }

            public void Translate(Vector3 translation)
            {
                Vector3 rotatedTranslation = Quaternion.Euler(pitch, yaw, roll) * translation;

                x += rotatedTranslation.x;
                y += rotatedTranslation.y;
                z += rotatedTranslation.z;
            }

            public void LerpTowards(CameraState target, float positionLerpPct, float rotationLerpPct)
            {
                yaw = Mathf.Lerp(yaw, target.yaw, rotationLerpPct);
                pitch = Mathf.Lerp(pitch, target.pitch, rotationLerpPct);
                roll = Mathf.Lerp(roll, target.roll, rotationLerpPct);

                x = Mathf.Lerp(x, target.x, positionLerpPct);
                y = Mathf.Lerp(y, target.y, positionLerpPct);
                z = Mathf.Lerp(z, target.z, positionLerpPct);
            }

            public void UpdateTransform(Transform tR, Transform tP)
            {
                tR.eulerAngles = new Vector3(pitch, yaw, roll);
                tP.position = new Vector3(x, 0f, z);
            }
        }

        [SerializeField] private Transform _firstPersonPoint;
        
        public float speed = 10f;
        
        [Range(0.001f, 1f)]
        public float positionLerpTime = 0.2f;
        [Range(0.001f, 1f)]
        public float rotationLerpTime = 0.01f;
        [Range(0.1f, 3f)]
        public float mouseSensitivity = 2f;

        CameraState m_TargetCameraState = new CameraState();

        private readonly string _mouseX = "Mouse X";
        private readonly string _mouseY = "Mouse Y";

        private CameraModeController _cameraModeController;

        private void Awake()
        {
            _cameraModeController = GameBootstrapper.Instance.CameraModeController;
        }

        private void OnEnable()
        {
            _cameraModeController.CameraParentSetup(false);
            SetupPosition();
            m_TargetCameraState.SetFromTransform(transform);
        }

        private void SetupPosition()
        {
            var transformOwn = this.transform;
            transformOwn.position = _firstPersonPoint.position;
            transformOwn.rotation = _firstPersonPoint.rotation;
        }

        private void LateUpdate()
        {
            var mouseMovement = new Vector2(Input.GetAxis(_mouseX),Input.GetAxis(_mouseY) * -1);

            var mouseSensitivityFactor = mouseSensitivity;

            m_TargetCameraState.yaw += mouseMovement.x * mouseSensitivityFactor;
            m_TargetCameraState.pitch += mouseMovement.y * mouseSensitivityFactor;

            var translation = GetInputTranslationDirection() * (Time.deltaTime * speed);

            m_TargetCameraState.Translate(translation);
            
            var positionLerpPct = 1f - positionLerpTime * Time.deltaTime;
            var rotationLerpPct = 1f - rotationLerpTime * Time.deltaTime;
            m_TargetCameraState.LerpTowards(m_TargetCameraState, positionLerpPct, rotationLerpPct);

            m_TargetCameraState.UpdateTransform(this.transform,
                _cameraModeController.gameObject.transform);
        }

        private static Vector3 GetInputTranslationDirection()
        {
            var direction = new Vector3();

            if (Input.GetKey(KeyCode.W)) direction += Vector3.forward;
            if (Input.GetKey(KeyCode.S)) direction += Vector3.back;
            if (Input.GetKey(KeyCode.A)) direction += Vector3.left;
            if (Input.GetKey(KeyCode.D)) direction += Vector3.right;

            return direction;
        }
    }
}