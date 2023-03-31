using Code.Configs;
using UnityEngine;

namespace Code.Controller
{
    internal class PlanetSurfaceController
    {
        private readonly Terrain _terrain;

        public PlanetSurfaceController(BuildingSettings config)
        {
            var planetSurface = Object.Instantiate(config.Terrain, new Vector3(0.0f, -387.0f, 0.0f),
                Quaternion.identity);
            _terrain = planetSurface.GetComponent<Terrain>();
        }

        public Vector2Int GetSurfaceGridSize()
        {
            return new Vector2Int((int) _terrain.terrainData.size.x, (int) _terrain.terrainData.size.z);
        }

        public int GetTerrainID => _terrain.gameObject.GetInstanceID();
    }
}
