using UnityEngine;


namespace Code.Configs
{
    [CreateAssetMenu(fileName = "InputSettings", menuName = "Data/InputSettings", order = 0)]
    public sealed class InputConfig : ScriptableObject
    {
        public string MouseLeftInput = "MouseButtonLeft";
        public string MouseRightInput = "MouseButtonRight";
        public string MouseScrollWheel = "Mouse ScrollWheel";
        public string Horizontal = "Horizontal";
        public string Vertical = "Vertical";
    }
}
