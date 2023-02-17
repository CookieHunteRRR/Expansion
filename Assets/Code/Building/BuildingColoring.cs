using Code.Assistance;
using UnityEngine;

namespace Code.Building
{
    internal class BuildingColoring : ISetColor
    {
        private readonly Renderer _buildingRenderer;

        public BuildingColoring(Transform building, Vector2Int size)
        {
            _buildingRenderer = building.GetComponent<Renderer>();
            building.gameObject.GetOrAddComponent<DrawGrid>().Init(size);
        }

        public void SetTransparent(bool available)
        {
            if (available)
            {
                _buildingRenderer.material.color = Color.green;
            }
            else
            {
                _buildingRenderer.material.color = Color.red;
            }
        }

        public void SetNormal()
        {
            _buildingRenderer.material.color = Color.white;
        }
    }
}
