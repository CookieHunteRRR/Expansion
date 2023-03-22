using Code.Configs;
using Code.UserInput;
using UnityEngine;

namespace Code.Controller
{
    public class StartController : MonoBehaviour
    {
        [SerializeField] private UnionData _unionData;

        private Controllers _controllers;

        private void Awake()
        {
            _controllers = new Controllers();
            var camera = Camera.main;

            var input = new InputInitialization(_unionData.InputConfig);
            var inputController = new InputController(input);

            var surfaceController = new PlanetSurfaceController(_unionData.BuildingsConfig);

            var cameraController = new CameraController(camera, _unionData.CameraConfig, input,
                surfaceController.GetSurfaceGridSize(), surfaceController.GetTerrainID);

            var viewController = new ViewController(_unionData.UIConfig, _unionData);

            var gridController = new BuildingsGridController(surfaceController.GetSurfaceGridSize(), camera,
                viewController.BuildingViewHandler, _unionData, input.InputMouseLeft, input.InputMouseRight);

            _controllers.Add(inputController);
            _controllers.Add(cameraController);
            //_controllers.Add(viewController);
            _controllers.Add(gridController);
        }

        private void Start()
        {
            _controllers.Initialize();
        }

        private void Update()
        {
            _controllers.Execute();
        }

        private void FixedUpdate()
        {
            _controllers.FixedExecute();
        }
    }
}
