using UnityEngine;
using UnityEngine.UI;

namespace Code.View
{
    public class BottomPanelView : MonoBehaviour
    {
        [SerializeField] private Button _moveButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _buildingButton;
        public Button BuildButton => _buildingButton;

        public Button SettingsButton => _settingsButton;

        public Button MoveButton => _moveButton;
    }
}
