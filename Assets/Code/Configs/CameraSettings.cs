using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "CameraSettings", menuName = "Data/CameraSettings", order = 0)]
    public class CameraSettings : ScriptableObject
    {
        public Transform StartPosition;
        
    }
}
