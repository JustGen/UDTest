using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        public static GameBootstrapper Instance;
        
        [SerializeField] private CameraModeController _cameraModeController;
        public CameraModeController CameraModeController => _cameraModeController;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }
}