using System;
using Code.Configs;
using Code.Interfaces;
using Code.UserInput;
using UnityEngine;

namespace Code.Controller
{
    internal class CameraController : IInitialize, IExecute, IDisposable
    {
        private readonly IUserInputProxy _horizontalInputProxy;
        private readonly IUserInputProxy _verticalInputProxy;
        private readonly IUserInputProxy _scrollWheelInputProxy;
        private readonly IUserInputButtonProxy _mouseLeftInput;
        private readonly IUserInputButtonProxy _mouseRightInput;
        private readonly Transform _cameraTransform;
        private readonly Transform _startPosition;
        private readonly Camera _camera;
        private readonly CameraSettings _config;
        private readonly Vector2Int _surfaceSize;
        private Vector3 _mousePosition;
        private Vector3 _startMousePosition;
        private Vector3 _newPosition;
        private Vector3 _cameraCenterOfView;
        private float _axisValueZ;
        private float _axisValueX;
        private float _axisValueY;
        private float _radius;
        private readonly int _terrainID;
        private bool _moveOnAxesX;
        private bool _moveOnAxesZ;
        private bool _moveOnAxesY;
        private bool _isLeftButtonHold;
        private bool _isRightButtonHold;
        private bool _isTerrain;


        public CameraController(Camera camera, CameraSettings config, InputInitialization input,
            Vector2Int surfaceSize, int terrainID)
        {
            _camera = camera;
            _cameraTransform = camera.transform;
            _config = config;
            _startPosition = _config.StartPosition;
            _horizontalInputProxy = input.InputHorizontal;
            _verticalInputProxy = input.InputVertical;
            _mouseLeftInput = input.InputMouseLeft;
            _mouseRightInput = input.InputMouseRight;
            _scrollWheelInputProxy = input.InputMouseScrollWheel;
            _surfaceSize = surfaceSize;
            _terrainID = terrainID;
        }

        public void Initialize()
        {
            _horizontalInputProxy.AxisOnChange += ChangeOnAxisX;
            _verticalInputProxy.AxisOnChange += ChangeOnAxisZ;
            _scrollWheelInputProxy.AxisOnChange += ChangeOnAxisY;
            _mouseLeftInput.OnButtonDown += OnButtonDown;
            _mouseLeftInput.OnButtonHold += OnLeftButtonHold;
            _mouseLeftInput.OnButtonUp += OnButtonUp;
            _mouseRightInput.OnButtonDown += OnButtonDown;
            _mouseRightInput.OnButtonDown += OnRightButtonDown;
            _mouseRightInput.OnButtonHold += OnRightButtonHold;
            _mouseRightInput.OnButtonUp += OnButtonUp;
            _mouseLeftInput.OnChangeMousePosition += GetMousePosition;
            _mouseRightInput.OnChangeMousePosition += GetMousePosition;
            _cameraTransform.transform.position = _startPosition.position;
            _cameraTransform.transform.rotation = _startPosition.rotation;
        }

        private void OnButtonUp(bool value)
        {
            if (value)
            {
                if (Cursor.visible == false)
                    Cursor.visible = true;
            }
        }

        private void ChangeOnAxisX(float value) => _axisValueX = value;
        private void ChangeOnAxisZ(float value) => _axisValueZ = value;
        private void ChangeOnAxisY(float value) => _axisValueY = value;
        private void GetMousePosition(Vector3 position) => _mousePosition = position;

        private void OnButtonDown(bool value)
        {
            if (value)
            {
                if (Physics.Raycast(_camera.ScreenPointToRay(_mousePosition), out var hit))
                {
                    var hitID = hit.transform.gameObject.GetInstanceID();
                    if (hitID == _terrainID)
                    {
                        _startMousePosition = _mousePosition;
                        Cursor.visible = false;
                        _isTerrain = true;
                    }
                    else
                    {
                        _isTerrain = false;
                    }
                }
            }
        }

        private void OnRightButtonDown(bool value)
        {
            if (value)
            {
                if (Physics.Raycast(
                    _camera.ScreenPointToRay(new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0)),
                    out var hit))
                {
                    _cameraCenterOfView = hit.point;
                }
            }
        }

