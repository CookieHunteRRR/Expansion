using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "ResourceConfig", menuName = "Data/ResourceConfig", order = 0)]
    public class ResourceConfig : ScriptableObject
    {
        public Sprite Icon;
        [SerializeField] private ResourcesType _type;
        [SerializeField] private int _startCount;

        public ResourcesType Type => _type;

        public int StartCount => _startCount;
    }
}
