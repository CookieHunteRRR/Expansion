using System;
using System.Collections.Generic;
using Code.Building;
using Code.Configs;
using Code.View;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.ViewHandler
{
    public class BuildPanelViewHandler : IDisposable, IBuildingCreate
    {
        public event Action<int> OnBuildingCreate; 
        private readonly Dictionary<int, int> _buildingsCompliance = new Dictionary<int, int>();
        private readonly List<BuildingListElement> _buildingsElementsList = new List<BuildingListElement>();
        private readonly BuildingConfig[] _allBuildings;
        private readonly BuildPanelView _buildPanelView;
        private readonly UIData _config;

        public BuildPanelViewHandler(UnionData unionData, BuildPanelView buildPanelView)
        {
            _buildPanelView = buildPanelView;
            _allBuildings = unionData.BuildingsConfig.AllBuildingsConfigs;
            _config = unionData.UIConfig;

            AddBuildingsLineElementsOnBuildingPanel();
        }

        private void AddBuildingsLineElementsOnBuildingPanel()
        {
            var rectContent = _buildPanelView.BuildingsContentPanel.GetComponent<RectTransform>();
            for (int i = 0; i < _allBuildings.Length; i++)
            {
                var newLine = Object.Instantiate(_buildPanelView.BuildElement, _buildPanelView.BuildingsContentPanel);
                newLine.gameObject.SetActive(true);
                rectContent.sizeDelta = new Vector2(rectContent.sizeDelta.x,
                    rectContent.sizeDelta.y + _config.ResIconBuildStep);
                _buildingsElementsList.Add(newLine);
                newLine.Build.onClick.AddListener(() => GetBuilding(newLine.Build.gameObject.GetInstanceID()));
                newLine.SetBuildingInfo(_allBuildings[i].Name, _allBuildings[i].Description);
                _buildingsCompliance.Add(newLine.Build.gameObject.GetInstanceID(), i);

                ShowBuildingRequiredResourcesInfo(_allBuildings[i].RequiredResources, newLine);
            }
        }

        private void ShowBuildingRequiredResourcesInfo(List<RequiredResourceProperties> requiredRes,
            BuildingListElement newLine)
        {
            for (int j = 0; j < requiredRes.Count; j++)
            {
                var newRes = Object.Instantiate(_config.ResTopElement);
                for (int k = 0; k < _config.AllResourcesConfigs.Length; k++)
                {
                    if (_config.AllResourcesConfigs[k].Type == requiredRes[j].Type)
                    {
                        newLine.AddRes(newRes.transform);
                        newRes.Init(_config.AllResourcesConfigs[k].Icon, requiredRes[j].Amount);
                    }
                }
            }
        }

        private void GetBuilding(int buttonID)
        {
            Debug.Log(_allBuildings[_buildingsCompliance[buttonID]].Name);
            OnBuildingCreate?.Invoke(_buildingsCompliance[buttonID]);
        }

        public void Dispose()
        {
            for (int i = 0; i < _buildingsElementsList.Count; i++)
            {
                _buildingsElementsList[i].Build.onClick.RemoveAllListeners();
            }
        }
    }
}
