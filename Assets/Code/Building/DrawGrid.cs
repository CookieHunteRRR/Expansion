using UnityEngine;

namespace Code.Building
{
    public class DrawGrid : MonoBehaviour
    {
        private Vector2Int Size = Vector2Int.one;

        public void Init(Vector2Int gridSize)
        {
            Size = gridSize;
        }

        private void OnDrawGizmos()
        {
            int deltaX = Size.x / 2;
            int deltaY = Size.y / 2;
            
            for (int x = 0; x < Size.x; x++)
            {
                for (int y = 0; y < Size.y; y++)
                {
                    if ((x + y) % 2 == 0)
                        Gizmos.color = new Color(0.88f, 0f, 1f, 0.3f);
                    else
                        Gizmos.color = new Color(1f, 0.68f, 0f, 0.3f);

                    Gizmos.DrawCube(transform.position + new Vector3(x - deltaX, 0.5f, y - deltaY), new Vector3(1.0f, 1.0f, 1.0f));
                }
            }
        }
    }
}
