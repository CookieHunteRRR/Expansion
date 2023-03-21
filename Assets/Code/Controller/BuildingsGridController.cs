using System;
using System.Collections.Generic;
using Code.Building;
using Code.Configs;
using Code.Interfaces;
using Code.UserInput;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Controller
{
    internal class BuildingsGridController : IInitialize, IExecute, IDisposable, IBuild
    {
        public event Action OnBuild;
        private readonly List<Transform> _allBuildings = new List<Transform>();
        private readonly int[,] _grid;
        private readonly IBuildingCreate _buildingCreate;
        //private readonly IPanelView _buildPanelView;
        private readonly IUserInputButtonProxy _mouseLeftInput;
        private readonly IUserInputButtonProxy _mouseRightInput;
        private readonly UnionData _unionData;
        private readonly Camera _camera;
        private readonly Vector2Int _gridSize;
        private BuildingColoring _flyingBuildingColoring;
        private Transform _flyingBuilding;
        private Vector2Int _flyingBuildingSize;
        private Vector3 _mousePosition;
        private bool _isLeftButtonDown;
        private bool _isRightButtonDown;

        public BuildingsGridController(Vector2Int gridSize, Camera camera, IBuildingCreate buildingCreate,
            UnionData unionData, IUserInputButtonProxy mouseLeftInput, IUserInputButtonProxy mouseRightInput)
        {
            _unionData = unionData;
            _gridSize = gridSize;
            _grid = new int[_gridSize.x, _gridSize.y];
            _camera = camera;
            _buildingCreate = buildingCreate;
            //_buildPanelView = buildPanelView;
            _mouseLeftInput = mouseLeftInput;
            _mouseRightInput = mouseRightInput;
        }

        public void Initialize()
        {
            _buildingCreate.OnBuildingCreate += OnBuildingStart;
            _mouseLeftInput.OnButtonDown += OnLeftButtonDown;
            _mouseRightInput.OnButtonDown += OnRightButtonDown;
            _mouseLeftInput.OnChangeMousePosition += GetMousePosition;
        }

        private void GetMousePosition(Vector3 position) => _mousePosition = position;
        private void OnLeftButtonDown(bool value) => _isLeftButtonDown = value;
        private void OnRightButtonDown(bool value) => _isRightButtonDown = value;

        public void Execute()
        {
            if (_flyingBuildingColoring != null)
            {
                CreateBuilding();
                if (_isRightButtonDown)
                {
                    Debug.Log("Open panel again");
                    //_buildPanelView.ViewObject.SetActive(true);
                }
            }
        }

        private void OnBuildingStart(int buildingID)
        {
            _flyingBuilding = Object.Instantiate(_unionData.BuildingsConfig.AllBuildingsConfigs[buildingID].Prefab);
            _flyingBuildingSize = _unionData.BuildingsConfig.AllBuildingsConfigs[buildingID].Size;
            _flyingBuildingColoring = new BuildingColoring(_flyingBuilding, _flyingBuildingSize);
        }

        private void CreateBuilding()
        {
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = _camera.ScreenPointToRay(_mousePosition);

            if (groundPlane.Raycast(ray, out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);

                int x = Mathf.RoundToInt(worldPosition.x);
                int z = Mathf.RoundToInt(worldPosition.z);

                bool available = true;

                if (x < 0 || x > _gridSize.x - _flyingBuildingSize.x)
                    available = false;
                if (z < 0 || z > _gridSize.y - _flyingBuildingSize.y)
                    available = false;

                var limits = GetLimits();

                if (available && IsPlaceTaken(x, z, limits.Item1, limits.Item2, limits.Item3, limits.Item4))
                    available = false;

                _flyingBuilding.position = new Vector3(x, 0.0f, z);
                _flyingBuildingColoring.SetTransparent(available);

                if (available && _isLeftButtonDown)
                {
                    PlaceFlyingBuilding(x, z, limits.Item1, limits.Item2, limits.Item3, limits.Item4);
                }
            }
        }

        private void PlaceFlyingBuilding(int placeX, int placeZ, int startX, int startY, int maxX, int maxY)
        {
            for (int x = startX; x < maxX; x++)
            {
                for (int y = startY; y < maxY; y++)
                {
                    _grid[placeX + x, placeZ + y] = _flyingBuilding.gameObject.GetInstanceID();
                    _allBuildings.Add(_flyingBuilding);
                    OnBuild?.Invoke();
                }
            }

            _flyingBuildingColoring.SetNormal();
            _flyingBuildingColoring = null;
        }

        private bool IsPlaceTaken(int placeX, int placeZ, int startX, int startY, int maxX, int maxY)
        {
            for (int x = startX; x < maxX; x++)
            {
                for (int y = startY; y < maxY; y++)
                {
                    if (_grid[placeX + x, placeZ + y] != 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private (int, int, int, int) GetLimits()
        {
            int startX = _flyingBuildingSize.x / 2 * -1;
            int startY = _flyingBuildingSize.y / 2 * -1;
            int maxX = _flyingBuildingSize.x / 2;
            int maxY = _flyingBuildingSize.y / 2;

            if (_flyingBuildingSize.x % 2 == 1)
            {
                maxX++;
            }

            if (_flyingBuildingSize.y % 2 == 1)
            {
                maxY++;
            }

            if (_flyingBuildingSize.x == 1)
            {
                startX = 0;
                maxX = _flyingBuildingSize.x;
            }

            if (_flyingBuildingSize.y == 1)
            {
                startY = 0;
                maxY = _flyingBuildingSize.y;
            }

            return (startX, startY, maxX, maxY);
        }


        public void Dispose()
        {
            _buildingCreate.OnBuildingCreate -= OnBuildingStart;
            _mouseLeftInput.OnButtonDown -= OnLeftButtonDown;
            _mouseLeftInput.OnChangeMousePosition -= GetMousePosition;
        }
    }

    internal interface IBuild
    {
        event Action OnBuild;
    }
}
