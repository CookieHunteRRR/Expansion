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

            var inputInitialization = new InputInitialization(_unionData.InputConfig);
            var inputController = new InputController(inputInitialization);

            var planetSurface = Object.Instantiate(_unionData.BuildingsConfig.Terrain, new Vector3(0.0f, 0.0f, 0.0f),
                Quaternion.identity);
            var terrain = planetSurface.GetComponent<Terrain>();
            var surfaceGrid = new Vector2Int((int) terrain.terrainData.size.x, (int) terrain.terrainData.size.z);

            
            
            var viewController = new ViewController(_unionData.UIConfig, _unionData);

            var gridController = new BuildingsGridController(surfaceGrid, camera, viewController.BuildingViewHandler,
                _unionData, inputInitialization.InputMouseLeft);

            _controllers.Add(inputController);
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
