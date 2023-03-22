using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "CameraSettings", menuName = "Data/CameraSettings", order = 0)]
    public class CameraSettings : ScriptableObject
    {
        public Transform StartPosition;
        [SerializeField] private float _moveSpeed = 3.0f;
        [SerializeField] private float _approachSpeed = 30.0f;
        [SerializeField] private float _rotationSpeed = 3.0f;
        [SerializeField] private float _dragAndDropSpeed = 10.0f;
        [SerializeField] private float _movingThresh = 0.01f;
        [SerializeField] private float _bordersSize = 30.0f;
        [SerializeField] private float _maxClose = 5.0f;
        [SerializeField] private float _maxFar = 45.0f;


        public float MoveSpeed => _moveSpeed;

        public float ApproachSpeed => _approachSpeed;

        public float RotationSpeed => _rotationSpeed;

        public float DragAndDropSpeed => _dragAndDropSpeed;

        public float MovingThresh => _movingThresh;

        public float BordersSize => _bordersSize;

        public float MAXClose => _maxClose;

        public float MAXFar => _maxFar;
    }
}
