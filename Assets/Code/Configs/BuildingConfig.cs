using System.Collections.Generic;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "BuildingConfig", menuName = "Data/BuildingConfig", order = 0)]
    public class BuildingConfig : ScriptableObject
    {
        public Transform Prefab;
        public Sprite Icon;
        [SerializeField] private BuildingsType _type;
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private Vector2Int _size = Vector2Int.one;
        [SerializeField] private List<RequiredResourceProperties> _requiredResources;
        [SerializeField] private int _minWorkerAmount; // Минимальное количество персонала, необходимое для работы здания
        [SerializeField] private int _maxWorkerAmount; // Максимальное количество персонала, необходимое для работы здания
        [SerializeField] private int _energyPerPerson; // Потребляемая энергия одним работником
        [SerializeField] private int _energyForUpkeep; // Потребляемая энергия вне зависимости от работников

        public List<RequiredResourceProperties> RequiredResources => _requiredResources;

        public BuildingsType Type => _type;

        public string Name => _name;

        public string Description => _description;

        public Vector2Int Size => _size;

        public int MINWorkerAmount => _minWorkerAmount;

        public int MAXWorkerAmount => _maxWorkerAmount;

        public int EnergyPerPerson => _energyPerPerson;

        public int EnergyForUpkeep => _energyForUpkeep;
    }
}