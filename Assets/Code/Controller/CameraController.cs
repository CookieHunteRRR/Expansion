using Code.Configs;
using Code.Interfaces;
using Code.UserInput;
using UnityEngine;

namespace Code.Controller
{
    internal class CameraController : IInitialize, IExecute
    {
        private readonly IUserInputProxy _horizontalInputProxy;
        private readonly IUserInputProxy _verticalInputProxy;
        private readonly IUserInputButtonProxy _mouseLeftInput;
        private readonly Transform _camera;
        private readonly Transform _startPosition;
        private readonly CameraSettings _config;
        private readonly Vector2Int _surfaceSize;
        private Vector3 _mousePosition;
        private Vector3 _newPosition;
        private float _vertical;
        private float _horizontal;
        private bool _moveHorizontal;
        private bool _moveVertical;
        private bool _isButtonHold;

        public CameraController(Transform camera, CameraSettings config, IUserInputProxy horizontal,
            IUserInputProxy vertical, IUserInputButtonProxy mouseLeftInput, Vector2Int surfaceSize)
        {
            _camera = camera;
            _config = config;
            _startPosition = _config.StartPosition;
            _horizontalInputProxy = horizontal;
            _verticalInputProxy = vertical;
            _mouseLeftInput = mouseLeftInput;
            _surfaceSize = surfaceSize;
        }

        public void Initialize()
        {
            _horizontalInputProxy.AxisOnChange += HorizontalOnAxisOnChange;
            _verticalInputProxy.AxisOnChange += VerticalOnAxisOnChange;
            _mouseLeftInput.OnButtonHold += OnButtonHold;
            _mouseLeftInput.OnChangeMousePosition += GetMousePosition;
            _camera.transform.position = _startPosition.position;
            _camera.transform.rotation = _startPosition.rotation;
        }

        private void VerticalOnAxisOnChange(float value) => _vertical = value;
        private void HorizontalOnAxisOnChange(float value) => _horizontal = value;
        private void GetMousePosition(Vector3 position) => _mousePosition = position;
        private void OnButtonHold(bool value) => _isButtonHold = value;

        public void Execute()
        {
            _moveHorizontal = Mathf.Abs(_horizontal) > _config.MovingThresh;
            _moveVertical = Mathf.Abs(_vertical) > _config.MovingThresh;

            _newPosition = _camera.position;

            MoveHorizontal();
            MoveVertical();

            _camera.position = _newPosition;
        }

        private void MoveHorizontal()
        {
            if (_moveHorizontal)
                _newPosition.x = _camera.position.x + _horizontal * _config.MoveSpeed * Time.deltaTime;
        }

        private void MoveVertical()
        {
            if (_moveVertical)
                _newPosition.z = _camera.position.z + _vertical * _config.MoveSpeed * Time.deltaTime;
        }
    }
}
