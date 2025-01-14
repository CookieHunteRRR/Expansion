﻿using System;
using Code.Configs;
using Code.View;
using Code.ViewHandler;
using Object = UnityEngine.Object;

namespace Code.Controller
{
    internal class ViewController : IDisposable
    {
        public BuildPanelViewHandler BuildingViewHandler { get; private set; }

        private readonly UIData _uiData;
        private readonly UnionData _unionData;
        private ResourcesIconElementViewHandler[] _resTopElementViewHandlers;
        private TopResourcePanelView _topResourcePanel;
        private BottomPanelView _bottomPanelView;
        private BuildPanelView _buildPanelView;


        public ViewController(UIData config, UnionData unionData)
        {
            _uiData = config;
            _unionData = unionData;

            CreateTopPanel();
            CreateBottomPanel();
            CreateBuildPanel();
        }

        private void CreateTopPanel()
        {
            var topPanel = Object.Instantiate(_uiData.TopResourcePanelView, _uiData.Canvas.transform);
            _topResourcePanel = topPanel.transform.GetComponent<TopResourcePanelView>();
            _topResourcePanel.Init(_uiData);

            _resTopElementViewHandlers = new ResourcesIconElementViewHandler[_uiData.AllResourcesConfigs.Length];
            for (int i = 0; i < _uiData.AllResourcesConfigs.Length; i++)
            {
                _resTopElementViewHandlers[i] = new ResourcesIconElementViewHandler(_uiData.ResTopElement,
                    _topResourcePanel,
                    _uiData.AllResourcesConfigs[i].Icon, _uiData.AllResourcesConfigs[i].StartCount);
            }
        }

        private void CreateBottomPanel()
        {
            var bottomPanel = Object.Instantiate(_uiData.BottomPanelView, _uiData.Canvas.transform);
            _bottomPanelView = bottomPanel.GetComponent<BottomPanelView>();
        }

        private void CreateBuildPanel()
        {
            var buildPanel = Object.Instantiate(_uiData.BuildPanelView, _uiData.Canvas.transform);
            _buildPanelView = buildPanel.GetComponent<BuildPanelView>();

            _bottomPanelView.BuildButton.onClick.AddListener(() => ActivateBuildPanel(_buildPanelView, true));
            _buildPanelView.ClosePanelButton.onClick.AddListener(() => ActivateBuildPanel(_buildPanelView, false));

            //_buildPanelView.gameObject.SetActive(false);
            ActivateBuildPanel(_buildPanelView, false);
            BuildingViewHandler = new BuildPanelViewHandler(_unionData, _buildPanelView);
        }


        private void ActivateBuildPanel(IPanelView panel, bool value)
        {
            panel.ViewObject.SetActive(value);
        }

        public void Dispose()
        {
        }
    }
}