        private void OnLeftButtonHold(bool value) => _isLeftButtonHold = value;

        private void OnRightButtonHold(bool value) => _isRightButtonHold = value;

        public void Execute()
        {
            _moveOnAxesX = Mathf.Abs(_axisValueX) > _config.MovingThresh;
            _moveOnAxesZ = Mathf.Abs(_axisValueZ) > _config.MovingThresh;
            _moveOnAxesY = Mathf.Abs(_axisValueY) > _config.MovingThresh;

            _newPosition = _cameraTransform.position;

            MoveOnAxisX();
            MoveOnAxisY();
            MoveOnAxisZ();
            DragAndDrop();

            _cameraTransform.position = CheckPosition(_newPosition);

            Rotate();
        }

        private void MoveOnAxisX()
        {
            if (_moveOnAxesX)
            {
                _newPosition.x = _cameraTransform.position.x + _axisValueX * _config.MoveSpeed * Time.deltaTime;
            }
        }

        private void MoveOnAxisZ()
        {
            if (_moveOnAxesZ)
            {
                _newPosition.z = _cameraTransform.position.z + _axisValueZ * _config.MoveSpeed * Time.deltaTime;
            }
        }

        private void MoveOnAxisY()
        {
            if (_moveOnAxesY)
            {
                var fieldOfView = _camera.fieldOfView + _axisValueY * _config.ApproachSpeed * -100 * Time.deltaTime;
                _camera.fieldOfView = Mathf.Clamp(fieldOfView, _config.MAXClose, _config.MAXFar);
            }
        }

        private void DragAndDrop()
        {
            if (_isLeftButtonHold && _isTerrain)
            {
                var deltaPosition = (_mousePosition - _startMousePosition).normalized;

                if (Mathf.Abs(deltaPosition.x) > _config.MovingThresh)
                {
                    _newPosition = _cameraTransform.position -
                                   new Vector3(deltaPosition.x, 0.0f, deltaPosition.y) *
                                   (_config.DragAndDropSpeed * Time.deltaTime);
                }
            }
        }

        private void Rotate()
        {
            if (_isRightButtonHold && _isTerrain)
            {
                if (_mousePosition.x > _startMousePosition.x)
                {
                    _cameraTransform.RotateAround(_cameraCenterOfView, Vector3.up,
                        _config.RotationSpeed * Time.deltaTime);
                    _cameraTransform.LookAt(_cameraCenterOfView);
                }

                if (_mousePosition.x < _startMousePosition.x)
                {
                    _cameraTransform.RotateAround(_cameraCenterOfView, Vector3.up,
                        _config.RotationSpeed * Time.deltaTime * -1);
                    _cameraTransform.LookAt(_cameraCenterOfView);
                }
            }
        }

        private Vector3 CheckPosition(Vector3 position)
        {
            var vector = position;
            vector.x = Mathf.Clamp(position.x, _config.BordersSize, _surfaceSize.x - _config.BordersSize);
            vector.z = Mathf.Clamp(position.z, _config.BordersSize, _surfaceSize.y - _config.BordersSize);

            return vector;
        }

        public void Dispose()
        {
            _horizontalInputProxy.AxisOnChange -= ChangeOnAxisX;
            _verticalInputProxy.AxisOnChange -= ChangeOnAxisZ;
            _scrollWheelInputProxy.AxisOnChange -= ChangeOnAxisY;
            _mouseLeftInput.OnButtonDown -= OnButtonDown;
            _mouseLeftInput.OnButtonHold -= OnLeftButtonHold;
            _mouseLeftInput.OnButtonUp -= OnButtonUp;
            _mouseRightInput.OnButtonDown -= OnButtonDown;
            _mouseRightInput.OnButtonDown -= OnRightButtonDown;
            _mouseRightInput.OnButtonHold -= OnRightButtonHold;
            _mouseRightInput.OnButtonUp -= OnButtonUp;
            _mouseLeftInput.OnChangeMousePosition -= GetMousePosition;
            _mouseRightInput.OnChangeMousePosition -= GetMousePosition;
        }
    }
}
