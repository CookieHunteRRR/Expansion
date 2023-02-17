using System;
using System.Collections.Generic;
using Code.Building;
using Code.Configs;
using Code.Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Controller
{
    internal class BuildingsGridController : IInitialize, IExecute, IDisposable
    {
        private readonly IBuildingCreate _buildingCreate;
        private readonly List<Transform> _allBuildings = new List<Transform>();
        private readonly UnionData _unionData;
        private Vector2Int _gridSize;

        private readonly int[,] _grid;

        private BuildingColoring _flyingBuildingColoring;
        private Transform _flyingBuilding;
        private Vector2Int _flyingBuildingSize;
        private readonly Camera _camera;

        public BuildingsGridController(Vector2Int gridSize, Camera camera, IBuildingCreate buildingCreate,
            UnionData unionData)
        {
            _unionData = unionData;
            _gridSize = gridSize; //new Vector2Int(20, 10); 
            _grid = new int[_gridSize.x, _gridSize.y];
            _camera = camera;
            _buildingCreate = buildingCreate;
            Debug.Log(buildingCreate);
        }

        public void Initialize()
        {
            _buildingCreate.OnBuildingCreate += OnBuildingStart;
        }

        public void Execute()
        {
            if (_flyingBuildingColoring != null)
                CreateBuilding();
        }

        private void OnBuildingStart(int buildingID)
        {
            Debug.Log(buildingID);
            _flyingBuilding = Object.Instantiate(_unionData.BuildingsConfig.AllBuildingsConfigs[buildingID].Prefab);
            _flyingBuildingSize = _unionData.BuildingsConfig.AllBuildingsConfigs[buildingID].Size;
            _flyingBuildingColoring = new BuildingColoring(_flyingBuilding, _flyingBuildingSize);
            _allBuildings.Add(_flyingBuilding);
        }

        private void CreateBuilding()
        {
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (groundPlane.Raycast(ray, out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);

                int x = Mathf.RoundToInt(worldPosition.x);
                int y = Mathf.RoundToInt(worldPosition.z);

                bool available = true;

                if (x < 0 || x > _gridSize.x - _flyingBuildingSize.x) 
                    available = false;
                if (y < 0 || y > _gridSize.y - _flyingBuildingSize.y) 
                    available = false;

                if (available && IsPlaceTaken(x, y)) 
                    available = false;

                _flyingBuilding.position = new Vector3(x, 0, y);
                _flyingBuildingColoring.SetTransparent(available);

                if (available && Input.GetMouseButtonDown(0))
                {
                    PlaceFlyingBuilding(x, y);
                }
            }
        }

        private void PlaceFlyingBuilding(int placeX, int placeY)
        {
            for (int x = 0; x < _flyingBuildingSize.x; x++)
            {
                for (int y = 0; y < _flyingBuildingSize.y; y++)
                {
                    _grid[placeX + x, placeY + y] = _flyingBuilding.gameObject.GetInstanceID();
                }
            }

            _flyingBuildingColoring.SetNormal();
            _flyingBuildingColoring = null;
        }

        private bool IsPlaceTaken(int placeX, int placeY)
        {
            for (int x = 0; x < _flyingBuildingSize.x; x++)
            {
                for (int y = 0; y < _flyingBuildingSize.y; y++)
                {
                    if (_grid[placeX + x, placeY + y] != null)
                        return true;
                }
            }

            return false;
        }

        public void Dispose()
        {
            _buildingCreate.OnBuildingCreate -= OnBuildingStart;
        }
    }
}
