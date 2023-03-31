using Code.Assistance;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "UnionData", menuName = "Data/UnionData", order = 0)]
    public class UnionData : ScriptableObject
    {
        [SerializeField] private string _inputConfigPath = "InputSettings";
        [SerializeField] private string _uiConfigPath = "UIData";
        [SerializeField] private string _buildingsConfigPath = "BuildingsSettings";
        [SerializeField] private string _cameraConfigPath = "CameraSettings";

        private InputConfig _inputConfig;
        private UIData _uiData;
        private BuildingSettings _buildingSettings;
        private CameraSettings _cameraSettings;
        
        public InputConfig InputConfig
        {
            get
            {
                if (_inputConfig == null)
                {
                    _inputConfig = Assistant.Load<InputConfig>(_inputConfigPath);
                }

                return _inputConfig;
            }
        }

        public UIData UIConfig
        {
            get
            {
                if (_uiData == null)
                {
                    _uiData = Assistant.Load<UIData>(_uiConfigPath);
                }

                return _uiData;
            }
        }

        public BuildingSettings BuildingsConfig
        {
            get
            {
                if (_buildingSettings == null)
                {
                    _buildingSettings = Assistant.Load<BuildingSettings>(_buildingsConfigPath);
                }

                return _buildingSettings;
            }
        }
        
        public CameraSettings CameraConfig
        {
            get
            {
                if (_cameraSettings == null)
                {
                    _cameraSettings = Assistant.Load<CameraSettings>(_cameraConfigPath);
                }

                return _cameraSettings;
            }
        }
    }
}
