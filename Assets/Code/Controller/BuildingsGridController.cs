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
    internal class BuildingsGridController : IInitialize, IExecute, IDisposable
    {
        private readonly List<Transform> _allBuildings = new List<Transform>();
        private readonly int[,] _grid;
        private readonly IBuildingCreate _buildingCreate;
        private readonly IUserInputButtonProxy _input;
        private readonly UnionData _unionData;
        private readonly Camera _camera;
        private readonly Vector2Int _gridSize;

        private BuildingColoring _flyingBuildingColoring;
        private Transform _flyingBuilding;
        private Vector2Int _flyingBuildingSize;
        private Vector3 _mousePosition;
        private bool _isButtonDown;

        public BuildingsGridController(Vector2Int gridSize, Camera camera, IBuildingCreate buildingCreate,
            UnionData unionData, IUserInputButtonProxy input)
        {
            _unionData = unionData;
            _gridSize = gridSize;
            _grid = new int[_gridSize.x, _gridSize.y];
            _camera = camera;
            _buildingCreate = buildingCreate;
            _input = input;
        }

        public void Initialize()
        {
            _buildingCreate.OnBuildingCreate += OnBuildingStart;
            _input.OnButtonDown += OnButtonDown;
            _input.OnChangeMousePosition += GetMousePosition;
        }

        private void GetMousePosition(Vector3 position) => _mousePosition = position;

        private void OnButtonDown(bool value) => _isButtonDown = value;

        public void Execute()
        {
            if (_flyingBuildingColoring != null)
                CreateBuilding();
        }

        private void OnBuildingStart(int buildingID)
        {
            _flyingBuilding = Object.Instantiate(_unionData.BuildingsConfig.AllBuildingsConfigs[buildingID].Prefab);
            _flyingBuildingSize = _unionData.BuildingsConfig.AllBuildingsConfigs[buildingID].Size;
            _flyingBuildingColoring = new BuildingColoring(_flyingBuilding, _flyingBuildingSize);
            _allBuildings.Add(_flyingBuilding);
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

                if (available && IsPlaceTaken(x, z))
                    available = false;

                _flyingBuilding.position = new Vector3(x, 0.0f, z);
                _flyingBuildingColoring.SetTransparent(available);

                if (available && _isButtonDown)
                {
                    PlaceFlyingBuilding(x, z);
                }
            }
        }

        private void PlaceFlyingBuilding(int placeX, int placeZ)
        {
            for (int x = 0; x < _flyingBuildingSize.x; x++)
            {
                for (int y = 0; y < _flyingBuildingSize.y; y++)
                {
                    _grid[placeX + x, placeZ + y] = _flyingBuilding.gameObject.GetInstanceID();
                }
            }

            _flyingBuildingColoring.SetNormal();
            _flyingBuildingColoring = null;
        }

        private bool IsPlaceTaken(int placeX, int placeZ)
        {
            for (int x = 0; x < _flyingBuildingSize.x; x++)
            {
                for (int y = 0; y < _flyingBuildingSize.y; y++)
                {
                    if (_grid[placeX + x, placeZ + y] != 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void Dispose()
        {
            _buildingCreate.OnBuildingCreate -= OnBuildingStart;
            _input.OnButtonDown -= OnButtonDown;
            _input.OnChangeMousePosition -= GetMousePosition;
        }
    }
}
