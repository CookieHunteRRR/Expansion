using Code.Assistance;
using Code.View;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "UIData", menuName = "Data/UIData")]
    public class UIData : ScriptableObject
    {
        [Header("Top panel settings")] 
        public TopResourcePanelView TopResourcePanelView;
        [SerializeField] private float _resIconTopStep = 92.0f;
        
        [Header("Bottom panel settings")] 
        public Transform BottomPanelView;

        [Header("Build panel settings")] 
        public Transform BuildPanelView;
        public Sprite OneColonistIcon;
        public Sprite TenColonistsIcon;
        public Sprite EnergyIcon;
        [SerializeField] private float _resIconBuildStep = 80.0f;

        [Header("Resources settings")] 
        public ResourceIconElementView ResTopElement;
        [SerializeField] private string _resourcesFolder = "ResConfigs";

        [Header("Task panel settings")] 
        public Transform TaskPanelView;
        public Transform TaskPanelButtonView;
        [SerializeField] private int _secondsUntilClosed = 1;
        [SerializeField] private string _firstTaskName = "Построить водородную электростанцию";
        [SerializeField] private string _firstTaskDesc = "Титан 0/20\nКремний 3/10";
        [SerializeField] private string _secondTaskName = "Построить химическую лабораторию";
        [SerializeField] private string _secondTaskDesc = "Титан 0/10\nКварц 8/20\nЭлектронные компоненты 1/5";

        private Canvas _canvas;
        private ResourceConfig[] _resourcesConfigs;

        public Canvas Canvas
        {
            get
            {
                if (_canvas == null)
                {
                    _canvas = Object.FindObjectOfType<Canvas>();
                }
                
                return _canvas;
            }
        }

        public ResourceConfig[] AllResourcesConfigs
        {
            get
            {
                _resourcesConfigs = Assistant.LoadAll<ResourceConfig>(_resourcesFolder);
                return _resourcesConfigs;
            }
        }

        public float ResIconBuildStep => _resIconBuildStep;

        public float ResIconTopStep => _resIconTopStep;

        public int SecondsUntilTaskPanelClosed => _secondsUntilClosed;
        public string FirstTaskName => _firstTaskName;
        public string FirstTaskDesc => _firstTaskDesc;
        public string SecondTaskName => _secondTaskName;
        public string SecondTaskDesc => _secondTaskDesc;
    }
}
